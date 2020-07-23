using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IO;
using System.Linq;

namespace WebApp.Helpers
{
    [HtmlTargetElement("ignite-textarea", Attributes = "for-property", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class IgniteTextareaTagHelper : TagHelper
    {
        [HtmlAttributeName("for-property")]
        public ModelExpression ForProperty { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlGenerator _generator;

        public IgniteTextareaTagHelper(IHtmlGenerator generator)
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
                var textarea = IgniteTagHelper.GetTextarea(_generator, ViewContext, ForProperty, context);
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
                    textarea.WriteTo(writer, NullHtmlEncoder.Default);
                    writer.Write("<span class=\"bar\"></span>");
                    label.WriteTo(writer, NullHtmlEncoder.Default);
                    validationErrors.WriteTo(writer, NullHtmlEncoder.Default);
                }
                else
                {
                    writer.Write("<div class=\"" + containerClasses + "\">");
                    label.WriteTo(writer, NullHtmlEncoder.Default);
                    textarea.AddCssClass("form-control-line");
                    textarea.WriteTo(writer, NullHtmlEncoder.Default);
                }

                writer.Write("</div>");
                output.Content.SetHtmlContent(writer.ToString());

            }
        }
    }
}