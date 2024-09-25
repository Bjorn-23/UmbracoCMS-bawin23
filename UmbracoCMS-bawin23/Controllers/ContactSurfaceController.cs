using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoCMS_bawin23.Models;

namespace UmbracoCMS_bawin23.Controllers;

public class ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor,
    IUmbracoDatabaseFactory databaseFactory,
    ServiceContext services, AppCaches appCaches,
    IProfilingLogger profilingLogger,
    IPublishedUrlProvider publishedUrlProvider) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
    public async Task<IActionResult> HandleCallbackRequestSubmit(CallbackRequestFormModel form)
    {
        if (!ModelState.IsValid)
        {
            ViewData["name"] = form.Name;
            ViewData["email"] = form.Email;
            ViewData["phone"] = form.Phone;
            //ViewData["message"] = form.Message;

            ViewData["name-error"] = string.IsNullOrEmpty(form.Name);
            ViewData["email-error"] = string.IsNullOrEmpty(form.Email);
            ViewData["phone-error"] = string.IsNullOrEmpty(form.Email);

            //ViewData["message-error"] = string.IsNullOrEmpty(form.Message);

            ViewData["form-success"] = "false";
            return CurrentUmbracoPage();
        }
        else
        {
            // logic for receiving and handling the message internally goes here.

            var consultingTypes = new Dictionary<string, string>
            {
                { "financialConsulting", "Financial Consulting" },
                { "riskManagement", "Risk Management" },
                { "bondsCommodities", "Bonds & Commodities" },
                { "assuranceAudits", "Assurance & Audits" },
                { "taxAndTrusts", "Tax and Trusts" },
                { "statisticAdvisors", "Statistic Advisors" },
                { "investmentPortfolio", "Investment Portfolio" },
                { "longTermInterest", "Long Term Interest" }
            };
            consultingTypes.TryGetValue(form.Subject, out var consultingType);

            var supportEmail = "sales@onatrix.com";

            await using var client = new ServiceBusClient("Endpoint=sb://sb-umbraco.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=7BrearpT4jkCwZR4oOQreea8UXEKfhaGQ+ASbCg95xI=");
            ServiceBusSender sender = client.CreateSender("email_request");
            var confirmationEmail = new confirmationEmailModel()
            {
                To = form.Email,
                Subject = $"Callback from Onatrix regarding {consultingType}.",
                PlainText = $"Thank you for contacting Onatrix, {form.Name}. We will get in touch with you on {form.Phone} within the next 24 hrs." +
                $"If you need to talk to us sooner please don't hesitate to call our local offices and speak to a specialist in {consultingType}." +
                $"Important: This is an automated message and the email address can not receive replies. For assistance, contact our support team at {supportEmail}",
                HtmlBody = "<!DOCTYPE html>" +
                           "<html lang=\"en\">" +
                           "<head>" +
                           "<meta charset=\"UTF-8\">" +
                           "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                           "<title>Thank You for Contacting Onatrix!</title>" +
                           "<style>" +
                           "body {" +
                           "max-width: 500px;" +
                           "font-family: Arial, sans-serif;" +
                           "margin: 0 16px;" +
                           "background-color: #fff;" +
                           $"color: ##4f5955" +
                           "}" +
                           ".container {" +
                           "background-color: #fff;" +
                           "padding: 20px;" +
                           "border-radius: 5px;" +
                           "box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);" +
                           "}" +
                           "h1 {" +
                           "font-size: 22px;" +
                           "background-color: #4f5955;" +
                           "padding: 16px;" +
                           "border-radius: 4px;" +
                           "color: #d9c3a9;" +
                           "@media (min-width: 768px) { font-size: 44px; } " +
                           "}" +
                           "</style>" +
                           "</head>" +
                           "<body>" +
                           "<div class=\" container\">" +
                           "<h1>Thank You for Contacting Onatrix!</h1>" +
                           $"<p>Thank you for contacting us {form.Name}, we will get in touch with you on <strong>{form.Phone}</strong> within the next 24 hours.</p>" +
                           $"<p>If you need to talk to us sooner, please don't hesitate to call our local offices and ask to speak to a specialist in <strong>{consultingType}</strong>.</p>" +
                           "<p style=\"font-style: italic;\">/The Onatrix Team</p>" +
                           "</div>" +
                           "</body>" +
                           $"<footer style=\"padding: 16px 0;\">" +
                           $"<strong>Important</strong>: This is an automated message and the email address can not receive replies.<br>For assistance, contact our support team at {supportEmail}</footer>" +
                           "</html>"
            };

            var json = JsonConvert.SerializeObject(confirmationEmail);
            ServiceBusMessage message = new ServiceBusMessage(json) { ContentType = "application/json" };

            try
            {
                await sender.SendMessageAsync(message);
                ViewData["form-success"] = "true";

                ViewData["name"] = string.Empty;
                ViewData["email"] = string.Empty;
                ViewData["phone"] = string.Empty;
                await sender.DisposeAsync();
                await client.DisposeAsync();

                return CurrentUmbracoPage();
            }
            catch (ServiceBusException ex)
            {
                Debug.WriteLine($"ERROR : ServiceBusException - ContactSurfaceController.HandleCallbackRequestSubmit() {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR : Exception - ContactSurfaceController.HandleCallbackRequestSubmit() {ex.Message}");
            }

            ViewData["form-success"] = "false";
            return CurrentUmbracoPage();
        }
    }

    public async Task<IActionResult> HandleContactRequestSubmit(ContactFormModel form)
    {
        var currentPageUrl = UmbracoContext.CleanedUmbracoUrl.ToString();
        int queryIndex = currentPageUrl.IndexOf('?');

        // If a query string exists, remove it
        if (queryIndex >= 0)
        {
            currentPageUrl = currentPageUrl.Substring(0, queryIndex);
        }

        var queryString = "";

        var fragmentId = "service-details-contact-form";
        var fragment = $"#{fragmentId}"; // Create the fragment

        if (!ModelState.IsValid || form.Message == null)
        {
            var paramName = !string.IsNullOrEmpty(form.Name) ? $"param={form.Name}" : "";
            var paramEmail = !string.IsNullOrEmpty(form.Email) ? $"paramEmail={form.Email}" : "";
            var paramMessage = !string.IsNullOrEmpty(form.Message) ? $"paramMessage={form.Message}" : "";
            var paramFormSubmitted = "paramFormSubmitted=true";

            // Combine query parameters correctly
            if (!string.IsNullOrEmpty(paramName))
            {
                queryString = $"?{paramName}";
            }
            if (!string.IsNullOrEmpty(paramEmail))
            {
                queryString += !string.IsNullOrEmpty(queryString) ? $"&{paramEmail}" : $"?{paramEmail}";
            }
            if (!string.IsNullOrEmpty(paramMessage))
            {
                queryString += !string.IsNullOrEmpty(queryString) ? $"&{paramMessage}" : $"?{paramMessage}";
            }
            if (!string.IsNullOrEmpty(paramFormSubmitted))
            {
                queryString += !string.IsNullOrEmpty(queryString) ? $"&{paramFormSubmitted}" : $"?{paramFormSubmitted}";
            }

            return Redirect(currentPageUrl + queryString + fragment);
        }
        else
        {

            // logic for receiving and handling the message internally goes here.

            var supportEmail = "sales@onatrix.com";

            await using var client = new ServiceBusClient("Endpoint=sb://sb-umbraco.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=7BrearpT4jkCwZR4oOQreea8UXEKfhaGQ+ASbCg95xI=");
            ServiceBusSender sender = client.CreateSender("email_request");
            var confirmationEmail = new confirmationEmailModel()
            {
                To = form.Email,
                Subject = $"Thank you for contacting Onatrix!",
                PlainText = $"Thank you for contacting us, {form.Name}. We will get in touch with you at {form.Email} within the next 24 hrs." +
                $"If you need to talk to us sooner please don't hesitate to call our local offices and speak to a customer support specialist." +
                $"Your Message: '{form.Message}'." +
                $"Important: This is an automated message and the email address can not receive replies. For assistance, contact our support team at {supportEmail}",
                HtmlBody = "<!DOCTYPE html>" +
                           "<html lang=\"en\">" +
                           "<head>" +
                           "<meta charset=\"UTF-8\">" +
                           "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                           "<title>Thank You for Contacting Onatrix!</title>" +
                           "<style>" +
                           "body {" +
                           "max-width: 500px;" +
                           "font-family: Arial, sans-serif;" +
                           "margin: 0 16px;" +
                           "background-color: #fff;" +
                           $"color: ##4f5955" +
                           "}" +
                           ".container {" +
                           "background-color: #fff;" +
                           "padding: 20px;" +
                           "border-radius: 5px;" +
                           "box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);" +
                           "}" +
                           "h1 {" +
                           "font-size: 22px;" +
                           "background-color: #4f5955;" +
                           "padding: 16px;" +
                           "border-radius: 4px;" +
                           "color: #d9c3a9;" +
                           "@media (min-width: 768px) { font-size: 44px; } " +
                           "}" +
                           "</style>" +
                           "</head>" +
                           "<body>" +
                           "<div class=\" container\">" +
                           "<h1>Thank You for Contacting Onatrix!</h1>" +
                           $"<p>Thank you for contacting us {form.Name}, we will get in touch with you at <strong>{form.Email}</strong> within the next 24 hours.</p>" +
                           $"<p>If you need to talk to us sooner, please don't hesitate to call our local offices and ask to speak to a customer support specialist.</p>" +
                           "<p style=\"font-style: italic;\">/The Onatrix Team</p>" +
                           "<p><br><strong>Your Message:</strong></p>" +
                           $"<p style=\"font-style: italic;\">'{form.Message}'</p>" +
                           "</div>" +
                           "</body>" +
                           $"<footer style=\"padding: 16px 0;\">" +
                           $"<strong>Important</strong>: This is an automated message and the email address can not receive replies.<br>For assistance, contact our support team at {supportEmail}</footer>" +
                           "</html>"
            };

            var json = JsonConvert.SerializeObject(confirmationEmail);
            ServiceBusMessage message = new ServiceBusMessage(json) { ContentType = "application/json" };

            try
            {
                await sender.SendMessageAsync(message);
                ViewData["form-success"] = "true";

                ViewData["name"] = string.Empty;
                ViewData["email"] = string.Empty;
                ViewData["phone"] = string.Empty;
                await sender.DisposeAsync();
                await client.DisposeAsync();
                queryString = "?paramFormSuccess=true";

                //return CurrentUmbracoPage();
                return Redirect(currentPageUrl + queryString + fragment);
            }
            catch (ServiceBusException ex)
            {
                Debug.WriteLine($"ERROR : ServiceBusException - ContactSurfaceController.HandleContactRequestSubmit() {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR : Exception - ContactSurfaceController.HandleContactRequestSubmit() {ex.Message}");
            }

            ViewData["form-success"] = "false";
            return CurrentUmbracoPage();
        }
    }

    public async Task<IActionResult> HandleOnlineSupportFormSubmit(OnlineSupportFormModel form)
    {
        if (!ModelState.IsValid)
        {
            ViewData["online-support"] = form.Email;
            ViewData["online-support-error"] = string.IsNullOrEmpty(form.Email);
            return CurrentUmbracoPage();
        }
        else
        {
            // logic for receiving and handling the message internally goes here.

            var supportEmail = "sales@onatrix.com";

            await using var client = new ServiceBusClient("Endpoint=sb://sb-umbraco.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=7BrearpT4jkCwZR4oOQreea8UXEKfhaGQ+ASbCg95xI=");
            ServiceBusSender sender = client.CreateSender("email_request");
            var confirmationEmail = new confirmationEmailModel()
            {
                To = form.Email,
                Subject = $"Thank you for contacting Onatrix - Online Support!",
                PlainText = $"Thank you for contacting us. A member of our online support team will get in touch with you at {form.Email} as soon as possible (normally within 30 minutes)." +
                $"If you need to talk to us sooner please don't hesitate to call our local offices and speak to a customer support specialist." +
                $"Important: This is an automated message and the email address can not receive replies. For assistance, contact our support team at {supportEmail}",
                HtmlBody = "<!DOCTYPE html>" +
                           "<html lang=\"en\">" +
                           "<head>" +
                           "<meta charset=\"UTF-8\">" +
                           "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                           "<title>Thank You for Contacting Onatrix!</title>" +
                           "<style>" +
                           "body {" +
                           "max-width: 500px;" +
                           "font-family: Arial, sans-serif;" +
                           "margin: 0 16px;" +
                           "background-color: #fff;" +
                           $"color: ##4f5955" +
                           "}" +
                           ".container {" +
                           "background-color: #fff;" +
                           "padding: 20px;" +
                           "border-radius: 5px;" +
                           "box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);" +
                           "}" +
                           "h1 {" +
                           "font-size: 22px;" +
                           "background-color: #4f5955;" +
                           "padding: 16px;" +
                           "border-radius: 4px;" +
                           "color: #d9c3a9;" +
                           "@media (min-width: 768px) { font-size: 44px; } " +
                           "}" +
                           "</style>" +
                           "</head>" +
                           "<body>" +
                           "<div class=\" container\">" +
                           "<h1>Thank you for contacting Onatrix - Online Support!</h1>" +
                           $"<p>Thank you for contacting us.  A member of our online support team will get in touch with you at {form.Email} as soon as possible (normally within 30 minutes).</p>" +
                           $"<p>If you need to talk to us sooner, please don't hesitate to call our local offices and ask to speak to a customer support specialist.</p>" +
                           "<p style=\"font-style: italic;\">/The Onatrix Team</p>" +                         
                           "</div>" +
                           "</body>" +
                           $"<footer style=\"padding: 16px 0;\">" +
                           $"<strong>Important</strong>: This is an automated message and the email address can not receive replies.<br>For assistance, contact our support team at {supportEmail}</footer>" +
                           "</html>"
            };

            var json = JsonConvert.SerializeObject(confirmationEmail);
            ServiceBusMessage message = new ServiceBusMessage(json) { ContentType = "application/json" };

            try
            {
                await sender.SendMessageAsync(message);
                ViewData["form-success"] = "true";
                ViewData["online-support"] = string.Empty;
                await sender.DisposeAsync();
                await client.DisposeAsync();

                return CurrentUmbracoPage();
            }
            catch (ServiceBusException ex)
            {
                Debug.WriteLine($"ERROR : ServiceBusException - ContactSurfaceController.HandleOnlineSupportFormSubmit() {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR : Exception - ContactSurfaceController.HandleOnlineSupportFormSubmit() {ex.Message}");
            }

            ViewData["form-success"] = "false";
            return CurrentUmbracoPage();
        }
    }
}
