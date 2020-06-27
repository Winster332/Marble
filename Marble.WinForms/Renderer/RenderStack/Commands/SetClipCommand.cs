using System.Drawing.Drawing2D;
using Marble.Core.Adapters.Entities;

namespace Marble.WinForms.Renderer.RenderStack.Commands
{
	public class SetClipCommand : RenderCommand
	{
		public RRect Rect { get; private set; }
		public CombineMode CombineMode { get; private set; }
		
		public SetClipCommand(RRect rect, CombineMode combineMode = CombineMode.Replace)
		{
			Rect = rect;
			CombineMode = combineMode;
		}
		
		public override void Draw(IRenderer renderer)
		{
			renderer.SetClip(Rect, CombineMode);
		}
	}
}