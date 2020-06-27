using System;
using System.Drawing;
using Marble.Core.Adapters.Entities;
using SkiaSharp;

namespace Marble.WinForms.Utilities
{
	/// <summary>
	/// Utilities for converting WinForms entities to HtmlRenderer core entities.
	/// </summary>
	internal static class Utils
	{
		/// <summary>
		/// Convert from WinForms point to core point.
		/// </summary>
		public static RPoint Convert(PointF p)
		{
			return new RPoint(p.X, p.Y);
		}

		/// <summary>
		/// Convert from WinForms point to core point.
		/// </summary>
		public static SKPoint[] Convert(RPoint[] points)
		{
			var myPoints = new SKPoint[points.Length];
			for (int i = 0; i < points.Length; i++)
				myPoints[i] = Convert(points[i]);
			return myPoints;
		}

		/// <summary>
		/// Convert from core point to WinForms point.
		/// </summary>
		public static SKPoint Convert(RPoint p)
		{
			return new SKPoint((float) p.X, (float) p.Y);
		}

		/// <summary>
		/// Convert from core point to WinForms point.
		/// </summary>
		public static Point ConvertRound(RPoint p)
		{
			return new Point((int) Math.Round(p.X), (int) Math.Round(p.Y));
		}

		/// <summary>
		/// Convert from WinForms size to core size.
		/// </summary>
		public static RSize Convert(SizeF s)
		{
			return new RSize(s.Width, s.Height);
		}

		/// <summary>
		/// Convert from core size to WinForms size.
		/// </summary>
		public static SizeF Convert(RSize s)
		{
			return new SizeF((float) s.Width, (float) s.Height);
		}

		/// <summary>
		/// Convert from WinForms rectangle to core rectangle.
		/// </summary>
		public static RRect Convert(RectangleF r)
		{
			return new RRect(r.X, r.Y, r.Width, r.Height);
		}

		/// <summary>
		/// Convert from core rectangle to WinForms rectangle.
		/// </summary>
		public static RectangleF Convert(RRect r)
		{
			return new RectangleF((float) r.X, (float) r.Y, (float) r.Width, (float) r.Height);
		}

		/// <summary>
		/// Convert from WinForms color to core color.
		/// </summary>
		public static RColor Convert(SKColor c)
		{
			return RColor.FromArgb(c.Alpha, c.Red, c.Green, c.Blue);
		}

		/// <summary>
		/// Convert from core color to WinForms color.
		/// </summary>
		public static SKColor Convert(RColor c)
		{
			return new SKColor(c.R, c.G, c.B, c.A);
		}
	}
}