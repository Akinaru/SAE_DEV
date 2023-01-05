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
        public static Texture2D _textureControls;
        public static Vector2 _positionPlayButton;


        //JEU

        public static Texture2D _textureombrePerso;



        public Texture2D _textureObscurite;
        public static Vector2 _positionObscurite;



        public static int _screenWidth;
        public static int _screenHeight;



        public Texture2D _textureSceptre;
        public static Vector2 _positionSceptre;
        public static float _rotationSceptre;

        public static SpriteFont _police;

        public static bool _debugMode;

        public static List<Monstre> _listeMonstre = new List<Monstre>();

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


            _positionPlayButton = new Vector2(490, 300);


            _viePerso = 6;
            Perso.Initialise();

            base.Initialize();

            _rotationSceptre = 0;


            for (int i = 0; i < 10; i++)
            {
                _listeMonstre.Add(new Monstre("monstreAnimation.sf",new Vector2(new Random().Next(0,1600), new Random().Next(0, 1600)), Content));
            }

            // Gestion de la caméra
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, _screenWidth, _screenHeight);
            Camera.Initialise(viewportadapter);
            Map.Initialise();

        }




        protected override void LoadContent()
        {
            //MENU
            _texturePlayButton = Content.Load<Texture2D>("play");
            _textureControls = Content.Load<Texture2D>("controls");
            _textureFondEcran = Content.Load<Texture2D>("background");


            //JEU

            Map.LoadContent(Content, GraphicsDevice);
            _textureombrePerso = Content.Load<Texture2D>("ombre");
            _textureObscurite = Content.Load<Texture2D>("obscurite");
            _textureMapUI = Content.Load<Texture2D>("map");
            _textureMapPerso = Content.Load<Texture2D>("Perso/mapPerso");
            _textureSceptre = Content.Load<Texture2D>("sceptre");
            _textureMonstreUI = Content.Load<Texture2D>("monstersUI");
            _texturePersoUI = Content.Load<Texture2D>("persoUI");

            _texturevieCoeurPlein = Content.Load<Texture2D>("coeur");
            _texturevieCoeurVide = Content.Load<Texture2D>("coeurvide");
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            Perso.LoadContent(spriteSheet);
            _police = Content.Load<SpriteFont>("font");

        }





        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //JEU

            if (_gameStarted)
            {

                if (_gameBegin)
                {
                    if (_wait < 4)
                    {
                        _wait += deltaTime;
                    }
                    else
                    {
                        _gameBegin = false;
                    }
                }
                float walkSpeed = deltaTime * Perso._vitessePerso;

                Perso.Update();
                //la classe KeyboardManager permet de gérer les touches
                KeyboardManager.Manage(Perso._positionPerso, Map._tiledMap, Perso.animation, walkSpeed, Map._mapWidth, Map._mapHeight, _graphics);

                Camera.Update();

                _positionSceptre = Perso._positionPerso;

                Game1._rotationSceptre += 0.05f / (float)Math.PI * 2;

                if (_showUI)
                    _positionMapPersoUI = new Vector2((Perso._positionPerso.X / 1600 * 600) + 340 - 8, (Perso._positionPerso.Y / 1600 * 600) + 60 - 8);

                _positionObscurite = new Vector2(Perso._positionPerso.X - 1080 / 2, Perso._positionPerso.Y - 720 / 2);
                if (!_gameBegin)
                    Monstre.Update(deltaTime);
                Perso._perso.Play(Perso.animation);
                Perso._perso.Update(deltaTime);
                Map.Update(gameTime);
            }

            //MENU

            else
            {
                var mouseState = Mouse.GetState();
                var mousePosition = new Point(mouseState.X, mouseState.Y);
                if (mousePosition.X >= _positionPlayButton.X &&
                    mousePosition.X <= _positionPlayButton.X + 300 &&
                    mousePosition.Y >= _positionPlayButton.Y &&
                    mousePosition.Y <= _positionPlayButton.Y + 100)
                {
                    _texturePlayButton = Content.Load<Texture2D>("playHover");
                }
                else
                {
                    _texturePlayButton = Content.Load<Texture2D>("play");
                }

                
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
                KeyboardState keyboardState = Keyboard.GetState();

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    _gameStarted = true;
                    _gameBegin = true;
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
                _spriteBatch.Draw(_textureControls, new Vector2(340, 570), Color.White);
                _spriteBatch.End();
            }
            else { 
                var transformMatrix = Camera._camera.GetViewMatrix();
                //affichage de la map et des sprites en fonction de la matrice créée depuis la caméra actuelle.

                _spriteBatch.Begin(transformMatrix: transformMatrix);

                Map.Draw(transformMatrix);


                Perso.Draw(_spriteBatch);
                Monstre.Draw(_spriteBatch);
                _spriteBatch.Draw(_textureSceptre, _positionSceptre, null, Color.White, _rotationSceptre, new Vector2(_textureSceptre.Width / 2, _textureSceptre.Height / 2), 1.0f, SpriteEffects.None, 1.0f);

                if (!_debugMode)
                    _spriteBatch.Draw(_textureObscurite, _positionObscurite, Color.White);
                _spriteBatch.End();


                _spriteBatch.Begin();
                MapUI.Draw(_spriteBatch);
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