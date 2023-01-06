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
            Vector2 pos = new Vector2(1050,15);
            _spriteBatch.Draw(Game1._textureMonstreUI, pos, Color.White);
            _spriteBatch.DrawString(Message._police, Game1._listeMonstre.Count + " monstres", pos + new Vector2(55, 15), Color.White);

            _spriteBatch.Draw(Game1._texturePersoUI, pos + new Vector2(0, 43), Color.White);
            _spriteBatch.DrawString(Message._police, "0 kills", pos + new Vector2(55, 58), Color.White);

            _spriteBatch.DrawString(Message._police, ""+Zone._zone, pos + new Vector2(55, 91), Color.White);
        }


    }
}
