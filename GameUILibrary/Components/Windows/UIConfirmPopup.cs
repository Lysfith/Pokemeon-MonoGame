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
//    public class UIConfirmPopup : UIPopup
//    {
//        private UIButton _btn_yes;
//        private UIButton _btn_no;

//        public event EventHandler<EventArgs> OnYes;
//        public event EventHandler<EventArgs> OnNo;

//        public UIConfirmPopup(int width, int height)
//            : base(width, height)
//        {
            
//        }

//        public override void LoadContent(ContentManager content)
//        {
//            _btn_yes = new UIButton(new Point((int)(Width * 0.4 - 50), Height - 50), 100, 40, "Yes");
//            _btn_yes.OnGainFocus += (sender, e) =>
//            {
//                if(OnYes != null)
//                {
//                    OnYes(this, null);
//                }
//            };

//            _btn_no = new UIButton(new Point((int)(Width - Width * 0.4 - 50), Height - 50), 100, 40, "No");
//            _btn_no.OnGainFocus += (sender, e) =>
//            {
//                if (OnNo != null)
//                {
//                    OnNo(this, null);
//                }
//            };

//            _btn_yes.LoadContent(content);
//            _btn_no.LoadContent(content);

//            base.LoadContent(content);
//        }

//        public override void Update(GameTime gameTime)
//        {
//            if (Enable)
//            {
//                _btn_yes.Position = new Point((int)(Position.X + Width * 0.3 - 50), Position.Y + Height - 50);
//                _btn_no.Position = new Point((int)(Position.X + Width - Width * 0.3 - 50), Position.Y + Height - 50);

//                _btn_yes.Update(gameTime);
//                _btn_no.Update(gameTime);

//                base.Update(gameTime);
//            }
//        }

//        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
//        {
//            if (Enable && Visible)
//            {
//                base.Draw(gameTime, spriteBatch);

//                _btn_yes.Draw(gameTime, spriteBatch);
//                _btn_no.Draw(gameTime, spriteBatch);
//            }
//        }
//    }
//}
