using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Managers
{
    public enum FontEnum
    {
        ARIAL_10,
        ARIAL_16,
        ARIAL_24,
        ARIAL_36
    }

    public class FontManager
    {
        private static FontManager _instance;

        public static FontManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new FontManager();
                }

                return _instance;
            }
        }

        private Dictionary<FontEnum, SpriteFont> _fonts;

        public FontManager()
        {

        }

        public void LoadFonts(Game game)
        {
            if (_fonts == null)
            {
                _fonts = new Dictionary<FontEnum, SpriteFont>();

                _fonts.Add(FontEnum.ARIAL_10, game.Content.Load<SpriteFont>("Fonts/Arial-10"));
                _fonts.Add(FontEnum.ARIAL_16, game.Content.Load<SpriteFont>("Fonts/Arial-16"));
                _fonts.Add(FontEnum.ARIAL_24, game.Content.Load<SpriteFont>("Fonts/Arial-24"));
                _fonts.Add(FontEnum.ARIAL_36, game.Content.Load<SpriteFont>("Fonts/Arial-36"));
            }
        }

        public SpriteFont GetFont(FontEnum font)
        {
            return _fonts[font];
        }
    }
}
