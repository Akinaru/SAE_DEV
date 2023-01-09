using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    public class Bombe
    {
        private AnimatedSprite _bombeSprite;
        private Vector2 _position;
        private float _timer;

        public Bombe(Vector2 position, ContentManager Content)
        {
            this.Position = position;
            //SpriteSheet spritesheet = Content.Load<SpriteSheet>("Bombe/bombeAnimation.sf", new JsonContentLoader());
            //this.BombeSprite = new AnimatedSprite(spritesheet);
            this.Timer = 3;
            Game1._listeBombe.Add(this);
        }

        public void Update(float deltaTime)
        {
            this.Timer -= this.Timer * deltaTime;
            if(this.Timer < 0)
            {
                //Explosion
                this.Explosion();
            }
        }

        public void Explosion()
        {
            Game1._listeBombe.Remove(this);
            for (int i = 0; i < Game1._listeMonstre.Count; i++)
            {
                if(Vector2.Distance(Game1._listeMonstre[i].Position, this.Position) < 16*3)
                {
                    Game1._listeMonstre[i].Vie -= 3;
                }
            }
        }

        public AnimatedSprite BombeSprite
        {
            get
            {
                return this._bombeSprite;
            }

            set
            {
                this._bombeSprite = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this._position;
            }

            set
            {
                this._position = value;
            }
        }

        public float Timer
        {
            get
            {
                return this._timer;
            }

            set
            {
                this._timer = value;
            }
        }
    }
}
