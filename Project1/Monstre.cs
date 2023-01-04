using Microsoft.Xna.Framework;
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
    public class Monstre
    {

        private Vector2 position;
        private AnimatedSprite monstre;

        public Monstre(String spritesheet, Vector2 position, ContentManager content)
        {
            this.Perso = new AnimatedSprite(content.Load<SpriteSheet>(spritesheet, new JsonContentLoader()));
            this.Position = position;
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        public AnimatedSprite Perso
        {
            get
            {
                return this.monstre;
            }

            set
            {
                this.monstre = value;
            }
        }

        public void Update(float deltaTime)
        {

            float distance = Vector2.Distance(Position, Game1._positionPerso);
            if(distance > 5)
            {
                Vector2 direction = Vector2.Normalize(Game1._positionPerso - Position);
                Position += direction * 50 * deltaTime;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(monstre, Position);
        }

    }
}
