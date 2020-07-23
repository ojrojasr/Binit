using Binit.Framework;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApp.Models;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.WebTools.IgniteAddress;
namespace WebApp.Helpers
{
    [HtmlTargetElement("ignite-address", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class IgniteAddressTagHelper : TagHelper
    {
        [HtmlAttributeName("for-property")]
        public ModelExpression ForProperty { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IHtmlGenerator generator;

        public IgniteAddressTagHelper(IStringLocalizer<SharedResources> localizer, IHtmlGenerator generator)
        {
            this.localizer = localizer;
            this.generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var disabled = context.AllAttributes["disabled"] != null; ;
            using (var writer = new StringWriter())
            {
                var label = IgniteTagHelper.GetLabel(generator, ViewContext, ForProperty);
                var data = (AddressViewModel)ForProperty.Model ?? null;
                var properties = ForProperty.ModelExplorer.Properties;
                // Create container element
                var container = new TagBuilder("div");
                container.AddCssClass("ignite-address form-group m-b-40");

                container.Attributes.Add(new KeyValuePair<string, string>("data-property-name", this.ForProperty.Name));
                if (context.AllAttributes["disabled"] != null)
                {
                    container.Attributes.Add(new KeyValuePair<string, string>("disabled", ""));
                }

                if (data != null)
                {
                    var jsonData = JsonConvert.SerializeObject(data);
                    jsonData = jsonData.Replace("\"", "'");
                    container.Attributes.Add(new KeyValuePair<string, string>("data-address-components", jsonData));
                }

                container.RenderStartTag().WriteTo(writer, NullHtmlEncoder.Default);
                // Add input group

                ViewContext.ViewData.ModelState.TryGetValue(ForProperty.Name, out ModelStateEntry entry);
                var validationClass = "";

                // Add validation styles after submit
                if (entry != null)
                {
                    validationClass = "has-danger";
                }
                else if (ViewContext.ViewData.ModelState.Keys.Any(k => k.Contains(ForProperty.Name)))
                {
                    validationClass = "has-success";
                }

                // Search control rendering
                writer.Write($@"
					<div class='form-group {validationClass}'>
						<div class='input-group'>
							<input class='search-input form-control' type='text' size='50'>
							<div class='input-group-append'><span class='input-group-text'><i class='ti-close'></i> {localizer[Lang.ClearCurrentLocation]}</span></div>
						</div>
					</div>
				");
                if (disabled)
                {
                    label.WriteTo(writer, NullHtmlEncoder.Default);
                }

                // Add address components
                writer.Write(@"
					<div class='d-flex'>
						<div class='w-100 w-lg-50 address-components-container d-flex align-items-center'>
							<div class='w-100 address-components'>");

                this.WriteAddressComponents(properties, writer);

                writer.Write(@"
							</div>
						</div>
						<div class='map w-100 w-lg-50'></div>
					</div>
				");

                container.RenderEndTag().WriteTo(writer, NullHtmlEncoder.Default);
                output.Content.SetHtmlContent(writer.ToString());
            }
        }

        public void WriteAddressComponents(IEnumerable<ModelExplorer> addressComponents, StringWriter writer)
        {
            foreach (ModelExplorer propertyExplorer in addressComponents)
            {
                var propertyName = propertyExplorer.Metadata.PropertyName;
                var propertyFullName = $"{ForProperty.Name}.{propertyName}";
                var componentContainer = new TagBuilder("div");
                componentContainer.AddCssClass("component");

                // Id and Code values will be hidden
                if (propertyName == "Id" || propertyName == "Code")
                {
                    componentContainer.AddCssClass("d-none");
                }

                var label = generator.GenerateLabel(
                    ViewContext,
                    propertyExplorer,
                    propertyFullName, null, new { });

                componentContainer.InnerHtml.AppendHtml(label);

                var input = generator.GenerateTextBox(
                    ViewContext,
                    propertyExplorer,
                    propertyFullName,
                    propertyExplorer.Model,
                    null, new { });

                componentContainer.InnerHtml.AppendHtml(input);
                componentContainer.WriteTo(writer, NullHtmlEncoder.Default);
            }

        }
    }
}