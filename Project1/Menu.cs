using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Menu : GameScreen
    {
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        public static Texture2D _textureFondEcran;
        public static Texture2D _texturePlayButton;
        public static Texture2D _textureControls;

        public static Texture2D _textureFacileButton;
        public static Texture2D _textureDifficileButton;
        public static Texture2D _textureExtremeButton;

        public static Texture2D _texturePlusButton;
        public static Texture2D _textureMoinButton;

        public static Texture2D _textureBoutonSortir;

        public static Texture2D _textureRaccourciEntree;
        public static Texture2D _textureRaccourciF;
        public static Texture2D _textureRaccourciD;
        public static Texture2D _textureRaccourciE;
        public static Texture2D _textureRaccourciS;
        public static Texture2D _textureRaccourciFleche;

        public static Vector2 _positionPlayButton;
        public static Vector2 _positionFacileButton;
        public static Vector2 _positionDifficileButton;
        public static Vector2 _positionExtremeButton;

        public static Vector2 _positionRaccourciEntree;
        public static Vector2 _positionRaccourciF;
        public static Vector2 _positionRaccourciD;
        public static Vector2 _positionRaccourciE;
        public static Vector2 _positionRaccourciS;
        public static Vector2 _positionRaccourciFleche;

        public static Vector2 _positionBoutonSon;
        public static Vector2 _positionBoutonSortir;

        private bool _sourisClick;
        private bool _boutonSon;
        private SoundEffect _sonJouer;
        private SoundEffect _sonDifficulte;


        public Menu(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {
            Jeu.difficulte = Jeu.NiveauDifficulte.Facile;
            _positionPlayButton = new Vector2(490, 300);
            _positionRaccourciEntree = new Vector2(611, 392);

            _positionFacileButton = new Vector2(500, 440);
            _positionDifficileButton = new Vector2(628, 440);
            _positionExtremeButton = new Vector2(547, 500);

            _positionRaccourciF = new Vector2(543, 470);
            _positionRaccourciD = new Vector2(695, 470);
            _positionRaccourciE = new Vector2(631, 532);
            _positionRaccourciS = new Vector2(1234, 700);
            _positionRaccourciFleche = new Vector2(24, 70);

            _positionBoutonSon = new Vector2(10, 10);
            _positionBoutonSortir = new Vector2(1220, 650);
            _boutonSon = false;
            _sourisClick = false;
            base.Initialize();
        }
        public override void LoadContent()
        {
            Message.LoadContent(Content);
            _texturePlayButton = Content.Load<Texture2D>("Menu/play");
            _textureControls = Content.Load<Texture2D>("Menu/controls");
            _textureFondEcran = Content.Load<Texture2D>("Menu/background");

            _textureFacileButton = Content.Load<Texture2D>("Menu/facile");
            _textureDifficileButton = Content.Load<Texture2D>("Menu/difficile");
            _textureExtremeButton = Content.Load<Texture2D>("Menu/extreme");

            _texturePlusButton = Content.Load<Texture2D>("Menu/plus");
            _textureMoinButton = Content.Load<Texture2D>("Menu/moins");

            _textureBoutonSortir = Content.Load<Texture2D>("Menu/sortir");

            _textureRaccourciEntree = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciEntree");
            _textureRaccourciD = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciDifficile");
            _textureRaccourciF = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciFacile");
            _textureRaccourciE = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciE");
            _textureRaccourciS = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciS");
            _textureRaccourciFleche = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciVolumeUp");

            _sonJouer = Content.Load<SoundEffect>("Son/Accept");
            _sonDifficulte = Content.Load<SoundEffect>("Son/Difficulte");

            MediaPlayer.Volume = Game1._volumeSon;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            //hover play
            if (mousePosition.X >= _positionPlayButton.X &&
                mousePosition.X <= _positionPlayButton.X + 300 &&
                mousePosition.Y >= _positionPlayButton.Y &&
                mousePosition.Y <= _positionPlayButton.Y + 100)
            {
                _texturePlayButton = Content.Load<Texture2D>("Menu/playHover");
            }
            else
            {
                _texturePlayButton = Content.Load<Texture2D>("Menu/play");
            }


            //hover facile
            if (Jeu.difficulte != Jeu.NiveauDifficulte.Facile)
            {
                if (mousePosition.X >= _positionFacileButton.X &&
                    mousePosition.X <= _positionFacileButton.X + 104 &&
                    mousePosition.Y >= _positionFacileButton.Y &&
                    mousePosition.Y <= _positionFacileButton.Y + 32)
                {
                    _textureFacileButton = Content.Load<Texture2D>("Menu/facileHover");
                }
                else
                {
                    _textureFacileButton = Content.Load<Texture2D>("Menu/facile");
                }
            }
            else
            {
                _textureFacileButton = Content.Load<Texture2D>("menu/facileSouligne");
            }

            //hover difficile
            if (Jeu.difficulte != Jeu.NiveauDifficulte.Difficile)
            {
                if (mousePosition.X >= _positionDifficileButton.X &&
                    mousePosition.X <= _positionDifficileButton.X + 104 &&
                    mousePosition.Y >= _positionDifficileButton.Y &&
                    mousePosition.Y <= _positionDifficileButton.Y + 32)
                {
                    _textureDifficileButton = Content.Load<Texture2D>("Menu/difficileHover");
                }
                else
                {
                    _textureDifficileButton = Content.Load<Texture2D>("Menu/difficile");
                }
            }
            else
            {
                _textureDifficileButton = Content.Load<Texture2D>("menu/difficileSouligne");
            }

			//hover extreme
			if (Jeu.difficulte != Jeu.NiveauDifficulte.Extreme)
			{
				if (mousePosition.X >= _positionExtremeButton.X &&
					mousePosition.X <= _positionExtremeButton.X + 186 &&
					mousePosition.Y >= _positionExtremeButton.Y &&
					mousePosition.Y <= _positionExtremeButton.Y + 32)
				{
					_textureExtremeButton = Content.Load<Texture2D>("Menu/extremeHover");
				}
				else
				{
                    _textureExtremeButton = Content.Load<Texture2D>("Menu/extreme");
				}
			}
			else
			{
                _textureExtremeButton = Content.Load<Texture2D>("menu/extremeSouligne");
			}

            //hover moins volume
            if (Game1._volumeSon == 0)
            {
                if (mousePosition.X >= _positionBoutonSon.X &&
                    mousePosition.X <= _positionBoutonSon.X + 50 &&
                    mousePosition.Y >= _positionBoutonSon.Y &&
                    mousePosition.Y <= _positionBoutonSon.Y + 50)
                {
                    _textureMoinButton = Content.Load<Texture2D>("Menu/moinsHover");
                }
                else
                {
                    _textureMoinButton = Content.Load<Texture2D>("Menu/moins");
                }
            }

            //hover plus volume
            if (Game1._volumeSon != 0)
            {
                if (mousePosition.X >= _positionBoutonSon.X &&
                    mousePosition.X <= _positionBoutonSon.X + 50 &&
                    mousePosition.Y >= _positionBoutonSon.Y &&
                    mousePosition.Y <= _positionBoutonSon.Y + 50)
                {
                    _texturePlusButton = Content.Load<Texture2D>("Menu/plusHover");
                }
                else
                {
                    _texturePlusButton = Content.Load<Texture2D>("Menu/plus");
                }
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


            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!_sourisClick) {
                    //BOUTON SORTIR
                    if (mousePosition.X >= _positionBoutonSortir.X &&
                        mousePosition.X <= _positionBoutonSortir.X + 50 &&
                        mousePosition.Y >= _positionBoutonSortir.Y &&
                        mousePosition.Y <= _positionBoutonSortir.Y + 50)
                    {
                        //euizhfuehzuifhzeuihzfi
                    }
                    //BOUTON JEU
                    if (mousePosition.X >= _positionPlayButton.X &&
                            mousePosition.X <= _positionPlayButton.X + 300 &&
                            mousePosition.Y >= _positionPlayButton.Y &&
                            mousePosition.Y <= _positionPlayButton.Y + 100)
                    {
                        if (!Jeu._gameStarted)
                            gameStart();

                    }
                    //BOUTON FACILE
                    if (mousePosition.X >= _positionFacileButton.X &&
                        mousePosition.X <= _positionFacileButton.X + 104 &&
                        mousePosition.Y >= _positionFacileButton.Y &&
                        mousePosition.Y <= _positionFacileButton.Y + 32)
                    {
                        Jeu.difficulte = Jeu.NiveauDifficulte.Facile;
                        _sonDifficulte.Play(Game1._volumeSon, 0, 0);
                    }
                    //BOUTON DIFFICILE
                    if (mousePosition.X >= _positionDifficileButton.X &&
                        mousePosition.X <= _positionDifficileButton.X + 104 &&
                        mousePosition.Y >= _positionDifficileButton.Y &&
                        mousePosition.Y <= _positionDifficileButton.Y + 32)
                    {
                        Jeu.difficulte = Jeu.NiveauDifficulte.Difficile;
                        _sonDifficulte.Play(Game1._volumeSon, 0, 0);
                    }
                    //BOUTON EXTREME
                    if (mousePosition.X >= _positionExtremeButton.X &&
                        mousePosition.X <= _positionExtremeButton.X + 186 &&
                        mousePosition.Y >= _positionExtremeButton.Y &&
                        mousePosition.Y <= _positionExtremeButton.Y + 32)
                    {
                        Jeu.difficulte = Jeu.NiveauDifficulte.Extreme;
                        _sonDifficulte.Play(Game1._volumeSon, 0, 0);
                    }
                    //BOUTON SON
                    if (mousePosition.X >= _positionBoutonSon.X &&
                        mousePosition.X <= _positionBoutonSon.X + 50 &&
                        mousePosition.Y >= _positionBoutonSon.Y &&
                        mousePosition.Y <= _positionBoutonSon.Y + 50)
                    {
                        if (!_boutonSon)
                        {
                            if (Game1._volumeSon == 0)
                                Game1._volumeSon += 0.5f;
                            else
                                Game1._volumeSon -= 0.5f;
                            _boutonSon = true;
                        }
                    }
                    _sourisClick = true;

                }

            }
            if (mouseState.LeftButton == ButtonState.Released)
            {
                if (_boutonSon)
                    _boutonSon = false;
                if (_sourisClick)
                    _sourisClick = false;
            }
                KeyboardState keyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (!Jeu._gameStarted)
                    gameStart();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                Jeu.difficulte = Jeu.NiveauDifficulte.Facile;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Jeu.difficulte = Jeu.NiveauDifficulte.Difficile;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Jeu.difficulte = Jeu.NiveauDifficulte.Extreme;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (Game1._volumeSon == 0.5f)
				{
                    _textureRaccourciFleche = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciVolumeUp");
                    Game1._volumeSon -= 0.5f;


                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (Game1._volumeSon == 0)
				{
                    _textureRaccourciFleche = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciVolumeDown");
                    Game1._volumeSon += 0.5f;
                }

            }
        }

        public void gameStart()
        {
            Jeu._gameStarted = true;
            Jeu._gameBegin = true;
            Game1.Etat = Game1.Etats.Play;
            _sonJouer.Play(Game1._volumeSon, 0, 0);
        }
        public override void Draw(GameTime gameTime)
        {
            Game1._spriteBatch.Begin();

            Game1._spriteBatch.Draw(_textureFondEcran, new Vector2(0, 0), Color.White);
            Game1._spriteBatch.Draw(_texturePlayButton, _positionPlayButton, Color.White);

            Game1._spriteBatch.Draw(_textureFacileButton, _positionFacileButton, Color.White);
            Game1._spriteBatch.Draw(_textureDifficileButton, _positionDifficileButton, Color.White);
            Game1._spriteBatch.Draw(_textureExtremeButton, _positionExtremeButton, Color.White);

            Game1._spriteBatch.Draw(_textureControls, new Vector2(340, 570), Color.White);

            Game1._spriteBatch.Draw(_textureRaccourciEntree, _positionRaccourciEntree, Color.White);
            Game1._spriteBatch.Draw(_textureRaccourciFleche, _positionRaccourciFleche, Color.White);

            Game1._spriteBatch.Draw(_textureBoutonSortir, _positionBoutonSortir, Color.White);
            Game1._spriteBatch.Draw(_textureRaccourciS, _positionRaccourciS, Color.White);



            if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
			{
                Game1._spriteBatch.Draw(_textureRaccourciD, _positionRaccourciD, Color.White);
                Game1._spriteBatch.Draw(_textureRaccourciE, _positionRaccourciE, Color.White);
            }
			else if (Jeu.difficulte == Jeu.NiveauDifficulte.Difficile)
			{
                Game1._spriteBatch.Draw(_textureRaccourciE, _positionRaccourciE, Color.White);
                Game1._spriteBatch.Draw(_textureRaccourciF, _positionRaccourciF, Color.White);
            }
            else
			{
                Game1._spriteBatch.Draw(_textureRaccourciF, _positionRaccourciF, Color.White);
                Game1._spriteBatch.Draw(_textureRaccourciD, _positionRaccourciD, Color.White);
            }


            if (Game1._volumeSon == 0)
			{
                Game1._spriteBatch.Draw(_textureMoinButton, _positionBoutonSon, Color.White);
            }
            else
			{
                Game1._spriteBatch.Draw(_texturePlusButton, _positionBoutonSon, Color.White);
                
            }

            Game1._spriteBatch.End();
        }
    }
}
