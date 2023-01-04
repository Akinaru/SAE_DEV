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
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

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

        public Texture2D _textureObscurite;
        public static Vector2 _positionObscurite;

        public static OrthographicCamera _camera;
        public static Vector2 _cameraPosition;
        public static float _positionCameraX;
        public static float _positionCameraY;

        public static int _screenWidth;
        public static int _screenHeight;
        public static int _mapHeight;
        public static int _mapWidth;

        public SpriteFont _police;

        public static bool _debugMode;

        public static List<string> _mapLayers = new List<string>() { "Batiments","Batiments2", "Objets" };
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //regler la transparence des tuiles
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _debugMode = false;

            _vitessePerso = 100;

            //définition de la taille de la fenetre en fonctiond des dimensions données
            _screenWidth = 1280;
            _screenHeight = 720;
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();

            base.Initialize();


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _positionPerso = new Vector2((float)4.5 * _tiledMap.TileWidth, 7 * _tiledMap.TileHeight);


            _positionCameraX = _positionPerso.X;
            _positionCameraY = _positionPerso.Y;


            // Gestion de la caméra
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, _screenWidth, _screenHeight);
            _camera = new OrthographicCamera(viewportadapter);
            _cameraPosition = new Vector2(_screenWidth, _screenHeight);
            _camera.ZoomIn(1.5f);

            _mapWidth = _tiledMap.Width * 16;
            _mapHeight = _tiledMap.Height * 16;

        }

        protected override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("Map/map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _textureObscurite = Content.Load<Texture2D>("obscurite");
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            _police = Content.Load<SpriteFont>("font");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            animation = "idle";
            float walkSpeed = deltaTime * _vitessePerso;

            //la classe KeyboardManager permet de gérer les touches
            KeyboardManager.Manage(_positionPerso, _tiledMap, animation, walkSpeed, _mapWidth, _mapHeight, _graphics);

            //ici on evite a la camera de sortie de la map et d'afficher une zone "morte" qui ne contient pas de tuile
            
            _positionCameraX = _positionPerso.X;
            _positionCameraY = _positionPerso.Y;

            //si le personnage arrive dans l'angle a gauche, on place la camera
            if (_positionPerso.X < _screenWidth / 5)
                _positionCameraX = _screenWidth / 5;
            //si le personnage arrive dans l'angle a droite, on place la camera
            if (_positionPerso.X > (_mapWidth - _screenWidth / 5))
                _positionCameraX = (_mapWidth - _screenWidth / 5);
            //si le personnage arrive dans l'angle en haut , on place la camera
            if (_positionPerso.Y < _screenHeight / 5)
                _positionCameraY = _screenHeight / 5;
            //si le personnage arrive dans l'angle en bas, on place la camera
            if (_positionPerso.Y > (_mapHeight - _screenHeight / 5))
                _positionCameraY = (_mapHeight - _screenHeight / 5);

            _camera.LookAt(new Vector2(_positionCameraX, _positionCameraY));
            _positionObscurite = new Vector2(_positionPerso.X - 1080/2, _positionPerso.Y - 720/2);
            _perso.Play(animation);
            _perso.Update(deltaTime);
            _tiledMapRenderer.Update(gameTime);
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {

            var transformMatrix = _camera.GetViewMatrix();
            //affichage de la map et des sprites en fonction de la matrice créée depuis la caméra actuelle.

            _spriteBatch.Begin(transformMatrix: transformMatrix);

            _tiledMapRenderer.Draw(transformMatrix);
            _spriteBatch.Draw(_textureObscurite, _positionObscurite, Color.White);
            _spriteBatch.Draw(_perso, _positionPerso);

            _spriteBatch.End();


            _spriteBatch.Begin();
            if (_debugMode)
            {
                _spriteBatch.DrawString(_police, $"Pos: " + Math.Round(_positionPerso.X, 0) + ";" + Math.Round(_positionPerso.Y, 0), new Vector2(0, 0), Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        public static bool IsCollision(ushort x, ushort y)
        {
            //gestion des collisions listé dans la liste _mapLayers

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


    }
}