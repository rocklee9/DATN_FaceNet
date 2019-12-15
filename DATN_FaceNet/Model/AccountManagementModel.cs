using DATN_FaceNet.Model.Schema;
using FaceRecognition.Common;
using FaceRecognition.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TblAccount = FaceRecognition.DataBase.Schema.Account;
using TblUser = FaceRecognition.DataBase.Schema.User;

namespace DATN_FaceNet.Model
{
    public class AccountManagementModel
    {
        private DataContext context = new DataContext();
        private static AccountManagementModel _instance;
        private AccountManagementModel()
        {
        }

        public static AccountManagementModel Instance => _instance ?? (_instance = new AccountManagementModel());

        public int AddAccount(NewAccount newAccount)
        {
            try
            {
                TblAccount account = context.Accounts.FirstOrDefault(x => x.UserName == newAccount.UserName && !x.DelFlag);
                if (account != null)
                {
                    return 2;
                }
                else
                {
                    account = context.Accounts.FirstOrDefault(x => x.Email == newAccount.Email && !x.DelFlag);
                    if (account != null)
                    {
                        return 3;
                    }
                    else
                    {
                        var saltPass = BaoMat.GetSalt();
                        account = new TblAccount
                        {
                            UserName = newAccount.UserName,
                            salt_Pass = saltPass,
                            hash_Pass = BaoMat.GetMD5(BaoMat.GetSimpleMD5(newAccount.Password), saltPass),
                            Email = newAccount.Email,
                            Id_Role = 2
                        };
                        TblUser user = new TblUser
                        {
                            Name = newAccount.Name,
                            FullName = newAccount.FullName,
                            Gender = newAccount.Gender,
                            Birthday = newAccount.Birthday
                        };

                        user.Accounts.Add(account);
                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                }
                return 1;
            }
            catch
            {
                return 0;
            }

        }

        public int EditAccount(NewAccount newAccount)
        {
            try
            {
                TblAccount account = context.Accounts.FirstOrDefault(x => x.UserName == newAccount.UserName &&x.Email==newAccount.Email && !x.DelFlag);
                if (account != null)
                {
                    account.User.Name = newAccount.Name;
                    account.User.FullName = newAccount.FullName;
                    account.User.Birthday = newAccount.Birthday;
                    account.User.Gender = newAccount.Gender;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public List<Account> getAccounts()
        {
            return context.Accounts.Where(x => !x.DelFlag).Select(x => new Account
            {
                Id = x.Id,
                UserName = x.UserName,
                Name = x.User.Name,
                FullName = x.User.FullName,
                Gender = x.User.Gender,
                Birthday = x.User.Birthday,
                Email = x.Email
            }).ToList();
        }



        public void Delete(int id)
        {
            var account = context.Accounts.FirstOrDefault(x => x.Id == id && !x.DelFlag);
            if (account != null)
            {
                account.DelFlag = true;
            }
            context.SaveChanges();
        }

        public List<Account> Search(string search)
        {
            return context.Accounts.Where(x =>x.Email.Contains(search)&& x.UserName.Contains(search)&& !x.DelFlag).Select(x => new Account
            {
                Id = x.Id,
                UserName = x.UserName,
                Name = x.User.Name,
                FullName = x.User.FullName,
                Gender = x.User.Gender,
                Birthday = x.User.Birthday,
                Email = x.Email
            }).ToList();
        }

        public int ChangePass(ChangePass changePass)
        {
            try
            {
                var account = context.Accounts.FirstOrDefault(x => x.Id == Common.User.Id);
                var hass_pass = BaoMat.GetMD5(BaoMat.GetSimpleMD5(changePass.OldPass), account.salt_Pass);
                if (string.Compare(hass_pass, account.hash_Pass) == 0)
                {
                    account.hash_Pass = BaoMat.GetMD5(BaoMat.GetSimpleMD5(changePass.Pass), account.salt_Pass);
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch
            {
                return 0;
            }
            
            
        }
    }
}
