using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Perso
    {

        public static SpriteSheet _spriteSheetWalkNormal;
        public static SpriteSheet _spriteSheetWalkEpee;
        public static Vector2 _positionPerso;
        public static AnimatedSprite _perso;
        public static int _vitessePerso;
        public static string _animation;
        public static bool _touche;
        public static float _waitBouclier;
        public static Texture2D _textureBouclier;
        public static AnimatedSprite _epee;
        public static bool _animEpee;
        public static Vector2 _positionEpee;
        public static SoundEffect _sonNewVague;
        public static SoundEffect _sonHit;
        public static double _viePerso;
        public static bool _mort;



        public static void Initialise()
        {
            _vitessePerso = 100;
            _positionPerso = new Vector2(130, 146);
            _positionEpee = new Vector2(130, 146);
            _touche = false;
            _waitBouclier = 0;
            _animEpee = false;

            _viePerso = 6;
            _mort = false;
            _animation = "idle";

        }

        public static void LoadContent(ContentManager Content)
        {
            _spriteSheetWalkNormal = Content.Load<SpriteSheet>("Perso/persoWalkNormal.sf", new JsonContentLoader());
            _spriteSheetWalkEpee = Content.Load<SpriteSheet>("Perso/persoWalkEpee.sf", new JsonContentLoader());
            SpriteSheet spritesheetEpee = Content.Load<SpriteSheet>("Perso/animationEpee.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(_spriteSheetWalkEpee);
            _epee = new AnimatedSprite(spritesheetEpee);
            
            _textureBouclier = Content.Load<Texture2D>("Perso/bouclier");
            _sonNewVague = Content.Load<SoundEffect>("Son/NewVague");
            _sonHit = Content.Load<SoundEffect>("Son/Hit");
        }

        public static void Update(float deltaTime)
        {
            _positionEpee = _positionPerso;
            _epee.Play("fight");
            _epee.Update(deltaTime);
            _perso.Play(_animation);
            _perso.Update(deltaTime);
        }

        public static void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(Jeu._textureombrePerso, _positionPerso + new Vector2(-16, -13), Color.White);
            _spriteBatch.Draw(_perso, _positionPerso);
            if (_animEpee)
            {
                _spriteBatch.Draw(_epee, _positionEpee);
            }
            if (_waitBouclier > 0)
            {
                _spriteBatch.Draw(_textureBouclier, _positionPerso + new Vector2(-10, -10), Color.White);
            }
        }

    }
}
