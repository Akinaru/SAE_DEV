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
        public static int _screenWidth;
        public static int _screenHeight;
        public static List<Monstre> _listeMonstre = new List<Monstre>();




        public enum Etats { Menu,Play, GameOver, Quit, Attente };
        public static Etats etat;
        private readonly ScreenManager _screenManager;
        private ScreenMenu _screenMenu;
        private ScreenJeu _screenJeu;
        private ScreenGameOver _screenGameOver;

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
            _screenGameOver = new ScreenGameOver(this);
        }





        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _screenWidth = 1280;
            _screenHeight = 720;
            _graphics.PreferredBackBufferWidth = Game1._screenWidth;
            _graphics.PreferredBackBufferHeight = Game1._screenHeight;
            _graphics.ApplyChanges();

            base.Initialize();

            Map.Initialise();

        }

        protected override void LoadContent()
        {
            //MENU


            //JEU
            Map.LoadContent(Content, GraphicsDevice);

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
            else if (Etat == Etats.GameOver)
            {
                Etat = Etats.Attente;
                _screenManager.LoadScreen(_screenGameOver, new FadeTransition(GraphicsDevice, Color.Black));
            }

            Message.Update(deltaTime);
            base.Update(gameTime);
        }





        protected override void Draw(GameTime gameTime)
        {



            

            base.Draw(gameTime);
        }
    }
}