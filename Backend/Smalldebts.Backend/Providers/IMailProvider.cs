using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Smalldebts.Backend.Providers
{
    public struct Email
    {
        public string Address { get; set; }
        public string Name { get; set; }
    }
    public interface IMailProvider : IDisposable
    {
        Task<bool> SendEmailAsync(Email from, string subject, string content, params Email[] to);
        Task<bool> SendEmailAsync(Email from, string subject, string content, string htmlContent, params Email[] to);
    }

    public class SendGridMailProvider : IMailProvider
    {
        public async Task<bool> SendEmailAsync(Email from, string subject, string content, params Email[] to)
        {
            var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(from.Address, from.Name),
                Subject = subject,
                PlainTextContent = content
            };
            foreach (var email in to)
            {
                msg.AddTo(new EmailAddress(email.Address, email.Name));
            }
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == HttpStatusCode.Accepted;
        }

        public async Task<bool> SendEmailAsync(Email from, string subject, string content, string htmlContent, params Email[] to)
        {
            var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(from.Address, from.Name),
                Subject = subject,
                PlainTextContent = content,
                HtmlContent = htmlContent
            };
            foreach (var email in to)
            {
                msg.AddTo(new EmailAddress(email.Address, email.Name));
            }
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == HttpStatusCode.Accepted;
        }

        public static IMailProvider Create()
        {
            return new SendGridMailProvider();
        }

        public void Dispose()
        {
        }
    }
}