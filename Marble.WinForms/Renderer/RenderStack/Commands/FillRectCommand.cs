using Marble.Core.Adapters;

namespace Marble.WinForms.Renderer.RenderStack.Commands
{
	public class FillRectCommand : RenderCommand
	{
		public RBrush Brush { get; private set; }
		public float X { get; private set; }
		public float Y { get; private set; }
		public float Width { get; private set; }
		public float Height { get; private set; }
		
		public FillRectCommand(RBrush brush, float x, float y, float w, float h)
		{
			Brush = brush;
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}
		
		public override void Draw(IRenderer renderer)
		{
			renderer.FillRectangle(Brush, X, Y, Width, Height, false);
		}
	}
}