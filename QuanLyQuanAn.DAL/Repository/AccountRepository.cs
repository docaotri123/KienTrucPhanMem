using QuanLyQuanAn.DAL.IRepository;
using QuanLyQuanAn.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyQuanAn.DAL.Repository
{
    public class AccountRepository : GenericRepository<QuanLyQuanAnDbContext, Account>, IAccountRepository, IDisposable
    {
        public AccountRepository()
        {
        }

        //public IEnumerable<Account> GetAccounts()
        //{
        //    return Context.Accounts.ToList();

        //}
        public IEnumerable<Account> GetAccounts() => GetAll().ToList();

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Login(string userName, string password)
        {
            return Context.Accounts.FirstOrDefault(m => m.userName.Equals(userName) && m.passWordUser.Equals(password)) != null ? true : false;
        }

        public Account GetAccountByUserName(string userName)
        {
            return Context.Accounts.FirstOrDefault(m => m.userName == userName);
        }

        public bool UpdateAccount(string userName, string newPassword)
        {
            var result = Context.Accounts.SingleOrDefault(m => m.userName == userName);
            if (result != null)
            {
                result.passWordUser = newPassword;
                Context.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool UpdateAccountFormAdmin(string userName, bool type)
        {
            var result = Context.Accounts.SingleOrDefault(m => m.userName == userName);
            if (result != null)
            {
                result.style = type;
                Context.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool AddAccount(string userName, bool type)
        {
            //kiểm tra có trùng tên đăng nhập hay không
            var result = Context.Accounts.SingleOrDefault(m => m.userName == userName);
            if (result != null)
            {
                return false;
            }
            //kêt thúc kiêm tra
            Account X = new Account();
            X.userName = userName;
            X.style = type;
            X.passWordUser = "0";
            Context.Accounts.Add(X);
            Context.SaveChanges();
            return true;

        }

        public bool DeleteAccount(string userName)
        {
            //xóa tài khoản
            var result = Context.Accounts.SingleOrDefault(m => m.userName == userName);
            if (result == null)//không tồn tại
            {
                return false;
            }

            Context.Accounts.Remove(result);
            Context.SaveChanges();
            return true;
        }
    }
}
