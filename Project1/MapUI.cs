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
        public static Texture2D _textureMapPerso;
        public static Texture2D _textureMapMonstre;
        public static Texture2D _textureMapUI;
        public static Texture2D _textureCoeurUI;
        public static Vector2 _positionMapPersoUI;


        public static bool _showUI;

        public static void Initialise()
        {
            _showUI = false;

        }

        public static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            _textureMapPerso = Content.Load<Texture2D>("MapUI/mapPerso");
            _textureMapMonstre = Content.Load<Texture2D>("MapUI/mapMonstre");
            _textureMapUI = Content.Load<Texture2D>("MapUI/map");
            _textureCoeurUI = Content.Load<Texture2D>("Perso/coeurDrop");

        }

        internal static void Draw(SpriteBatch _spriteBatch)
        {
            if (_showUI)
            {
            _spriteBatch.Draw(_textureMapUI, new Vector2(340, 60), Color.White);
				if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
				{
                    for (int i = 0; i < Game1._listeMonstre.Count; i++)
                    {
                        _spriteBatch.Draw(_textureMapMonstre, new Vector2((Game1._listeMonstre[i].Position.X / 1600 * 600) + 340 - 8, (Game1._listeMonstre[i].Position.Y / 1600 * 600) + 60 - 8), Color.Red);
                    }
                }
                
                for (int i = 0; i < Jeu._listeCoeur.Count; i++)
                {
                    _spriteBatch.Draw(_textureCoeurUI, new Vector2((Jeu._listeCoeur[i].Position.X / 1600 * 600) + 340 - 8, (Jeu._listeCoeur[i].Position.Y / 1600 * 600) + 60 - 8), Color.White);
                }
                _spriteBatch.Draw(_textureMapPerso, _positionMapPersoUI, Color.White);

            }

        }

        internal static void Update()
        {
            if (_showUI)
                _positionMapPersoUI = new Vector2((Perso._positionPerso.X / 1600 * 600) + 340 - 8, (Perso._positionPerso.Y / 1600 * 600) + 60 - 8);

        }
    }
}
