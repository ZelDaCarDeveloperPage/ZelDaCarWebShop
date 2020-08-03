using AutoShop.DAL;
using AutoShop.Models.Enum;
using AutoShop.Models.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AutoShop.Models.Repository
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly ApplicationContext _APPContext;
        private readonly IDbSet<Role> _DbSet;
        public RoleRepository() 
        {
            _APPContext = ApplicationContext.getInstance();
            _DbSet = _APPContext.Set<Role>();
        }
        public Role Create(Role item)
        {
            _DbSet.Add(item);
            _APPContext.SaveChanges();
            return item;
        }

        public Role Delete(int id)
        {
            _DbSet.Remove(Get(id));
            _APPContext.SaveChanges();
            return null;
        }

        public Role Get(int id)
        {
           return _DbSet.Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Role> GetList()
        {
            return _DbSet;
        }

        public Role Update(Role item)
        {
            Role role = Get(item.ID);
            role.TITLE = item.TITLE;
            role.TYPE = item.TYPE;
            _APPContext.SaveChanges();
            return role;
        }

        public int FindRoleIdByType(RoleType type) 
        {
            switch (type) 
            {
                case RoleType.ADMIN:
                    return 1;
                case RoleType.CLIENT:
                    return 2;
                case RoleType.PARTHNER:
                    return 3;
                default: return -1;
            }
        }
    }
}