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
        public static Texture2D _textureMapFantome;
        public static Texture2D _textureMapBoss;
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
            //chargement des textures liés à la minimap
            _textureMapPerso = Content.Load<Texture2D>("MapUI/mapPerso");
            _textureMapMonstre = Content.Load<Texture2D>("MapUI/mapMonstre");
            _textureMapFantome = Content.Load<Texture2D>("MapUI/mapFantome");
            _textureMapBoss = Content.Load<Texture2D>("MapUI/mapBoss");
            _textureMapUI = Content.Load<Texture2D>("MapUI/map");
            _textureCoeurUI = Content.Load<Texture2D>("Perso/coeurDrop");

        }

        internal static void Draw(SpriteBatch _spriteBatch)
        {
            //on affiche la minimap que si la variable _showUI est true
            //la variable est true quand la touche tab est préssé
            if (_showUI)
            {
                _spriteBatch.Draw(_textureMapUI, new Vector2(340, 60), Color.White);

                //on cache les monstres  sur la minimap si la difficulte est à extreme et que le nombre de monstre est plus grand que 10
                //donc si en extreme le nombre de monstre arrive a 10 ou moins, les monstres apparaissent sur la mini map
                if (Jeu.difficulte != Jeu.NiveauDifficulte.Extreme ||
                    (Jeu.difficulte == Jeu.NiveauDifficulte.Extreme && Jeu._listeMonstre.Count <= 10))
                {
                    //affichage monstre sur la carte
                    for (int i = 0; i < Jeu._listeMonstre.Count; i++)
                    {
                        _spriteBatch.Draw(_textureMapMonstre, new Vector2((Jeu._listeMonstre[i].Position.X / 1600 * 600) + 340 - 8, (Jeu._listeMonstre[i].Position.Y / 1600 * 600) + 60 - 8), Color.Red);
                    }
                    //affichage fantome sur la carte
                    for (int i = 0; i < Jeu._listeFantome.Count; i++)
                    {
                        _spriteBatch.Draw(_textureMapFantome, new Vector2((Jeu._listeFantome[i].Position.X / 1600 * 600) + 340 - 8, (Jeu._listeFantome[i].Position.Y / 1600 * 600) + 60 - 8), Color.Cyan);
                    }
                    //affiche le boss
                    for (int i = 0; i < Jeu._listeBoss.Count; i++)
                    {
                        _spriteBatch.Draw(_textureMapBoss, new Vector2((Jeu._listeBoss[i].Position.X / 1600 * 600) + 340 - 8, (Jeu._listeBoss[i].Position.Y / 1600 * 600) + 60 - 8), Color.White);
                    }
                }

                //affichage coeurs sur la carte
                for (int i = 0; i < Jeu._listeCoeur.Count; i++)
                {
                    _spriteBatch.Draw(_textureCoeurUI, new Vector2((Jeu._listeCoeur[i].Position.X / 1600 * 600) + 340 - 8, (Jeu._listeCoeur[i].Position.Y / 1600 * 600) + 60 - 8), Color.White);
                }
                //affichage perso sur la carte
                _spriteBatch.Draw(_textureMapPerso, new Vector2((Perso._positionPerso.X / 1600 * 600) + 340 - 8, (Perso._positionPerso.Y / 1600 * 600) + 60 - 8), Color.White);

            }

        }
    }
}
