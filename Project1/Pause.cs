using Microsoft.Xna.Framework;
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
        public static Texture2D _textureRaccourciM;
        public static Texture2D _textureRaccourciR;

        public static Vector2 _positionBoutonPause;
        public static Vector2 _positionBoutonReprendre;
        public static Vector2 _positionBoutonMenu;
        public static Vector2 _positionRaccourciM;
        public static Vector2 _positionRaccourciR;

        public static void LoadContent(ContentManager Content){
            _textureblurBackground = Content.Load<Texture2D>("Menu/blurBackground");
            _textureBoutonMenu = Content.Load<Texture2D>("Menu/boutonMenu");
            _textureBoutonPause = Content.Load<Texture2D>("Menu/pause");
            _textureRaccourciM = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciM");
            _textureRaccourciR = Content.Load<Texture2D>("Menu/raccourciTouche/raccourciM");

            _positionBoutonPause = new Vector2(1280 / 2 - 250, 720 / 2 - 300);
            _positionBoutonReprendre = new Vector2(1280 / 2 - 350, 720 / 2 + 130);
            _positionBoutonMenu = new Vector2(1280 / 2 - 50 + 100, 720 / 2 + 130);
            _positionRaccourciM = new Vector2(1280 / 2 - 50 + 100, 720 / 2 + 230);
            _positionRaccourciR = new Vector2(1280 / 2 - 350 + 100, 720 / 2 + 230);
        }

        public static void Update(ContentManager Content)
        {
            MouseState mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            //hover play
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

            KeyboardState keyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                if (!Jeu._gameStarted)
                    Game1.Etat = Game1.Etats.BackMenu;
            }
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            if (Jeu._pause)
            {
                _spriteBatch.Draw(_textureblurBackground, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(_textureBoutonPause, _positionBoutonPause, Color.White);
                _spriteBatch.Draw(_textureBoutonMenu, _positionBoutonReprendre, Color.White);
                _spriteBatch.Draw(_textureBoutonMenu, _positionBoutonMenu, Color.White);
                _spriteBatch.Draw(_textureRaccourciM, _positionRaccourciM + new Vector2(150- 9,0), Color.White);
                _spriteBatch.Draw(_textureRaccourciR, _positionRaccourciR, Color.White);
            }

        }

    }
}
