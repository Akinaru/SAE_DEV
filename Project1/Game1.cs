using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public static int _largeurEcran;
        public static int _hauteurEcran;
        public static List<Monstre> _listeMonstre = new List<Monstre>();
        public static List<Bombe> _listeBombe = new List<Bombe>();
        public static float _volumeSon;



        public enum Etats { Menu,Play, GameOver, Quit, Attente, BackMenu };
        public static Etats etat;
        private readonly ScreenManager _screenManager;
        private Menu _screenMenu;
        private Jeu _screenJeu;
        private GameOver _screenGameOver;

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
            _screenMenu = new Menu(this);
            _screenJeu = new Jeu(this);
            _screenGameOver = new GameOver(this);
        }





        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _volumeSon = 0.5f;
            _largeurEcran = 1280;
            _hauteurEcran = 720;
            _graphics.PreferredBackBufferWidth = Game1._largeurEcran;
            _graphics.PreferredBackBufferHeight = Game1._hauteurEcran;
            _graphics.ApplyChanges();
            base.Initialize();
            Map.Initialise();

        }

        protected override void LoadContent()
        {
            //on laod la map avant le reste car on va utiliser les valeur de la tileMap dans l'initialise
            Map.LoadContent(Content, GraphicsDevice);
        }





        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //JEU
            if (Etat == Etats.Quit)
                Exit();
            else if (Etat == Etats.Menu)
            {
                IsMouseVisible = true;
                Etat = Etats.Attente;
                _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black, 0.01f));
            }
            else if (Etat == Etats.BackMenu)
            {
                IsMouseVisible = true;
                Etat = Etats.Attente;
                _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
            }
            else if (Etat == Etats.Play)
            {
                IsMouseVisible = false;
                Etat = Etats.Attente;
                _screenManager.LoadScreen(_screenJeu, new FadeTransition(GraphicsDevice, Color.Black));
            }
            else if (Etat == Etats.GameOver)
            {
                IsMouseVisible = true;
                Etat = Etats.Attente;
                _screenManager.LoadScreen(_screenGameOver, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (Jeu._pause )
                IsMouseVisible = true;
            else
                if(Etat == Etats.Play)
                    IsMouseVisible = false;

            Message.Update(deltaTime);
            base.Update(gameTime);
        }





        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}