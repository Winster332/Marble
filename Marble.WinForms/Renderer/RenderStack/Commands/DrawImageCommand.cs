using System.Drawing;
using Marble.Core.Adapters;
using Marble.Core.Adapters.Entities;

namespace Marble.WinForms.Renderer.RenderStack.Commands
{
	public class DrawImageCommand : RenderCommand
	{
		public RImage Image { get; private set; }
		public RRect Rect1 { get; private set; }
		public RRect Rect2 { get; private set; }
		public GraphicsUnit GraphicsUnit { get; private set; }
		
		public DrawImageCommand(RImage image, RRect rect1, RRect rect2, GraphicsUnit gu = GraphicsUnit.World)
		{
			Image = image;
			Rect1 = rect1;
			Rect2 = rect2;
			GraphicsUnit = gu;
		}
		
		public override void Draw(IRenderer renderer)
		{
			if (Rect2 == RRect.Empty)
			{
				renderer.DrawImage(Image, Rect1, false);
			}
			else
			{
				renderer.DrawImage(Image, Rect1, Rect2, GraphicsUnit, false);
			}
		}
	}
}