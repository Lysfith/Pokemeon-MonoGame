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
//    public class UIInfoPopup : UIPopup
//    {
//        private UIButton _btn_ok;

//        public event EventHandler<EventArgs> OnOk;

//        public UIInfoPopup(int width, int height)
//            : base(width, height)
//        {
            
//        }

//        public override void LoadContent(ContentManager content)
//        {
//            _btn_ok = new UIButton(new Point((int)(Width * 0.4 - 50), Height - 50), 100, 40, "Okay");
//            _btn_ok.OnGainFocus += (sender, e) =>
//            {
//                if(OnOk != null)
//                {
//                    OnOk(this, null);
//                }
//            };

//            _btn_ok.LoadContent(content);

//            base.LoadContent(content);
//        }

//        public override void Update(GameTime gameTime)
//        {
//            if (Enable)
//            {
//                _btn_ok.Position = new Point((int)(Position.X + Width * 0.3 - 50), Position.Y + Height - 50);

//                _btn_ok.Update(gameTime);

//                base.Update(gameTime);
//            }
//        }

//        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
//        {
//            if (Enable && Visible)
//            {
//                base.Draw(gameTime, spriteBatch);

//                _btn_ok.Draw(gameTime, spriteBatch);
//            }
//        }
//    }
//}
