using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Vie
    {

        public static void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < Game1._viePerso; i++)
            {
                _spriteBatch.Draw(Game1._texturevieCoeurPlein, new Vector2(10 + (65 * i), 10), Color.White);
            }
            for (int i = 0; i < 6 - Game1._viePerso; i++)
            {
                _spriteBatch.Draw(Game1._texturevieCoeurVide, new Vector2(10+ (float)(65 * Game1._viePerso) + 65 * i, 10), Color.White);
            }
        }

    }
}
