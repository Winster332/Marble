using System.Drawing;
using Marble.Core.Adapters;

namespace Marble.WinForms.Renderer.RenderStack.Commands
{
	public class DrawTextCommand : RenderCommand
	{
		public string Text { get; private set; }
		public RFont Font { get; private set; }
		public RBrush Brush { get; private set; }
		public int X { get; private set; }
		public int Y { get; private set; }
		public StringFormat Format { get; private set; }
		
		public DrawTextCommand(string text, RFont font, RBrush brush, int x, int y, StringFormat format)
		{
			Text = text;
			Font = font;
			Brush = brush;
			X = x;
			Y = y;
			Format = format;
		}
		
		public override void Draw(IRenderer renderer)
		{
			renderer.DrawString(Text, Font, Brush, X, Y, Format, false);
		}
	}
}