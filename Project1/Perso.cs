using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Perso
    {
        public static Vector2 _positionPerso;
        public static AnimatedSprite _perso;
        public static int _vitessePerso;
        public static string _animation;
        public static bool _touche;
        public static float _wait;
        public static Texture2D _textureBouclier;


        public static void Initialise()
        {
            _vitessePerso = 100;
            _positionPerso = new Vector2(130, 146);
            _touche = false;
            _wait = 0;

        }

        public static void LoadContent(SpriteSheet spriteSheet, ContentManager Content)
        {
            _perso = new AnimatedSprite(spriteSheet);
            _textureBouclier = Content.Load<Texture2D>("boucllier");

        }

        public static void Update()
        {
            _animation = "idle";

        }

        public static void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {


            _spriteBatch.Draw(Game1._textureombrePerso, _positionPerso + new Vector2(-16, -13), Color.White);
            _spriteBatch.Draw(_perso, _positionPerso);
            if (_wait > 0)
            {
                _spriteBatch.Draw(_textureBouclier, _positionPerso + new Vector2(-10, -10), Color.White);
            }
        }

    }
}
