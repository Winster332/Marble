using Marble.Core.Adapters;
using SkiaSharp;

namespace Marble.WinForms.Adapters
{
	/// <summary>
	/// Adapter for WinForms Image object for core.
	/// </summary>
	internal sealed class ImageAdapter : RImage
	{
		/// <summary>
		/// the underline win-forms image.
		/// </summary>
		private readonly SKImage _image;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public ImageAdapter(SKImage image)
		{
			_image = image;
		}

		/// <summary>
		/// the underline win-forms image.
		/// </summary>
		public SKImage Image
		{
			get { return _image; }
		}

		public override double Width
		{
			get { return _image.Width; }
		}

		public override double Height
		{
			get { return _image.Height; }
		}

		public override void Dispose()
		{
			_image.Dispose();
		}
	}
}