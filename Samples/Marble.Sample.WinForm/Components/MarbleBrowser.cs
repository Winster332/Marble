using System.Drawing;
using System.Windows.Forms;
using Marble.Core.System.Entities;
using Marble.WinForms;

namespace Marble.Sample.WinForm.Components
{
	public class MarbleBrowser
	{
		private HtmlPanel _htmlPanel;

		public MarbleBrowser(Control control)
		{
			_htmlPanel = new HtmlPanel();
			control.Controls.Add(_htmlPanel);
			
			control.Resize += (sender, args) =>
			{

				_htmlPanel.Width = control.Width / 2;
				_htmlPanel.Height = control.Height ;
				_htmlPanel.Location = new Point(control.Width / 2, 0);
			};
			
			_htmlPanel.BaseStylesheet = null;
			_htmlPanel.Text = null;

			_htmlPanel.RenderError += OnRenderError;
			_htmlPanel.StylesheetLoad += OnStylesheetLoad;
		}

		public void Open(string source)
		{
			_htmlPanel.Text = source;
		}
		
		private void OnStylesheetLoad(object sender, HtmlStylesheetLoadEventArgs e)
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