using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Smalldebts.Backend.DataObjects;
using Smalldebts.Backend.Models;
using Owin;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Smalldebts.Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();


            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
= Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DataObjects.Debt, ItermediateObjects.Debt>();
                cfg.CreateMap<DataObjects.Movement, ItermediateObjects.Movement>();
            });

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);
            //config.Routes.MapHttpRoute("DefaultWeb","{controller}/{action}");


            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = Constants.SigninKey,
                    ValidAudiences = new[] { Constants.Host },
                    ValidIssuers = new[] { Constants.Host },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);

        }
    }


    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        private ApplicationUser DemoUser;


        Random r = new Random();

        protected override async void Seed(MobileServiceContext context)
        {
            InitializeIdentityForEF(context);
            var debts = new Debt[]
            {
                new Debt
                {
                    Id = "50b39267-d1ec-4c44-a1ec-bf16960bc6c2",
                    Name = "Earl"
                },
                new Debt
                {
                    Id = "d8e40c04-7be9-4941-808b-a0fcfbb14061",
                    Name = "Christine"
                },
                new Debt
                {
                    Id = "fb8f36c3-0479-4ee6-9275-10c48aaff5f1",
                    Name = "Debra"
                },
                new Debt
                {
                    Id = "d34bb64c-37c1-4bd8-a7ff-2345cabccfa4",
                    Name = "Alan"
                },
                new Debt
                {
                    Id = "c92a4237-fcb3-4879-ac80-2f199a52e57e",
                    Name = "Andrea"
                },
                new Debt
                {
                    Id = "8f2b7c79-0735-4af9-addb-84fca8662ad3",
                    Name = "Louis"
                },
                new Debt
                {
                    Id = "4aab7d1f-02db-4372-945e-e3e46beb70de",
                    Name = "Brandon"
                },
                new Debt
                {
                    Id = "976acb7f-720c-4089-a535-fb5a3370ca3e",
                    Name = "Frances"
                },
                new Debt
                {
                    Id = "d7d3d8fe-240d-4db6-961f-b440738d343a",
                    Name = "Juan"
                },
                new Debt
                {
                    Id = "331f1681-c476-464e-9bd0-d62b9e6ade3a",
                    Name = "Craig"
                },
                new Debt
                {
                    Id = "bcc34853-c76c-40d0-8263-ce202d555406",
                    Name = "Katherine"
                },
                new Debt
                {
                    Id = "d953baaa-ea29-4de8-af90-16027a4a4cfe",
                    Name = "Timothy"
                },
                new Debt
                {
                    Id = "e3c09d52-4313-42dd-ad13-2578a19b3383",
                    Name = "Mildred"
                },
                new Debt
                {
                    Id = "048a7fb3-7a1e-438d-a3b0-66669c8f4550",
                    Name = "Nicholas"
                },
                new Debt
                {
                    Id = "7022fec2-59a1-4655-afaf-a8da938abd27",
                    Name = "Jonathan"
                }
            };


            foreach (var d in debts)
            {
                d.UserId = DemoUser.Id;
                d.User = DemoUser;
                context.Debts.Add(d);
            }


            var movs = new Movement[]

            {
new Movement { Amount = 9571.68m , Date = new DateTime(2016,7,16)},
new Movement { Amount = 4655.35m , Date = new DateTime(2016,12,17)},
new Movement { Amount = 146.58m , Date = new DateTime(2016,11,1)},
new Movement { Amount = 242.93m , Date = new DateTime(2017,1,10)},
new Movement { Amount = 9159.79m , Date = new DateTime(2016,5,3)},
new Movement { Amount = -7305.66m , Date = new DateTime(2016,8,28)},
new Movement { Amount = -9573.97m , Date = new DateTime(2016,10,28)},
new Movement { Amount = -1015.45m , Date = new DateTime(2016,9,29)},
new Movement { Amount = 8994.61m , Date = new DateTime(2016,10,8)},
new Movement { Amount = -6754.36m , Date = new DateTime(2016,11,6)},
new Movement { Amount = -4425.59m , Date = new DateTime(2016,3,12)},
new Movement { Amount = 9393.32m , Date = new DateTime(2016,9,19)},
new Movement { Amount = -9770.06m , Date = new DateTime(2016,9,3)},
new Movement { Amount = -4339.93m , Date = new DateTime(2016,6,29)},
new Movement { Amount = 3091.59m , Date = new DateTime(2016,8,30)},
new Movement { Amount = -1480.29m , Date = new DateTime(2016,6,7)},
new Movement { Amount = -8150.51m , Date = new DateTime(2016,4,11)},
new Movement { Amount = -5068.79m , Date = new DateTime(2016,6,25)},
new Movement { Amount = 6242.8m , Date = new DateTime(2016,4,18)},
new Movement { Amount = -7372.08m , Date = new DateTime(2016,5,21)},
new Movement { Amount = -1521.86m , Date = new DateTime(2017,1,29)},
new Movement { Amount = -9378.07m , Date = new DateTime(2016,4,8)},
new Movement { Amount = 7369.25m , Date = new DateTime(2016,11,9)},
new Movement { Amount = 2727.3m , Date = new DateTime(2016,8,4)},
new Movement { Amount = -864.5m , Date = new DateTime(2016,6,23)},
new Movement { Amount = -6330.05m , Date = new DateTime(2017,1,7)},
new Movement { Amount = 1854.98m , Date = new DateTime(2016,11,20)},
new Movement { Amount = -6892.04m , Date = new DateTime(2016,10,24)},
new Movement { Amount = 6022.13m , Date = new DateTime(2016,11,9)},
new Movement { Amount = 8013.53m , Date = new DateTime(2016,6,30)},
new Movement { Amount = 5790.58m , Date = new DateTime(2016,7,27)},
new Movement { Amount = -4278.99m , Date = new DateTime(2016,9,24)},
new Movement { Amount = 93.72m , Date = new DateTime(2016,12,27)},
new Movement { Amount = 8833.96m , Date = new DateTime(2016,11,7)},
new Movement { Amount = 4279.33m , Date = new DateTime(2016,8,27)},
new Movement { Amount = 6926.94m , Date = new DateTime(2016,8,18)},
new Movement { Amount = -2552.39m , Date = new DateTime(2016,3,22)},
new Movement { Amount = -696.46m , Date = new DateTime(2016,12,12)},
new Movement { Amount = -3171.57m , Date = new DateTime(2016,4,22)},
new Movement { Amount = 2897.64m , Date = new DateTime(2016,5,4)},
new Movement { Amount = 9834.71m , Date = new DateTime(2016,12,11)},
new Movement { Amount = 5205.38m , Date = new DateTime(2016,5,27)},
new Movement { Amount = -6380.99m , Date = new DateTime(2016,4,3)},
new Movement { Amount = -3282.71m , Date = new DateTime(2017,1,17)},
new Movement { Amount = 6935.96m , Date = new DateTime(2016,6,2)},
new Movement { Amount = 3500.45m , Date = new DateTime(2016,7,27)},
new Movement { Amount = 450.45m , Date = new DateTime(2016,5,20)},
new Movement { Amount = 1852.69m , Date = new DateTime(2016,12,11)},
new Movement { Amount = -8540.99m , Date = new DateTime(2016,10,6)},
new Movement { Amount = 2617.15m , Date = new DateTime(2016,5,18)},
new Movement { Amount = -4693.78m , Date = new DateTime(2016,12,17)},
new Movement { Amount = -1556.72m , Date = new DateTime(2016,6,10)},
new Movement { Amount = -3867.71m , Date = new DateTime(2016,3,18)},
new Movement { Amount = 9257.97m , Date = new DateTime(2016,9,7)},
new Movement { Amount = 8202.53m , Date = new DateTime(2017,1,4)},
new Movement { Amount = 9193.81m , Date = new DateTime(2016,8,1)},
new Movement { Amount = 4819.12m , Date = new DateTime(2016,10,4)},
new Movement { Amount = -6551.89m , Date = new DateTime(2016,12,20)},
new Movement { Amount = -3926.76m , Date = new DateTime(2016,7,30)},
new Movement { Amount = -3435.82m , Date = new DateTime(2016,12,17)},
new Movement { Amount = 8319.59m , Date = new DateTime(2016,7,26)},
new Movement { Amount = -9045.41m , Date = new DateTime(2016,4,4)},
new Movement { Amount = 6857.05m , Date = new DateTime(2016,9,23)},
new Movement { Amount = 8658.34m , Date = new DateTime(2016,9,24)},
new Movement { Amount = -30.36m , Date = new DateTime(2016,8,27)},
new Movement { Amount = 3062.14m , Date = new DateTime(2016,7,15)},
new Movement { Amount = -4222.62m , Date = new DateTime(2016,7,30)},
new Movement { Amount = -3448.62m , Date = new DateTime(2016,10,10)},
new Movement { Amount = -5161.82m , Date = new DateTime(2016,5,2)},
new Movement { Amount = -35.1m , Date = new DateTime(2016,11,19)},
new Movement { Amount = -8960.04m , Date = new DateTime(2016,12,25)},
new Movement { Amount = 9644.23m , Date = new DateTime(2016,11,6)},
new Movement { Amount = -1363.4m , Date = new DateTime(2017,1,3)},
new Movement { Amount = -7118.11m , Date = new DateTime(2016,3,24)},
new Movement { Amount = 9287.32m , Date = new DateTime(2017,2,10)},
new Movement { Amount = 5149.36m , Date = new DateTime(2016,6,21)},
new Movement { Amount = 252.32m , Date = new DateTime(2016,5,2)},
new Movement { Amount = -4798.75m , Date = new DateTime(2016,4,4)},
new Movement { Amount = 5478.01m , Date = new DateTime(2017,1,1)},
new Movement { Amount = 2262.49m , Date = new DateTime(2016,5,7)},
new Movement { Amount = 9577.08m , Date = new DateTime(2016,3,25)},
new Movement { Amount = 4168.02m , Date = new DateTime(2016,8,22)},
new Movement { Amount = 63.49m , Date = new DateTime(2016,11,15)},
new Movement { Amount = 9293.43m , Date = new DateTime(2017,1,18)},
new Movement { Amount = 2020.7m , Date = new DateTime(2016,4,10)},
new Movement { Amount = -6436.46m , Date = new DateTime(2016,10,11)},
new Movement { Amount = 9850.19m , Date = new DateTime(2016,5,4)},
new Movement { Amount = 6010.16m , Date = new DateTime(2016,11,15)},
new Movement { Amount = 3195.2m , Date = new DateTime(2016,4,6)},
new Movement { Amount = -2883.64m , Date = new DateTime(2016,11,14)},
new Movement { Amount = -7456.79m , Date = new DateTime(2016,7,21)},
new Movement { Amount = -2661.23m , Date = new DateTime(2016,3,29)},
new Movement { Amount = 2922.93m , Date = new DateTime(2016,4,23)},
new Movement { Amount = -8602.97m , Date = new DateTime(2016,5,9)},
new Movement { Amount = 6949.93m , Date = new DateTime(2016,10,7)},
new Movement { Amount = -2890.51m , Date = new DateTime(2016,8,9)},
new Movement { Amount = 2923.12m , Date = new DateTime(2016,5,22)},
new Movement { Amount = -6109.83m , Date = new DateTime(2017,2,7)},
new Movement { Amount = -6568.94m , Date = new DateTime(2016,12,16)},
new Movement { Amount = -4955.99m , Date = new DateTime(2016,6,15)},
            };

            var sorted = movs.OrderBy(d => d.Date);


            foreach (var d in debts)
            {
                var takenMovs = sorted.Take(r.Next(0, movs.Length));

                foreach (var m in takenMovs)
                {
                    var newM = new Movement
                    {
                        Amount = m.Amount,
                        Date = m.Date,
                        Id = Guid.NewGuid().ToString(),
                        CreatedAt = m.Date
                    };
                    d.Balance += m.Amount;
                    context.Movements.Add(newM);
                    d.Movements.Add(newM);
                }
            }

            base.Seed(context);
        }

        public void InitializeIdentityForEF(MobileServiceContext db)
        {

            if (!db.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);


                DemoUser = userManager.FindByName("admin");
                if (DemoUser == null)
                {
                    DemoUser = new ApplicationUser()
                    {
                        UserName = "demo@messier16.com",
                        Email = "demo@messier16.com",
                        EmailConfirmed = true,
                        JoinDate = DateTimeOffset.Now
                    };
                    userManager.Create(DemoUser, "9XN@p#=8yZdu?dg");
                    userManager.SetLockoutEnabled(DemoUser.Id, false);
                }


            }

        }
    }
}

