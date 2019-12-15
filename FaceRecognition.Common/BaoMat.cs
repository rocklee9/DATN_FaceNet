using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace FaceRecognition.Common
{
    /// <summary>
    /// Tổng hợp các phương thức hay dùng lien quan đến mã hóa và giải mã.
    /// Author       :   HoangNM - 01/11/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   FaceRecognition.Common
    /// Copyright    :   Team HoangC#
    /// Version      :   1.0.0
    /// </remarks>
    public class BaoMat
    {
        /// <summary>
        /// Lấy chuỗi salt cho Tài khoản mới
        /// Author       :   HoangNM - 28/02/2019 - create
        /// </summary>
        /// <param name="length">
        /// Dộ dài của salt
        /// </param>
        /// <returns>
        /// Chuỗi sau khi đã được mã hóa.
        /// </returns>
        public static string GetSalt(int length = 10)
        {
            string salt = "";
            Random ran = new Random();
            string tmp = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";
            for (int i = 0; i < length; i++)
            {
                salt += tmp.Substring(ran.Next(0, 63), 1);
            }
            return salt;
        }

        /// <summary>
        /// Mã hóa MD5 của 1 chuỗi có thêm chuối khóa đầu và cuối.
        /// Author       :   HoangNM - 23/02/2019 - create
        /// </summary>
        /// <param name="str">
        /// Chuỗi cần mã hóa.
        /// </param>
        /// <returns>
        /// Chuỗi sau khi đã được mã hóa.
        /// </returns>
        public static string GetMD5(string str, string salt)
        {
            str = "DATN_Face" + str + salt;
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);
            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");
            }
            return str_md5;
        }
        /// <summary>
        /// Mã hóa MD5 của 1 chuỗi không có thêm chuối khóa đầu và cuối.
        /// Author       :   HoangNM - 23/02/2019 - create
        /// </summary>
        /// <param name="str">
        /// Chuỗi cần mã hóa.
        /// </param>
        /// <returns>
        /// Chuỗi sau khi đã được mã hóa
        /// </returns>
        public static string GetSimpleMD5(string str)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);
            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");
            }
            return str_md5;
        }
        /// <summary>
        /// Mã hóa base64 của 1 chuỗi
        /// Author       :   HoangNM - 23/02/2019 - create
        /// </summary>
        /// <param name="plainText">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi sau khi mã hóa</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// Chuyển mã base64 về chuỗi trước khi mã hóa.
        /// Author       :   HoangNM - 23/02/2019 - create
        /// </summary>
        /// <param name="base64EncodedData">Chuỗi mã hóa</param>
        /// <returns>Chuỗi sau khi giải mã</returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Tạo mật khẩu mới cho tài khoản
        /// Author       :   HoangNM - 02/11/2019 - create
        /// </summary>
        /// <returns>
        /// mật khẩu tài khoản
        /// </returns>
        ///
        public static string AutoPassword()
        {
            string pass = "";
            Random ran = new Random();
            string tmp = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            pass += tmp.Substring(ran.Next(0, 25), 1);
            tmp = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            for (int i = 0; i < 7; i++)
            {
                pass += tmp.Substring(ran.Next(0, 61), 1);
            }

            return pass;
        }
    }
}