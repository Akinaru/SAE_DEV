using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class HUD
    {
        public static Texture2D _textureMonstreHUD;
        public static Texture2D _textureFantomeHUD;
        public static Texture2D _texturePersoHUD;
        public static Texture2D _textureLocHUD;
        public static Texture2D _textureVagueHUD;
        public static Texture2D _textureChronoHUD;
        public static Texture2D _textureBouclierHUD;

        public static Texture2D _textureBackgroundHUD;

        public static Vector2 _positionBaseImage;
        public static Vector2 _positionBaseTexte;

        internal static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {

            _textureMonstreHUD = Content.Load<Texture2D>("HUD/monstreHUD");
            _textureFantomeHUD = Content.Load<Texture2D>("HUD/fantomeHUD");
            _texturePersoHUD = Content.Load<Texture2D>("HUD/persoHUD");
            _textureLocHUD = Content.Load<Texture2D>("HUD/locHUD");
            _textureVagueHUD = Content.Load<Texture2D>("HUD/vagueHUD");
            _textureChronoHUD = Content.Load<Texture2D>("HUD/chronoHUD");
            _textureBouclierHUD = Content.Load<Texture2D>("HUD/bouclierHUD");

            _textureBackgroundHUD = Content.Load<Texture2D>("HUD/backgroundHUD");

            _positionBaseImage = new Vector2(1050, 15);
            _positionBaseTexte = _positionBaseImage + new Vector2(55,15);
        }
        public static void Draw(SpriteBatch _spriteBatch)
        {
            if(Perso._mort)
            {
                return;
            }
            //monstre
            if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
            {
                _spriteBatch.Draw(_textureBackgroundHUD, _positionBaseImage - new Vector2(5,3), Color.White);
            }
            string pluriel = "";
            if (Jeu._nombreMonstre > 1)
                pluriel = "s";
            _spriteBatch.Draw(_textureMonstreHUD, _positionBaseImage, Color.White);
            _spriteBatch.DrawString(Message._police, Jeu._listeMonstre.Count + " monstre"+pluriel, _positionBaseTexte, Color.White);
            
            //fantome
            if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
            {
                _spriteBatch.Draw(_textureBackgroundHUD, _positionBaseImage - new Vector2(5, 3) + new Vector2(0, 45 * 1), Color.White);
            }
            pluriel = "";
            if (Jeu._nombreFantome > 1)
                pluriel = "s";
            _spriteBatch.Draw(_textureFantomeHUD, _positionBaseImage + new Vector2(0, 45 * 1), Color.White);
            _spriteBatch.DrawString(Message._police, Jeu._listeFantome.Count + " fantome"+pluriel, _positionBaseTexte + new Vector2(0, 45 * 1), Color.White);

            //kills
            if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
            {
                _spriteBatch.Draw(_textureBackgroundHUD, _positionBaseImage - new Vector2(5, 3) + new Vector2(0, 45 * 2), Color.White);
            }
            _spriteBatch.Draw(_texturePersoHUD, _positionBaseImage + new Vector2(0, 45 * 2), Color.White);
            _spriteBatch.DrawString(Message._police, Jeu._nombreKill+" kills", _positionBaseTexte + new Vector2(0, 45 * 2), Color.White);

            //zone
            if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
            {
                _spriteBatch.Draw(_textureBackgroundHUD, _positionBaseImage - new Vector2(5, 3) + new Vector2(0, 45 * 3), Color.White);
            }
            _spriteBatch.Draw(_textureLocHUD, _positionBaseImage + new Vector2(0, 45 * 3), Color.White);
            _spriteBatch.DrawString(Message._police, ""+Zone._zone, _positionBaseTexte + new Vector2(0, 45 * 3), Color.White);

            //vague
            if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
            {
                _spriteBatch.Draw(_textureBackgroundHUD, _positionBaseImage - new Vector2(5, 3) + new Vector2(0, 45 * 4), Color.White);
            }
            _spriteBatch.Draw(_textureVagueHUD, _positionBaseImage + new Vector2(0, 45 * 4), Color.White);
            _spriteBatch.DrawString(Message._police, "Vague "+Jeu._vague, _positionBaseTexte + new Vector2(0, 45 * 4), Color.White);

            //chrono
            if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
            {
                _spriteBatch.Draw(_textureBackgroundHUD, _positionBaseImage - new Vector2(5, 3) + new Vector2(0, 45 * 5), Color.White);
            }
            _spriteBatch.Draw(_textureChronoHUD, _positionBaseImage + new Vector2(0, 45 * 5), Color.White);
            _spriteBatch.DrawString(Message._police, "Chrono " + Jeu.getChrono(), _positionBaseTexte + new Vector2(0, 45 * 5), Color.White);


            //bouclier
            if (Perso._waitBouclier > 0)
            {
                if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
                {
                    _spriteBatch.Draw(_textureBackgroundHUD, _positionBaseImage - new Vector2(5, 3) + new Vector2(0, 45 * 6), Color.White);
                }
                _spriteBatch.Draw(_textureBouclierHUD, _positionBaseImage + new Vector2(0, 45 * 6), Color.White);
                _spriteBatch.DrawString(Message._police,"Invincible " + (3 - Math.Round(Perso._waitBouclier,0)) + "s", _positionBaseTexte + new Vector2(0, 45 * 6), Color.White);
            }
        }


    }
}
