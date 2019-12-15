using DATN_FaceNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_FaceNet.Controller
{
    class LoginController
    {
        private static LoginController _instance;

        private LoginController()
        {
        }

        public static LoginController Instance => _instance ?? (_instance = new LoginController());
        public int IsCheckLogin(string username, string password)
        {

            return LoginModel.Instance.IsCheckLogin(username,password);
        }

        public bool IsSendMail(string email, string username)
        {
            return LoginModel.Instance.IsSendMail(email,username);
        }
    }
}
