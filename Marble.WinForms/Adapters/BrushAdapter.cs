using Marble.Core.Adapters;
using SkiaSharp;

namespace Marble.WinForms.Adapters
{
	/// <summary>
	/// Adapter for WinForms brushes objects for core.
	/// </summary>
	public sealed class BrushAdapter : RBrush
	{
		/// <summary>
		/// The actual WinForms brush instance.
		/// </summary>
		private readonly SKPaint _brush;

		/// <summary>
		/// If to dispose the brush when <see cref="Dispose"/> is called.<br/>
		/// Ignore dispose for cached brushes.
		/// </summary>
		private readonly bool _dispose;

		/// <summary>
		/// Init.
		/// </summary>
		public BrushAdapter(SKPaint brush, bool dispose)
		{
			_brush = brush;
			_dispose = dispose;
		}

		/// <summary>
		/// The actual WinForms brush instance.
		/// </summary>
		public SKPaint Brush
		{
			get { return _brush; }
		}

		public override void Dispose()
		{
			if (_dispose)
			{
				_brush.Dispose();
			}
		}

		public override object Clone()
		{
			var cloneBrush = (SKPaint) Brush.Clone();
			return new BrushAdapter(cloneBrush, this._dispose);
		}
	}
}