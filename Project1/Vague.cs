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

        public static void NewVague(ContentManager Content)
        {
            if (Game1._listeMonstre.Count == 0 && Game1._listeFantome.Count == 0)
            {
                Perso._sonNewVague.Play(Game1._volumeSon, 0, 0);
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
                    Jeu._nombreMonstre += 6;
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Difficile)
                    Jeu._nombreMonstre += 12;
                else
                    Jeu._nombreMonstre += 18;
                Jeu._vague += 1;
                Message.Display("Bravo ! Tu es a la vague " + Jeu._vague + ". ", "Les monstres arrivent!", 5);
                for (int i = 0; i < Jeu._nombreMonstre; i++)
                    Game1._listeMonstre.Add(new Monstre("monstreAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
                    Jeu._nombreFantome += 2;
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Difficile)
                    Jeu._nombreFantome += 4;
                else
                    Jeu._nombreFantome += 8;
                for (int i = 0; i < Jeu._nombreFantome; i++)
                    Game1._listeFantome.Add(new Fantome("fantome.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
            }

        }
    }
}
