using System.Drawing;
using System.Drawing.Drawing2D;
using Marble.Core.Adapters;
using Marble.Core.Adapters.Entities;
using Marble.WinForms.Adapters;
using Marble.WinForms.Renderer.RenderStack;
using Marble.WinForms.Renderer.RenderStack.Commands;
using SkiaSharp;

namespace Marble.WinForms.Renderer.Renderers
{
	public class SkRenderer 
		: IRenderer
	{
		public RenderBus Bus { get; set; }
		private SKCanvas _c;
		public SKSurface Surface { get; set; }
		
		public SkRenderer(SKSurface surface, SKCanvas canvas)
		{
			_c = canvas;
			Surface = surface;
			Bus = new RenderBus();
		}

		public void Dispose()
		{
			_c?.Dispose();
		}

		private RRect ConvertRectI(SKRectI rect)
		{
			return new RRect(rect.Left, rect.Top, rect.Width, rect.Height);
		}
		private RRect ConvertRect(SKRect rect)
		{
			return new RRect(0, 0, 700, 100);
		}

		public RRect ClipBounds => ConvertRect(_c.DeviceClipBounds);
		public void SetClip(int x, int y, int w, int h)
		{
			_c.ClipRect(new SKRect(x, y, w, h), SKClipOperation.Intersect);
		}

		public void SetClip(RRect rect, CombineMode cm)
		{
		}

		public void UseAntialias()
		{
		}

		public RSize MeasureCharacterRanges(string text, RFont font, RRect rect, StringFormat format)
		{
			// throw new NotImplementedException();
			return RSize.Empty;
		}

		public void DrawString(string text, RFont font, RBrush brush, int x, int y, StringFormat format, bool useStack = true)
		{
			if (useStack)
			{
				Bus.Publish(new DrawTextCommand(text, font, (RBrush) brush.Clone(), x, y, format));
			}
			else
			{
				var b = ((BrushAdapter) brush).Brush;
				// var color = default(SKColor);

				// if (b is SolidBrush)
				// {
					// var solid = b as SolidBrush;
					// color = new SKColor(solid.Color.R, solid.Color.G, solid.Color.B, solid.Color.A);
				// }

				var f = ((FontAdapter) font).Font;
				var paint = PaintFromFont(f);
				paint.Color = b.Color;

				_c.DrawText(text, x, y, paint);
				
				if (GloablRenderSettings.IsDebug)
				{
					var textSize = GetTextBound(text, paint);
					_c.DrawRect(x, y - textSize.Height, textSize.Width, textSize.Height, new SKPaint
					{
						Style = SKPaintStyle.Stroke,
						Color = SKColors.Coral
					});
				}
			}
		}

		public static SKPaint PaintFromFont(SKFont font)
		{
			// var fontStyle = font.Typeface.FontStyle;
			// var typeface = SKTypeface.FromFamilyName(font.Typeface.FamilyName, fontStyle); // f.FontFamily.Name

			return new SKPaint
			{
				// TextAlign = 
				TextSize = font.Size * 1.2f,
				Typeface = font.Typeface,
			};
		}

		public static SKSize GetTextBound(string text, SKFont font)
		{
			var paint = PaintFromFont(font);
			var textSize = GetTextBound(text, paint);
			return textSize;
		}

		private static SKSize GetTextBound(string text, SKPaint paint)
		{
			var rect = new SKRect();
			// var width = paint.MeasureText(text, ref rect);

			var d = paint.MeasureText(text, ref rect);
			var whiteSpaceWidth = d - rect.Width;
			rect.Left -= whiteSpaceWidth / 2;
			rect.Right += whiteSpaceWidth / 2;

			rect.Top -= whiteSpaceWidth * 2;
			rect.Bottom += whiteSpaceWidth * 2;

			return new SKSize(rect.Width, rect.Height);
		}

		public void DrawLine(RPen pen, float x1, float y1, float x2, float y2, bool useStack = true)
		{
			if (useStack)
			{
				Bus.Publish(new DrawLineCommand(pen, x1, y1, x2, y2));
			}
			else
			{
				var aPen = ((PenAdapter) pen).Pen;
				// var color = new SKColor(aPen.Color.R, aPen.Color.G, aPen.Color.B, aPen.Color.A);
				_c.DrawLine(x1, y1, x2, y2, aPen);
			}
		}

		public void DrawRectangle(RPen pen, float x, float y, float w, float h, bool useStack = true)
		{
			if (useStack)
			{
				Bus.Publish(new DrawRectCommand(pen, x, y, w, h));
			}
			else
			{
				var aPen = ((PenAdapter) pen).Pen;
				// var color = new SKColor(aPen.Color.R, aPen.Color.G, aPen.Color.B, aPen.Color.A);
				_c.DrawRect(x, y, w, h, aPen);
			}
		}

		public void FillRectangle(RBrush brush, float x, float y, float w, float h, bool useStack = true)
		{
			if (useStack)
			{
				Bus.Publish(new FillRectCommand((RBrush) brush.Clone(), x, y, w, h));
			}
			else
			{
				var b = ((BrushAdapter) brush).Brush;
				// var color = default(SKColor);
				// var skPaint = new SKPaint
				// {
					// Style = SKPaintStyle.Fill,
				// };

				// if (b is SolidBrush)
				// {
					// var solid = b as SolidBrush;
					// skPaint.Color = new SKColor(solid.Color.R, solid.Color.G, solid.Color.B, solid.Color.A);
				// } else if (b is LinearGradientBrush)
				// {
					// var gradient = b as LinearGradientBrush;
					// var colors = gradient.LinearColors.Select(c => new SKColor(c.R, c.G, c.B, c.A)).ToArray();
					// var skRectStart = new SKPoint(gradient.Rectangle.Left, gradient.Rectangle.Top);
					// var skRectEnd = new SKPoint(gradient.Rectangle.Right, gradient.Rectangle.Bottom);
					// skPaint.Shader = SKShader.CreateLinearGradient(skRectStart, skRectEnd, colors,
						// new[] {0.0f, 1.0f}, SKShaderTileMode.Repeat);
				// }

				// TODO: Тут не только Solid
				_c.DrawRect(x, y, w, h, b);
			}
		}

		public void DrawImage(RImage image, RRect rect1, RRect rect2, GraphicsUnit gu, bool useStack = true)
		{
			// throw new NotImplementedException();
		}

		public void DrawImage(RImage image, RRect rect, bool useStack = true)
		{
			// throw new NotImplementedException();
		}

		public void DrawPath(RPen pen, SKPath path, bool useStack = true)
		{
			if (useStack)
			{
				Bus.Publish(new DrawPathCommand(pen, new SKPath(path)));
			}
			else
			{
				var aPen = ((PenAdapter) pen).Pen;
				aPen.IsAntialias = true;
				// var color = new SKColor(aPen.Color.R, aPen.Color.G, aPen.Color.B, aPen.Color.A);
				// var skPath = new SKPath();
				// skPath.AddPoly(path.PathPoints.Select(p => new SKPoint(p.X, p.Y)).ToArray(), true);

				_c.DrawPath(path, aPen);
			}
		}

		public void FillPath(RBrush brush, SKPath path, bool useStack = true)
		{
			if (useStack)
			{
				Bus.Publish(new FillPathCommand((RBrush) brush.Clone(), new SKPath(path)));
			}
			else
			{
				var b = ((BrushAdapter) brush).Brush;
				b.IsAntialias = true;
				// var skPath = new SKPath();
				// skPath.AddPoly(path.PathPoints.Select(p => new SKPoint(p.X, p.Y)).ToArray(), true);

				// var color = default(SKColor);
				//
				// if (b is SolidBrush)
				// {
				// 	var solid = b as SolidBrush;
				// 	color = new SKColor(solid.Color.R, solid.Color.G, solid.Color.B, solid.Color.A);
				// }

				_c.DrawPath(path, b);
			}
		}

		public void FillPolygon(RBrush brush, SKPoint[] points, bool useStack)
		{
			if (useStack)
			{
				Bus.Publish(new FillPolygonCommand((RBrush) brush.Clone(), points));
			}
			else
			{
				var b = ((BrushAdapter) brush).Brush;
				b.IsAntialias = true;
				var skPath = new SKPath();
				skPath.AddPoly(points, true);

				// var color = default(SKColor);
				//
				// if (b is SolidBrush)
				// {
				// 	var solid = b as SolidBrush;
				// 	color = new SKColor(solid.Color.R, solid.Color.G, solid.Color.B, solid.Color.A);
				// }

				_c.DrawPath(skPath, b);
			}
		}

		public void Release()
		{
			// if (GloablRenderSettings.UseBus)
			// {
				Bus.Flush(this);
			// }
		}
	}
}