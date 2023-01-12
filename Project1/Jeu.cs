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
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Audio;

namespace Project1
{
    public class Jeu : GameScreen
    {

        public static Texture2D _textureombrePerso;
        public static Texture2D _textureObscurite;
        public static Texture2D _textureSang;
        public static Vector2 _positionObscurite;
        public static bool _gameStarted;
        public static bool _gameBegin;
        public static float _wait;
        public static int _vague;
        public static int _nombreMonstre;
        public static int _nombreFantome;
        public static int _nombreKill;
        public static int _nombreCoup;
        public static double _precision;
        public static float _chrono;
        public static bool _pause;
        public enum NiveauDifficulte { Facile, Difficile, Extreme};
        public static NiveauDifficulte difficulte;
        public static SoundEffect _sonEpee;



        public static List<Coeur> _listeCoeur = new List<Coeur>();

        public Jeu(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {
            _gameStarted = false;
            _gameBegin = false;
            _wait = 0;
            _chrono = 0;
            _pause = false;
            _nombreCoup = 0;
            KeyboardManager._frappe = false;
            KeyboardManager._wait = 0;
            KeyboardManager.Initialise();
            MapUI.Initialise();
            Perso.Initialise();
            Fee.Initialise();
            Zone.Initialise();
            base.Initialize();

            _vague = 1;
            if(difficulte == NiveauDifficulte.Facile)
            {
                _nombreMonstre = 15;
                _nombreFantome = 3;
            }
            else if (difficulte == NiveauDifficulte.Difficile)
            {
                _nombreMonstre = 25;
                _nombreFantome = 5;
            }
            else {
                _nombreMonstre = 10; //50
                _nombreFantome = 25;
            }
            _nombreKill = 0;
            _precision = 0;
            if (Game1._listeMonstre.Count > 0)
            {
                Game1._listeMonstre.Clear();
            }
            if (Game1._listeFantome.Count > 0)
            {
                Game1._listeFantome.Clear();
            }
            for (int i = 0; i < _nombreMonstre; i++)
            {
                Game1._listeMonstre.Add(new Monstre("monstreAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
            }
            for (int i = 0; i < _nombreFantome; i++)
            {
                Game1._listeFantome.Add(new Fantome("fantome.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
            }
            Game1._listeBoss.Add(new Boss("bossAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
            Game1._listeBoss.Add(new Boss("bossAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
            var viewportadapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, Game1._largeurEcran, Game1._hauteurEcran);
            Camera.Initialise(viewportadapter);
        }
        public override void LoadContent()
        {

            _textureombrePerso = Content.Load<Texture2D>("ombre");
            _textureObscurite = Content.Load<Texture2D>("obscurite");
            _textureSang = Content.Load<Texture2D>("Perso/sang");
            _sonEpee = Content.Load<SoundEffect>("Son/epee");
            Pause.LoadContent(Content);
            HUD.LoadContent(Content);
            Fee.LoadContent(Content);
            MapUI.LoadContent(Content);
            HUD.LoadContent(Content);
            ViePerso.LoadContent(Content);
            VieBoss.LoadContent(Content);
            Perso.LoadContent(Content);
            Message.LoadContent(Content);
            Message.Display("Libere la ville des monstres !", "Fais vite... Je crois en toi !", 5);

        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * Perso._vitessePerso;
            Perso._animation = "idle";
            KeyboardManager.Manage(Perso._positionPerso, Map._tiledMap, Perso._animation, walkSpeed, Map._mapWidth, Map._mapHeight, Game1._graphics, deltaTime, Content);

            if (!_pause)
            {
                _chrono += 1 * deltaTime;
                if (_gameBegin)
                {
                    if (_wait < 4)
                        _wait += deltaTime;
                    else
                        _gameBegin = false;
                }

                if (KeyboardManager._frappe)
                {
                    KeyboardManager._wait += 2 * deltaTime;
                    if (KeyboardManager._wait >= 0.5)
                    {
                        for (int i = 0; i < Game1._listeMonstre.Count; i++)
                        {
                            Monstre monstre = Game1._listeMonstre[i];
                            if (monstre.Hit)
                            {
                                monstre.Hit = false;
                            }
                        }
                        for (int i = 0; i < Game1._listeFantome.Count; i++)
                        {
                            Fantome fantome = Game1._listeFantome[i];
                            if (fantome.Hit)
                            {
                                fantome.Hit = false;
                            }
                        }
                        for (int i = 0; i < Game1._listeBoss.Count; i++)
                        {
                            Boss fantome = Game1._listeBoss[i];
                            if (fantome.Hit)
                            {
                                fantome.Hit = false;
                            }
                        }
                    }
                    if (KeyboardManager._wait >= 0.7)
                    {
                        KeyboardManager._frappe = false;
                        Perso._perso = new AnimatedSprite(Perso._spriteSheetWalkEpee);
                        Perso._animEpee = false;
                        Perso._epee.Play("fight");
                        Perso._epee.Update(deltaTime);
                        KeyboardManager._wait = 0;
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
                for (int i = 0; i < _listeCoeur.Count; i++)
                {
                    _listeCoeur[i].CheckRecuperer(deltaTime);
                }
                Perso.Update(deltaTime);
                ViePerso.Update();
                VieBoss.Update();
                Camera.Update();
                Zone.Update();
                _positionObscurite = new Vector2(Perso._positionPerso.X - 1080 / 2, Perso._positionPerso.Y - 720 / 2);
                for (int i = 0; i < Game1._listeMonstre.Count; i++)
                {
                    Game1._listeMonstre[i].Update(deltaTime, Content);
                }
                for (int i = 0; i < Game1._listeFantome.Count; i++)
                {
                    Game1._listeFantome[i].Update(deltaTime, Content);
                }
                for (int i = 0; i < Game1._listeBoss.Count; i++)
                {
                    Game1._listeBoss[i].Update(deltaTime, Content);
                }
            }
            else
            {
                Pause.Update(Content);
            }

            Fee.Update();
            Map.Update(gameTime);
            
        }
        public override void Draw(GameTime gameTime)
        {
            var matriceCamera = Camera._camera.GetViewMatrix();
            Game1._spriteBatch.Begin(transformMatrix: matriceCamera);
            Map.Draw(matriceCamera);

            for (int i = 0; i < _listeCoeur.Count; i++)
                Game1._spriteBatch.Draw(_listeCoeur[i].CoeurSprite, _listeCoeur[i].Position);
            for (int i = 0; i < Game1._listeMonstre.Count; i++)
                Game1._listeMonstre[i].Draw(Game1._spriteBatch, Content);
            for (int i = 0; i < Game1._listeFantome.Count; i++)
                Game1._listeFantome[i].Draw(Game1._spriteBatch, Content);
            for (int i = 0; i < Game1._listeBoss.Count; i++)
                Game1._listeBoss[i].Draw(Game1._spriteBatch, Content);

            Perso.Draw(Game1._spriteBatch);
            ViePerso.Draw(Game1._spriteBatch);
            Fee.Draw(Game1._spriteBatch);
            if ((difficulte == NiveauDifficulte.Difficile || difficulte == NiveauDifficulte.Extreme))
                
                Game1._spriteBatch.Draw(_textureObscurite, _positionObscurite, Color.White);

            if (Perso._waitBouclier > 0)
                Game1._spriteBatch.Draw(_textureSang, Camera._cameraPosition-new Vector2(300,150), Color.White);
            Game1._spriteBatch.End();

            Game1._spriteBatch.Begin();
            MapUI.Draw(Game1._spriteBatch);
            HUD.Draw(Game1._spriteBatch);
            Message.Draw(Game1._spriteBatch);
            VieBoss.Draw(Game1._spriteBatch);
            Pause.Draw(Game1._spriteBatch);

            Game1._spriteBatch.End();
        }

        public static String getChrono()
        {
            int minute = (int)Jeu._chrono / 60;
            int seconde = (int)Jeu._chrono % 60;
            return "" + minute + "m et "+seconde+"s";
        }

    }
}
