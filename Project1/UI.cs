using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class UI
    {
        public static Texture2D _texturevieCoeurPlein;
        public static Texture2D _texturevieCoeurVide;

        public static void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < Game1._viePerso; i++)
            {
                _spriteBatch.Draw(_texturevieCoeurPlein, new Vector2(10 + (65 * i), 10), Color.White);
            }
            for (int i = 0; i < 6 - Game1._viePerso; i++)
            {
                _spriteBatch.Draw(_texturevieCoeurVide, new Vector2(10+ (float)(65 * Game1._viePerso) + 65 * i, 10), Color.White);
            }
            _spriteBatch.Draw(Game1._textureMonstreUI, new Vector2(1125,15), Color.White);
            _spriteBatch.DrawString(Game1._police, Game1._listeMonstre.Count + " monstres", new Vector2(1180, 30), Color.White);

            _spriteBatch.Draw(Game1._texturePersoUI, new Vector2(1125, 58), Color.White);
            _spriteBatch.DrawString(Game1._police, "0 kills", new Vector2(1180, 73), Color.White);
        }

        internal static void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            _texturevieCoeurPlein = Content.Load<Texture2D>("coeur");
            _texturevieCoeurVide = Content.Load<Texture2D>("coeurvide");
        }
    }
}
