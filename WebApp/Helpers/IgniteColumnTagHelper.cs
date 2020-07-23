using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace WebApp.Helpers
{
    [HtmlTargetElement("ignite-column", ParentTag = "ignite-table", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class IgniteColumnTagHelper : TagHelper
    {
        [HtmlAttributeName("name")]
        public string Name { get; set; }

        [HtmlAttributeName("priority")]
        public int Priority { get; set; } = 0;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public IgniteColumnTagHelper()
        { }

        public async override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var th = new TagBuilder("th");
            th.Attributes.Add(new KeyValuePair<string, string>("data-column-name", Name));
            if (Priority > 0)
            {
                th.Attributes.Add(new KeyValuePair<string, string>("data-priority", Priority.ToString()));
            }
            var label = await output.GetChildContentAsync();
            th.InnerHtml.AppendHtml(label);

            output.Content.SetHtmlContent(th);
        }
    }
}