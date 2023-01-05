using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
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
        //BASIC
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch { get; set; }

        //MAIN MENU
        public static Texture2D _textureFondEcran;
        public static Texture2D _texturePlayButton;
        public static Vector2 _positionPlayButton;


        //JEU
        public static TiledMap _tiledMap;
        public static TiledMap _tiledMapInterieur;

        public static TiledMapRenderer _tiledMapRenderer;

        public static Vector2 _positionPerso;
        public static AnimatedSprite _perso;
        public static Texture2D _textureombrePerso;
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


        public Texture2D _textureSceptre;
        public static Vector2 _positionSceptre;
        public static float _rotationSceptre;

        public static SpriteFont _police;

        public static bool _debugMode;

        public static List<Monstre> _listeMonstre = new List<Monstre>();
        public static int _nombreMonstre;

        public static bool _showUI;
        public static Texture2D _textureMapUI;
        public static Texture2D _textureMapPerso;
        public static Vector2 _positionMapPersoUI;

        public static double _viePerso;
        public static Texture2D _texturevieCoeurPlein;
        public static Texture2D _texturevieCoeurDemi;
        public static Texture2D _texturevieCoeurVide;

        public static Texture2D _textureMonstreUI;
        public static Texture2D _texturePersoUI;


        public static bool _gameStarted;
        public static bool _gameBegin;
        public static float _wait;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }





        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;


            _gameStarted = false;
            _gameBegin = false;
            _debugMode = false;
            _showUI = false;
            _wait = 0;

            _screenWidth = 1280;
            _screenHeight = 720;
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();


            _positionPlayButton = new Vector2(490, 200);


            _vitessePerso = 100;
            _viePerso = 6;


            base.Initialize();

            _rotationSceptre = 0;
            _positionPerso = new Vector2(130,146);
            _positionSceptre = _positionPerso;

            _positionCameraX = _positionPerso.X;
            _positionCameraY = _positionPerso.Y;
            _nombreMonstre = 0;
            for (int i = 0; i < 10; i++)
            {
                _listeMonstre.Add(new Monstre("persoAnimation.sf", new Vector2(new Random().Next(0,1600), new Random().Next(0, 1600)), Content));
            }

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
            //MENU
            _texturePlayButton = Content.Load<Texture2D>("play");
            _textureFondEcran = Content.Load<Texture2D>("background");


            //JEU

            _tiledMap = Content.Load<TiledMap>("Map/map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _textureombrePerso = Content.Load<Texture2D>("ombre");
            _textureObscurite = Content.Load<Texture2D>("obscurite");
            _textureMapUI = Content.Load<Texture2D>("map");
            _textureMapPerso = Content.Load<Texture2D>("Perso/mapPerso");
            _textureSceptre = Content.Load<Texture2D>("sceptre");
            _textureMonstreUI = Content.Load<Texture2D>("monstersUI");
            _texturePersoUI = Content.Load<Texture2D>("persoUI");

            _texturevieCoeurPlein = Content.Load<Texture2D>("coeur");
            _texturevieCoeurVide = Content.Load<Texture2D>("coeurvide");
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnim.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            _police = Content.Load<SpriteFont>("font");

            // TODO: use this.Content to load your game content here
        }





        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //JEU

            if (_gameStarted)
            {
                
                if (_gameBegin) { 
                    if (_wait < 4)
                    {
                        _wait += deltaTime;
                    }
                    else
                    {
                        _gameBegin = false;
                    }
                 }
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

                _positionSceptre = _positionPerso;

                Game1._rotationSceptre += 0.05f / (float)Math.PI * 2;

                if (_showUI)
                    _positionMapPersoUI = new Vector2((_positionPerso.X / 1600 * 600) + 340 - 8, (_positionPerso.Y / 1600 * 600) + 60 - 8);

                _camera.LookAt(new Vector2(_positionCameraX, _positionCameraY));
                _positionObscurite = new Vector2(_positionPerso.X - 1080 / 2, _positionPerso.Y - 720 / 2);
                if(!_gameBegin)    
                    Monstre.Update(deltaTime);
                _perso.Play(animation);
                _perso.Update(deltaTime);
                _tiledMapRenderer.Update(gameTime);
            }

            //MENU

            else 
            {
                var mouseState = Mouse.GetState();
                var mousePosition = new Point(mouseState.X, mouseState.Y);
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (mousePosition.X >= _positionPlayButton.X &&
                            mousePosition.X <= _positionPlayButton.X + 300 &&
                            mousePosition.Y >= _positionPlayButton.Y &&
                            mousePosition.Y <= _positionPlayButton.Y + 100)
                        {
                            _gameStarted = true;
                            _gameBegin = true;
                        }
                }

            }
            base.Update(gameTime);
        }





        protected override void Draw(GameTime gameTime)
        {
            if (!_gameStarted)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(_textureFondEcran, new Vector2(0,0), Color.White);
                _spriteBatch.Draw(_texturePlayButton, _positionPlayButton, Color.White);
                _spriteBatch.End();
            }
            else { 
                var transformMatrix = _camera.GetViewMatrix();
                //affichage de la map et des sprites en fonction de la matrice créée depuis la caméra actuelle.

                _spriteBatch.Begin(transformMatrix: transformMatrix);

                _tiledMapRenderer.Draw(transformMatrix);


                _spriteBatch.Draw(_textureombrePerso, _positionPerso + new Vector2(-6, 5), Color.White);
                _spriteBatch.Draw(_perso, _positionPerso);
                Monstre.Draw(_spriteBatch);
                _spriteBatch.Draw(_textureSceptre, _positionSceptre, null, Color.White, _rotationSceptre, new Vector2(_textureSceptre.Width / 2, _textureSceptre.Height / 2), 1.0f, SpriteEffects.None, 1.0f);

                if (!_debugMode)
                    _spriteBatch.Draw(_textureObscurite, _positionObscurite, Color.White);
                _spriteBatch.End();


                _spriteBatch.Begin();
                MapUI.Draw(_spriteBatch);


                if (_debugMode)
                {
                    _spriteBatch.DrawString(_police, $"Pos: " + Math.Round(_positionPerso.X, 0) + ";" + Math.Round(_positionPerso.Y, 0), new Vector2(0, 0), Color.Black);
                    _spriteBatch.DrawString(_police, $"Vitesse: " + _vitessePerso, new Vector2(0, 20), Color.Black);
                }
                UI.Draw(_spriteBatch);

                if (_gameBegin)
                {
                    _spriteBatch.DrawString(_police, "Les monstres arrivent...", new Vector2(550,670), Color.White);
                }
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}