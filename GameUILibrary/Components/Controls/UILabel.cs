using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUILibrary.Components.Controls
{
    public class UILabel : UIBaseElement
    {
        public string Text { get; set; }
        public bool TextCentered { get; set; }

        private SpriteFont _font { get; set; }
        private int _fontSize { get; set; }


        public UILabel(ContentManager content, Point position, int width, int height, string text, int fontSize = 10)
            : base(content)
        {
            Position = position;
            Width = width;
            Height = height;
            Text = text;
            TextCentered = false;
            _fontSize = fontSize;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _font = _content.Load<SpriteFont>("Fonts/Arial-" + _fontSize);
        }

        public override void Update(GameTime gameTime)
        {
           
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            var bounds = GetLocalBounds();

            var stringSize = _font.MeasureString(Text);

            if (TextCentered)
            {
                spriteBatch.DrawString(_font, Text, new Vector2(bounds.X + (bounds.Width - stringSize.X) * 0.5f, bounds.Y + (bounds.Height - stringSize.Y) * 0.5f), Color.White);
            }
            else
            {
                spriteBatch.DrawString(_font, Text, new Vector2(bounds.X + 15, bounds.Y + (bounds.Height - stringSize.Y) * 0.5f), Color.White);
            }
        }

        public void AddLine(string line)
        {
            Text += line + "\n";
        }
    }
}
