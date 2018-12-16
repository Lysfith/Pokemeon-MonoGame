//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using GameUILibrary.Components.Controls;

//namespace _2016_Project_2.UI
//{
//    public class UILog : UIPanel
//    {
//        public List<LogMessage> LogMessages { get; set; }

//        private SpriteFont _font { get; set; }

//        private int _lineHeight = 14;
//        private int _offsetHeight = 0;
//        private UIScrollBar _scrollbar;
//        private DateTime _lastInput;

//        public UILog(Point position, int width, int height)
//            :base(position, width, height, new Color(0, 0, 173))
//        {
//            LogMessages = new List<LogMessage>();

//            _scrollbar = new UIScrollBar(new Point(Width - 20, Position.Y), 20, 100, true, Height, null, ScrollBar_Move);

//            AddChild(_scrollbar);

//            ManagerNetwork.Instance.OnNewLogMessage += NewLogMessageEvent;

//            Visible = false;
//        }

//        public override void LoadContent(ContentManager content)
//        {
//            base.LoadContent(content);

//            _font = content.Load<SpriteFont>("Fonts/Arial-10");

//            _lastInput = DateTime.Now;
//        }

//        public override void Update(GameTime gameTime)
//        {
//            if(Enable)
//            {
//                _scrollbar.Enable = GetSizeText() > Height;

//                var keyboardState = Keyboard.GetState();

//                if ((DateTime.Now - _lastInput).TotalMilliseconds > 200)
//                {
//                    if (keyboardState.IsKeyDown(Keys.F1))
//                    {
//                        Visible = !Visible;
//                        _lastInput = DateTime.Now;
//                    }
//                }
//            }

//            base.Update(gameTime);
//        }

//        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
//        {
//            if (Enable && Visible)
//            {
//                base.Draw(gameTime, spriteBatch);

//                int indexStart = 0;

//                if(_offsetHeight > 0)
//                {
//                    indexStart = _offsetHeight / _lineHeight;
//                }

//                for (int i = indexStart; i < LogMessages.Count; i++)
//                {
//                    if(i * _lineHeight < _offsetHeight + Height - _lineHeight - 10)
//                    {
//                        var log = LogMessages[i];
//                        var color = Color.White;

//                        switch (log.Type)
//                        {
//                            case TypeLog.INFO:
//                                color = Color.White;
//                                break;
//                            case TypeLog.DEBUG:
//                                color = Color.Gray;
//                                break;
//                            case TypeLog.WARNING:
//                                color = Color.Yellow;
//                                break;
//                            case TypeLog.ERROR:
//                                color = Color.Red;
//                                break;
//                        }

//                        spriteBatch.DrawString(_font, log.Message, new Vector2(Position.X + 10, Position.Y + 10 + (i * _lineHeight) - _offsetHeight), color);
//                    }
//                }

                
//            }
//        }

//        public void AddLog(LogMessage log)
//        {
//            LogMessages.Add(log);

//            var strSize = (int)_font.MeasureString(log.Message).X + 20;
//            Width = strSize > Width ? strSize : Width;
//        }

//        private int GetSizeText()
//        {
//            return LogMessages.Count * _lineHeight + 20;
//        }

//        public void ScrollBar_Move(float percent)
//        {
//            var outer = GetSizeText() - Height;

//            _offsetHeight = (int)(outer * percent);
//        }

//        void NewLogMessageEvent(object sender, LogMessage e)
//        {
//            Color fontColor = Color.White;

//            switch (e.Type)
//            {
//                case TypeLog.DEBUG:
//                    fontColor = Color.Green;
//                    break;
//                case TypeLog.WARNING:
//                    fontColor = Color.Yellow;
//                    break;
//                case TypeLog.ERROR:
//                    fontColor = Color.Red;
//                    break;
//            }

//            AddLog(e);
//        }
//    }
//}
