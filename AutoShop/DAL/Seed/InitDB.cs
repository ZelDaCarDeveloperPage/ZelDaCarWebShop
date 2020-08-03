using AutoShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace AutoShop.DAL.Seed
{
    public class InitDBController : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            context.Roles.AddRange(new List<Role>()
            { 
                new Role() { ID = 0, TITLE = "Admin", TYPE = Models.Enum.RoleType.ADMIN},
                new Role() { ID = 1, TITLE = "User", TYPE = Models.Enum.RoleType.CLIENT}
            });
            context.Users.AddRange(new List<User>()
            {
                new User() { ID = 0, EMAIL = "test@mail.ru", FNAME = "Петр", SNAME = "Петров", TNAME = "Петрович", TEL = "8-8888-888-88-88"},
                new User() { ID = 1, EMAIL = "test2@mail.ru", FNAME = "Александр", SNAME = "Петров", TNAME = "Петрович", TEL = "8-7777-888-88-88"}
            });
            context.Role_Users.AddRange(new List<Role_User>() 
            {
                new Role_User() { ID = 0, ROLE_ID = 0, USER_ID = 0, GIVERID = 0},
                new Role_User() { ID = 1, ROLE_ID = 1, USER_ID = 1, GIVERID = 1},
            });
            context.SaveChanges();
        }
    }
}