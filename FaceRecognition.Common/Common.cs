using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace FaceRecognition.Common
{
    /// <summary>
    /// Chứa các function sẽ sử dụng chung và nhiều lần trong dự án.
    /// Author       :   HoangNM - 01/11/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   FaceRecognition.Common
    /// Copyright    :   Nguyễn Minh Hoàng
    /// Version      :   1.0.0
    /// </remarks>
    public class Common
    {
        public static User User=new User();

        public static string SaveFileUpload(HttpPostedFileBase file, string folder = "/public/img/upload/", string fileName = "", List<string> typeFiles = null, int sizeFile = 10)
        {
            try
            {
                if (fileName == "")
                {
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss_") + file.FileName;
                }
                string path = HostingEnvironment.MapPath("~" + folder + fileName);
                int fileSize = file.ContentLength;
                string mimeType = Path.GetExtension(path);
                if (typeFiles != null && typeFiles.FirstOrDefault(x => x == mimeType) == null)
                {
                    return "1";
                }
                if (fileSize / 1024 / 1024 > 10)
                {
                    return "2";
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                file.SaveAs(path);
                return folder + fileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Chuyển đổi từ base64String về Image
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <param name="base64String">
        /// base64String
        /// </param>
        /// <returns>
        /// file ảnh
        /// </returns>
        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Bitmap tempBmp;
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true))
                {
                    //Create another object image for dispose old image handler
                    tempBmp = new Bitmap(image.Width, image.Height);
                    Graphics g = Graphics.FromImage(tempBmp);
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                }
            }
            return tempBmp;
        }

       

        /// <summary>
        /// Chuyển đổi từ Image về base64String 
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <param name="image">
        /// file ảnh cần chuyển đổi
        /// </param>
        /// <returns>
        /// chuổi base 64
        /// </returns>
        private string ImageToBase64(System.Drawing.Image image, ImageFormat format)
        {
            string base64String;
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                ms.Position = 0;
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                base64String = Convert.ToBase64String(imageBytes);
            }
            return base64String;
        }

        public string GetImgager(string linkAnh)
        {
            string imagePath;
            string base64String = null;
            try
            {
                imagePath = HostingEnvironment.MapPath("~/" + linkAnh);
                if (File.Exists(imagePath))
                {
                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                    {
                        if (img != null)
                        {
                            base64String = ImageToBase64(img, ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return base64String;
        }



        // Ham goi HTTP Get len server
        public string sendGet(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        // Ham chuyen Image thanh Base 64
        public static string ConvertImageToBase64String(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        // Ham convert B64 de gui len server
        private String EscapeData(String B64)
        {
            int B64_length = B64.Length;
            if (B64_length <= 32000)
            {
                return Uri.EscapeDataString(B64);
            }


            int idx = 0;
            StringBuilder builder = new StringBuilder();
            String substr = B64.Substring(idx, 32000);
            while (idx < B64_length)
            {
                builder.Append(Uri.EscapeDataString(substr));
                idx += 32000;

                if (idx < B64_length)
                {

                    substr = B64.Substring(idx, Math.Min(32000, B64_length - idx));
                }

            }
            return builder.ToString();

        }

        // Ham goi HTTP POST len server de detect
        private String sendPOST(String url, String B64)
        {
            try
            {

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000;
                var postData = "image=" + EscapeData(B64) + "id=" + EscapeData(Common.User.Id.ToString());

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;


                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }


                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;
            }
            catch (Exception ex)
            {
                return "Exception" + ex.ToString();
            }
        }
    }
}