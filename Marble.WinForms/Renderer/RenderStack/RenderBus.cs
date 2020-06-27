using System.Collections.Generic;

namespace Marble.WinForms.Renderer.RenderStack
{
	public class RenderBus
	{
		private List<RenderCommand> _stack;
		
		public RenderBus()
		{
			_stack = new List<RenderCommand>();
		}

		public void Publish(RenderCommand command)
		{
			_stack.Add(command);
		}

		public void Flush(IRenderer renderer)
		{
			for (var i = 0; i < _stack.Count; i++)
			{
				var command = _stack[i];
				command.Draw(renderer);
				// _stack.Remove(command);
			}
		}
	}
}