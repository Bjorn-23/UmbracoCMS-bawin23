using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoCMS_bawin23.Models;

namespace UmbracoCMS_bawin23.Controllers;

public class ContactSurfaceController : SurfaceController
{
    public ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory databaseFactory,
        ServiceContext services, AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
    }

    public IActionResult HandleCallbackRequestSubmit(ContactFormsModel form)
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
            // logic for sending of form goes here.

            ViewData["form-success"] = "true";

            ViewData["name"] = string.Empty;
            ViewData["email"] = string.Empty;
            ViewData["phone"] = string.Empty;

            return CurrentUmbracoPage();
        }

    }

	public IActionResult HandleContactRequestSubmit(ContactFormsModel form)
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

		if (!ModelState.IsValid || form.Message == null )
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
			// logic for sending of form goes here.
			queryString = "?paramFormSuccess=true";

			return Redirect(currentPageUrl + queryString + fragment);
		}

	}

	public IActionResult HandleOnlineSupportFormSubmit(EmailSupportModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["online-support"] = model.Email;
            ViewData["online-support-error"] = string.IsNullOrEmpty(model.Email);
            return CurrentUmbracoPage();
        }
        else
        {
            ViewData["form-success"] = "true";
            ViewData["online-support"] = string.Empty;
            return CurrentUmbracoPage();
        }
    }
}
