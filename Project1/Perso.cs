﻿using Microsoft.Xna.Framework;
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
        public static Vector2 _positionPerso;
        public static AnimatedSprite _perso;
        public static int _vitessePerso;
        public static string animation;

        public static void Initialise()
        {
            _vitessePerso = 100;
            _positionPerso = new Vector2(130, 146);

        }

        public static void LoadContent(SpriteSheet spriteSheet)
        {
            _perso = new AnimatedSprite(spriteSheet);

        }

        public static void Update()
        {
            animation = "idle";

        }

        public static void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Game1._textureombrePerso, _positionPerso + new Vector2(-16, -12), Color.White);
            _spriteBatch.Draw(_perso, _positionPerso);
        }

    }
}