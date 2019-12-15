using FaceRecognition.Common;
using FaceRecognition.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_FaceNet.Model
{
    public class LoginModel
    {
        public DataContext context = new DataContext();
        private static LoginModel _instance;

        private LoginModel()
        {
        }

        public static LoginModel Instance => _instance ?? (_instance = new LoginModel());

        public int IsCheckLogin(string UserName,string PassWord)
        {
            var account = context.Accounts.FirstOrDefault(x => x.UserName == UserName);
            if (account == null)
            {
                return 1;
            }else if((account.hash_Pass) != BaoMat.GetMD5(BaoMat.GetSimpleMD5(PassWord), account.salt_Pass))
            {
                return 2;
            }
            else
            {
                Common.User.Id = account.Id;
                Common.User.Name = account.User.Name;
                Common.User.FullName = account.User.FullName;
                Common.User.Birthday = account.User.Birthday;
                Common.User.Role = account.Id_Role;
                Common.User.UserName = account.UserName;
                return 0;
            }
                
            
        }

        public bool IsSendMail(string email,string username)
        {
            var taikhoan = context.Accounts.FirstOrDefault(x => string.Compare(x.UserName, username) == 0 && string.Compare(x.Email, email) == 0 && !x.DelFlag);
            if (taikhoan == null)
            {
                return false;
            }
            else
            {
                string newPass = BaoMat.AutoPassword();
                SendMail.Send(taikhoan.Email, newPass, "Face Recognition Forget Password");
                taikhoan.hash_Pass = BaoMat.GetMD5(BaoMat.GetSimpleMD5(newPass), taikhoan.salt_Pass);
                context.SaveChanges();
                return true;
            }
        }
    }
}
