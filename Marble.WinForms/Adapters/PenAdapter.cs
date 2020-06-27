using Marble.Core.Adapters;
using Marble.Core.Adapters.Entities;
using SkiaSharp;

namespace Marble.WinForms.Adapters
{
	/// <summary>
	/// Adapter for WinForms pens objects for core.
	/// </summary>
	public sealed class PenAdapter : RPen
	{
		/// <summary>
		/// The actual WinForms brush instance.
		/// </summary>
		private readonly SKPaint _pen;

		/// <summary>
		/// Init.
		/// </summary>
		public PenAdapter(SKPaint pen)
		{
			_pen = pen;
		}

		/// <summary>
		/// The actual WinForms brush instance.
		/// </summary>
		public SKPaint Pen
		{
			get { return _pen; }
		}

		public override double Width
		{
			get { return _pen.StrokeWidth; }
			set { _pen.StrokeWidth = (float) value; }
		}

		public override RDashStyle DashStyle
		{
			set
			{
				switch (value)
				{
					case RDashStyle.Solid:
						// _pen.PathEffect = SKPathEffect..DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
						break;
					case RDashStyle.Dash:
						_pen.PathEffect = SKPathEffect.CreateDash(Width < 2 ? new[] {4f, 4f} : new[] {1f, 1f}, 1);
							// .DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
						// if (Width < 2)
							// _pen.DashPattern = new[] {4, 4f}; // better looking
						break;
					case RDashStyle.Dot:
						// _pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
						// _pen.PathEffect = SKPathE
						break;
					case RDashStyle.DashDot:
						// _pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
						break;
					case RDashStyle.DashDotDot:
						// _pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
						break;
					case RDashStyle.Custom:
						// _pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
						break;
					default:
						// _pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
						break;
				}
			}
		}
	}
}