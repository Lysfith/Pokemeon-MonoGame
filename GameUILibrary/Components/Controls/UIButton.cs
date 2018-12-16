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
    public class UIButton : UIBaseElement
    {

        private Texture2D _upperLeftCornerTexture;
        private Texture2D _upperRightCornerTexture;
        private Texture2D _lowerLeftCornerTexture;
        private Texture2D _lowerRightCornerTexture;
        private Texture2D _topTexture;
        private Texture2D _leftTexture;
        private Texture2D _cursor;

        private SpriteFont _font { get; set; }
        private int _fontSize { get; set; }
        private Action _callback { get; set; }

        public string Text { get; set; }
        public bool ButtonDisabled { get; set; }
        public bool TextCentered { get; set; }

        public Color Color { get; set; }

        public UIButton(ContentManager content, Point position, int width, int height, string text, int fontSize = 16)
            : base(content)
        {
            Position = position;
            Height = height;
            Width = width;
            Text = text;
            ButtonDisabled = false;
            TextCentered = true;
            _fontSize = fontSize;

            Color = Color.White;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            //_upperLeftCornerTexture = content.Load<Texture2D>("gui/window_upper_left_corner");
            //_upperRightCornerTexture = content.Load<Texture2D>("gui/window_upper_right_corner");
            //_lowerLeftCornerTexture = content.Load<Texture2D>("gui/window_lower_left_corner");
            //_lowerRightCornerTexture = content.Load<Texture2D>("gui/window_lower_right_corner");
            //_topTexture = content.Load<Texture2D>("gui/window_up");
            //_leftTexture = content.Load<Texture2D>("gui/window_left");
            //_fillerTexture = content.Load<Texture2D>("gui/window_filler");

            _font = _content.Load<SpriteFont>("Fonts/Arial-" + _fontSize);
            _cursor = _content.Load<Texture2D>("Textures/FF7Cursor");
        }

        public override void Update(GameTime gameTime)
        {
            if (!ButtonDisabled)
            {
                base.Update(gameTime);
            }

            if (Enable)
            {
                var mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed && !ButtonDisabled && _callback != null)
                {
                    _callback();
                    ButtonDisabled = true;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Enable && Visible)
            {
                base.Draw(gameTime, spriteBatch);

                //spriteBatch.Draw(_upperLeftCornerTexture, Position, Color.White);
                //spriteBatch.Draw(_upperRightCornerTexture, new Vector2(Position.X + Width - 15, Position.Y), Color.White);
                //spriteBatch.Draw(_lowerLeftCornerTexture, new Vector2(Position.X, Position.Y + Height - 12), Color.White);
                //spriteBatch.Draw(_lowerRightCornerTexture, new Vector2(Position.X + Width - 15, Position.Y + Height - 12), Color.White);
                //for (int n = 0; n < ((Width - 30) / 6) + 1; n++)
                //{
                //    spriteBatch.Draw(_topTexture, new Vector2(Position.X + 15 + n * 6, Position.Y), Color.White);
                //    spriteBatch.Draw(_topTexture, new Rectangle((int)Position.X + 15 + n * 6, (int)Position.Y + Height - 12, 6, 12), new Rectangle(0, 0, 6, 12), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
                //}
                //for (int n = 0; n < ((Height - 26) / 6) + 1; n++)
                //{
                //    spriteBatch.Draw(_leftTexture, new Vector2(Position.X, Position.Y + 12 + 6 * n), Color.White);
                //    spriteBatch.Draw(_leftTexture, new Rectangle((int)Position.X + Width - 12, (int)Position.Y + 12 + 6 * n, 12, 6), new Rectangle(0, 0, 12, 6), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                //}
                //spriteBatch.Draw(_fillerTexture, new Rectangle((int)Position.X + 12, (int)Position.Y + 12, Width - 24, Height - 24), Color.White);

                var bounds = GetLocalBounds();

                var stringSize = _font.MeasureString(Text);

                if (HasHover && !ButtonDisabled)
                {
                    //spriteBatch.Draw(_cursor, new Rectangle((int)(Position.X + (Width - stringSize.X) * 0.5f - 40), (int)Position.Y + 15, 36, 18), Color.White);
                    if (TextCentered)
                    {
                        spriteBatch.Draw(_cursor, new Rectangle((int)(bounds.X + (bounds.Width - stringSize.X) * 0.5f - 40), (int)(bounds.Y + bounds.Height * 0.5 - 5), 36, 18), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(_cursor, new Rectangle((int)(bounds.X + 15 - 40), (int)(bounds.Y + bounds.Height * 0.5 - 5), 36, 18), Color.White);
                    }
                }

                var color = Color;

                if(ButtonDisabled)
                {
                    color = Color.Gray;
                }

                if(TextCentered)
                {
                    spriteBatch.DrawString(_font, Text, new Vector2(bounds.X + (bounds.Width - stringSize.X) * 0.5f, bounds.Y + (bounds.Height - stringSize.Y) * 0.5f), color);
                }
                else
                {
                    spriteBatch.DrawString(_font, Text, new Vector2(bounds.X + 15, bounds.Y + (bounds.Height - stringSize.Y) * 0.5f), color);
                }
            }
        }

        //public override bool Mouse(MouseState mouse)
        //{
        //    base.Mouse(mouse);

        //    if (HasFocus && mouse.LeftButton == ButtonState.Pressed && _callback != null)
        //    {
        //        _callback();
        //    }

        //    return HasFocus;
        //}
    }
}
