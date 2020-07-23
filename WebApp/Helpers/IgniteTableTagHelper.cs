using Binit.Framework;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.WebTools.Datatable;
namespace WebApp.Helpers
{
    [HtmlTargetElement("ignite-table", TagStructure = TagStructure.NormalOrSelfClosing)]
    [RestrictChildren("ignite-column")]
    public class IgniteTableTagHelper : TagHelper
    {
        [HtmlAttributeName("get-url")]
        public string GetUrl { get; set; }

        [HtmlAttributeName("hide-action-buttons")]
        public bool HideActionButtons { get; set; } = false;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IStringLocalizer<SharedResources> localizer;

        public IgniteTableTagHelper(IStringLocalizer<SharedResources> localizer)
        {
            this.localizer = localizer;
        }

        public async override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Create container element
            var container = new TagBuilder("div");
            container.AddCssClass("table-responsive");

            // Table element
            var table = new TagBuilder("table");
            // Add classes for datatable initialization
            table.AddCssClass("ignite-datatable table display table-bordered table-striped table-hover no-wrap w-100");
            // Setup get url for server side search
            table.Attributes.Add(new KeyValuePair<string, string>("data-get-url", GetUrl));
            if (HideActionButtons)
            {
                table.Attributes.Add(new KeyValuePair<string, string>("data-hide-action-buttons", string.Empty));
            }

            // Table header element
            var thead = new TagBuilder("thead");

            // Add ignite-column childs
            var headRow = new TagBuilder("tr");
            var columns = await output.GetChildContentAsync();

            // Append column headers
            headRow.InnerHtml.AppendHtml(columns);

            // Append actions column
            if (!HideActionButtons)
            {
                var actionsLabel = localizer[Lang.ActionsLabel];
                headRow.InnerHtml.AppendHtml("<th data-priority=\"2\">" + actionsLabel + "</th>");
            }

            thead.InnerHtml.AppendHtml(headRow);

            table.InnerHtml.AppendHtml(thead);
            table.InnerHtml.AppendHtml(new TagBuilder("tbody"));

            container.InnerHtml.AppendHtml(table);

            output.Content.SetHtmlContent(container);
        }
    }
}