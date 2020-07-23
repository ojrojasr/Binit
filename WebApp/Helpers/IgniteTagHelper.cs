using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;

namespace WebApp.Helpers
{
    public static class IgniteTagHelper
    {
        public static TagBuilder GetValidationErrors(IHtmlGenerator generator, ViewContext context, ModelExpression expression)
        {
            return generator.GenerateValidationMessage(
                    context,
                    expression.ModelExplorer,
                    expression.Name, null, null, new { @class = "form-control-feedback" });
        }

        public static TagBuilder GetLabel(IHtmlGenerator generator, ViewContext context, ModelExpression expression, string classes = "")
        {
            return generator.GenerateLabel(
                    context,
                    expression.ModelExplorer,
                    expression.Name, null, new { @class = classes });
        }

        public static TagBuilder GetInput(IHtmlGenerator generator, ViewContext context, ModelExpression expression, TagHelperContext tagContext, string classes = "form-control")
        {
            var input = generator.GenerateTextBox(
                    context,
                    expression.ModelExplorer,
                    expression.Name,
                    expression.Model, null, new { @class = classes });

            // Set type
            if (tagContext.AllAttributes["type"] != null)
            {
                input.Attributes["type"] = tagContext.AllAttributes["type"].Value.ToString();
            }

            // Set min value
            if (tagContext.AllAttributes["min"] != null)
            {
                input.Attributes["min"] = tagContext.AllAttributes["min"].Value.ToString();
            }

            // Set max value
            if (tagContext.AllAttributes["max"] != null)
            {
                input.Attributes["max"] = tagContext.AllAttributes["max"].Value.ToString();
            }

            // Set disabled status
            if (tagContext.AllAttributes["disabled"] != null)
            {
                input.Attributes.Add(new KeyValuePair<string, string>("readonly", ""));
            }

            return input;
        }

        public static TagBuilder GetTextarea(IHtmlGenerator generator, ViewContext context, ModelExpression expression, TagHelperContext tagContext, string classes = "form-control")
        {
            var textarea = generator.GenerateTextArea(
                context,
                expression.ModelExplorer,
                expression.Name,
                3, 0, new { @class = classes }
            );

            // Set disabled status
            if (tagContext.AllAttributes["disabled"] != null)
            {
                textarea.Attributes.Add(new KeyValuePair<string, string>("disabled", ""));
            }

            return textarea;
        }

        public static TagBuilder GetSelect(IHtmlGenerator generator, ViewContext context, ModelExpression expression, TagHelperContext tagContext, List<SelectListItem> items, bool multiple, string placeholder, string searchUrl)
        {
            var select = generator.GenerateSelect(
                context,
                expression.ModelExplorer,
                placeholder,
                expression.Name,
                items,
                multiple,
                new { @class = "select2 col-12" }
            );

            // Set disabled status
            if (tagContext.AllAttributes["disabled"] != null)
            {
                select.Attributes.Add(new KeyValuePair<string, string>("disabled", ""));
            }

            // Set search url
            if (!string.IsNullOrEmpty(searchUrl))
            {
                select.Attributes.Add(new KeyValuePair<string, string>("data-search-url", searchUrl));
            }


            return select;
        }
    }
}