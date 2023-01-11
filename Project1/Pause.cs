using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Pause
    {
        public static Texture2D _textureblurBackground;
        public static Texture2D _textureBoutonPause;
        public static Texture2D _textureBoutonMenu;
        public static Texture2D _textureReprendre;
        public static Texture2D _textureRaccourciM;
        public static Texture2D _textureRaccourciR;

        public static Texture2D _texturePlusButton;
        public static Texture2D _textureMoinButton;

        public static Vector2 _positionBoutonPause;
        public static Vector2 _positionBoutonReprendre;
        public static Vector2 _positionBoutonMenu;
        public static Vector2 _positionRaccourciM;
        public static Vector2 _positionRaccourciR;
        public static Vector2 _positionBoutonSon;
        public static bool _boutonSon;


        public static SoundEffect _sonMenuBack;
        public static bool _sourisClick;

        public static void LoadContent(ContentManager Content){
            _sonMenuBack = Content.Load<SoundEffect>("Son/MenuBack");
            _sourisClick = false;
            _boutonSon = false;

            _textureblurBackground = Content.Load<Texture2D>("Menu/blurBackground");
            _textureBoutonMenu = Content.Load<Texture2D>("Menu/boutonMenu");
            _textureReprendre = Content.Load<Texture2D>("Menu/reprendre");
            _textureBoutonPause = Content.Load<Texture2D>("Menu/pause");
            _textureRaccourciM = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciM");
            _textureRaccourciR = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciR");
            _texturePlusButton = Content.Load<Texture2D>("Menu/plus");
            _textureMoinButton = Content.Load<Texture2D>("Menu/moins");

            _positionBoutonPause = new Vector2(1280 / 2 - 250, 720 / 2 - 300);
            _positionBoutonReprendre = new Vector2(1280 / 2 - 351, 420);
            _positionBoutonMenu = new Vector2(1280 / 2 - 150, 550);
            _positionRaccourciM = new Vector2(1280 / 2 - 9, 655);
            _positionRaccourciR = new Vector2(1280 / 2 - 9, 525);
            _positionBoutonSon = new Vector2(10, 10);
        }

        public static void Update(ContentManager Content)
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
            //hover reprendre
            if (mousePosition.X >= _positionBoutonReprendre.X &&
                mousePosition.X <= _positionBoutonReprendre.X + 702 &&
                mousePosition.Y >= _positionBoutonReprendre.Y &&
                mousePosition.Y <= _positionBoutonReprendre.Y + 100)
            {
                _textureReprendre = Content.Load<Texture2D>("Menu/reprendreHover");
            }
            else
            {
                _textureReprendre = Content.Load<Texture2D>("Menu/reprendre");
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
			{
                if (!_sourisClick)
                {
                    //clique pour menu

                    if (mousePosition.X >= _positionBoutonMenu.X &&
                                            mousePosition.X <= _positionBoutonMenu.X + 300 &&
                                            mousePosition.Y >= _positionBoutonMenu.Y &&
                                            mousePosition.Y <= _positionBoutonMenu.Y + 100)
                    {
                        _sonMenuBack.Play(Game1._volumeSon, 0, 0);
                        Game1.etat = Game1.Etats.BackMenu;
                    }

                    //clique pour reprendre
                    if (mousePosition.X >= _positionBoutonReprendre.X &&
                                            mousePosition.X <= _positionBoutonReprendre.X + 702 &&
                                            mousePosition.Y >= _positionBoutonReprendre.Y &&
                                            mousePosition.Y <= _positionBoutonReprendre.Y + 100)
                    {
                        Jeu._pause = false;
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
                if (_sourisClick)
                    _sourisClick = false;
                if (_boutonSon)
                    _boutonSon = false;
            }


                    //raccourci pour retourner au menu et pour reprendre
                    KeyboardState keyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                if (!Jeu._gameStarted)
                    Game1.Etat = Game1.Etats.BackMenu;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                if (!Jeu._gameStarted)
                    Jeu._pause = false;
            }
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            if (Jeu._pause)
            {
                _spriteBatch.Draw(_textureblurBackground, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(_textureBoutonPause, _positionBoutonPause, Color.White);
                _spriteBatch.Draw(_textureReprendre, _positionBoutonReprendre, Color.White);
                _spriteBatch.Draw(_textureBoutonMenu, _positionBoutonMenu, Color.White);
                _spriteBatch.Draw(_textureRaccourciM, _positionRaccourciM, Color.White);
                _spriteBatch.Draw(_textureRaccourciR, _positionRaccourciR, Color.White);
                if (Game1._volumeSon == 0)
                    Game1._spriteBatch.Draw(_textureMoinButton, _positionBoutonSon, Color.White);
                else
                    Game1._spriteBatch.Draw(_texturePlusButton, _positionBoutonSon, Color.White);
            }

        }

    }
}
