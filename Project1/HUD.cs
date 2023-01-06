using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class HUD
    {

        internal static void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {

        }
        public static void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(Game1._textureMonstreUI, new Vector2(1125,15), Color.White);
            _spriteBatch.DrawString(Message._police, Game1._listeMonstre.Count + " monstres", new Vector2(1180, 30), Color.White);

            _spriteBatch.Draw(Game1._texturePersoUI, new Vector2(1125, 58), Color.White);
            _spriteBatch.DrawString(Message._police, "0 kills", new Vector2(1180, 73), Color.White);
        }


    }
}
