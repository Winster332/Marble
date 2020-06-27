using Marble.Core.Adapters;

namespace Marble.WinForms.Adapters
{
	/// <summary>
	/// Adapter for WinForms Font family object for core.
	/// </summary>
	internal sealed class FontFamilyAdapter : RFontFamily
	{
		public FontFamilyAdapter(string fontFamily)
		{
			Name = fontFamily;
		}

		public override string Name { get; }
	}
}