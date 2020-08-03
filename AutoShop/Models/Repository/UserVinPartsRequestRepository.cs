using AutoShop.DAL;
using AutoShop.Models.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AutoShop.Models.Repository
{
    public class UserVinPartsRequestRepository : IRepository<UserVinPartsRequest>
    {
        private readonly ApplicationContext _APPContext;
        private readonly IDbSet<UserVinPartsRequest> _DbSet;
        public UserVinPartsRequestRepository()
        {
            _APPContext = ApplicationContext.getInstance(); ;
            _DbSet = _APPContext.Set<UserVinPartsRequest>();
        }

        public UserVinPartsRequest Create(UserVinPartsRequest item)
        {
            _DbSet.Add(item);
            _APPContext.SaveChanges();
            return item;
        }

        public UserVinPartsRequest Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserVinPartsRequest Get(int id)
        {
            return _DbSet.Where(gid => gid.ID == id).FirstOrDefault();
        }

        public IEnumerable<UserVinPartsRequest> GetList()
        {
            return _DbSet;
        }

        public UserVinPartsRequest Update(UserVinPartsRequest item)
        {
            UserVinPartsRequest request = Get(item.ID);
            request.IsCompleted = item.IsCompleted;
            request.ManagerId = item.ManagerId;
            request.Request = item.Request;
            request.UserID = item.UserID;
            request.VinCode = item.VinCode;
            _APPContext.SaveChanges();
            return request;
        }

        public List<UserVinPartsRequest> GetActiveRequests() 
        {
            return _DbSet.Where(x => x.IsCompleted == false).ToList();
        }
        public UserVinPartsRequest GetByUserId(int userId) 
        {
            return _DbSet.Where(x => x.UserID == userId).FirstOrDefault();
        }
        public UserVinPartsRequest GetActiveUserRequest(int userId) 
        {
            return _DbSet.Where(x => (x.UserID == userId && x.IsCompleted == false)).FirstOrDefault();
        }
    }
}