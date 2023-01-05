using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Map
    {

        public static TiledMap _tiledMap;
        public static TiledMapRenderer _tiledMapRenderer;
        public static int _mapHeight;
        public static int _mapWidth;

        public static void Initialise()
        {

            _mapWidth = _tiledMap.Width * 16;
            _mapHeight = _tiledMap.Height * 16;
        }

        public static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            _tiledMap = Content.Load<TiledMap>("Map/map");
            _tiledMapRenderer = new TiledMapRenderer(graphicsDevice, _tiledMap);
        }

        public static void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);

        }

        internal static void Draw(Matrix transformMatrix)
        {
            _tiledMapRenderer.Draw(transformMatrix);

        }
    }
}
