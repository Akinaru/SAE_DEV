using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using static Project1.Game1;
using static System.Formats.Asn1.AsnWriter;

namespace Project1
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch { get; set; }

        public static Texture2D _textureombrePerso;
        public Texture2D _textureObscurite;
        public static Vector2 _positionObscurite;

        public static int _screenWidth;
        public static int _screenHeight;

        public static bool _debugMode;

        public static List<Monstre> _listeMonstre = new List<Monstre>();

        public static double _viePerso;

        public static bool _gameStarted;
        public static bool _gameBegin;

        public static float _wait;

        public static int _vague;
        public static int _nombreMonstre;

        public enum Etats { Menu,Play, GameOver, Quit, Attente };
        public static Etats etat;
        private readonly ScreenManager _screenManager;
        private ScreenMenu _screenMenu;
        private ScreenJeu _screenJeu;
        //private ScreenGameOver _screenGameOver;

        public static Etats Etat
        {
            get
            {
                return etat;
            }

            set
            {
                etat = value;
            }
        }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
            Etat = Etats.Menu;

            // on charge les 2 écrans 
            _screenMenu = new ScreenMenu(this);
            _screenJeu = new ScreenJeu(this);
            //_screenGameOver = new ScreenGameOver(this);
        }





        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _gameStarted = false;
            _gameBegin = false;
            _debugMode = false;
            _wait = 0;

            KeyboardManager.frappe = false;
            KeyboardManager.wait = 0;

            _screenWidth = 1280;
            _screenHeight = 720;
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();

            MapUI.Initialise();
            _viePerso = 6;
            Perso.Initialise();
            Fee.Initialise();
            Zone.Initialise();
            base.Initialize();


            _vague = 1;
            _nombreMonstre = 1;
            for (int i = 0; i < _nombreMonstre; i++)
            {
                _listeMonstre.Add(new Monstre("monstreAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
            }
            // Gestion de la caméra
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, _screenWidth, _screenHeight);
            Camera.Initialise(viewportadapter);
            Map.Initialise();

        }

        protected override void LoadContent()
        {
            //MENU


            //JEU
            HUD.LoadContent(Content);
            Map.LoadContent(Content, GraphicsDevice);
            Fee.LoadContent(Content);
            _textureombrePerso = Content.Load<Texture2D>("ombre");
            _textureObscurite = Content.Load<Texture2D>("obscurite");


            MapUI.LoadContent(Content);
            HUD.LoadContent(Content);
            ViePerso.LoadContent(Content);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            Perso.LoadContent(Content);
            Message.LoadContent(Content);
            _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
        }





        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //JEU

            if (Etat == Etats.Quit)
                Exit();

            else if (Etat == Etats.Play)
            {
                Etat = Etats.Attente;
                _screenManager.LoadScreen(_screenJeu, new FadeTransition(GraphicsDevice, Color.Black));
            }
                _viePerso = 6;

                //else if (this.Etat == Etats.GameOver)
                //    _screenManager.LoadScreen(_screenGameOver, new FadeTransition(GraphicsDevice, Color.Black));

                Message.Update(deltaTime);

            if (_gameStarted)
            {
                if (_gameBegin)
                {
                    if (_wait < 4)
                        _wait += deltaTime;
                    else
                        _gameBegin = false;
                }

                if (KeyboardManager.frappe)
                {
                    KeyboardManager.wait += 2 * deltaTime;
                    if(KeyboardManager.wait >= 0.5)
                    {
                        for (int i = 0; i < Game1._listeMonstre.Count; i++)
                        {
                            Monstre monstre = Game1._listeMonstre[i];
                            if (monstre.Hit)
                            {
                                monstre.Hit = false;
                            }
                        }
                    }
                    if(KeyboardManager.wait >= 1)
                    {
                        KeyboardManager.frappe = false;
                        Perso._animEpee = false;
                        Perso._epee.Play("fight");
                        Perso._epee.Update(deltaTime);
                        KeyboardManager.wait = 0;
                    }
                }
                if (Perso._touche)
                {
                    Perso._waitBouclier += 1 * deltaTime;
                    if(Math.Round(Perso._waitBouclier, 0) == 3)
                    {
                        Perso._touche = false;
                        Perso._waitBouclier = 0;
                    }
                    
                }

                float walkSpeed = deltaTime * Perso._vitessePerso;
                Perso.Update(deltaTime);
                Fee.Update();
                ViePerso.Update();
                KeyboardManager.Manage(Perso._positionPerso, Map._tiledMap, Perso._animation, walkSpeed, Map._mapWidth, Map._mapHeight, _graphics, deltaTime);
                Camera.Update();
                MapUI.Update();
                Zone.Update();
                _positionObscurite = new Vector2(Perso._positionPerso.X - 1080 / 2, Perso._positionPerso.Y - 720 / 2);
                Monstre.Update(deltaTime);
                Perso._perso.Play(Perso._animation);
                Perso._perso.Update(deltaTime);
                Map.Update(gameTime);
            }

            //MENU

            else
            {
            
            }
            base.Update(gameTime);
        }





        protected override void Draw(GameTime gameTime)
        {

            if (!_gameStarted)
            {
                _spriteBatch.Begin();

                _spriteBatch.End();
            }
            else { 
                var transformMatrix = Camera._camera.GetViewMatrix();

                //affichage de la map et des sprites en fonction de la matrice créée depuis la caméra actuelle.
                _spriteBatch.Begin(transformMatrix: transformMatrix);

                Map.Draw(transformMatrix);
                
                Monstre.Draw(_spriteBatch, Content);
                Perso.Draw(_spriteBatch);

                ViePerso.Draw(_spriteBatch);
                Fee.Draw(_spriteBatch);

                if (!_debugMode)
                    _spriteBatch.Draw(_textureObscurite, _positionObscurite, Color.White);
                _spriteBatch.End();




                _spriteBatch.Begin();
                MapUI.Draw(_spriteBatch);
                HUD.Draw(_spriteBatch);
                Message.Draw(_spriteBatch);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}