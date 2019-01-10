using QuanLyQuanAn.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanAn.DAL.Model;
using QuanLyQuanAn.DAL.ViewModel;

namespace QuanLyQuanAn.DAL.Repository
{
    public class TableFoodRepository :GenericRepository<QuanLyQuanAnDbContext, TableFood> ,ITableFoodRepository, IDisposable
    {
        private readonly QuanLyQuanAnDbContext db;
        private bool disposed;

        public TableFoodRepository(QuanLyQuanAnDbContext _db)
        {
            db = _db;
        }

 
        public IEnumerable<TableFood> GetTableFoods()
        {
            //return GetAll().ToList();
            return db.TableFoods.ToList();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TableFoodDetail> TableFoodDetails(int? idTable)
        {
            var results = from table in db.TableFoods
                          join bill in db.Bills on table.idTable equals bill.idTable
                          join billInfo in db.BillInfoes on bill.idBill equals billInfo.idBill
                          join food in db.Foods on billInfo.idFood equals food.idFood
                          where table.idTable == idTable && bill.statusBill == false
                          group new { table, food, billInfo }
                          by new { table.nameTable, food.nameFood, food.price } into abc
                          select new TableFoodDetail
                          {
                              nameFood = abc.Key.nameFood,
                              price = (double) abc.Key.price,
                              count = abc.Count()
                          };

            return results;
        }

        public void SetStatusTable(int? idTable, string status)
        {
            var result = db.TableFoods.SingleOrDefault(m => m.idTable == idTable);
            if (result != null)
            {
                result.statusTable = status;
                db.SaveChanges();
            }
        }

        public string GetStatusTable(int? idTable)
        {
            var result = db.TableFoods.SingleOrDefault(m => m.idTable == idTable);
            string x = result.statusTable;
            return x;
        }

        public bool AddTable(string nameTable)
        {
            var result = db.TableFoods.SingleOrDefault(m => m.nameTable == nameTable);
            if (result != null)
            {
                return false;
            }
            TableFood T = new TableFood();
            T.nameTable = nameTable;
            T.statusTable = "Trống";

            db.TableFoods.Add(T);
            db.SaveChanges();
            return true;
        }

        public bool DeleteTable(int idTable)
        {
            var result = db.TableFoods.SingleOrDefault(m => m.idTable == idTable);
            if (result == null)
            {
                return false;
            }
            db.TableFoods.Remove(result);
            db.SaveChanges();
            return true;
        }

        public bool EditNameTable(int idTable, string newNameTable)
        {
            var result = db.TableFoods.SingleOrDefault(m => m.idTable == idTable);//id read only nên không cần kiểm tra
            var result2 = db.TableFoods.SingleOrDefault(m => m.nameTable == newNameTable);//kiểm tra xem có trùng tên hay không
            if (result2 != null)
            {
                return false;
            }
            result.nameTable = newNameTable;
            db.SaveChanges();
            return true;
        }
    }
}
