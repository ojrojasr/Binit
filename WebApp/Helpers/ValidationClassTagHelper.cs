using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace WebApp.Helpers
{
    [HtmlTargetElement("div", Attributes = ValidationForAttributeName + "," +
                                           ValidationErrorClassName + "," +
                                           ValidationSuccessClassName)]
    public class ValidationClassTagHelper : TagHelper
    {
        private const string ValidationForAttributeName = "binit-validation-for";
        private const string ValidationErrorClassName = "binit-onerror-class";
        private const string ValidationSuccessClassName = "binit-onsuccess-class";

        [HtmlAttributeName(ValidationForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(ValidationErrorClassName)]
        public string ValidationErrorClass { get; set; }

        [HtmlAttributeName(ValidationSuccessClassName)]
        public string ValidationSuccessClass { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ViewContext.ViewData.ModelState.TryGetValue(For.Name, out ModelStateEntry entry);
            var tagBuilder = new TagBuilder("div");
            if (entry == null)
            {
                return;
            }
            else if (!entry.Errors.Any())
            {
                tagBuilder.AddCssClass(ValidationSuccessClass);
            }
            else
            {
                tagBuilder.AddCssClass(ValidationErrorClass);
            }
            output.MergeAttributes(tagBuilder);
        }
    }
}