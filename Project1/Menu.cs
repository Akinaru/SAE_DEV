using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Project1
{
    internal class Menu
    {

        public static Texture2D _textureFondEcran;
        public static Texture2D _texturePlayButton;
        public static Texture2D _textureControls;
        public static Texture2D _textureFacileButton;
        public static Texture2D _textureDifficileButton;

        public static Vector2 _positionPlayButton;
        public static Vector2 _positionFacileButton;
        public static Vector2 _positionDifficileButton;

        public static void Initialise()
        {
            _positionPlayButton = new Vector2(490, 300);
            _positionFacileButton = new Vector2(400, 500);
            _positionDifficileButton = new Vector2(Game1._screenWidth/2, 500);

        }

        public static void LoadContent(ContentManager Content)
        {
            _texturePlayButton = Content.Load<Texture2D>("Menu/play");
            _textureControls = Content.Load<Texture2D>("Menu/controls");
            _textureFondEcran = Content.Load<Texture2D>("Menu/background");
            _textureFacileButton = Content.Load<Texture2D>("Menu/facile");
            _textureDifficileButton = Content.Load<Texture2D>("Menu/difficile");
        }

        public static void Update(MouseState mouseState, ContentManager Content)
        {
            var mousePosition = new Point(mouseState.X, mouseState.Y);
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


            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (mousePosition.X >= _positionPlayButton.X &&
                        mousePosition.X <= _positionPlayButton.X + 300 &&
                        mousePosition.Y >= _positionPlayButton.Y &&
                        mousePosition.Y <= _positionPlayButton.Y + 100)
                {
                    Game1._gameStarted = true;
                    Game1._gameBegin = true;
                    Message.Display("Libere la ville des mechants !", "Fais vite... Je crois en toi !", 5);

                }
            }
            KeyboardState keyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Game1._gameStarted = true;
                Game1._gameBegin = true;
                Message.Display("Libere la ville des mechants !", "Fais vite... Je crois en toi !", 5);

            }
        }

        public static void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_textureFondEcran, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_texturePlayButton, _positionPlayButton, Color.White);
            _spriteBatch.Draw(_textureFacileButton, _positionFacileButton, Color.White);
            _spriteBatch.Draw(_textureDifficileButton, _positionDifficileButton, Color.White);
            _spriteBatch.Draw(_textureControls, new Vector2(340, 570), Color.White);
        }

    }
}
