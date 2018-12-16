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
    public class UITextBox : UIPanel
    {
        public string Text { get; set; }
        public int MaxSize { get; set; }
        public bool TextCentered { get; set; }
        public bool IsPassword { get; set; }

        private SpriteFont _font { get; set; }
        private DateTime _lastKeyPressed { get; set; }
        private DateTime _lastChangeCursor { get; set; }
        private bool _cursorIsShowing { get; set; }
        private string _charactersAllowed { get; set; }


        public UITextBox(ContentManager content, Point position, int width, int height)
            :base(content, position, width, height, new Color(0, 0, 173))
        {
            Text = "";
            MaxSize = 0;
            TextCentered = true;
            _lastKeyPressed = DateTime.Now;
            _lastChangeCursor = DateTime.Now;
            _charactersAllowed = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _font = _content.Load<SpriteFont>("Fonts/Arial-16");
        }

        public override void Update(GameTime gameTime)
        {
            if (HasFocus)
            {
                var keyboard = Keyboard.GetState();

                var keyboardState = Keyboard.GetState();
                var keys = keyboardState.GetPressedKeys();

                if (keys.Length > 0)
                {
                    var key = keys[0];
                    if (key == Keys.Back && (DateTime.Now - _lastKeyPressed).TotalMilliseconds > 100)
                    {
                        if (Text.Length > 0)
                        {
                            Text = Text.Substring(0, Text.Length - 1);
                        }
                        _lastKeyPressed = DateTime.Now;
                    }
                    else if ((MaxSize == 0 || Text.Length < MaxSize) && (DateTime.Now - _lastKeyPressed).TotalMilliseconds > 200)
                    {
                        var keyValue = keys[0].ToString();

                        if (_charactersAllowed.Contains(keyValue))
                        {
                            if(!(keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)))
                            {
                                keyValue = keyValue.ToLower();
                            }

                            Text += keyValue;
                            _lastKeyPressed = DateTime.Now;
                        }
                    }

                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            var text = "";
            
            if(IsPassword)
            {
                foreach(var c in Text)
                {
                    text += "*";
                }
            }
            else
            {
                text = Text;
            }

            var stringSize = text == "" ? _font.MeasureString(" ") : _font.MeasureString(text);

            if (HasFocus)
            {
                if ((DateTime.Now - _lastChangeCursor).TotalMilliseconds > 500)
                {
                    _cursorIsShowing = !_cursorIsShowing;
                    _lastChangeCursor = DateTime.Now;
                }

                if (_cursorIsShowing)
                {
                    text += "_";
                }
            }

            if(TextCentered)
            {
                spriteBatch.DrawString(_font, text, new Vector2(Position.X + (Width - stringSize.X) * 0.5f, Position.Y + (Height - stringSize.Y) * 0.5f), Color.White);
            }
            else
            {
                spriteBatch.DrawString(_font, text, new Vector2(Position.X + 15, Position.Y + (Height - stringSize.Y) * 0.5f), Color.White);
            }
        }
    }
}
