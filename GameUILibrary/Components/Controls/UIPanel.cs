using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUILibrary.Components.Controls
{
    public class UIPanel : UIBaseElement
    {
        private Texture2D _lineTexture;
        private Texture2D _cornerTexture;
        private Texture2D _fillerTexture;
        private Color _color;

        public UIPanel(ContentManager content, Point position, int width, int height, Color color)
            : base(content)
        {
            Position = position;
            Height = height;
            Width = width;
            _color = color;
        }

        public override void LoadContent()
        {
            ChangeColor(_content, _color);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Enable)
            {


                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Enable && Visible)
            {
                var bounds = GetLocalBounds();

                spriteBatch.Draw(_cornerTexture, new Rectangle((int)bounds.X, (int)bounds.Y, 4, 4), new Rectangle(0, 0, 4, 4), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);

                spriteBatch.Draw(_cornerTexture, new Rectangle((int)bounds.X, (int)bounds.Y + bounds.Height - 4, 4, 4), new Rectangle(0, 0, 4, 4), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);

                spriteBatch.Draw(_cornerTexture, new Rectangle((int)bounds.X + bounds.Width - 2, (int)bounds.Y + 2, 4, 4), new Rectangle(0, 0, 4, 4), Color.White, 1.5708f, new Vector2(2, 2), SpriteEffects.None, 0);

                spriteBatch.Draw(_cornerTexture, new Rectangle((int)bounds.X + bounds.Width - 2, (int)bounds.Y + bounds.Height - 2, 4, 4), new Rectangle(0, 0, 4, 4), Color.White, -1.5708f, new Vector2(2, 2), SpriteEffects.FlipVertically, 0);

                //Top
                spriteBatch.Draw(_lineTexture, new Rectangle((int)bounds.X + 4, (int)bounds.Y, bounds.Width-8, 4), new Rectangle(0, 0, 4, 4), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);

                //Bottom
                spriteBatch.Draw(_lineTexture, new Rectangle((int)bounds.X + 4, (int)bounds.Y + bounds.Height - 4, bounds.Width - 8, 4), new Rectangle(0, 0, 4, 4), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);

                //Left
                spriteBatch.Draw(_lineTexture, new Rectangle((int)bounds.X + 2, (int)(bounds.Y + bounds.Height * 0.5), bounds.Height - 8, 4), new Rectangle(0, 0, 4, 4), Color.White, 1.5708f, new Vector2(2, 2), SpriteEffects.FlipVertically, 0);

                //Right
                spriteBatch.Draw(_lineTexture, new Rectangle((int)bounds.X + bounds.Width - 2, (int)(bounds.Y + bounds.Height * 0.5), bounds.Height - 8, 4), new Rectangle(0, 0, 4, 4), Color.White, 1.5708f, new Vector2(2, 2), SpriteEffects.None, 0);

                spriteBatch.Draw(_fillerTexture, new Rectangle((int)bounds.X + 4, (int)bounds.Y + 4, bounds.Width - 8, bounds.Height - 8), Color.White);

                base.Draw(gameTime, spriteBatch);
            }
        }

        public void ChangeColor(ContentManager content, Color color)
        {
            var lineTexture = content.Load<Texture2D>("Textures/window_line");
            var cornerTexture = content.Load<Texture2D>("Textures/window_corner");
            var fillerTexture = content.Load<Texture2D>("Textures/blank");

            _lineTexture = new Texture2D(Layer.Game.GraphicsDevice, lineTexture.Width, lineTexture.Height);
            _cornerTexture = new Texture2D(Layer.Game.GraphicsDevice, cornerTexture.Width, cornerTexture.Height);
            _fillerTexture = new Texture2D(Layer.Game.GraphicsDevice, fillerTexture.Width, fillerTexture.Height);


            Color[] data = new Color[_fillerTexture.Width * _fillerTexture.Height];
            fillerTexture.GetData(data);

            for (int i = 0; i < data.Length; i++)
                if (data[i] == Color.White)
                    data[i] = color;

            _fillerTexture.SetData(data);

            data = new Color[_cornerTexture.Width * _cornerTexture.Height];
            cornerTexture.GetData(data);

            for (int i = 0; i < data.Length; i++)
                if (data[i] == Color.White)
                    data[i] = color;

            _cornerTexture.SetData(data);

            data = new Color[_lineTexture.Width * _lineTexture.Height];
            lineTexture.GetData(data);

            for (int i = 0; i < data.Length; i++)
                if (data[i] == Color.White)
                    data[i] = color;

            _lineTexture.SetData(data);
        }

       
    }
}
