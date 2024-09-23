using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace UmbracoCMS_bawin23.Controllers;

public class SearchSurfaceController : SurfaceController   
{
    public SearchSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
    }

    public IActionResult HandleNavBarSearchQuery(string searchQuery)
    {
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            ViewData["searchQuery"] = searchQuery;
            return CurrentUmbracoPage();
        }
        else
        {
            return CurrentUmbracoPage();
        }
    }
}
