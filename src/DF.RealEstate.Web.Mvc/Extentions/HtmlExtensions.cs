using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using Abp.Domain.Entities;
using Abp.Localization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using DF.RealEstate.Dto;
using DF.RealEstate.Localization;

namespace DF.RealEstate.Web.Extentions
{
    public static class HtmlExtensions
    {
        public static IHtmlContent LocalizedEditor<T, TLocalizedModelLocal>(this IHtmlHelper<T> helper,
            string name,
       Func<int, HelperResult> localizedTemplate)
       where T : ILocalizedDto<TLocalizedModelLocal>
       where TLocalizedModelLocal : IEntityTranslationDto
        {
            var _LanguageManager = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<ILanguageManager>();
            var Languages = _LanguageManager.GetLanguages().ToList();

            var tabStrip = new StringBuilder();
            tabStrip.AppendLine($"<div id=\"{name}\">");
            var tabNameToSelect = $"{name}-{Languages.First().Name}-tab";
            tabStrip.AppendLine($"<ul class=\"nav nav-tabs nav-light-dark\" id=\"tab-{name}\" role=\"tablist\">");
            foreach (var locale in helper.ViewData.Model.Translations)
            {
                var language = Languages.FirstOrDefault(x => x.Name == locale.Language);
                if (language == null)
                    throw new Exception("Language cannot be loaded");
                var localizedTabName = $"{name}-{language.Name}-tab";
                var active = localizedTabName == tabNameToSelect ? " active" : null;
                var iconUrl = $"{language.Icon}";
                tabStrip.AppendLine("<li class=\"nav-item\">");
                tabStrip.AppendLine($"<a class=\"nav-link{active}\" href=\"#{localizedTabName}\" data-toggle=\"tab\">");
                tabStrip.AppendLine($"<span class=\"nav-icon\"><i class=\"{iconUrl}\" aria-label=\"{iconUrl}\"></i></span>");
                tabStrip.AppendLine($"<span class=\"nav-text\">{WebUtility.HtmlEncode(language.DisplayName)}</span>");
                tabStrip.AppendLine("</a>");
                tabStrip.AppendLine("</li>");
            }
            tabStrip.AppendLine("</ul>");
            tabStrip.AppendLine("<div class=\"tab-content m-3 mt-5\">");
            for (var i = 0; i < Languages.Count; i++)
            {
                var currLangname = helper.ViewData.Model.Translations[i].Language;
                var language = Languages.FirstOrDefault(x => x.Name == currLangname);
                if (language == null)
                    throw new Exception("Language cannot be loaded");
                var localizedTabName = $"{name}-{language.Name}-tab";
                tabStrip.AppendLine(string.Format("<div class=\"tab-pane fade{0}\" id=\"{1}\">", localizedTabName == tabNameToSelect ? " active show" : null, localizedTabName));
                tabStrip.AppendLine(localizedTemplate(i).ToHtmlString());
                tabStrip.AppendLine("</div>");
            }
            tabStrip.AppendLine("</div>");
            tabStrip.AppendLine("</div>");
            return new HtmlString(tabStrip.ToString());
        }

        public static string ToHtmlString(this IHtmlContent tag)
        {
            using (var writer = new StringWriter())
            {
                tag.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
