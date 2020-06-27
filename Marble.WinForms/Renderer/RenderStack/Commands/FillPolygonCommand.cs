using Marble.Core.Adapters;
using SkiaSharp;

namespace Marble.WinForms.Renderer.RenderStack.Commands
{
	public class FillPolygonCommand : RenderCommand
	{
		public RBrush Brush { get; private set; }
		public SKPoint[] Points { get; private set; }
		
		public FillPolygonCommand(RBrush brush, SKPoint[] points)
		{
			Brush = brush;
			Points = points;
		}

		public override void Draw(IRenderer renderer)
		{
			renderer.FillPolygon(Brush, Points, false);
		}
	}
}