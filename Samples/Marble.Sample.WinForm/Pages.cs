using System.Collections.Generic;
using System.IO;

namespace Marble.Sample.WinForm
{
	public class Pages
	{
		private Dictionary<PageType, string> _pages;

		public Pages()
		{
			_pages = new Dictionary<PageType, string>();
			
			_pages.Add(PageType.Buttons, "C:/git/Marble/Samples/Marble.Sample.WinForm/bin/Debug/netcoreapp3.1/pages/buttons.html");
			_pages.Add(PageType.Intro, "C:/git/Marble/Samples/Marble.Sample.WinForm/bin/Debug/netcoreapp3.1/pages/00.Intro.htm");
			_pages.Add(PageType.Index, "C:/git/Marble/Samples/Marble.Sample.WinForm/bin/Debug/netcoreapp3.1/pages/index.html");
			_pages.Add(PageType.I, "C:/git/Marble/Samples/Marble.Sample.WinForm/bin/Debug/netcoreapp3.1/pages/i.html");
			_pages.Add(PageType.Checkbox, "C:/git/Marble/Samples/Marble.Sample.WinForm/bin/Debug/netcoreapp3.1/pages/checkbox.html");
		}

		public string GetPageAsPath(PageType type)
		{
			return _pages[type];
		}

		public string GetPageAsText(PageType type)
		{
			return File.ReadAllText(GetPageAsPath(type));
		}

		public static string GetPathAsWeb(string path)
		{
			return $"file:///{path}";
		}
	}

	public enum PageType
	{
		Buttons,
		Intro,
		Index,
		I,
		Checkbox
	}
}