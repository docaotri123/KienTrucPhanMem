using QuanLyQuanAn.DAL.IRepository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace QuanLyQuanAn.DAL.Repository
{
    public abstract class GenericRepository<dbContext, T> :
    IGenericRepository<T> where T : class where dbContext : DbContext, new()
    {
        private dbContext _dbContext = new dbContext();
        public dbContext Context
        {

            get { return _dbContext; }
            set { _dbContext = value; }
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);
            return query;
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _dbContext.Set<T>();
            return query;
        }
    }

}
