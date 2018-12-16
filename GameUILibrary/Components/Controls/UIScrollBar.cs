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
    public class UIScrollBar : UIBaseElement
    {

        private Texture2D _upperLeftCornerTexture;
        private Texture2D _upperRightCornerTexture;
        private Texture2D _lowerLeftCornerTexture;
        private Texture2D _lowerRightCornerTexture;
        private Texture2D _topTexture;
        private Texture2D _leftTexture;
        private Texture2D _fillerTexture;

        private bool _isVertical;
        private int _distance;
        private Point _originalPosition;

        private SpriteFont _font { get; set; }
        private Action<float> _callbackClick { get; set; }
        private Action<float> _callbackMove { get; set; }

        public string Text { get; set; }

        public UIScrollBar(ContentManager content, Point position, int width, int height, bool isVertical, int distance, 
            Action<float> callbackClick = null, Action<float> callbackMove = null)
            : base(content)
        {
            Position = position;
            _originalPosition = position;
            Height = height;
            Width = width;
            _callbackClick = callbackClick;
            _callbackMove = callbackMove;
            _isVertical = isVertical;
            _distance = distance;
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

            _font = _content.Load<SpriteFont>("Fonts/Arial-16");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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

            }
        }

        //public override bool Mouse(MouseState mouse)
        //{
        //    base.Mouse(mouse);

        //    if (HasFocus && mouse.LeftButton == ButtonState.Pressed && _callbackClick != null)
        //    {
        //        _callbackClick(0);
        //    }

        //    return HasFocus;
        //}

        //public override void Move(int x, int y)
        //{
        //    base.Move(x, y);

        //    if (HasFocus)
        //    {
        //        if(_isVertical && y != 0)
        //        {
        //            if(Position.Y + y >= _originalPosition.Y
        //                && Position.Y + y + Height <= _originalPosition.Y + _distance)
        //            {
        //                Position = new Vector2(Position.X, Position.Y + y);
        //            }
        //            else if (Position.Y + y < _originalPosition.Y)
        //            {
        //                Position = _originalPosition;
        //            }
        //            else if (Position.Y + y + Height > _originalPosition.Y + _distance)
        //            {
        //                Position = new Vector2(Position.X, _originalPosition.Y + _distance - Height);
        //            }

        //            if (_callbackMove != null)
        //            {
        //                float percent = (Position.Y - _originalPosition.Y) / (_distance - Height);
        //                _callbackMove(percent);
        //            }
        //        }
        //    }
        //}
    }
}
