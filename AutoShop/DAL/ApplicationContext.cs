using AutoShop.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using AutoShop.api.Payment.YandexMoney;

namespace AutoShop.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } // пользователи
        public DbSet<Order> Orders { get; set; } // заказы
        public DbSet<Role> Roles { get; set; } // роли пользователей
        public DbSet<Role_User> Role_Users { get; set; } // пользователь-роль
        public DbSet<UsersPartActs> UsersPartActs { get; set; } // данные об оригинальных автозапчастях пользователя
        public DbSet<UserVinPartsRequest> UserVinPartsRequests { get; set; } // запросы на определение ориг. автозапчастей
        public DbSet<PayResponse> payResponses { get; set; } // данные об оплате заказов


        public static ApplicationContext instance;
        public static ApplicationContext getInstance()
        {
            if (instance == null)
                instance = new ApplicationContext();
            return instance;
        }
        public ApplicationContext() : base("ProdContext") 
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}