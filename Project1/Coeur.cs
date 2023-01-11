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
    public class Coeur
    {

        private AnimatedSprite coeur;
        private Vector2 position;
        private bool recupere;
        private float timer;

        public Coeur(Vector2 position, ContentManager Content)
        {
            this.Position = position;
            this.CoeurSprite = new AnimatedSprite(Content.Load<SpriteSheet>("coeurAnim.sf", new JsonContentLoader()));
            this.Recupere = false;
            this.Timer = 10;
            Jeu._listeCoeur.Add(this);
            
        }

        public void CheckRecuperer(float deltaTime)
        {
            this.CoeurSprite.Play("anim");
            this.CoeurSprite.Update(deltaTime);
            if (Vector2.Distance(Perso._positionPerso, this.Position) < 16)
            {
                if(Perso._viePerso < 6)
                {
                    if (!this.Recupere)
                    {
                        Perso._viePerso += 1;
                        this.Recupere = true;
                        this.Remove();
                    }
                }
            }
            this.Timer -= 1 * deltaTime;
            if(this.Timer <= 0)
            {
                this.Remove();
            }
        }

        public void Remove()
        {
            Jeu._listeCoeur.Remove(this);
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

        public bool Recupere
        {
            get
            {
                return this.recupere;
            }

            set
            {
                this.recupere = value;
            }
        }

        public float Timer
        {
            get
            {
                return this.timer;
            }

            set
            {
                this.timer = value;
            }
        }

        public AnimatedSprite CoeurSprite
        {
            get
            {
                return this.coeur;
            }

            set
            {
                this.coeur = value;
            }
        }
    }
}
