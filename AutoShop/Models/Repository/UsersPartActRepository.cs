using AutoShop.DAL;
using AutoShop.Models.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AutoShop.Models.Repository
{
    public class UsersPartActRepository : IRepository<UsersPartActs>
    {
        private readonly ApplicationContext _APPContext;
        private readonly IDbSet<UsersPartActs> _DbSet;
        public UsersPartActRepository()
        {
            _APPContext = ApplicationContext.getInstance(); ;
            _DbSet = _APPContext.Set<UsersPartActs>();
        }
        public UsersPartActs Create(UsersPartActs item)
        {
            _DbSet.Add(item);
            _APPContext.SaveChanges();
            return item;
        }

        public UsersPartActs Delete(int id)
        {
            _DbSet.Remove(Get(id));
            _APPContext.SaveChanges();
            return null;
        }

        public UsersPartActs Get(int id)
        {
            return _DbSet.Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<UsersPartActs> GetList()
        {
            return _DbSet;
        }

        public UsersPartActs Update(UsersPartActs item)
        {
            UsersPartActs model = Get(item.ID);
            model.Arts = item.Arts;
            model.UserId = item.UserId;
            return model;
        }
        public UsersPartActs GetByUserId(int id) 
        {
            return _DbSet.Where(x => x.UserId == id).FirstOrDefault();
        }
    }
}