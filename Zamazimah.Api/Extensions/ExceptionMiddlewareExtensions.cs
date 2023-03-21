using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Zamazimah.Core.Utils.Mailing;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Zamazimah.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {


                        string bodyString = "";
                        try
                        {
                            using (StreamReader reader = new StreamReader(context.Request.Body, true))
                            {
                                if (reader != null)
                                {
                                    bodyString = reader.ReadToEnd();
                                }
                            };
                        }  catch (Exception) { }
                        var data = new { URL = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}",
                            Payload = $"{bodyString}",
                            Detail = $"{contextFeature.Error}"
                        };
                        try
                        {
                            var mailSettings = new MailSettings
                            {
                                EnableSsl = true,
                                Host = configuration.GetValue<string>("MailSettings:Host"),
                                Port = configuration.GetValue<int>("MailSettings:port"),
                                Password = configuration.GetValue<string>("MailSettings:Password"),
                                Sender = configuration.GetValue<string>("MailSettings:Sender"),
                                UserName = configuration.GetValue<string>("MailSettings:Username"),
                            };
                            MessageModel message = new MessageModel
                            {
                                Subject = "ZAMAZIMAH ERROR LOG",
                                Body = JsonSerializer.Serialize(data),
                                To = new List<string> { "younesmabrouk@gmail.com" },
                                From = mailSettings.Sender,
                            };
                            SMTPMailSender.SendMail(message, mailSettings);
                        }
                        catch (Exception) { }


                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            Title = "An unexpected error occurred!",
                            Status = context.Response.StatusCode,
                            Detail = $"{contextFeature.Error}",
                            Success = false
                        }));
                    }
                });
            });
        }
    }
}
