using System.Drawing;
using Marble.Core.Adapters;

namespace Marble.WinForms.Adapters
{
	/// <summary>
	/// Adapter for WinForms Font family object for core.
	/// </summary>
	internal sealed class FontFamilyAdapter : RFontFamily
	{
		/// <summary>
		/// the underline win-forms font.
		/// </summary>
		private readonly FontFamily _fontFamily;

		/// <summary>
		/// Init.
		/// </summary>
		public FontFamilyAdapter(FontFamily fontFamily)
		{
			_fontFamily = fontFamily;
		}

		/// <summary>
		/// the underline win-forms font family.
		/// </summary>
		public FontFamily FontFamily
		{
			get { return _fontFamily; }
		}

		public override string Name
		{
			get { return _fontFamily.Name; }
		}
	}
}