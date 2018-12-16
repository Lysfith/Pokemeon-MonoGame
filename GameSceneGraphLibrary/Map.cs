using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace GameMapLibrary
{
    public class Map
    {
        private TmxMap _map;
        private List<Texture2D> _tilesets;

        private Rectangle _screenSize;
        private Point _playerPosition;

        private Point _center;
        private Point _offset;

        public EventHandler<string> OnInteract;
        public EventHandler<string> OnMove;

        public Map(string file)
        {
            _map = new TmxMap(file);

            _tilesets = new List<Texture2D>();

        }

        public virtual void Initialize(Game game)
        {
            foreach(var tileset in _map.Tilesets)
            {
                _tilesets.Add(game.Content.Load<Texture2D>(tileset.Image.Source.Replace("Maps\\../Content/", "").Split('.')[0]));
            }

            _screenSize = game.Window.ClientBounds;
            _center = new Point((int)(_screenSize.Width * 0.5), (int)(_screenSize.Height * 0.5));
            _playerPosition = GetPlayerStart();

            _offset = new Point(
                   (int)(_center.X - _map.TileWidth * 0.5 - _playerPosition.X * _map.TileWidth),
                   (int)(_center.Y - _map.TileHeight * 0.5 - _playerPosition.Y * _map.TileHeight)
                   );
        }

        public Point GetPlayerStart()
        {
            var tile = _map.Layers.First(x => x.Name == "Player").Tiles.First(x => x.Gid != 0);
            return new Point(tile.X, tile.Y);
        }

        public void Move(int x, int y)
        {
            var newCellIndex = new Point(_playerPosition.X+x, _playerPosition.Y+y);
            var newCell = _map.Layers[1].Tiles.First(cell => cell.X == newCellIndex.X && cell.Y == newCellIndex.Y);

            if (newCell.Gid == 0)
            {
                _playerPosition.X += x;
                _playerPosition.Y += y;

                _offset = new Point(
                   (int)(_center.X - _map.TileWidth * 0.5 - _playerPosition.X * _map.TileWidth),
                   (int)(_center.Y - _map.TileHeight * 0.5 - _playerPosition.Y * _map.TileHeight)
                   );

                if (OnMove != null)
                {
                    OnMove(this, null);
                }
            }
            else
            {
                //On test si la case possède une interaction
                var objectFind = _map.ObjectGroups.First().Objects.FirstOrDefault(obj =>
                obj.X == newCellIndex.X * _map.TileWidth
                && obj.Y == (newCellIndex.Y - y) * _map.TileHeight);

                if(objectFind != null && OnInteract != null)
                {
                    OnInteract(this, objectFind.Name);
                }
            }
        }

        public void Resize(Game game)
        {
            _screenSize = game.Window.ClientBounds;
            _center = new Point((int)(_screenSize.Width * 0.5), (int)(_screenSize.Height * 0.5));

            _offset = new Point(
                  (int)(_center.X - _map.TileWidth * 0.5 - _playerPosition.X * _map.TileWidth),
                  (int)(_center.Y - _map.TileHeight * 0.5 - _playerPosition.Y * _map.TileHeight)
                  );

        }

        public virtual void Update(GameTime gameTime)
        {

     
        }

        public virtual void DrawFloor(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var lineSize = _tilesets[0].Width / 16;
            foreach (var tile in _map.Layers[0].Tiles)
            {
                if (tile.Gid != 0)
                {
                    spriteBatch.Draw(_tilesets[0], new Rectangle(tile.X * 16 + _offset.X, tile.Y * 16 + _offset.Y, 16, 16), new Rectangle((tile.Gid % lineSize - 1) * 16, tile.Gid / lineSize * 16, 16, 16), Color.White);
                }
            }
        }

        public virtual void DrawCollisions(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var lineSize = _tilesets[0].Width / 16;
            foreach (var tile in _map.Layers[1].Tiles)
            {
                if (tile.Gid != 0)
                {
                    spriteBatch.Draw(_tilesets[0], new Rectangle(tile.X * 16 + _offset.X, tile.Y * 16 + _offset.Y, 16, 16), new Rectangle((tile.Gid % lineSize - 1) * 16, tile.Gid / lineSize * 16, 16, 16), Color.White);
                }
            }
        }

        //public virtual void DrawInteractions(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //    var lineSize = _tilesets[0].Width / 16;
        //    foreach (var tile in _map.Layers[2].Tiles)
        //    {
        //        if (tile.Gid != 0)
        //        {
        //            spriteBatch.Draw(_tilesets[0], new Rectangle(tile.X * 16 + _offset.X, tile.Y * 16 + _offset.Y, 16, 16), new Rectangle((tile.Gid % lineSize - 1) * 16, tile.Gid / lineSize * 16, 16, 16), Color.White);
        //        }
        //    }
        //}

        public virtual void DrawPlayer(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_tilesets[0], new Rectangle(_playerPosition.X * 16 + _offset.X, _playerPosition.Y * 16 + _offset.Y, 16, 16), new Rectangle(0, 0, 16, 16), Color.White);
            
        }

        public virtual void DrawForeground(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var lineSize = _tilesets[0].Width / 16;
            foreach (var tile in _map.Layers[2].Tiles)
            {
                if (tile.Gid != 0)
                {
                    spriteBatch.Draw(_tilesets[0], new Rectangle(tile.X * 16 + _offset.X, tile.Y * 16 + _offset.Y, 16, 16), new Rectangle((tile.Gid % lineSize - 1) * 16, tile.Gid / lineSize * 16, 16, 16), Color.White);
                }
            }
        }
    }
}
