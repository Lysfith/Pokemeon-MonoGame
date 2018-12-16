using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameUILibrary.Parser;
using Microsoft.Xna.Framework.Graphics;
using GameUILibrary.Components;

namespace GameUILibrary
{
    public class UI : GameComponent
    {
        private List<UILayer> _layers;

        public UI(Game game) 
            : base(game)
        {
            _layers = new List<UILayer>();
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (var layer in _layers)
            {
                layer.Initialize();
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var layer in _layers)
            {
                layer.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var layer in _layers)
            {
                layer.Draw(gameTime, spriteBatch);
            }
        }

        protected override void OnEnabledChanged(object sender, EventArgs args)
        {
            base.OnEnabledChanged(sender, args);
        }
        protected override void OnUpdateOrderChanged(object sender, EventArgs args)
        {
            base.OnUpdateOrderChanged(sender, args);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        internal void AddLayer(UILayer layer)
        {
            _layers.Add(layer);
        }

        public UILayer GetLayer(int index)
        {
            return _layers.ElementAt(index);
        }

        public int GetLayersCount()
        {
            return _layers.Count;
        }

        public void Clear()
        {
            foreach(var layer in _layers)
            {
                layer.Clear();
            }
            _layers.Clear();
        }


        public static UI Load(string path, Game game, Dictionary<string, string> texts, Dictionary<string, Action<object, EventArgs>> callbacks)
        {
            var p = new UIParser(path);
            return p.Parse(game, texts, callbacks);
        }

        public static UIBaseElement LoadControl(string path, Game game, Dictionary<string, string> texts, Dictionary<string, Action<object, EventArgs>> callbacks)
        {
            var p = new UIParser(path);
            return p.ParseControl(game, texts, callbacks);
        }
    }
}
