using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Marble.Core.Adapters;
using Marble.Core.Adapters.Entities;
using Marble.WinForms.Utilities;
using SkiaSharp;

namespace Marble.WinForms.Adapters
{
	/// <summary>
	/// Adapter for WinForms platforms.
	/// </summary>
	internal sealed class WinFormsAdapter : RAdapter
	{
		#region Fields and Consts

		/// <summary>
		/// Singleton instance of global adapter.
		/// </summary>
		private static readonly WinFormsAdapter _instance = new WinFormsAdapter();

		#endregion


		/// <summary>
		/// Init installed font families and set default font families mapping.
		/// </summary>
		private WinFormsAdapter()
		{
			AddFontFamilyMapping("monospace", "Courier New");
			AddFontFamilyMapping("Helvetica", "Arial");

			foreach (var family in FontFamily.Families)
			{
				AddFontFamily(new FontFamilyAdapter(family.Name));
			}
		}

		/// <summary>
		/// Singleton instance of global adapter.
		/// </summary>
		public static WinFormsAdapter Instance
		{
			get { return _instance; }
		}

		protected override RColor GetColorInt(string colorName)
		{
			var color = Color.FromName(colorName);
			return RColor.FromArgb(color.A, color.R, color.G, color.B);
		}

		protected override RPen CreatePen(RColor color)
		{
			return new PenAdapter(new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = Utils.Convert(color)
			});
		}

		protected override RBrush CreateSolidBrush(RColor color)
		{
			// Brush solidBrush;
			// if (color == RColor.White)
			// 	solidBrush = Brushes.White;
			// else if (color == RColor.Black)
			// 	solidBrush = Brushes.Black;
			// else if (color.A < 1)
			// 	solidBrush = Brushes.Transparent;
			// else
			// 	solidBrush = new SolidBrush(Utils.Convert(color));

			return new BrushAdapter(new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = Utils.Convert(color)
			}, false);
		}

		protected override RBrush CreateLinearGradientBrush(RRect rect, RColor color1, RColor color2, double angle)
		{
			var rotation = (float) (angle / 180.0f * Math.PI);
			var angleX = (float)(rect.Width / 2);
			var angleY = (float)(rect.Height / 2);
			return new BrushAdapter(new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Shader = SKShader.CreateLinearGradient(
					new SKPoint((float) rect.Left, (float) rect.Top),
					new SKPoint((float) rect.Right, (float) rect.Bottom),
					new[]
					{
						Utils.Convert(color1), Utils.Convert(color2)
					}, new[] {0.0f, 1.0f}, SKShaderTileMode.Repeat, SKMatrix.CreateRotation(rotation, angleX, angleY))
			}, true);
			// new LinearGradientBrush(Utils.Convert(rect), Utils.Convert(color1), Utils.Convert(color2),
			// (float) angle), true);
		}

		protected override RImage ConvertImageInt(object image)
		{
			return image != null ? new ImageAdapter((SKImage) image) : null;
		}

		protected override RImage ImageFromStreamInt(Stream memoryStream)
		{
			var image = SKImage.FromEncodedData(memoryStream);
			return new ImageAdapter(image);
		}

		protected override RFont CreateFontInt(string family, double size, RFontStyle style)
		{
			// var fontStyle = (FontStyle) ((int) style);
			return new FontAdapter(new SKFont(SKTypeface.FromFamilyName(family, DJKJLAW(style)), (float) size));
		}

		private SKTypefaceStyle DJKJLAW(RFontStyle style)
		{
			switch (style)
			{
				case RFontStyle.Bold:
					return SKTypefaceStyle.Bold;
				case RFontStyle.Italic: return SKTypefaceStyle.Italic;
				case RFontStyle.Regular: return SKTypefaceStyle.Normal;
				case RFontStyle.Strikeout: return SKTypefaceStyle.Normal;
				case RFontStyle.Underline: return SKTypefaceStyle.Normal;
			}
			// SKTypefaceStyle.Bold
			return SKTypefaceStyle.Normal;
		}

		protected override RFont CreateFontInt(RFontFamily family, double size, RFontStyle style)
		{
			// var fontStyle = (FontStyle) ((int) style);
			// return new FontAdapter(new SKFont(((FontFamilyAdapter) family).FontFamily, (float) size, fontStyle));
			return new FontAdapter(new SKFont(SKTypeface.FromFamilyName(family.Name, DJKJLAW(style)), (float) size * 1.1f));
		}

		protected override object GetClipboardDataObjectInt(string html, string plainText)
		{
			return ClipboardHelper.CreateDataObject(html, plainText);
		}

		protected override void SetToClipboardInt(string text)
		{
			ClipboardHelper.CopyToClipboard(text);
		}

		protected override void SetToClipboardInt(string html, string plainText)
		{
			ClipboardHelper.CopyToClipboard(html, plainText);
		}

		protected override void SetToClipboardInt(RImage image)
		{
			Console.WriteLine("!!!");
			// Clipboard.SetImage(((ImageAdapter) image).Image);
		}

		// protected override RContextMenu CreateContextMenuInt()
		// {
			// return new ContextMenuAdapter();
		// }

		protected override void SaveToFileInt(RImage image, string name, string extension, RControl control = null)
		{
			using (var saveDialog = new SaveFileDialog())
			{
				saveDialog.Filter = "Images|*.png;*.bmp;*.jpg";
				saveDialog.FileName = name;
				saveDialog.DefaultExt = extension;

				var dialogResult = control == null
					? saveDialog.ShowDialog()
					: saveDialog.ShowDialog(((ControlAdapter) control).Control);
				if (dialogResult == DialogResult.OK)
				{
					// ((ImageAdapter) image).Image.Save(saveDialog.FileName);
					Console.WriteLine("123!!!");
				}
			}
		}
	}
}