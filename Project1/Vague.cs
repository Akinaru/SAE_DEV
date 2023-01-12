using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Vague
    {
        private const int NOMBRE_MONSTRE_PLUS_FACILE = 6;
        private const int NOMBRE_MONSTRE_PLUS_DIFFICILE = 12;
        private const int NOMBRE_MONSTRE_PLUS_EXTREME = 18;

        private const int NOMBRE_FANTOME_PLUS_FACILE = 2;
        private const int NOMBRE_FANTOME_PLUS_DIFFICILE = 4;
        private const int NOMBRE_FANTOME_PLUS_EXTREME = 8;
        public static void NewVague(ContentManager Content)
        {
            //si il n'y a plus de monstre et de fantome et de boss
            if (Jeu._listeMonstre.Count == 0 && Jeu._listeFantome.Count == 0 && Jeu._listeBoss.Count == 0)
            {
                Jeu._vague += 1;
                Message.Display("Bravo ! Tu es a la vague " + Jeu._vague + ". ", "Les monstres arrivent!", 5);
                Perso._sonNewVague.Play(Jeu._volumeSon, 0, 0);

                //ajout de monstre
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
                    Jeu._nombreMonstre += NOMBRE_MONSTRE_PLUS_FACILE;
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Difficile)
                    Jeu._nombreMonstre += NOMBRE_MONSTRE_PLUS_DIFFICILE;
                else
                    Jeu._nombreMonstre += NOMBRE_MONSTRE_PLUS_EXTREME;
                for (int i = 0; i < Jeu._nombreMonstre; i++)
                    Jeu._listeMonstre.Add(new Monstre("monstreAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
                
                //ajout de fantome
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
                    Jeu._nombreFantome += NOMBRE_FANTOME_PLUS_FACILE;
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Difficile)
                    Jeu._nombreFantome += NOMBRE_FANTOME_PLUS_DIFFICILE;
                else
                    Jeu._nombreFantome += NOMBRE_FANTOME_PLUS_EXTREME;
                for (int i = 0; i < Jeu._nombreFantome; i++)
                    Jeu._listeFantome.Add(new Fantome("fantome.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
                
                //ajout de boss
                if(Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
                {
                    if(Jeu._vague % 5 == 0)
                        Jeu._listeBoss.Add(new Boss("bossAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
                }
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Difficile)
                {
                    if (Jeu._vague % 3 == 0)
                        Jeu._listeBoss.Add(new Boss("bossAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
                }
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Extreme)
                {
                    if (Jeu._vague % 2 == 0)
                        Jeu._listeBoss.Add(new Boss("bossAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
                }
            }

        }
    }
}
