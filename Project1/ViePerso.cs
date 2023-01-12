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
    internal class ViePerso
    {

        public static Texture2D _texturevieCoeurPlein;
        public static Texture2D _texturevieCoeurVide;

        private const int VIE_MAX_FACILE_DIFFICILE = 6;
        private const int VIE_MAX_EXTREME = 1;


        public static void LoadContent(ContentManager Content)
        {
            _texturevieCoeurPlein = Content.Load<Texture2D>("Perso/coeur");
            _texturevieCoeurVide = Content.Load<Texture2D>("Perso/coeurvide");
        }

        public static void Update()
        {
            if(Perso._viePerso <= 0)
            {
                Perso._mort = true;
                Game1.Etat = Game1.Etats.GameOver;
            }
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {

            Vector2 pos = Perso._positionPerso + new Vector2(-25, -25);
            int vieMax;

            if (Jeu.difficulte != Jeu.NiveauDifficulte.Extreme)
                vieMax = VIE_MAX_FACILE_DIFFICILE;
            else
            {
                vieMax = VIE_MAX_EXTREME;
                pos = Perso._positionPerso + new Vector2(-12, -25);
            }
                

            //affichage coeur plein
            for (int i = 0; i < Perso._viePerso; i++)
                _spriteBatch.Draw(_texturevieCoeurPlein, new Vector2(pos.X + 10 + (5 * i), pos.Y + 10), Color.White);
            //afichage coeur vide

            
            for (int i = 0; i < vieMax - Perso._viePerso; i++)
                _spriteBatch.Draw(_texturevieCoeurVide, new Vector2(pos.X + 10 + (float)(5 * Perso._viePerso) + 5 * i, pos.Y + 10), Color.White);
        }

    }
}
