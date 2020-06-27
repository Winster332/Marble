using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using Marble.Core.System.Entities;
using Marble.WinForms;

namespace Marble.Sample.WinForm
{
	public class MainWindow : Form
	{
		private HtmlPanel _htmlPanel;
		private readonly PrivateFontCollection _privateFont = new PrivateFontCollection();
		public MainWindow()
		{
			StartPosition = FormStartPosition.CenterScreen;
			var size = Screen.GetWorkingArea(Point.Empty);
			Size = new Size((int) (size.Width * 0.7), (int) (size.Height * 0.8));
			
			_htmlPanel = new HtmlPanel();
			Controls.Add(_htmlPanel);
			_htmlPanel.BaseStylesheet = null;
			_htmlPanel.Size = new Size(Width, Height);
			_htmlPanel.Text = null;

			_htmlPanel.RenderError += OnRenderError;
			_htmlPanel.StylesheetLoad += OnStylesheetLoad;

			var h1 = "pages/00.Intro.htm";
			var h2 = "pages/index.html";
			var h3 = "pages/i.html";
			
			_htmlPanel.Text = File.ReadAllText(h3);

			var file = Path.GetTempFileName();
			File.WriteAllBytes(file, CustomFont);
			_privateFont.AddFontFile(file);

			// foreach (var fontFamily in _privateFont.Families)
			// 	HtmlRender.AddFontFamily(fontFamily);
		}
		
		public static byte[] CustomFont
		{
			get
			{
				var stream = File.OpenRead("CustomFont.ttf");

				byte[] buffer = new byte[16 * 1024];
				using (MemoryStream ms = new MemoryStream())
				{
					int read;
					while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
					{
						ms.Write(buffer, 0, read);
					}

					return ms.ToArray();
				}
			}
		}

		public void OnStylesheetLoad(object sender, HtmlStylesheetLoadEventArgs e)
		{
			// e.SetStyleSheet = e.Src == null
			// 	? e.Src
			// 	: @"h1, h2, h3 { color: navy; font-weight:normal; }
   //                                        h1 { margin-bottom: .47em }
   //                                        h2 { margin-bottom: .3em }
   //                                        h3 { margin-bottom: .4em }
   //                                        ul { margin-top: .5em }
   //                                        ul li {margin: .25em}
   //                                        body { font:10pt Tahoma }
   //                    		            pre  { border:solid 1px gray; background-color:#eee; padding:1em }
   //                                        a:link { text-decoration: none; }
   //                                        a:hover { text-decoration: underline; }
   //                                        .gray    { color:gray; }
   //                                        .example { background-color:#efefef; corner-radius:5px; padding:0.5em; }
   //                                        .whitehole { background-color:white; corner-radius:10px; padding:15px; }
   //                                        .caption { font-size: 1.1em }
   //                                        .comment { color: green; margin-bottom: 5px; margin-left: 3px; }
   //                                        .comment2 { color: green; }";
		}

		private static void OnRenderError(object sender, HtmlRenderErrorEventArgs e)
		{
			MessageBox.Show(e.Message + (e.Exception != null ? "\r\n" + e.Exception : null), "Error in Html Renderer",
				MessageBoxButtons.OK);
		}
	}
}