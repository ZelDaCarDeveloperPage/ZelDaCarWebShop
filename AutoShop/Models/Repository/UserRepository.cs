using AutoShop.DAL;
using AutoShop.Models.Repository.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AutoShop.Models.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext _APPContext;
        private readonly IDbSet<User> _DbSet; 

        public UserRepository() 
        {
            this._APPContext = ApplicationContext.getInstance();
            _DbSet = _APPContext.Set<User>();
        }
        

        public User Create(User item)
        {
            _DbSet.Add(item);
            _APPContext.SaveChanges();
            return item;
        }

        public User Delete(int id)
        {
            User user = Get(id);
            _DbSet.Remove(user);
            _APPContext.SaveChanges();
            return null;
        }

        public User Get(int id)
        {
           return _DbSet.Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<User> GetList()
        {
            return _DbSet;
        }

        public User Update(User item)
        {
            User curUser = Get(item.ID);
            curUser.FNAME = item.FNAME;
            curUser.SNAME = item.SNAME;
            curUser.TNAME = item.TNAME;
            curUser.TEL = item.TEL;
            curUser.EMAIL = item.EMAIL;
            _APPContext.SaveChanges();
            return curUser;
        }
        public User GetUserById(int userId)
        {
            return _DbSet.Where(x => x.ID == userId).FirstOrDefault();
        }
        public User GetUserByEmailAndPass(string email, string pass) 
        {
            return _DbSet.Where(x => (x.EMAIL == email && x.PASSWORD == pass)).FirstOrDefault();
        }
    }
}