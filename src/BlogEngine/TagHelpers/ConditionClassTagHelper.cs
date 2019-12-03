using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogEngine.TagHelpers
{
    [HtmlTargetElement(Attributes = ClassPrefix + "*")]
    public class ConditionClassTagHelper : TagHelper
    {
        private const string ClassPrefix = "condition-class-";

        /// <summary>
        /// Klasa css
        /// </summary>
        [HtmlAttributeName("class")]
        public string CssClass { get; set; }

        private IDictionary<string, bool> _classValues;

        /// <summary>
        /// Klasy css
        /// </summary>
        [HtmlAttributeName("", DictionaryAttributePrefix = ClassPrefix)]
        public IDictionary<string, bool> ClassValues
        {
            get
            {
                return _classValues ?? (_classValues =
                    new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase));
            }

            set { _classValues = value; }
        }

        /// <summary>
        /// Przetworzenie
        /// </summary>
        /// <param name="context"><see cref="TagHelperContext"/></param>
        /// <param name="output"><see cref="TagHelperOutput"/></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var items = _classValues.Where(e => e.Value).Select(e => e.Key).ToList();

            if (!string.IsNullOrEmpty(CssClass))
            {
                items.Insert(0, CssClass);
            }

            if (items.Any())
            {
                var classes = string.Join(" ", items);
                output.Attributes.Add("class", classes);
            }
        }
    }
}
