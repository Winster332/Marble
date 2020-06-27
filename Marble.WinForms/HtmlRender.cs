using System.Drawing;
using Marble.Core.System;
using Marble.Core.System.Utils;
using Marble.WinForms.Adapters;

namespace Marble.WinForms
{
	public static class HtmlRender
	{
		public static void AddFontFamily(FontFamily fontFamily)
		{
			ArgChecker.AssertArgNotNull(fontFamily, "fontFamily");

			WinFormsAdapter.Instance.AddFontFamily(new FontFamilyAdapter(fontFamily.Name));
		}

		public static CssData ParseStyleSheet(string stylesheet, bool combineWithDefault = true)
		{
			return CssData.Parse(WinFormsAdapter.Instance, stylesheet, combineWithDefault);
		}
	}
}