using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Marble.Core.Adapters;
using Marble.Core.Adapters.Entities;
using Marble.Core.System.Utils;
using Marble.WinForms.Renderer;
using Marble.WinForms.Renderer.Renderers;
using Marble.WinForms.Utilities;
using SkiaSharp;

namespace Marble.WinForms.Adapters
{
	public sealed class SkiaAdapter : RGraphics
	{
		private readonly IRenderer _g;

		public SkiaAdapter(IRenderer g)
			: base(WinFormsAdapter.Instance, g.ClipBounds)
		{
			ArgChecker.AssertArgNotNull(g, "g");

			_g = g;
		}

		public override void PopClip()
		{
			_clipStack.Pop();
			_g.SetClip(_clipStack.Peek(), CombineMode.Replace);
		}

		public override void PushClip(RRect rect)
		{
			_clipStack.Push(rect);
			_g.SetClip(rect, CombineMode.Replace);
		}

		public override void PushClipExclude(RRect rect)
		{
			_clipStack.Push(_clipStack.Peek());
			_g.SetClip(rect, CombineMode.Exclude);
		}

		public override Object SetAntiAliasSmoothingMode()
		{

			// var prevMode = _g.SmoothingMode;
			// _g.SmoothingMode = SmoothingMode.AntiAlias;
			// return prevMode;

			_g.UseAntialias();
			return SmoothingMode.AntiAlias;
		}

		public override void ReturnPreviousSmoothingMode(Object prevMode)
		{
			if (prevMode != null)
			{
				// _g.SmoothingMode = (SmoothingMode) prevMode;
			}
		}

		public override RSize MeasureString(string str, RFont font)
		{
			var f = ((FontAdapter) font).Font;
			var textSize = SkRenderer.GetTextBound(str, f);
			var size = new Size((int) textSize.Width, (int) textSize.Height);

			var p = SkRenderer.PaintFromFont(f);
			// if (!GloablRenderSettings.UseGdiMeasureFont)
			// {
			//
			// p.Typeface = SKTypeface.FromFamilyName(f.OriginalFontName);
			// p.TextSize = f.Size;
			// p.TextAlign = SKTextAlign.Left;
			//
			// var r = new SKRect();
			// var d = p.MeasureText(str, ref r);
			// float whiteSpaceWidth = d - r.Width;
			// r.Left -= whiteSpaceWidth / 2;
			// r.Right += whiteSpaceWidth / 2;

			// r.Top -= whiteSpaceWidth * 2;
			// r.Bottom += whiteSpaceWidth * 2;


			// size = new Size((int) r.Width + (int) (r.Width / 2), (int) f.Height);

			// var winSize = new Size();
			// SkRendererAdapter.GetTextExtentPoint32(_hdc, str, str.Length, ref winSize);

			// Console.WriteLine("123");
			// }
			// else
			// {
			// SkRendererAdapter.GetTextExtentPoint32(_hdc, str, str.Length, ref size);
			// }

			if (font.Height < 0)
			{

				// if (!GloablRenderSettings.UseGdiMeasureFont)
				// {
				
				var metrics = new SKFontMetrics();
				var m = p.GetFontMetrics(out metrics);
				var underlineOffset = (int)
					(metrics.XHeight - metrics.Descent + (metrics.UnderlinePosition ?? 0) + 1);
				((FontAdapter) font).SetMetrics(size.Height, underlineOffset);
				Console.WriteLine();
				
				// }
				// else
				// {
				// TextMetric lptm;
				// SkRendererAdapter.GetTextMetrics(_hdc, out lptm);
				// var underlineOffset = lptm.tmHeight - lptm.tmDescent + lptm.tmUnderlined + 1;
				// ((FontAdapter) font).SetMetrics(size.Height, underlineOffset);
				// }
			}

			return Utils.Convert(size);
		}


		public override void DrawString(string str, RFont font, RColor color, RPoint point, RSize size, bool rtl)
		{
			var pointConv = Utils.ConvertRound(point);
			var colorConv = Utils.Convert(color);

			if (color.A == 255)
			{
				// if (GloablRenderSettings.UseBus)
				// {
				_g.DrawString(str, font,
					new BrushAdapter(new SKPaint
					{
						Style = SKPaintStyle.Fill,
						Color = colorConv
					}, false),
					pointConv.X,
					pointConv.Y, StringFormat.GenericTypographic);
				// }
				// else
				// {
				// SetFont(font);
				// SetTextColor(colorConv);
				// SkRendererAdapter.TextOut(_hdc, pointConv.X, pointConv.Y, str, str.Length);
				// }

			}
		}

		public override RBrush GetTextureBrush(RImage image, RRect dstRect, RPoint translateTransformLocation)
		{
			// var brush = new TextureBrush(((ImageAdapter) image).Image, Utils.Convert(dstRect));
			// brush.TranslateTransform((float) translateTransformLocation.X, (float) translateTransformLocation.Y);
			// return new BrushAdapter(brush, true);
			return null;
		}

		public override RGraphicsPath GetGraphicsPath()
		{
			return new GraphicsPathAdapter();
		}

		public override void Dispose()
		{
				// _g.Dispose();
		}

		public override void DrawLine(RPen pen, double x1, double y1, double x2, double y2)
		{
			_g.DrawLine(pen, (float) x1, (float) y1, (float) x2, (float) y2);
		}

		public override void DrawRectangle(RPen pen, double x, double y, double width, double height)
		{
			_g.DrawRectangle(pen, (float) x, (float) y, (float) width, (float) height);
		}

		public override void DrawRectangle(RBrush brush, double x, double y, double width, double height)
		{
			_g.FillRectangle(brush, (float) x, (float) y, (float) width, (float) height);
		}

		public override void DrawImage(RImage image, RRect destRect, RRect srcRect)
		{
			_g.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
		}

		public override void DrawImage(RImage image, RRect destRect)
		{
			_g.DrawImage(image, destRect);
		}

		public override void DrawPath(RPen pen, RGraphicsPath path)
		{
			_g.DrawPath(pen, ((GraphicsPathAdapter) path).GraphicsPath);
		}

		public override void DrawPath(RBrush brush, RGraphicsPath path)
		{
			_g.FillPath(brush, ((GraphicsPathAdapter) path).GraphicsPath);
		}

		public override void DrawPolygon(RBrush brush, RPoint[] points)
		{
			if (points != null && points.Length > 0)
			{
				_g.FillPolygon(brush, Utils.Convert(points));
			}
		}
	}
}