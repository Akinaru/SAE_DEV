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

        public static Texture2D _texturePlusButton;
        public static Texture2D _textureMoinButton;

        public static Texture2D _textureRaccourciEntree;
        public static Texture2D _textureRaccourciVolumeDown;
        public static Texture2D _textureRaccourciVolumeUp;
        public static Texture2D _textureRaccourciF;
        public static Texture2D _textureRaccourciD;

        public static Vector2 _positionPlayButton;
        public static Vector2 _positionFacileButton;
        public static Vector2 _positionDifficileButton;

        public static Vector2 _positionRaccourciEntree;
        public static Vector2 _positionRaccourciVolumeDown;
        public static Vector2 _positionRaccourciVolumeUp;
        public static Vector2 _positionRaccourciF;
        public static Vector2 _positionRaccourciD;



        private SoundEffect _sonJouer;
        private Song _musique;


        public Menu(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {
            _positionPlayButton = new Vector2(490, 300);
            _positionFacileButton = new Vector2(500, 400);
            _positionDifficileButton = new Vector2(500 + 104 + 24, 400);

            _positionRaccourciEntree = new Vector2(200, 310);
            _positionRaccourciVolumeDown = new Vector2(200, 320);
            _positionRaccourciVolumeUp = new Vector2(200, 330);
            _positionRaccourciF = new Vector2(200, 340);
            _positionRaccourciD = new Vector2(200, 350);

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

            _texturePlusButton = Content.Load<Texture2D>("Menu/plus");
            _textureMoinButton = Content.Load<Texture2D>("Menu/moin");

            _textureRaccourciEntree = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciEntree");
            _textureRaccourciVolumeDown = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciVolumeDown");
            _textureRaccourciVolumeUp = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciVolumeUp");
            _textureRaccourciD = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciDifficile");
            _textureRaccourciF = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciFacile");

            _sonJouer = Content.Load<SoundEffect>("Son/Accept");
            _musique = Content.Load<Song>("Son/MusiqueMenu");
            MediaPlayer.Volume = Game1._volumeSon;
            MediaPlayer.Play(_musique);
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


            if (mouseState.LeftButton == ButtonState.Pressed)
            {
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
                }
                //BOUTON DIFFICILE
                if (mousePosition.X >= _positionDifficileButton.X &&
                    mousePosition.X <= _positionDifficileButton.X + 104 &&
                    mousePosition.Y >= _positionDifficileButton.Y &&
                    mousePosition.Y <= _positionDifficileButton.Y + 32)
                {
                    Jeu.difficulte = Jeu.NiveauDifficulte.Difficile;
                }
                //BOUTON PLUS
                if (mousePosition.X >= 10 &&
                    mousePosition.X <= 10 + 50 &&
                    mousePosition.Y >= 10 &&
                    mousePosition.Y <= 10 + 50)
                {
                    if (Math.Round(Game1._volumeSon, 1) < 1)
                        Game1._volumeSon += 0.01f;
                }

                //BOUTON MOIN
                if (mousePosition.X >= 100 &&
                    mousePosition.X <= 100 + 50 &&
                    mousePosition.Y >= 10 &&
                    mousePosition.Y <= 10 + 50)
                {
                    if (Game1._volumeSon > 0)
                        Game1._volumeSon -= 0.01f;
                }
            }
            KeyboardState keyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (!Jeu._gameStarted)
                    gameStart();

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game.Exit();   
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

            Game1._spriteBatch.Draw(_textureControls, new Vector2(340, 570), Color.White);

            Game1._spriteBatch.Draw(_textureRaccourciEntree, _positionRaccourciEntree, Color.White);
            Game1._spriteBatch.Draw(_textureRaccourciVolumeDown, _positionRaccourciVolumeDown, Color.White);
            Game1._spriteBatch.Draw(_textureRaccourciVolumeUp, _positionRaccourciVolumeUp, Color.White);
            Game1._spriteBatch.Draw(_textureRaccourciD, _positionRaccourciD, Color.White);
            Game1._spriteBatch.Draw(_textureRaccourciF, _positionRaccourciF, Color.White);


            //Game1._spriteBatch.Draw(_texturePlusButton, new Vector2(10, 10), Color.White);
            //Game1._spriteBatch.DrawString(Message._police, ""+Math.Round((Game1._volumeSon*100),0), new Vector2(80, 30), Color.White);
            //Game1._spriteBatch.Draw(_textureMoinButton, new Vector2(100, 10), Color.White);
            Game1._spriteBatch.End();
        }
    }
}
