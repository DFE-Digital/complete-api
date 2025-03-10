﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Dfe.Complete.TagHelpers
{
	public abstract class InputTagHelperBase : TagHelper
	{
		protected readonly IHtmlHelper _htmlHelper;

		[HtmlAttributeName("id")]
		public string Id { get; set; }

		[HtmlAttributeName("name")]
		public string Name { get; set; }

		[HtmlAttributeName("label")]
		public string Label { get; set; }
		
		[HtmlAttributeName("bold-label")]
		
		public bool? BoldLabel { get; set; }

		[HtmlAttributeName("suffix")]
		public string Suffix { get; set; }

		[HtmlAttributeName("asp-for")]
		public ModelExpression For { get; set; }

		[HtmlAttributeName("hint")]
		public string Hint { get; set; }

		[HtmlAttributeName("test-id")]
		public string TestId { get; set; }

		[ViewContext]
		public ViewContext ViewContext { get; set; }

		protected InputTagHelperBase(IHtmlHelper htmlHelper)
		{
			_htmlHelper = htmlHelper;
		}

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			if (_htmlHelper is IViewContextAware viewContextAware)
			{
				viewContextAware.Contextualize(ViewContext);
			}

			if (string.IsNullOrWhiteSpace(Id))
			{
				Id = Name;
			}

			if (string.IsNullOrWhiteSpace(Name))
			{
				Name = Id;
			}

			var content = await RenderContentAsync();
			output.TagName = null;
			output.PostContent.AppendHtml(content);
		}

		protected abstract Task<IHtmlContent> RenderContentAsync();
	}
}
