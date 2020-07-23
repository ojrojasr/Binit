using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebApp.Helpers
{
    [HtmlTargetElement("ignite-dropdown", Attributes = "for-property", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class IgniteDropdownTagHelper : TagHelper
    {
        [HtmlAttributeName("for-property")]
        public ModelExpression ForProperty { get; set; }

        [HtmlAttributeName("placeholder")]
        public string Placeholder { get; set; }

        [HtmlAttributeName("items")]
        public List<SelectListItem> Items { get; set; }

        [HtmlAttributeName("multiple")]
        public bool Multiple { get; set; } = false;

        [HtmlAttributeName("search-url")]
        public string SearchUrl { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlGenerator _generator;

        public IgniteDropdownTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var disabled = false;
            using (var writer = new StringWriter())
            {
                // Set disabled status
                disabled = context.AllAttributes["disabled"] != null;
                var label = IgniteTagHelper.GetLabel(_generator, ViewContext, ForProperty, "select");
                var select = IgniteTagHelper.GetSelect(_generator, ViewContext, ForProperty, context, Items, Multiple, Placeholder, SearchUrl);
                var validationErrors = IgniteTagHelper.GetValidationErrors(_generator, ViewContext, ForProperty);

                var containerClasses = "form-group";

                if (!disabled)
                {
                    containerClasses += " m-b-40";

                    ViewContext.ViewData.ModelState.TryGetValue(ForProperty.Name, out ModelStateEntry entry);
                    if (entry != null)
                    {
                        containerClasses += !entry.Errors.Any() ? " has-success" : " has-danger";
                    }
                    writer.Write("<div class=\"" + containerClasses + "\">");
                    label.WriteTo(writer, NullHtmlEncoder.Default);
                    select.WriteTo(writer, NullHtmlEncoder.Default);
                    validationErrors.WriteTo(writer, NullHtmlEncoder.Default);
                }
                else
                {
                    if (!Multiple)
                    {
                        var input = IgniteTagHelper.GetInput(_generator, ViewContext, ForProperty, context);
                        writer.Write("<div class=\"" + containerClasses + "\">");
                        label.WriteTo(writer, NullHtmlEncoder.Default);
                        input.AddCssClass("form-control-line");
                        input.WriteTo(writer, NullHtmlEncoder.Default);
                    }
                    else
                    {
                        writer.Write("<div class=\"" + containerClasses + "\">");
                        label.WriteTo(writer, NullHtmlEncoder.Default);
                        select.WriteTo(writer, NullHtmlEncoder.Default);
                    }
                }

                writer.Write("</div>");
                output.Content.SetHtmlContent(writer.ToString());

            }
        }
    }
}