﻿using Microsoft.Xna.Framework;
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

        public static void Initialise()
        {

        }

        public static void LoadContent(ContentManager Content)
        {
            _texturevieCoeurPlein = Content.Load<Texture2D>("Perso/coeur");
            _texturevieCoeurVide = Content.Load<Texture2D>("Perso/coeurvide");
        }

        public static void Update()
        {
            if(ScreenJeu._viePerso == 0)
            {
                Game1.Etat = Game1.Etats.GameOver;
            }
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            Vector2 pos = Perso._positionPerso + new Vector2(-25, -25);
            for (int i = 0; i < ScreenJeu._viePerso; i++)
            {
                _spriteBatch.Draw(_texturevieCoeurPlein, new Vector2(pos.X + 10 + (5 * i), pos.Y + 10), Color.White);
            }
            for (int i = 0; i < 6 - ScreenJeu._viePerso; i++)
            {
                _spriteBatch.Draw(_texturevieCoeurVide, new Vector2(pos.X + 10 + (float)(5 * ScreenJeu._viePerso) + 5 * i, pos.Y + 10), Color.White);
            }
        }

    }
}
