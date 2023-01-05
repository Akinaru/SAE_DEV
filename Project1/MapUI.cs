using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class MapUI
    {
        internal static void Draw(SpriteBatch _spriteBatch)
        {
            if (Game1._showUI)
            {
            _spriteBatch.Draw(Game1._textureMapUI, new Vector2(340, 60), Color.White);
                _spriteBatch.Draw(Game1._textureMapPerso, Game1._positionMapPersoUI, Color.White);
                for (int i = 0; i < Game1._listeMonstre.Count; i++)
                {
                    _spriteBatch.Draw(Game1._textureMapPerso, new Vector2((Game1._listeMonstre[i].Position.X / 1600 * 600) + 340 - 8, (Game1._listeMonstre[i].Position.Y / 1600 * 600) + 60 - 8), Color.Red);
                }
            }

        }
    }
}
