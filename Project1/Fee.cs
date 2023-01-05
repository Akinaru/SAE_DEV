using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Fee
    {

        public static Texture2D _textureFee;
        public static Vector2 _positionFee;
        private static float _angle;

        public static void Initialise()
        {
            _positionFee = Perso._positionPerso + new Vector2(-32,-32);
            _angle = 0;
        }

        public static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            _textureFee = Content.Load<Texture2D>("fee");
        }

        public static void Update()
        {
            _angle += 0.05f;
            _positionFee = new Vector2(5 * (float) Math.Cos(_angle) + Perso._positionPerso.X - 16, 5* (float)Math.Sin(_angle) + Perso._positionPerso.Y - 16);
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_textureFee, _positionFee, Color.White); 
        }

    }
}
