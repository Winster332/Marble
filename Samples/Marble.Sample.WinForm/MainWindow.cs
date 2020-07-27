using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using Marble.Core.System.Entities;
using Marble.Sample.WinForm.Components;
using Marble.WinForms;

namespace Marble.Sample.WinForm
{
	public class MainWindow : Form
	{
		private readonly PrivateFontCollection _privateFont = new PrivateFontCollection();
		public MainWindow()
		{
			var targetPage = PageType.I;
			var pages = new Pages();
			
			StartPosition = FormStartPosition.CenterScreen;
			var size = Screen.GetWorkingArea(Point.Empty);
			Size = new Size((int) (size.Width * 0.9), (int) (size.Height * 0.8));

			var browser = new LocalBrowser(this);
			var marble = new MarbleBrowser(this);
			
			this.OnResize(null);
			
			marble.Open(pages.GetPageAsText(targetPage));
			browser.Open(Pages.GetPathAsWeb(pages.GetPageAsPath(targetPage)));
			
			// var file = Path.GetTempFileName();
			// File.WriteAllBytes(file, CustomFont);
			// _privateFont.AddFontFile(file);

			// foreach (var fontFamily in _privateFont.Families)
			// 	HtmlRender.AddFontFamily(fontFamily);
		}
		
		// public static byte[] CustomFont
		// {
		// 	get
		// 	{
		// 		var stream = File.OpenRead("CustomFont.ttf");
		//
		// 		byte[] buffer = new byte[16 * 1024];
		// 		using (MemoryStream ms = new MemoryStream())
		// 		{
		// 			int read;
		// 			while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
		// 			{
		// 				ms.Write(buffer, 0, read);
		// 			}
		//
		// 			return ms.ToArray();
		// 		}
		// 	}
		// }

	}
}