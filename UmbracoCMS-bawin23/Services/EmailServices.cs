using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Diagnostics;
using UmbracoCMS_bawin23.Models;

namespace UmbracoCMS_bawin23.Services;

public class EmailServices
{
    public async Task<bool> TryDetermineFormModel(object formModel)
    {
        switch (formModel)
        {
            case CallbackRequestFormModel:
                var callbackCast  = formModel.SafeCast<CallbackRequestFormModel>();
                if (callbackCast != null)
                {
                    var callbackEmail = BuildEmail(callbackCast, null, null);
                    if (callbackEmail != null)
                    {
                        var sendEmail = await SendEmailAsync(callbackEmail);
                        if (sendEmail)
                        {
                            return true;
                        }
                    }
                }
                return false;

            case ContactFormModel:
                var contactCast = formModel.SafeCast<ContactFormModel>();
                if (contactCast != null)
                {
                    var contactEmail = BuildEmail(null, contactCast, null);
                    if (contactEmail != null)
                    {
                        var sendEmail = await SendEmailAsync(contactEmail);
                        if (sendEmail)
                        {
                            return true;
                        }
                    }

                }
                return false;

            case OnlineSupportFormModel:
                var supportCast = formModel.SafeCast<OnlineSupportFormModel>();
                if (supportCast != null)
                {
                    var supportEmail = BuildEmail(null, null, supportCast);
                    if (supportEmail != null)
                    {
                        var sendEmail = await SendEmailAsync(supportEmail);
                        if (sendEmail)
                        {
                            return true;
                        }
                    }
                }
                return false;

            default:
                return false;
        }       
    }

    public async Task<bool> SendEmailAsync(ConfirmationEmailModel email)
    {
        await using var client = new ServiceBusClient("Endpoint=sb://sb-umbraco.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=7BrearpT4jkCwZR4oOQreea8UXEKfhaGQ+ASbCg95xI=");
        ServiceBusSender sender = client.CreateSender("email_request");
        var json = JsonConvert.SerializeObject(email);
        ServiceBusMessage message = new ServiceBusMessage(json) { ContentType = "application/json" };

        try
        {
            await sender.SendMessageAsync(message);
            return true;
        }
        catch (ServiceBusException ex)
        {
            Debug.WriteLine($"ERROR : ServiceBusException - EmailServices.SendEmailAsync() {ex.Message}");
        }
        return false;
    }

    public ConfirmationEmailModel BuildEmail(CallbackRequestFormModel? callback = null, ContactFormModel? contact = null, OnlineSupportFormModel? supportRequest = null)
    {
        var supportEmail = "sales@onatrix.com";
        var subject = string.Empty;

        var confirmationEmail = new ConfirmationEmailModel();

        if (callback != null)
        {
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

            consultingTypes.TryGetValue(callback.Subject, out var consultingType);
            subject = $"Callback From Onatrix Regarding {consultingType}.";
            var plainText = PlainText(subject, supportEmail, false, consultingType, callback.Name, callback.Email, callback.Phone, "");
            var htmlBody = HtmlBody(subject, supportEmail, false, consultingType, callback.Name, callback.Email, callback.Phone, "");

            confirmationEmail.To = callback.Email;
            confirmationEmail.Subject = subject;
            confirmationEmail.PlainText = plainText;
            confirmationEmail.HtmlBody = htmlBody;
        }
        if (contact != null)
        {
            subject = "Thank You For Contacting Onatrix!";
            var plainText = PlainText(subject, supportEmail, false, "", contact.Name, contact.Email, "", contact.Message);
            var htmlBody = HtmlBody(subject, supportEmail, false, "", contact.Name, contact.Email, "", contact.Message);

            confirmationEmail.To = contact.Email;
            confirmationEmail.Subject = subject;
            confirmationEmail.PlainText = plainText;
            confirmationEmail.HtmlBody = htmlBody;
        }
        if (supportRequest != null)
        {
            subject = "Thank You For Contacting Onatrix - Online Support!";
            var plainText = PlainText(subject, supportEmail, true, "", "", supportRequest.Email, "", "");
            var htmlBody = HtmlBody(subject, supportEmail, true, "", "", supportRequest.Email, "", "");

            confirmationEmail.To = supportRequest.Email;
            confirmationEmail.Subject = subject;
            confirmationEmail.PlainText = plainText;
            confirmationEmail.HtmlBody = htmlBody;
        }
        return confirmationEmail;


    }

