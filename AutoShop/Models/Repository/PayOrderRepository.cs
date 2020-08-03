using AutoShop.api.Payment.YandexMoney;
using AutoShop.DAL;
using AutoShop.Models.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoShop.Models.Repository
{
    public class PayOrderRepository : IRepository<AutoShop.api.Payment.YandexMoney.YandexOrderResponseModel>
    {
        private readonly ApplicationContext _APPContext;
        private readonly IDbSet<AutoShop.api.Payment.YandexMoney.YandexOrderResponseModel> _DbSet;
        public PayOrderRepository()
        {
            _APPContext = ApplicationContext.getInstance(); ;
            _DbSet = _APPContext.Set<AutoShop.api.Payment.YandexMoney.YandexOrderResponseModel>();
        }
        public YandexOrderResponseModel Create(YandexOrderResponseModel item)
        {
            _DbSet.Add(item);
            _APPContext.SaveChanges();
            return item;
        }

        public YandexOrderResponseModel Delete(int id)
        {
            _DbSet.Remove(Get(id));
            _APPContext.SaveChanges();
            return null;
        }

        public YandexOrderResponseModel Get(int id)
        {
            return _DbSet.Where(x => x.OrderId == id.ToString()).FirstOrDefault();
        }

        public IEnumerable<YandexOrderResponseModel> GetList()
        {
            throw new NotImplementedException();
        }

        public YandexOrderResponseModel Update(YandexOrderResponseModel item)
        {
            throw new NotImplementedException();
        }
    }
}