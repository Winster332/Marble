using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Marble.Core.Adapters;
using Marble.Core.Adapters.Entities;
using SkiaSharp;

namespace Marble.WinForms.Renderer
{
	public interface IRenderer : IDisposable
	{
		RRect ClipBounds { get; }
		void SetClip(int x, int y, int w, int h);
		void SetClip(RRect rect, CombineMode cm);
		void UseAntialias();
		RSize MeasureCharacterRanges(string text, RFont font, RRect rect, StringFormat format);
		void DrawString(string text, RFont font, RBrush brush, int x, int y, StringFormat format, bool useStack = true);
		void DrawLine(RPen pen, float x1, float y1, float x2, float y2, bool useStack = true);
		void DrawRectangle(RPen pen, float x, float y, float w, float h, bool useStack = true);
		void FillRectangle(RBrush brush, float x, float y, float w, float h, bool useStack = true);
		void DrawImage(RImage image, RRect rect1, RRect rect2, GraphicsUnit gu, bool useStack = true);
		void DrawImage(RImage image, RRect rect, bool useStack = true);
		void DrawPath(RPen pen, SKPath path, bool useStack = true);
		void FillPath(RBrush brush, SKPath path, bool useStack = true);
		void FillPolygon(RBrush brush, SKPoint[] points, bool useStack = true);
		void Release();
		// void DrawImageUnscaled(Image image, int x, int y);
		// void Clear(Color color);
		// TextRenderingHint TextRenderingHint { get; set; }
		// Region Clip { get; set; }
		// void SetClip(Region region, CombineMode cm);
	}
}