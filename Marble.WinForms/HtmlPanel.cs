using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Marble.Core.System;
using Marble.Core.System.Entities;
using Marble.WinForms.Renderer;
using Marble.WinForms.Renderer.Renderers;
using SkiaSharp;

namespace Marble.WinForms
{
	public class HtmlPanel : PictureBox
	{
		protected HtmlContainer _htmlContainer;
		protected CssData _baseCssData;
		protected string _text;

		public event EventHandler<HtmlRenderErrorEventArgs> RenderError;
		public event EventHandler<HtmlStylesheetLoadEventArgs> StylesheetLoad;

		public HtmlPanel()
		{
			_htmlContainer = new HtmlContainer();
			_htmlContainer.RenderError += OnRenderError;
			_htmlContainer.Refresh += OnRefresh;
			_htmlContainer.StylesheetLoad += OnStylesheetLoad;

			Invalidate();
		}

		public virtual string BaseStylesheet
		{
			set
			{
				_baseCssData = HtmlRender.ParseStyleSheet(value);
				_htmlContainer.SetHtml(_text, _baseCssData);
			}
		}

		public override string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				base.Text = value;
				if (!IsDisposed)
				{
					_htmlContainer.SetHtml(_text, _baseCssData);
					PerformLayout();
					Invalidate();
				}
			}
		}

		protected override void OnLayout(LayoutEventArgs levent)
		{
			PerformHtmlLayout();

			base.OnLayout(levent);

			if (_htmlContainer != null && Math.Abs(_htmlContainer.MaxSize.Width - ClientSize.Width) > 0.1)
			{
				PerformHtmlLayout();
				base.OnLayout(levent);
			}
		}

		protected void PerformHtmlLayout()
		{
			if (_htmlContainer != null)
			{
				_htmlContainer.MaxSize = new SizeF(ClientSize.Width - Padding.Horizontal, 0);

				var g = RendererFactory.FromControl(this) as SkRenderer;
				if (g != null)
				{
					using (g)
					{
						_htmlContainer.PerformLayout(g);

						// using (var image = g.Surface.Snapshot())
						// using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
						// using (var mStream = new MemoryStream(data.ToArray()))
						// {
						// 	var bm = new Bitmap(mStream);
						// 	this.Image = bm;
						// }
					}
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (_htmlContainer != null)
			{
				var imageInfo = new SKImageInfo(Width, Height);
				using (var surface = SKSurface.Create(imageInfo))
				{
					var canvas = surface.Canvas;
					// canvas.Clear(SKColors.Red);
					// canvas.DrawRect(new SKRect(100, 100, 200, 200), new SKPaint
					// {
					//  Style = SKPaintStyle.Fill,
					//  Color = SKColors.Green
					// });

					var renderer = (IRenderer) new SkRenderer(surface, canvas);
					renderer.SetClip(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width,
						ClientRectangle.Height);
					_htmlContainer.Location = new PointF(Padding.Left, Padding.Top);
					_htmlContainer.PerformPaint(renderer);
					renderer.Release();

					using (var image = surface.Snapshot())
					using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
					using (var mStream = new MemoryStream(data.ToArray()))
					{
						var bm = new Bitmap(mStream);
						Image = bm;
					}
				}
			}
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			Focus();
		}

		protected virtual void OnRenderError(HtmlRenderErrorEventArgs e)
		{
			var handler = RenderError;
			if (handler != null)
				handler(this, e);
		}

		protected virtual void OnStylesheetLoad(HtmlStylesheetLoadEventArgs e)
		{
			var handler = StylesheetLoad;
			if (handler != null)
				handler(this, e);
		}

		protected virtual void OnRefresh(HtmlRefreshEventArgs e)
		{
			if (e.Layout)
				PerformLayout();
			Invalidate();
		}

		protected override void Dispose(bool disposing)
		{
			if (_htmlContainer != null)
			{
				_htmlContainer.RenderError -= OnRenderError;
				_htmlContainer.Refresh -= OnRefresh;
				_htmlContainer.StylesheetLoad -= OnStylesheetLoad;
				_htmlContainer.Dispose();
				_htmlContainer = null;
			}

			base.Dispose(disposing);
		}

		private void OnRenderError(object sender, HtmlRenderErrorEventArgs e)
		{
			if (InvokeRequired)
				Invoke(new MethodInvoker(() => OnRenderError(e)));
			else
				OnRenderError(e);
		}

		private void OnStylesheetLoad(object sender, HtmlStylesheetLoadEventArgs e)
		{
			OnStylesheetLoad(e);
		}

		private void OnRefresh(object sender, HtmlRefreshEventArgs e)
		{
			if (InvokeRequired)
				Invoke(new MethodInvoker(() => OnRefresh(e)));
			else
				OnRefresh(e);
		}
	}
}