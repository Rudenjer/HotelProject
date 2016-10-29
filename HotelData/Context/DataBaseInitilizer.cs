using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HotelData.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HotelData.Context;


namespace HotelData.Context
{
    public class DataBaseInitilizer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = "denjers@yandex.ru", UserName = "denjers@yandex.ru" };
            string password = "1!Gfhjkm";
            var result = userManager.Create(admin, password);


            if(result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
               

            }

            List<Record> records1 = new List<Record>
            {

                new Record {ClientID="1", DateIn= new DateTime(2016,3,14), DateOut = new DateTime(2016,3,16)},
                new Record {ClientID="2", DateIn=new DateTime(2016,4,15), DateOut= new DateTime(2016,4,20) },
                new Record {ClientID="2", DateIn=DateTime.Now.AddDays(-3),  DateOut= DateTime.Now.AddDays(3)  },
                new Record {ClientID="2", DateIn=DateTime.Now.AddDays(12),  DateOut= DateTime.Now.AddDays(17)}
            };

            List<Record> records2 = new List<Record>
            {

                new Record {ClientID="1", DateIn= new DateTime(2015,10,1), DateOut = new DateTime(2015,10,5)},
                new Record {ClientID="3", DateIn=new DateTime(2015,11,3), DateOut = new DateTime(2015,11,8)},
                new Record {ClientID="2", DateIn=DateTime.Now.AddDays(-2),  DateOut= DateTime.Now.AddDays(2)},
                new Record {ClientID="2", DateIn=DateTime.Now.AddDays(10),  DateOut= DateTime.Now.AddDays(15)}
            };


            List<Room> rooms = new List<Room>
            {
                new Room { Class="1", Persons=2, Price=100, Records = records1 },
                new Room { Class="2", Persons=3, Price=120, Records = records2 }
            };

            



            context.Rooms.AddRange(rooms);
            context.SaveChanges();

            //base.Seed(context);
        }

    }
}
