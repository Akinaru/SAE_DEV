using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class HUD
    {
        public static Texture2D _textureMonstreHUD;
        public static Texture2D _texturePersoHUD;
        public static Texture2D _textureLocHUD;
        public static Texture2D _textureVagueHUD;
        public static Texture2D _textureBouclierHUD;

        internal static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {

            _textureMonstreHUD = Content.Load<Texture2D>("HUD/monstreHUD");
            _texturePersoHUD = Content.Load<Texture2D>("HUD/persoHUD");
            _textureLocHUD = Content.Load<Texture2D>("HUD/locHUD");
            _textureVagueHUD = Content.Load<Texture2D>("HUD/vagueHUD");
            _textureBouclierHUD = Content.Load<Texture2D>("HUD/bouclierHUD");
        }
        public static void Draw(SpriteBatch _spriteBatch)
        {
            Vector2 pos = new Vector2(1050,15);
            _spriteBatch.Draw(_textureMonstreHUD, pos, Color.White);
            _spriteBatch.DrawString(Message._police, Game1._listeMonstre.Count + " monstres", pos + new Vector2(55, 15), Color.White);

            _spriteBatch.Draw(_texturePersoHUD, pos + new Vector2(0, 45), Color.White);
            _spriteBatch.DrawString(Message._police, Jeu._nombreKill+" kills", pos + new Vector2(55, 60), Color.White);

            _spriteBatch.Draw(_textureLocHUD, pos + new Vector2(0, 90), Color.White);
            _spriteBatch.DrawString(Message._police, ""+Zone._zone, pos + new Vector2(55, 105), Color.White);

            _spriteBatch.Draw(_textureVagueHUD, pos + new Vector2(0, 135), Color.White);
            _spriteBatch.DrawString(Message._police, "Vague "+Jeu._vague, pos + new Vector2(55, 150), Color.White);

            if (Perso._waitBouclier > 0)
            {
                _spriteBatch.Draw(_textureBouclierHUD, pos + new Vector2(0, 180), Color.White);
                _spriteBatch.DrawString(Message._police,"Invincible " + (3 - Math.Round(Perso._waitBouclier,0)) + "s", pos + new Vector2(55, 195), Color.White);
            }
        }


    }
}
