using Marble.Core.Adapters;
using SkiaSharp;

namespace Marble.WinForms.Renderer.RenderStack.Commands
{
	public class DrawPathCommand : RenderCommand
	{
		public RPen Pen { get; private set; }
		public SKPath Path { get; private set; }
		
		public DrawPathCommand(RPen pen, SKPath path)
		{
			Pen = pen;
			Path = path;
		}
		
		public override void Draw(IRenderer renderer)
		{
			renderer.DrawPath(Pen, Path, false);
		}
	}
}