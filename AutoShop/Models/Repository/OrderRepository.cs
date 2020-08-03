using AutoShop.DAL;
using AutoShop.Models.Repository.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace AutoShop.Models.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly ApplicationContext _APPContext;
        private readonly IDbSet<Order> _DbSet;
        public OrderRepository()
        {
            _APPContext = ApplicationContext.getInstance(); ;
            _DbSet = _APPContext.Set<Order>();
        }
        public Order Create(Order item)
        {
            _DbSet.Add(item);
            _APPContext.SaveChanges();
            return item;
        }

        public Order Delete(int id)
        {
            _DbSet.Remove(Get(id));
            _APPContext.SaveChanges();
            return null;
        }

        public Order Get(int id)
        {
            return _DbSet.Where(x=>x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Order> GetList()
        {
            return _DbSet;
        }

        public Order Update(Order item)
        {
            Order curUser = Get(item.ID);
            curUser.MAKER = item.MAKER;
            curUser.ITEMNAME = item.ITEMNAME;
            curUser.SUM = item.SUM;
            curUser.ENDDATE = item.ENDDATE;
            curUser.USER_ID = item.USER_ID;
            _APPContext.SaveChanges();
            return item;
        }
        public List<Order> GetMyOrders(int userId)
        {
            return _DbSet.Where(x => x.USER_ID == userId).ToList();
        }
    }
}