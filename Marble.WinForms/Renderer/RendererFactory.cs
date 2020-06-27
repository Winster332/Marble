using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Marble.WinForms.Renderer.Renderers;
using SkiaSharp;

namespace Marble.WinForms.Renderer
{
	public class RendererFactory
	{
		// public static IRenderer FromHdc(IntPtr intPtr)
		// {
		// 	return new GraphicsRenderer(Graphics.FromHdc(intPtr));
		// }
		//
		// public static IRenderer FromImage(Image image)
		// {
		// 	return new GraphicsRenderer(Graphics.FromImage(image));
		// }
		//
		// public static IRenderer FromHwnd(IntPtr handler)
		// {
		// 	return new GraphicsRenderer(Graphics.FromHwnd(handler));
		// }
		
		public static IRenderer FromControl(Control control)
		{
			// if (GloablRenderSettings.UseSkia)
			// {
				var pbox = control as PictureBox;
				
		       // var imageInfo = new SKImageInfo(pbox.Width, pbox.Height);
		       var imageInfo = new SKImageInfo(pbox.Width, pbox.Height);
		       // using (
		       var surface = SKSurface.Create(imageInfo);
			       // )
		       // {
			       var canvas = surface.Canvas;
			       var oldWidth = pbox.Size.Width;
			       var oldHeight = pbox.Size.Height;
			       // canvas.Clear(SKColors.Red);
			       // canvas.DrawRect(new SKRect(100, 100, 200, 200), new SKPaint
			       // {
				       // Style = SKPaintStyle.Fill,
				       // Color = SKColors.Green
			       // });

			       using (var image = surface.Snapshot())
			       using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
			       using (var mStream = new MemoryStream(data.ToArray()))
			       {
				       var bm = new Bitmap(mStream);
				       pbox.Image = bm;
			       }
		       // }
		       pbox.Size = new Size(oldWidth, oldHeight);

		       return new SkRenderer(surface, canvas);
			// }
			// else
			// {
				// return new GraphicsRenderer(control.CreateGraphics());
			// }
		}
	}
}