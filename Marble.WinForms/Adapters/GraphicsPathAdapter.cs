using System;
using Marble.Core.Adapters;
using Marble.Core.Adapters.Entities;
using SkiaSharp;

namespace Marble.WinForms.Adapters
{
	/// <summary>
	/// Adapter for WinForms graphics path object for core.
	/// </summary>
	public sealed class GraphicsPathAdapter : RGraphicsPath
	{
		/// <summary>
		/// The actual WinForms graphics path instance.
		/// </summary>
		private readonly SKPath _graphicsPath = new SKPath();

		/// <summary>
		/// the last point added to the path to begin next segment from
		/// </summary>
		private RPoint _lastPoint => new RPoint(_graphicsPath.LastPoint.X, _graphicsPath.LastPoint.Y);

		/// <summary>
		/// The actual WinForms graphics path instance.
		/// </summary>
		public SKPath GraphicsPath
		{
			get { return _graphicsPath; }
		}

		public override void Start(double x, double y)
		{
			_graphicsPath.MoveTo((float)x, (float) y);
		}

		public override void LineTo(double x, double y)
		{
			_graphicsPath.LineTo((float) x, (float) y);
		}

		public override void ArcTo(double x, double y, double size, Corner corner)
		{
			var left = (float) (Math.Min(x, _lastPoint.X) - (corner == Corner.TopRight || corner == Corner.BottomRight ? size : 0));
			var top = (float) (Math.Min(y, _lastPoint.Y) - (corner == Corner.BottomLeft || corner == Corner.BottomRight ? size : 0));
			var rect = new SKRect(left, top, left + (float) size * 2, top + (float) size * 2);
			var startAngle = GetStartAngle(corner);
			var sweepAngle = 90.0f;
			_graphicsPath.ArcTo(rect, startAngle, sweepAngle, false);
		}

		public override void AddRoundRect(float x, float y, float w, float h, float nwRadius, float neRadius, float seRadius, float swRadius)
		{
			var rect = new SKRect(x, y, w, h);
			_graphicsPath.AddRoundRect(rect, nwRadius,seRadius);
		}

		public override void Dispose()
		{
			_graphicsPath.Dispose();
		}

		/// <summary>
		/// Get arc start angle for the given corner.
		/// </summary>
		private static float GetStartAngle(Corner corner)
		{
			float startAngle;
			switch (corner)
			{
				case Corner.TopLeft:
					startAngle = 180;
					break;
				case Corner.TopRight:
					startAngle = 270;
					break;
				case Corner.BottomLeft:
					startAngle = 90;
					break;
				case Corner.BottomRight:
					startAngle = 0;
					break;
				default:
					throw new ArgumentOutOfRangeException("corner");
			}

			return startAngle;
		}
	}
}