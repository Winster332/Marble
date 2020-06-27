using Marble.Core.Adapters;
using SkiaSharp;

namespace Marble.WinForms.Renderer.RenderStack.Commands
{
	public class FillPathCommand : RenderCommand
	{
		public RBrush Brush { get; private set; }
		public SKPath Path { get; private set; }
		
		public FillPathCommand(RBrush brush, SKPath path)
		{
			Brush = brush;
			Path = path;
		}
		
		public override void Draw(IRenderer renderer)
		{
			renderer.FillPath(Brush, Path, false);
		}
	}
}