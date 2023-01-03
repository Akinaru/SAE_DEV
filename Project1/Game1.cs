﻿using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static TiledMap _tiledMap;
        public static TiledMapRenderer _tiledMapRenderer;


        public int largeurFenetre = 1080;
        public int hauteurFenetre = 720;

        public static OrthographicCamera _camera;
        public static Vector2 _cameraPosition;

        public static int _screenWidth;
        public static int _screenHeight;
        public static int _mapHeight;
        public static int _mapWidth;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Créez une nouvelle caméra
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _graphics.PreferredBackBufferWidth = largeurFenetre;
            _graphics.PreferredBackBufferHeight = hauteurFenetre;
            _graphics.ApplyChanges();

            // Camera Stuff
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, largeurFenetre, hauteurFenetre);
            _camera = new OrthographicCamera(viewportadapter);
            _cameraPosition = new Vector2(0, 0);
            _camera.Position = _cameraPosition;
            _camera.ZoomIn(0);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("Map/map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            System.Console.WriteLine(_camera.Position.X + " " + _camera.Position.Y);
            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _camera.Position += new Vector2(-1, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _camera.Position += new Vector2(1, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _camera.Position += new Vector2(0, -1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _camera.Position += new Vector2(0, 1);
            }
            _tiledMapRenderer.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Matrix zoomMatrix = Matrix.CreateScale(1.5f);
            var transformMatrix = _camera.GetViewMatrix() * zoomMatrix;
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            _tiledMapRenderer.Draw(transformMatrix);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}