using GameUILibrary.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUILibrary
{
    public class UILayer : GameComponent
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackgroundColor { get; set; }

        public List<UIBaseElement> Controls { get; private set; }

        private Texture2D _blankTexture;

        public UILayer(Game game)
            : base(game)
        {
            BackgroundColor = Color.Transparent;

            Controls = new List<UIBaseElement>();
        }

        public override void Initialize()
        {
            base.Initialize();

            _blankTexture = new Texture2D(Game.GraphicsDevice, 1, 1);

            Color[] data = new Color[_blankTexture.Width * _blankTexture.Height];
            _blankTexture.GetData(data);

            for (int i = 0; i < data.Length; i++)
                data[i] = BackgroundColor;

            _blankTexture.SetData(data);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var control in Controls)
            {
                control.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_blankTexture, GetLocalBounds(), Color.White);

            foreach(var control in Controls)
            {
                control.Draw(gameTime, spriteBatch);
            }
        }

        public void Clear()
        {
            Controls.Clear();
        }

        public void AddControl(UIBaseElement control)
        {
            control.Layer = this;
            Controls.Add(control);
        }

        public Rectangle GetLocalBounds()
        {
            var x = 0;
            var y = 0;
            var width = 0;
            var height = 0;

            x = X * Game.Window.ClientBounds.Width / 100;
            y = Y * Game.Window.ClientBounds.Height / 100;
            width = Width * Game.Window.ClientBounds.Width / 100;
            height = Height * Game.Window.ClientBounds.Height / 100;

            return new Rectangle(x, y, width, height);
        }
    }
}
