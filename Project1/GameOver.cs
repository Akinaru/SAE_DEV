using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project1.Jeu;

namespace Project1
{
    public class GameOver : GameScreen
    {

        public static Texture2D _textureBoutonMenu;
        public static Texture2D _textureblurBackground;
        public static Texture2D _textureGameOver;
        public static Texture2D _textureBoutonRejouer;
        public static Texture2D _textureRaccourciM;
        public static Texture2D _textureRaccourciR;
        public static Texture2D _textureBoutonSortir;

        public static Vector2 _positionGameOver;
        public static Vector2 _positionBoutonMenu;
        public static Vector2 _positionBoutonRejouer;
        public static Vector2 _positionRaccourciM;
        public static Vector2 _positionRaccourciR;
        public static Vector2 _positionBoutonSortir;
        public GameOver(Game1 game) : base(game) 
        {
        }

        public override void Initialize()
        {
            _positionGameOver = new Vector2(490, 30);
            _positionRaccourciM = new Vector2(631, 685);
            _positionRaccourciR = new Vector2(631, 555);
            _positionBoutonRejouer = new Vector2(1280/2 - (538/2), 450);
            _positionBoutonMenu = new Vector2(490, 580);
            _positionBoutonSortir = new Vector2(1220, 650);
            base.Initialize();
        }
        public override void LoadContent()
        {
            _textureBoutonMenu = Content.Load<Texture2D>("Menu/boutonMenu");
            _textureBoutonRejouer = Content.Load<Texture2D>("Menu/rejouer");
            _textureblurBackground = Content.Load<Texture2D>("Menu/blurBackground");
            _textureGameOver = Content.Load<Texture2D>("Menu/gameOver");
            _textureRaccourciM = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciM");
            _textureRaccourciR = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciR");
            _textureBoutonSortir = Content.Load<Texture2D>("Menu/sortir");
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);


            //hover menu
            if (mousePosition.X >= _positionBoutonMenu.X &&
                mousePosition.X <= _positionBoutonMenu.X + 300 &&
                mousePosition.Y >= _positionBoutonMenu.Y &&
                mousePosition.Y <= _positionBoutonMenu.Y + 100)
            {
                _textureBoutonMenu = Content.Load<Texture2D>("Menu/boutonMenuHover");
            }
            else
            {
                _textureBoutonMenu = Content.Load<Texture2D>("Menu/boutonMenu");
            }
            //hover rejouer
            if (mousePosition.X >= _positionBoutonRejouer.X &&
                mousePosition.X <= _positionBoutonRejouer.X + 538 &&
                mousePosition.Y >= _positionBoutonRejouer.Y &&
                mousePosition.Y <= _positionBoutonRejouer.Y + 100)
            {
                _textureBoutonRejouer = Content.Load<Texture2D>("Menu/rejouerHover");
            }
            else
            {
                _textureBoutonRejouer = Content.Load<Texture2D>("Menu/rejouer");
            }
            //hover sortir
            if (Game1._volumeSon != 0)
            {
                if (mousePosition.X >= _positionBoutonSortir.X &&
                    mousePosition.X <= _positionBoutonSortir.X + 50 &&
                    mousePosition.Y >= _positionBoutonSortir.Y &&
                    mousePosition.Y <= _positionBoutonSortir.Y + 50)
                {
                    _textureBoutonSortir = Content.Load<Texture2D>("Menu/sortirHovevr");
                }
                else
                {
                    _textureBoutonSortir = Content.Load<Texture2D>("Menu/sortir");
                }
            }

