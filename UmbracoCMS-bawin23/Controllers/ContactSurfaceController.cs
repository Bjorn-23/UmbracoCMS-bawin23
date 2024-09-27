using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoCMS_bawin23.Models;
using UmbracoCMS_bawin23.Services;

namespace UmbracoCMS_bawin23.Controllers;

public class ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor,
    IUmbracoDatabaseFactory databaseFactory,
    ServiceContext services, AppCaches appCaches,
    IProfilingLogger profilingLogger,
    IPublishedUrlProvider publishedUrlProvider, FormServices formServices, EmailServices emailServices) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
    private readonly FormServices _formServices = formServices;
    private readonly EmailServices _emailServices = emailServices;


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
            var saveData = _formServices.SaveFormData(form, Services);
            if (saveData)
            {
                try
                {
                    var tryBuildAndSendEmail = await _emailServices.TryDetermineFormModel(form);
                    if (tryBuildAndSendEmail)
                    {
                        TempData["form-success"] = "true";                       
                        return RedirectToCurrentUmbracoPage();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"ERROR : Exception - ContactSurfaceController.HandleCallbackRequestSubmit() {ex.Message}");
                }
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
            var saveData = _formServices.SaveFormData(form, Services);
            if (saveData)
            {
                try
                {
                    var tryBuildAndSendEmail = await _emailServices.TryDetermineFormModel(form);
                    if (tryBuildAndSendEmail)
                    {
                        TempData["form-success"] = "true";
                        queryString = "?paramFormSuccess=true";
                        return Redirect(currentPageUrl + queryString + fragment);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"ERROR : Exception - ContactSurfaceController.HandleContactRequestSubmit() {ex.Message}");
                }
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
            var saveData = _formServices.SaveFormData(form, Services);
            if (saveData)
            {
                try
                {
                    var tryBuildAndSendEmail = await _emailServices.TryDetermineFormModel(form);
                    if (tryBuildAndSendEmail)
                    {
                        TempData["form-success"] = "true";
                        return RedirectToCurrentUmbracoPage();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"ERROR : Exception - ContactSurfaceController.HandleOnlineSupportFormSubmit() {ex.Message}");
                }
            }
            
            ViewData["form-success"] = "false";
            return CurrentUmbracoPage();           
        }
    }
}
