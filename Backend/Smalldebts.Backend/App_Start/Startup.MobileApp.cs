using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Smalldebts.Backend.DataObjects;
using Smalldebts.Backend.Models;
using Owin;

namespace Smalldebts.Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
= Newtonsoft.Json.ReferenceLoopHandling.Ignore;

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
        protected override void Seed(MobileServiceContext context)
        {
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false }
            };
            List<Debt> debts = new List<Debt>
            {
                new Debt { Id = "551ef9ab-0714-4b70-b887-f1290d2053c3", Name = "Ruby", Balance = 0m },
                new Debt { Id = "2a5f867b-6bf6-4bbe-9e1f-74f63e8b4dd2", Name = "David", Balance = -2878.34m },
                new Debt { Id = "09c07cfd-bc19-4240-86bf-008adbcbc064", Name = "Benjamin", Balance = 4911.41m },
                new Debt { Id = "9ae84c63-bfc4-453b-995c-81787fc2d6d1", Name = "Janet", Balance = -9043.6m },
                new Debt { Id = "102c5c81-9ef9-4228-bb76-c68ac66734ca", Name = "Louise", Balance = 7922.95m },
                new Debt { Id = "c2465f12-32a6-4bbd-8f0a-f5973ac8dc88", Name = "Michael", Balance = -9865.14m },
                new Debt { Id = "975367d6-7770-40b5-b9dd-66cf7bd373b2", Name = "Anna", Balance = 8334.43m },
                new Debt { Id = "fb85522e-9870-46f6-8f33-fdc327d46998", Name = "Lillian", Balance = 1066.01m },
                new Debt { Id = "ebbb87b7-07c9-4ac5-a2fd-14e860146fe5", Name = "Brenda", Balance = 0m },
                new Debt { Id = "d3053457-5ff7-4a16-b561-08d14a9850cb", Name = "Brian", Balance = 4969.52m },
                new Debt { Id = "7e85de8c-572a-471a-885f-7a27fd4eaa3a", Name = "Jimmy", Balance = 4874.23m },
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            foreach (var debt in debts)
            {
                context.Set<Debt>().Add(debt);
            }

            base.Seed(context);
        }
    }
}

