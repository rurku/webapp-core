using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace WaCore.Web.Infrastructure
{
    public class WaCoreRazorViewEngine : RazorViewEngine
    {
        public WaCoreRazorViewEngine(
            IRazorPageFactoryProvider pageFactory,
            IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            ILoggerFactory loggerFactory) : base(pageFactory, pageActivator, htmlEncoder, optionsAccessor, loggerFactory)
        {
            //https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNet.Mvc.Razor/RazorViewEngine.cs

            // {0} represents the name of the view
            // {1} represents the name of the controller
            // {2} represents the name of the area
        }

        private const string ViewExtension = ".cshtml";

        //TODO: This doesn't work in current version of razor. Find a way to add formats to RazorViewEngineOptions. Maybe add to IServiceCollection in ConfigureServices?
        //public override IEnumerable<string> ViewLocationFormats => new[]
        //{
        //    "/Views/{1}/{0}" + ViewExtension,
        //    "/Views/Shared/{0}" + ViewExtension,
        //    "/Views/WaCore/{1}/{0}" + ViewExtension,
        //    "/Views/WaCore/Shared/{0}" + ViewExtension,
        //};

        //public override IEnumerable<string> AreaViewLocationFormats => new[]
        //{
        //    "/Areas/{2}/Views/{1}/{0}" + ViewExtension,
        //    "/Areas/{2}/Views/Shared/{0}" + ViewExtension,
        //    "/Views/Shared/{0}" + ViewExtension,
        //    "/Areas/{2}/Views/WaCore/{1}/{0}" + ViewExtension,
        //    "/Areas/{2}/Views/WaCore/Shared/{0}" + ViewExtension,
        //    "/Views/WaCore/Shared/{0}" + ViewExtension,
        //};
    }
}
