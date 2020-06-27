using System;

namespace Marble.Core.Adapters
{
	/// <summary>
	/// Adapter for platform specific graphics path object - used to render (draw/fill) path shape.
	/// </summary>
	public abstract class RGraphicsPath : IDisposable
	{
		/// <summary>
		/// Start path at the given point.
		/// </summary>
		public abstract void Start(double x, double y);

		/// <summary>
		/// Add stright line to the given point from te last point.
		/// </summary>
		public abstract void LineTo(double x, double y);

		/// <summary>
		/// Add circular arc of the given size to the given point from the last point.
		/// </summary>
		public abstract void ArcTo(double x, double y, double size, Corner corner);

		public abstract void AddRoundRect(float x, float y, float w, float h, float nwRadius, float neRadius,
			float seRadius, float swRadius);

		/// <summary>
		/// Release path resources.
		/// </summary>
		public abstract void Dispose();

		/// <summary>
		/// The 4 corners that are handled in arc rendering.
		/// </summary>
		public enum Corner
		{
			TopLeft,
			TopRight,
			BottomLeft,
			BottomRight,
		}
	}
}
