using Marble.Core.Adapters;

namespace Marble.WinForms.Renderer.RenderStack.Commands
{
	public class DrawLineCommand : RenderCommand
	{
		public RPen Pen { get; private set; }
		public float X1 { get; private set; }
		public float Y1 { get; private set; }
		public float X2 { get; private set; }
		public float Y2 { get; private set; }
		
		public DrawLineCommand(RPen pen, float x1, float y1, float x2, float y2)
		{
			Pen = pen;
			X1 = x1;
			Y1 = y1;
			X2 = x2;
			Y2 = y2;
		}

		public override void Draw(IRenderer renderer)
		{
			renderer.DrawLine(Pen, X1, Y1, X2, Y2, false);
		}
	}
}