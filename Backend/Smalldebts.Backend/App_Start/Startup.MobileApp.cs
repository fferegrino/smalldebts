using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
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
            

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);

        }
    }


    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        private ApplicationUser DemoUser;

        

        protected override void Seed(MobileServiceContext context)
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
                },
                new Debt
                {
                    Id = "4a6c885f-62fb-4783-a4f2-f2b1be639fa0",
                    Name = "Willie"
                },
                new Debt
                {
                    Id = "dd2bebb7-0d1f-450d-ae2b-4148567ea04d",
                    Name = "Ralph"
                },
                new Debt
                {
                    Id = "18ee4cce-d6ac-4007-affd-21348a1b9660",
                    Name = "Deborah"
                },
                new Debt
                {
                    Id = "ecba5e73-c7eb-47fa-acac-da83aba44cc9",
                    Name = "Patrick"
                },
                new Debt
                {
                    Id = "46865106-e11b-4281-a23f-a5a062e90438",
                    Name = "Phyllis"
                }
            };

            foreach (var d in debts)
            {
                d.UserId = DemoUser.Id;
                d.User = DemoUser;
                context.Debts.Add(d);
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

