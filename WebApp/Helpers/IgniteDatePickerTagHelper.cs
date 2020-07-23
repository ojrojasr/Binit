using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IO;
using System.Linq;

namespace WebApp.Helpers
{
    [HtmlTargetElement("ignite-date-picker", Attributes = "for-property", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class IgniteDatePickerTagHelper : TagHelper
    {
        [HtmlAttributeName("for-property")]
        public ModelExpression ForProperty { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlGenerator _generator;

        public IgniteDatePickerTagHelper(IHtmlGenerator generator)
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
                var label = IgniteTagHelper.GetLabel(_generator, ViewContext, ForProperty);
                var input = IgniteTagHelper.GetInput(_generator, ViewContext, ForProperty, context, "form-control date-picker");

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

                    // Start input-group element
                    writer.Write("<div class=\"input-group\">");
                    input.WriteTo(writer, NullHtmlEncoder.Default);
                    writer.Write("<span class=\"bar\"></span>");
                    label.WriteTo(writer, NullHtmlEncoder.Default);
                    // Start input-group-append element
                    writer.Write("<div class=\"input-group-append\">");
                    writer.Write("<span class=\"input-group-text\"><i class=\"ti-calendar\"></i></span>");
                    writer.Write("</div>");
                    // End input-group-append element
                    writer.Write("</div>");
                    // End input-group element
                    validationErrors.WriteTo(writer, NullHtmlEncoder.Default);
                }
                else
                {
                    writer.Write("<div class=\"" + containerClasses + "\">");
                    label.WriteTo(writer, NullHtmlEncoder.Default);
                    input.AddCssClass("form-control-line");
                    input.WriteTo(writer, NullHtmlEncoder.Default);
                }

                writer.Write("</div>");
                output.Content.SetHtmlContent(writer.ToString());
            }
        }
    }
}