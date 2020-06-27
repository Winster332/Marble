using System.IO;

namespace Marble.Core.System
{
	internal class CssDefaults
	{
		public const string CssFileNameDefault = "default_style.css";
		public const string CssFileNameMozila = "mozila_style.css";
		public const string CssFileNameChromium = "chromium_style.css";
		public string Css { get; private set; }

		public CssDefaults()
		{
			Css = File.ReadAllText(CssFileNameMozila);
		}

		public static CssDefaults Instance => _instance ??= new CssDefaults();

		private static CssDefaults _instance;
	}
}
