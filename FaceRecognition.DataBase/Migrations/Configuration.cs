namespace FaceRecognition.DataBase.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;

    internal sealed class Configuration : DbMigrationsConfiguration<FaceRecognition.DataBase.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FaceRecognition.DataBase.DataContext context)
        {


            context.Users.Add(new Schema.User
            {
                Name = "Hoang",
                FullName = "Nguyễn Minh Hoàng",
                Gender = true,
                Birthday = new DateTime(1997, 4, 16),
            });

            context.SaveChanges();

            context.Roles.Add(new Schema.Role
            {
                Name = "Admin"
            });
            context.Roles.Add(new Schema.Role
            {
                Name = "Member"
            });
            context.SaveChanges();

            context.Accounts.Add(new Schema.Account
            {
                Id_User = 1,
                Id_Role = 1,
                UserName = "MinhHoang",
                salt_Pass = "hoang01",
                hash_Pass = GetMD5(GetSimpleMD5("Rocklee97"), "hoang01"),
                Email = "minhhoang97hk@gmail.com",

            });
            context.SaveChanges();


        }

        /// <summary>
        /// Mã hóa MD5 của 1 chuỗi có thêm chuối khóa đầu và cuối.
        /// Author       :   HoangNM - 01/11/2019 - create
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
        /// Author       :   HoangNM - 01/11/2019 - create
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
    }
}
