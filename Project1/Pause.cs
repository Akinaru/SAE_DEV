using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        public static Texture2D _textureBoutonMenu;

        public static Vector2 _positionBoutonReprendre;
        public static Vector2 _positionBoutonMenu;

        public static void LoadContent(ContentManager Content){
            _textureblurBackground = Content.Load<Texture2D>("Menu/blurBackground");
            _textureBoutonMenu = Content.Load<Texture2D>("Menu/boutonMenu");
            _positionBoutonReprendre = new Vector2(1280 / 2 - 350, 720 / 2 + 130);
            _positionBoutonMenu = new Vector2(1280 / 2 - 50 + 100, 720 / 2 + 130);
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            if (Jeu._pause)
            {
                _spriteBatch.Draw(_textureblurBackground, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(_textureBoutonMenu, _positionBoutonReprendre, Color.White);
                _spriteBatch.Draw(_textureBoutonMenu, _positionBoutonMenu, Color.White);
            }

        }

    }
}
