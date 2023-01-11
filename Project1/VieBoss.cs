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
    internal class VieBoss
    {

        public static Texture2D _texturevieCoeurPlein;
        public static Texture2D _texturevieCoeurVide;

        private const int VIE_MAX_FACILE_DIFFICILE = 10;


        public static void LoadContent(ContentManager Content)
        {
            _texturevieCoeurPlein = Content.Load<Texture2D>("vieBossFull");
            _texturevieCoeurVide = Content.Load<Texture2D>("vieBossVide");
        }

        public static void Update()
        {
            if (Perso._viePerso == 0)
            {
                Perso._mort = true;
                Game1.Etat = Game1.Etats.GameOver;
            }

        }

        public static void Draw(SpriteBatch _spriteBatch)
        {

            Vector2 pos = new Vector2(Game1._largeurEcran/2 - 200, 10);
            
            //affichage coeur plein
            for (int y = 0; y < Game1._listeBoss.Count; y++)
            {
                for (int i = 0; i < Game1._listeBoss[y].Vie; i++)
                {
                    _spriteBatch.Draw(_texturevieCoeurPlein, new Vector2(pos.X + 10 + (40 * i), pos.Y + 10 + (45*y)), Color.White);
                }
            }

            //afichage coeur vide
            for (int y = 0; y < Game1._listeBoss.Count; y++)
            {
                for (int i = 0; i < VIE_MAX_FACILE_DIFFICILE - Game1._listeBoss[y].Vie; i++)
                {
                    _spriteBatch.Draw(_texturevieCoeurVide, new Vector2(pos.X + 10 + (float)(40 * Game1._listeBoss[y].Vie) + 40 * i, pos.Y + 10 + (45 * y)), Color.White);
                }
            }
        }
    }
}
