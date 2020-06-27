using Marble.Core.Adapters;

namespace Marble.WinForms.Renderer.RenderStack.Commands
{
	public class DrawRectCommand : RenderCommand
	{
		public RPen Pen { get; private set; }
		public float X { get; private set; }
		public float Y { get; private set; }
		public float Width { get; private set; }
		public float Height { get; private set; }
		
		public DrawRectCommand(RPen pen, float x, float y, float w, float h)
		{
			Pen = pen;
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}

		public override void Draw(IRenderer renderer)
		{
			renderer.DrawRectangle(Pen, X, Y, Width, Height, false);
		}
	}
}