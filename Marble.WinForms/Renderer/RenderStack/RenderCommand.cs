namespace Marble.WinForms.Renderer.RenderStack
{
	public abstract class RenderCommand
	{
		public abstract void Draw(IRenderer renderer);
	}
}