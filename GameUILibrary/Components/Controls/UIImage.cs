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
    public class UIImage : UIBaseElement
    {
        public string Path { get; set; }

        private Texture2D _texture;


        public UIImage(ContentManager content, Point position, int width, int height, string path)
            : base(content)
        {
            Position = position;
            Width = width;
            Height = height;
            Path = path;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _texture = _content.Load<Texture2D>(Path);
        }

        public override void Update(GameTime gameTime)
        {
           
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Enable && Visible)
            {
                var bounds = GetLocalBounds();

                spriteBatch.Draw(_texture, new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height), Color.White);
            }

            base.Draw(gameTime, spriteBatch);
        }
    }
}
