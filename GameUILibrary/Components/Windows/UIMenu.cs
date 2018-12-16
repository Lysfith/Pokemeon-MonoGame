//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _2016_Project_2.UI
//{
//    public class UIMenu : UIWindow
//    {
//        private Dictionary<string, Action> _menuItems;

//        private SpriteFont _font;
//        private Texture2D _cursor;
//        private int indexSelected = 0;

//        private DateTime _lastInput;

//        public UIMenu(Point position, int width, int height)
//            :base(position, width, height, new Color(0, 0, 173))
//        {
//            Position = position;
//            Height = height;
//            Width = width;

//            _menuItems = new Dictionary<string, Action>();

//            _lastInput = DateTime.Now;
//        }

//        public override void LoadContent(ContentManager content)
//        {
//            _font = content.Load<SpriteFont>("Fonts/Arial-16");
//            _cursor = content.Load<Texture2D>("gui/FF7Cursor");

//            base.LoadContent(content);
//        }

//        public override void Update(GameTime gameTime)
//        {
//            if (Enable)
//            {
//                if(HasFocus)
//                {
//                    var keyboardState = Keyboard.GetState();
//                    if (keyboardState.IsKeyDown(Keys.Up) && (DateTime.Now - _lastInput).TotalMilliseconds > 200)
//                    {
//                        indexSelected--;
//                        if (indexSelected < 0)
//                        {
//                            indexSelected = _menuItems.Count - 1;
//                        }
//                        _lastInput = DateTime.Now;
//                    }
//                    else if (keyboardState.IsKeyDown(Keys.Down) && (DateTime.Now - _lastInput).TotalMilliseconds > 200)
//                    {
//                        indexSelected++;
//                        if (indexSelected >= _menuItems.Count)
//                        {
//                            indexSelected = 0;
//                        }
//                        _lastInput = DateTime.Now;
//                    }
//                }

//                base.Update(gameTime);
//            }
//        }

//        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
//        {
//            if (Enable && Visible)
//            {
//                base.Draw(gameTime, spriteBatch);

//                if (_menuItems != null && _menuItems.Any())
//                {
//                    var lineHeight = Height / _menuItems.Count;

//                    for (int i = 0; i < _menuItems.Count; i++)
//                    {
//                        var item = _menuItems.ElementAt(i);

//                        if (i == indexSelected)
//                        {
//                            spriteBatch.Draw(_cursor, new Rectangle((int)Position.X + 20, (int)Position.Y + 22 + (i * lineHeight), 36, 18), Color.White);
//                        }
//                        spriteBatch.DrawString(_font, item.Key, new Vector2(Position.X + 60, Position.Y + 15 + (i * lineHeight)), Color.White);
//                    }
//                }

                
//            }
//        }

//        public void AddMenuItem(string label, Action callback)
//        {
//            _menuItems.Add(label, callback);
//        }

//        //public override bool Mouse(MouseState mouse)
//        //{
//        //    base.Mouse(mouse);

//        //    if (HasFocus)
//        //    {
                
//        //    }

//        //    return HasFocus;
//        //}

//        //public override void Keyboard(KeyboardState state)
//        //{
//        //    if (HasFocus)
//        //    {
//        //        if(state.IsKeyDown(Keys.Up))
//        //        {
//        //            indexSelected--;
//        //            if(indexSelected < 0)
//        //            {
//        //                indexSelected = _menuItems.Count - 1;
//        //            }
//        //        }
//        //        else if (state.IsKeyDown(Keys.Down))
//        //        {
//        //            indexSelected++;
//        //            if (indexSelected >= _menuItems.Count)
//        //            {
//        //                indexSelected = 0;
//        //            }
//        //        }
//        //    }
//        //}
//    }
//}
