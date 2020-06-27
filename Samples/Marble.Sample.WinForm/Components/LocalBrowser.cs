using System;
using System.Windows.Forms;

namespace Marble.Sample.WinForm.Components
{
	public class LocalBrowser
	{
		private WebBrowser _browser;
		
		public LocalBrowser(Control control)
		{
			_browser = new WebBrowser();
			_browser.Width = control.Width / 2;
			_browser.Height = control.Height;
			control.Controls.Add(_browser);

			control.Resize += (sender, args) =>
			{
				_browser.Width = control.Width / 2;
				_browser.Height = control.Height;
			};
		}

		public void Open(string path)
		{
			_browser.Url = new Uri(path);
		}
	}
}