using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using Newtonsoft.Json.Linq;

namespace Project1
{
    public class Jeu : GameScreen
    {

        public static Texture2D _textureombrePerso;
        public static Texture2D _textureObscurite;
        public static Texture2D _textureSang;
        public static Vector2 _positionObscurite;
        public static bool _debugMode;
        public static double _viePerso;
        public static bool _gameStarted;
        public static bool _gameBegin;
        public static float _wait;
        public static int _vague;
        public static int _nombreMonstre;
        public static int _nombreKill;

        public static List<Coeur> _listeCoeur = new List<Coeur>();

        public Jeu(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {
            _gameStarted = false;
            _gameBegin = false;
            _debugMode = false;
            _wait = 0;

            KeyboardManager.frappe = false;
            KeyboardManager.wait = 0;



            MapUI.Initialise();
            _viePerso = 6;
            Perso.Initialise();
            Fee.Initialise();
            Zone.Initialise();
            base.Initialize();

            _vague = 1;
            _nombreMonstre = 15;
            _nombreKill = 0;
            if (Game1._listeMonstre.Count > 0)
            {
                Game1._listeMonstre.Clear();
            }
            for (int i = 0; i < _nombreMonstre; i++)
            {
                Game1._listeMonstre.Add(new Monstre("monstreAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
            }
            var viewportadapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, Game1._screenWidth, Game1._screenHeight);
            Camera.Initialise(viewportadapter);
            Message.Display("Libere la ville des monstres !", "Fais vite... Je crois en toi !", 5);

        }
        public override void LoadContent()
        {
            HUD.LoadContent(Content);

            Fee.LoadContent(Content);
            _textureombrePerso = Content.Load<Texture2D>("ombre");
            _textureObscurite = Content.Load<Texture2D>("obscurite");
            _textureSang = Content.Load<Texture2D>("Perso/sang");


            MapUI.LoadContent(Content);
            HUD.LoadContent(Content);
            ViePerso.LoadContent(Content);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            Perso.LoadContent(Content);
            Message.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                if (KeyboardManager.wait >= 0.5)
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
                if (KeyboardManager.wait >= 1)
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
                if (Math.Round(Perso._waitBouclier, 0) == 3)
                {
                    Perso._touche = false;
                    Perso._waitBouclier = 0;
                }

            }

            float walkSpeed = deltaTime * Perso._vitessePerso;
            Perso.Update(deltaTime);
            Fee.Update();
            ViePerso.Update();
            KeyboardManager.Manage(Perso._positionPerso, Map._tiledMap, Perso._animation, walkSpeed, Map._mapWidth, Map._mapHeight, Game1._graphics, deltaTime);
            Camera.Update();
            MapUI.Update();
            Zone.Update();
            _positionObscurite = new Vector2(Perso._positionPerso.X - 1080 / 2, Perso._positionPerso.Y - 720 / 2);
            for (int i = 0; i < Game1._listeMonstre.Count; i++)
            {
                Game1._listeMonstre[i].Update(deltaTime, Content);
            }
            Perso._perso.Play(Perso._animation);
            Perso._perso.Update(deltaTime);
            Map.Update(gameTime);
            
        }
        public override void Draw(GameTime gameTime)
        {
            var matriceCamera = Camera._camera.GetViewMatrix();

            //affichage de la map et des sprites en fonction de la matrice créée depuis la caméra actuelle.
            Game1._spriteBatch.Begin(transformMatrix: matriceCamera);

            Map.Draw(matriceCamera);
            for (int i = 0; i < _listeCoeur.Count; i++)
            {
                Game1._spriteBatch.Draw(_listeCoeur[i].Texture, _listeCoeur[i].Position, Color.White);
            }
            Monstre.Draw(Game1._spriteBatch, Content);
            Perso.Draw(Game1._spriteBatch);

            ViePerso.Draw(Game1._spriteBatch);
            Fee.Draw(Game1._spriteBatch);

            if (!_debugMode)
                Game1._spriteBatch.Draw(_textureObscurite, _positionObscurite, Color.White);
            if (Perso._waitBouclier > 0)
                Game1._spriteBatch.Draw(_textureSang, Camera._cameraPosition-new Vector2(300,150), Color.White);
            Game1._spriteBatch.End();
            Game1._spriteBatch.Begin();
            MapUI.Draw(Game1._spriteBatch);
            HUD.Draw(Game1._spriteBatch);
            Message.Draw(Game1._spriteBatch);
            Game1._spriteBatch.End();
        }
    }
}
