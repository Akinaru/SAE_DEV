using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;

namespace Project1
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public static TiledMap _tiledMap;
        public static TiledMap _tiledMapInterieur;

        public static TiledMapRenderer _tiledMapRenderer;

        public static Vector2 _positionPerso;
        public static AnimatedSprite _perso;
        public static int _vitessePerso;
        public static string animation;

        public static OrthographicCamera _camera;
        public static Vector2 _cameraPosition;
        public static float _positionCameraX;
        public static float _positionCameraY;

        public static int _screenWidth;
        public static int _screenHeight;
        public static int _mapHeight;
        public static int _mapWidth;

        public static List<string> _mapLayers = new List<string>() { "Arbres2" };
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Créez une nouvelle caméra
        }

        protected override void Initialize()
        {

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _vitessePerso = 100;

            _screenWidth = 1280;
            _screenHeight = 720;

            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();

            base.Initialize();


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //_positionPerso = new Vector2((int)(_screenWidth / 2), _screenHeight / 2);
            _positionPerso = new Vector2((float)4.5 * _tiledMap.TileWidth, 7 * _tiledMap.TileHeight);

            _positionCameraX = _positionPerso.X;
            _positionCameraY = _positionPerso.Y;
            // Camera Stuff
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, _screenWidth, _screenHeight);
            _camera = new OrthographicCamera(viewportadapter);
            _cameraPosition = new Vector2(_screenWidth, _screenHeight);
            _camera.ZoomIn(0.5f);

            _mapWidth = _tiledMap.Width * 16;
            _mapHeight = _tiledMap.Height * 16;
        }

        protected override void LoadContent()
        {


            _tiledMap = Content.Load<TiledMap>("Map/map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            //SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            //_perso = new AnimatedSprite(spriteSheet);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            animation = "idle";
            float walkSpeed = deltaTime * _vitessePerso;

            KeyboardManager.Manage(_positionPerso, _tiledMap, animation, walkSpeed, _mapWidth, _mapHeight, _graphics);

            _positionCameraX = _positionPerso.X;
            _positionCameraY = _positionPerso.Y;

            if (_positionPerso.X < _screenWidth / 2)
                _positionCameraX = _screenWidth / 2;

            if (_positionPerso.X > (_mapWidth - _screenWidth / 2))
                _positionCameraX = (_mapWidth - _screenWidth / 2);

            if (_positionPerso.Y < _screenHeight / 2)
                _positionCameraY = _screenHeight / 2;

            if (_positionPerso.Y > (_mapHeight - _screenHeight / 2))
                _positionCameraY = (_mapHeight - _screenHeight / 2);

            _camera.LookAt(new Vector2(_positionCameraX, _positionCameraY));

            //_perso.Play(animation);
            //_perso.Update(deltaTime);
            _tiledMapRenderer.Update(gameTime);
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {

            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);

            _tiledMapRenderer.Draw(transformMatrix);
            //_spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        public static bool IsCollision(ushort x, ushort y)
        {


            for (int i = 0; i < _mapLayers.Count; i++)
            {
                TiledMapTileLayer _Layer = _tiledMap.GetLayer<TiledMapTileLayer>(_mapLayers[i]);
                TiledMapTile? tile;
                if (_Layer.TryGetTile(x, y, out tile) == false)
                    return false;
                if (!tile.Value.IsBlank)
                    return true;

            }
            return false;

        }

        public static bool isCollisionPorte(ushort x, ushort y)
        {
            TiledMapTileLayer _porte = _tiledMap.GetLayer<TiledMapTileLayer>("Porte");
            TiledMapTile? tile;
            if (_porte.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }


    }
}