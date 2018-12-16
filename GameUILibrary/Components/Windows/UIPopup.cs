//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _2016_Project_2.UI
//{
//    public class UIPopup : UIWindow
//    {
//        private Texture2D _background;
//        private Color _backgroundColor;
//        private UILabel _lbl_text;

//        public bool Modal { get; set; }
//        public string Text { get; set; }

//        public UIPopup(int width, int height)
//            : base(Point.Zero, width, height, new Color(0, 0, 173))
//        {
//            _backgroundColor = new Color(50, 50, 50, 200);
//        }

//        public override void LoadContent(ContentManager content)
//        {
//            _background = new Texture2D(MyGame.Instance.GraphicsDevice, MyGame.Instance.Graphics.PreferredBackBufferWidth, MyGame.Instance.Graphics.PreferredBackBufferHeight);

//            Color[] data = new Color[_background.Width * _background.Height];
//            _background.GetData(data);

//            for (int i = 0; i < data.Length; i++)
//                data[i] = _backgroundColor;

//            _background.SetData(data);

//            _lbl_text = new UILabel(new Point(0, 0), Width, 40, "");

//            _lbl_text.LoadContent(content);

//            base.LoadContent(content);
//        }

//        public override void Update(GameTime gameTime)
//        {
//            if (Enable)
//            {
//                Position = new Point((int)(MyGame.Instance.Graphics.PreferredBackBufferWidth * 0.5 - Width * 0.5),
//                    (int)(MyGame.Instance.Graphics.PreferredBackBufferHeight * 0.5 - Height * 0.5));

//                _lbl_text.Position = new Point((int)(Position.X + Width * 0.5 - 150),
//                    (int)(Position.Y + Height * 0.5 - 20));

//                _lbl_text.Text = Text;
//                _lbl_text.Update(gameTime);

//                base.Update(gameTime);
//            }
//        }

//        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
//        {
//            if (Enable && Visible)
//            {
//                if (Modal)
//                {
//                    spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
//                }

//                base.Draw(gameTime, spriteBatch);

//                _lbl_text.Draw(gameTime, spriteBatch);
//            }
//        }
//    }
//}
