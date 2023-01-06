using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class ScreenMenu : GameScreen
    {
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        private Game1 _myGame;
        public static Texture2D _textureFondEcran;
        public static Texture2D _texturePlayButton;
        public static Texture2D _textureControls;
        public static Texture2D _textureFacileButton;
        public static Texture2D _textureDifficileButton;

        public static Vector2 _positionPlayButton;
        public static Vector2 _positionFacileButton;
        public static Vector2 _positionDifficileButton;


        public ScreenMenu(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {
            _positionPlayButton = new Vector2(490, 300);
            _positionFacileButton = new Vector2(400, 500);
            _positionDifficileButton = new Vector2(Game1._screenWidth / 2, 500);
            base.Initialize();
        }
        public override void LoadContent()
        {
            _texturePlayButton = Content.Load<Texture2D>("Menu/play");
            _textureControls = Content.Load<Texture2D>("Menu/controls");
            _textureFondEcran = Content.Load<Texture2D>("Menu/background");
            _textureFacileButton = Content.Load<Texture2D>("Menu/facile");
            _textureDifficileButton = Content.Load<Texture2D>("Menu/difficile");
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

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
                    ScreenJeu._gameStarted = true;
                    ScreenJeu._gameBegin = true;
                    Game1.Etat = Game1.Etats.Play;
                    Message.Display("Libere la ville des monstres !", "Fais vite... Je crois en toi !", 5);

                }
            }
            KeyboardState keyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                ScreenJeu._gameStarted = true;
                ScreenJeu._gameBegin = true;
                Game1.Etat = Game1.Etats.Play;
                Message.Display("Libere la ville des monstres !", "Fais vite... Je crois en toi !", 5);

            }
        }
        public override void Draw(GameTime gameTime)
        {
            Game1._spriteBatch.Begin();
            Game1._spriteBatch.Draw(_textureFondEcran, new Vector2(0, 0), Color.White);
            Game1._spriteBatch.Draw(_texturePlayButton, _positionPlayButton, Color.White);
            Game1._spriteBatch.Draw(_textureFacileButton, _positionFacileButton, Color.White);
            Game1._spriteBatch.Draw(_textureDifficileButton, _positionDifficileButton, Color.White);
            Game1._spriteBatch.Draw(_textureControls, new Vector2(340, 570), Color.White);
            Game1._spriteBatch.End();
        }
    }
}
