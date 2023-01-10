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


        public static void LoadContent(ContentManager Content){
            _textureblurBackground = Content.Load<Texture2D>("Menu/blurBackground");

        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            if (Jeu._pause)
            {
                _spriteBatch.Draw(_textureblurBackground, new Vector2(0, 0), Color.White);
            }

        }

    }
}
