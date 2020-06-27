using System;
using System.Drawing;
using Marble.Core.Adapters.Entities;
using Marble.Core.System;
using Marble.Core.System.Entities;
using Marble.Core.System.Utils;
using Marble.WinForms.Adapters;
using Marble.WinForms.Renderer;
using Marble.WinForms.Utilities;

namespace Marble.WinForms
{
	/// <summary>
	/// Low level handling of Html Renderer logic, this class is used by <see cref="HtmlParser"/>, 
	/// <see cref="HtmlLabel"/>, <see cref="HtmlToolTip"/> and <see cref="HtmlRender"/>.<br/>
	/// </summary>
	/// <seealso cref="HtmlContainerInt"/>
	public sealed class HtmlContainer : IDisposable
	{
		/// <summary>
		/// The internal core html container
		/// </summary>
		private readonly HtmlContainerInt _htmlContainerInt;


		/// <summary>
		/// Init.
		/// </summary>
		public HtmlContainer()
		{
			_htmlContainerInt = new HtmlContainerInt(WinFormsAdapter.Instance);
			_htmlContainerInt.SetMargins(0);
			_htmlContainerInt.PageSize = new RSize(99999, 99999);
		}

		/// <summary>
		/// Raised when the user clicks on a link in the html.<br/>
		/// Allows canceling the execution of the link.
		/// </summary>
		public event EventHandler<HtmlLinkClickedEventArgs> LinkClicked
		{
			add { _htmlContainerInt.LinkClicked += value; }
			remove { _htmlContainerInt.LinkClicked -= value; }
		}

		/// <summary>
		/// Raised when html renderer requires refresh of the control hosting (invalidation and re-layout).
		/// </summary>
		/// <remarks>
		/// There is no guarantee that the event will be raised on the main thread, it can be raised on thread-pool thread.
		/// </remarks>
		public event EventHandler<HtmlRefreshEventArgs> Refresh
		{
			add { _htmlContainerInt.Refresh += value; }
			remove { _htmlContainerInt.Refresh -= value; }
		}

		/// <summary>
		/// Raised when an error occurred during html rendering.<br/>
		/// </summary>
		/// <remarks>
		/// There is no guarantee that the event will be raised on the main thread, it can be raised on thread-pool thread.
		/// </remarks>
		public event EventHandler<HtmlRenderErrorEventArgs> RenderError
		{
			add { _htmlContainerInt.RenderError += value; }
			remove { _htmlContainerInt.RenderError -= value; }
		}

		/// <summary>
		/// Raised when a stylesheet is about to be loaded by file path or URI by link element.<br/>
		/// This event allows to provide the stylesheet manually or provide new source (file or Uri) to load from.<br/>
		/// If no alternative data is provided the original source will be used.<br/>
		/// </summary>
		public event EventHandler<HtmlStylesheetLoadEventArgs> StylesheetLoad
		{
			add { _htmlContainerInt.StylesheetLoad += value; }
			remove { _htmlContainerInt.StylesheetLoad -= value; }
		}

		/// <summary>
		/// The top-left most location of the rendered html.<br/>
		/// This will offset the top-left corner of the rendered html.
		/// </summary>
		public PointF Location
		{
			get { return new PointF((float) _htmlContainerInt.Location.X, (float) _htmlContainerInt.Location.Y); }
			set { _htmlContainerInt.Location = Utils.Convert(value); }
		}

		/// <summary>
		/// The max width and height of the rendered html.<br/>
		/// The max width will effect the html layout wrapping lines, resize images and tables where possible.<br/>
		/// The max height does NOT effect layout, but will not render outside it (clip).<br/>
		/// <see cref="ActualSize"/> can be exceed the max size by layout restrictions (unwrappable line, set image size, etc.).<br/>
		/// Set zero for unlimited (width\height separately).<br/>
		/// </summary>
		public SizeF MaxSize
		{
			get { return Utils.Convert(_htmlContainerInt.MaxSize); }
			set { _htmlContainerInt.MaxSize = Utils.Convert(value); }
		}

		/// <summary>
		/// Init with optional document and stylesheet.
		/// </summary>
		/// <param name="htmlSource">the html to init with, init empty if not given</param>
		/// <param name="baseCssData">optional: the stylesheet to init with, init default if not given</param>
		public void SetHtml(string htmlSource, CssData baseCssData = null)
		{
			_htmlContainerInt.SetHtml(htmlSource, baseCssData);
		}

		/// <summary>
		/// Measures the bounds of box and children, recursively.
		/// </summary>
		/// <param name="g">Device context to draw</param>
		public void PerformLayout(IRenderer g)
		{
			ArgChecker.AssertArgNotNull(g, "g");

			using (var ig = new SkiaAdapter(g))
			{
				_htmlContainerInt.PerformLayout(ig);
			}
		}

		/// <summary>
		/// Render the html using the given device.
		/// </summary>
		/// <param name="g">the device to use to render</param>
		public void PerformPaint(IRenderer g)
		{
			ArgChecker.AssertArgNotNull(g, "g");

			using (var ig = new SkiaAdapter(g))
			{
				_htmlContainerInt.PerformPaint(ig);
			}
		}

		public void Dispose()
		{
			_htmlContainerInt.Dispose();
		}
	}
}