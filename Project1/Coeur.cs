using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Coeur
    {

        private Texture2D texture;
        private Vector2 position;
        private bool recupere;

        public Coeur(Vector2 position, ContentManager Content)
        {
            this.Position = position - new Vector2(8,8);
            this.Texture = Content.Load<Texture2D>("Perso/coeurDrop");
            this.Recupere = false;
            Jeu._listeCoeur.Add(this);
        }

        public void AEteRecupere()
        {
            if(Vector2.Distance(Perso._positionPerso, this.Position) < 5)
            {
             
                this.Recupere = true;
            }
        }

        public void Remove()
        {
            Jeu._listeCoeur.Remove(this);
        }

        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }

            set
            {
                this.texture = value;
            }
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
    }
}
