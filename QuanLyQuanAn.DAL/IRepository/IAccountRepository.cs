using QuanLyQuanAn.DAL.Model;

namespace QuanLyQuanAn.DAL.IRepository
{
    public interface IAccountRepository: IGenericRepository<Account>
    {
        bool Login(string userName, string password);
        Account GetAccountByUserName(string userName);
        bool UpdateAccount(string userName, string newPassword);
        bool UpdateAccountFormAdmin(string userName, bool type);
        bool AddAccount(string userName, bool type);
        bool DeleteAccount(string userName);
    }
}