    public string HtmlBody(string subject, string supportEmail, bool onlineSupport, string? consultingType = "", string? name = "", string? email = "", string? phone = "", string? message = "")
    {
        var greeting = string.Empty;
        if (!string.IsNullOrEmpty(name))
        {
            greeting = $"<p>Thank you for contacting us {name}.</p>";
        }
        else
        {
            greeting = "<p>Thank you for contacting us.</p>";
        }

        var contactType = string.Empty;
        if (onlineSupport)
        {
            contactType = $"<p>A member of our online support team will get in touch with you at {email} as soon as possible (normally within 30 minutes).</p>";
        }
        else if (!string.IsNullOrWhiteSpace(phone))
        {
            contactType = $"<p>We will get in touch with you on {phone} within the next 24 hrs.</p>";
        }
        else
        {
            contactType = $"<p>We will get in touch with you at {email} within the next 24 hrs.</p>";
        }

        var urgent = string.Empty;
        if (!string.IsNullOrWhiteSpace(consultingType))
        {
            urgent = $"<p>If you need to talk to us sooner please don't hesitate to call our local offices and speak to a specialist in {consultingType}.</p>";
        }
        else
        {
            urgent = "<p>If you need to talk to us sooner please don't hesitate to call our local offices and speak to a customer support specialist.</p>";
        }

        if (!string.IsNullOrEmpty(message))
        {
            message =   "<p><br><strong>Your Message:</strong></p>" +
                        $"<p style=\"font-style: italic;\">'{message}'</p>";
        }

        var donNotReply = $"<p>Important: This is an automated message and the email address can not receive replies. For assistance, contact our support team at {supportEmail}.</p>";

        var htmlBody = "<!DOCTYPE html>" +
                          "<html lang=\"en\">" +
                          "<head>" +
                          "<meta charset=\"UTF-8\">" +
                          "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                          $"<title>{subject}</title>" +
                          "<style>" +
                          "body {" +
                          "max-width: 500px;" +
                          "font-family: Arial, sans-serif;" +
                          "margin: 0 8x;" +
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
                          $"<h1>{subject}</h1>" +
                          greeting +
                          contactType +
                          urgent +
                          "<p style=\"font-style: italic;\">/The Onatrix Team</p>" +
                          message +
                          "</div>" +
                          "</body>" +
                          $"<footer style=\"padding: 16px 0;\">" +
                          $"<strong>{donNotReply}</footer>" +
                          "</html>";
        return htmlBody;
    }

    public string PlainText(string subject, string supportEmail, bool onlineSupport, string? consultingType = "", string? name = "", string? email = "", string? phone = "", string? message = "")
    {
        var greeting = string.Empty;
        if (!string.IsNullOrEmpty(name))
        {
            greeting = $"Thank you for contacting us, {name}. ";
        }
        else
        {
            greeting = "Thank you for contacting us. ";
        }

        var contactType = string.Empty;
        if (onlineSupport)
        {
            contactType = $"A member of our online support team will get in touch with you at {email} as soon as possible (normally within 30 minutes). ";
        }
        else if (!string.IsNullOrWhiteSpace(phone))
        {
            contactType = $"We will get in touch with you on {phone} within the next 24 hrs. ";
        }
        else
        {
            contactType = $"We will get in touch with you at {email} within the next 24 hrs. ";
        }              

        var urgent = string.Empty;
        if (!string.IsNullOrWhiteSpace(consultingType))
        {
            urgent = $"If you need to talk to us sooner please don't hesitate to call our local offices and speak to a specialist in {consultingType}. ";
        }
        else
        {
            urgent = "If you need to talk to us sooner please don't hesitate to call our local offices and speak to a customer support specialist. ";
        }
        
        if (!string.IsNullOrEmpty(message))
        {
            message = $"Your Message: '{message}'. ";
        }

        var donNotReply = $"Important: This is an automated message and the email address can not receive replies. For assistance, contact our support team at {supportEmail}.";

        var plainText = greeting + contactType + urgent + message + donNotReply;
        return plainText;
    }
}