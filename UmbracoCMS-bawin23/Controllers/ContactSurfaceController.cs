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

    public IActionResult HandleCallbackRequestSubmit(CallbackRequestFormModel form)
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
}
