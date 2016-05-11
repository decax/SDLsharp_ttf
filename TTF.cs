using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace SDL
{
	public class TTF : IDisposable
	{
		public TTF()
		{
			TTF_Init();
		}

		public void Dispose()
		{
			TTF_Quit();
		}

		#region Native
		const string sdlTTFDLL = "/Library/Frameworks/SDL2_ttf.framework/SDL2_ttf";

		[DllImport(sdlTTFDLL)]
		private static extern int TTF_Init();

		[DllImport(sdlTTFDLL)]
		private static extern void TTF_Quit();

		#endregion
	}

	public class Font : IDisposable
	{
		public enum Style
		{
			Normal        = 0x00, 
			Bold          = 0x01, 
			Italic        = 0x02, 
			Underline     = 0x04, 
			StrikeThrough = 0x08
		}

		public enum Hinting
		{
			Normal, 
			Light, 
			Mono, 
			None
		}

		public Font(string filename, int pointSize)
		{
			sdlFont = TTF_OpenFont(filename, pointSize);
		}

		public void Dispose()
		{
			TTF_CloseFont(sdlFont);
		}

		public Style FontStyle
		{
			get
			{
				return (Style)TTF_GetFontStyle(sdlFont);
			}
			set
			{
				TTF_SetFontStyle(sdlFont, (int)value);
			}
		}

		public Hinting FontHinting
		{
			get
			{
				return (Hinting)TTF_GetFontHinting(sdlFont);
			}
			set
			{
				TTF_SetFontHinting(sdlFont, (int)value);
			}
		}

		/// <summary>
		///  Get the total height of the font - usually equal to point size.
		/// </summary>
		/// <value>The total height of the font.</value>
		public int Height   { get { return TTF_FontHeight(sdlFont); }   }

		/// <summary>
		/// Get the offset from the baseline to the top of the font.
		/// This is a positive value, relative to the baseline.
		/// </summary>
		/// <value>The offset from the baseline to the top of the font.</value>
		public int Ascent   { get { return TTF_FontAscent(sdlFont); }   }

		/// <summary>
		/// Get the offset from the baseline to the bottom of the font.
		/// This is a negative value, relative to the baseline.
		/// </summary>
		/// <value>The offset from the baseline to the bottom of the font.</value>
		public int Descent  { get { return TTF_FontDescent(sdlFont); }  }

		/// <summary>
		/// Get the recommended spacing between lines of text for this font.
		/// </summary>
		/// <value>The recommended spacing between lines of text for this font.</value>
		public int LineSkip { get { return TTF_FontLineSkip(sdlFont); } }

		/// <summary>
		/// Create an 8-bit palettized surface and render the given text at fast quality with the given font and color. 
		/// The 0 pixel is the colorkey, giving a transparent background, and the 1 pixel is set to the text color.
		/// </summary>
		/// <returns>The new surface.</returns>
		/// <param name="text">Text.</param>
		/// <param name="color">Color.</param>
		public Surface RenderTextSolid(string text, Color color)
		{
			return new Surface(TTF_RenderText_Solid(sdlFont, text, new SDL_Color { r = color.R, g = color.G, b = color.B, a = color.A }));
		}

		IntPtr sdlFont;

		#region Native

		struct SDL_Color
		{
			public byte r;
			public byte g;
			public byte b;
			public byte a;
		}

		const string sdlTTFDLL = "/Library/Frameworks/SDL2_ttf.framework/SDL2_ttf";

		[DllImport("/Library/Frameworks/SDL2.framework/SDL2")]
		private static extern IntPtr SDL_GetError();

		[DllImport(sdlTTFDLL)]
		private static extern IntPtr TTF_OpenFont(string file, int ptsize);

		[DllImport(sdlTTFDLL)]
		private static extern void TTF_CloseFont(IntPtr font);

		[DllImport(sdlTTFDLL)]
		private static extern int TTF_GetFontStyle(IntPtr font);

		[DllImport(sdlTTFDLL)]
		private static extern void TTF_SetFontStyle(IntPtr font, int style);

		[DllImport(sdlTTFDLL)]
		private static extern int TTF_GetFontHinting(IntPtr font);

		[DllImport(sdlTTFDLL)]
		private static extern void TTF_SetFontHinting(IntPtr font, int hinting);

		[DllImport(sdlTTFDLL)]
		private static extern int TTF_FontHeight(IntPtr font);

		[DllImport(sdlTTFDLL)]
		private static extern int TTF_FontAscent(IntPtr font);

		[DllImport(sdlTTFDLL)]
		private static extern int TTF_FontDescent(IntPtr font);

		[DllImport(sdlTTFDLL)]
		private static extern int TTF_FontLineSkip(IntPtr font);

		[DllImport(sdlTTFDLL)]
		private static extern IntPtr TTF_RenderText_Solid(IntPtr font, string text, SDL_Color fg);

		[DllImport(sdlTTFDLL)]
		private static extern IntPtr TTF_RenderUTF8_Solid(IntPtr font, string text, SDL_Color fg);

		[DllImport(sdlTTFDLL)]
		private static extern IntPtr TTF_RenderUNICODE_Solid(IntPtr font, string text, SDL_Color fg);

		#endregion
	}
}