            //clique pour menu
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if(mousePosition.X >= _positionBoutonMenu.X &&
                    mousePosition.X <= _positionBoutonMenu.X + 300 &&
                    mousePosition.Y >= _positionBoutonMenu.Y &&
                    mousePosition.Y <= _positionBoutonMenu.Y + 100)
                {
                    Game1.Etat = Game1.Etats.BackMenu;

                }
            }
            //clique pour rejouer
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (mousePosition.X >= _positionBoutonRejouer.X &&
                    mousePosition.X <= _positionBoutonRejouer.X + 538 &&
                    mousePosition.Y >= _positionBoutonRejouer.Y &&
                    mousePosition.Y <= _positionBoutonRejouer.Y + 100)
                {
                    Game1.Etat = Game1.Etats.Play;

                }
            }
            //BOUTON SORTIR
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (mousePosition.X >= _positionBoutonSortir.X &&
                    mousePosition.X <= _positionBoutonSortir.X + 50 &&
                    mousePosition.Y >= _positionBoutonSortir.Y &&
                    mousePosition.Y <= _positionBoutonSortir.Y + 50)
                {
                    Game1.Etat = Game1.Etats.Exit;
                }
            }
            //raccourci pour menu
            KeyboardState keyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                if (!Jeu._gameStarted)
                    Game1.Etat = Game1.Etats.BackMenu;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                if (!Jeu._gameStarted)
                    Game1.Etat = Game1.Etats.Play;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            var matriceCamera = Camera._camera.GetViewMatrix();
            Game1._spriteBatch.Begin(transformMatrix: matriceCamera);
            Map.Draw(matriceCamera);
            for (int i = 0; i < Game1._listeMonstre.Count; i++)
                Game1._listeMonstre[i].Draw(Game1._spriteBatch, Content);
            for (int i = 0; i < Game1._listeFantome.Count; i++)
                Game1._listeFantome[i].Draw(Game1._spriteBatch, Content);
            for (int i = 0; i < Game1._listeBoss.Count; i++)
                Game1._listeBoss[i].Draw(Game1._spriteBatch, Content);
            Perso.Draw(Game1._spriteBatch);

            ViePerso.Draw(Game1._spriteBatch);
            Fee.Draw(Game1._spriteBatch);
            if (difficulte == NiveauDifficulte.Difficile)
                Game1._spriteBatch.Draw(_textureObscurite, _positionObscurite, Color.White);
            if (Perso._waitBouclier > 0)
                Game1._spriteBatch.Draw(_textureSang, Camera._cameraPosition - new Vector2(300, 150), Color.White);
            Game1._spriteBatch.End();

            Game1._spriteBatch.Begin();
            HUD.Draw(Game1._spriteBatch);
            Message.Draw(Game1._spriteBatch);

            Game1._spriteBatch.End();
            Game1._spriteBatch.Begin();
            if (Perso._mort)
            {

                Game1._spriteBatch.Draw(_textureblurBackground, new Vector2(0, 0), Color.White);
                Game1._spriteBatch.Draw(_textureBoutonMenu, _positionBoutonMenu, Color.White);
                Game1._spriteBatch.Draw(_textureBoutonRejouer, _positionBoutonRejouer, Color.White);
                Game1._spriteBatch.Draw(_textureGameOver, _positionGameOver, Color.White);
                Game1._spriteBatch.Draw(_textureRaccourciM, _positionRaccourciM, Color.White);
                Game1._spriteBatch.Draw(_textureRaccourciR, _positionRaccourciR, Color.White);
                Game1._spriteBatch.Draw(_textureBoutonSortir, _positionBoutonSortir, Color.White);
                double multiplicateurNiveau;
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
                    multiplicateurNiveau = 1;
                else if (Jeu.difficulte == Jeu.NiveauDifficulte.Difficile)
                    multiplicateurNiveau = 2;
                else
                    multiplicateurNiveau = 2;
                double score = Math.Round((Jeu._nombreKill * Math.Round((Jeu._precision / Jeu._nombreCoup * 100), 0) + (multiplicateurNiveau * Jeu._vague) - Jeu._chrono),0);
                Game1._spriteBatch.DrawString(Message._police, "Score: " + score, new Vector2(1280 / 2 - 100, 300 ), Color.White);
                Game1._spriteBatch.DrawString(Message._police, "Temps survecu: " + Jeu.getChrono(), new Vector2(1280 / 2 - 100, 720 / 2 - 40), Color.White);
                Game1._spriteBatch.DrawString(Message._police, "Nombre de kills: " + Jeu._nombreKill, new Vector2(1280 / 2 - 100, 720 / 2 - 20), Color.White);
                Game1._spriteBatch.DrawString(Message._police, "Nombre de vagues: " + Jeu._vague, new Vector2(1280 / 2 - 100, 720 / 2 + 0), Color.White);
                Game1._spriteBatch.DrawString(Message._police, "Nombre de coups: " + Jeu._nombreCoup, new Vector2(1280 / 2 - 100, 720 / 2 + 20), Color.White);
                double precision = 0;
                if (Jeu._nombreCoup > 0) {
                    precision = Math.Round((Jeu._precision / Jeu._nombreCoup * 100), 0);
                    Game1._spriteBatch.DrawString(Message._police, "Precision: " + precision + " pourcent", new Vector2(1280 / 2 - 100, 720 / 2 + 40), Color.White);
                }
            }
            Game1._spriteBatch.End();
        }
    }
}
