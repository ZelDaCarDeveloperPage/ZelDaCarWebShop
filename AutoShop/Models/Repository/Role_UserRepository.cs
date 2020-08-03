using AutoShop.DAL;
using AutoShop.Models.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoShop.Models.Repository
{
    public class Role_UserRepository : IRepository<Role_User>
    {
        private readonly ApplicationContext _APPContext;
        private readonly IDbSet<Role_User> _DbSet;
        public Role_UserRepository() 
        {
            _APPContext = ApplicationContext.getInstance();
            _DbSet = _APPContext.Set<Role_User>();
        }
        public Role_User Create(Role_User item)
        {
            _DbSet.Add(item);
            _APPContext.SaveChanges();
            return item;
        }

        public Role_User Delete(int id)
        {
            _DbSet.Remove(Get(id));
            _APPContext.SaveChanges();
            return null;
        }

        public Role_User Get(int id)
        {
            return _DbSet.Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Role_User> GetList()
        {
            return _DbSet;
        }

        public Role_User Update(Role_User item)
        {
            Role_User role_User = _DbSet.Find(item);
            role_User.ROLE_ID = item.ROLE_ID;
            role_User.USER_ID = item.USER_ID;
            _APPContext.SaveChanges();
            return item;
        }
        public Role_User GetByUserId(int userId) 
        {
            return _DbSet.Where(x => x.USER_ID == userId).FirstOrDefault();
        }
    }
}