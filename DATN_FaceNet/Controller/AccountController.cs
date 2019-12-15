using DATN_FaceNet.Model;
using DATN_FaceNet.Model.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_FaceNet.Controller
{
    class AccountController
    {

        private static AccountController _instance;

        private AccountController()
        {
        }

        public static AccountController Instance => _instance ?? (_instance = new AccountController());
        public int AddAccount(NewAccount newAccount)
        {
            return AccountManagementModel.Instance.AddAccount(newAccount);
        }

        public int EditAccount(NewAccount newAccount)
        {
            return AccountManagementModel.Instance.EditAccount(newAccount);
        }

        public List<Account> getAccounts()
        {
            return AccountManagementModel.Instance.getAccounts();
        }

        public void Delete(int id)
        {
            AccountManagementModel.Instance.Delete(id);
        }

        public List<Account> Search(string search)
        {
            return AccountManagementModel.Instance.Search(search);
        }

        public int ChangePass(ChangePass changePass)
        {
            return AccountManagementModel.Instance.ChangePass(changePass);
        }
    }
}
