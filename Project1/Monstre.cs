using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Monstre
    {

        private Vector2 _position;
        private AnimatedSprite _monstre;
        private AnimatedSprite _fumee;
        private double _vitesse;
        private int _vie;
        private Texture2D _texturelowLife;
        private Texture2D _texturemidLife;
        private Texture2D _texturefullLife;
        private Texture2D _textureMonstreHit;
        private bool _hit;
        private bool _mort;
        private float _mortWait;
        private float _deplaceWait;
        private SoundEffect _sonHit;

        private const int DROP_COEUR_FACILE = 25;
        private const int DROP_COEUR_DIFFICILE = 10;



        public Monstre(String spritesheet, Vector2 position, ContentManager content)
        {
            this.MonstreSprite = new AnimatedSprite(content.Load<SpriteSheet>(spritesheet, new JsonContentLoader()));
            this._fumee = new AnimatedSprite(content.Load<SpriteSheet>("fumee.sf", new JsonContentLoader())); 
            this.Vitesse = new Random().Next(400,550) / 10;
            this.Vie = 3;
            this._texturelowLife = content.Load<Texture2D>("lowLife");
            this._texturemidLife = content.Load<Texture2D>("midLife");
            this._texturefullLife = content.Load<Texture2D>("fullLife");
            this._textureMonstreHit = content.Load<Texture2D>("monstreHit");
            this._sonHit = content.Load<SoundEffect>("Son/playerHurt");
            this.Spawn();
            this.Hit = false;
            this.Mort = false;
            this._mortWait = 0;
            this._deplaceWait = 0;
            
        }

        public void Spawn()
        {
            bool positionne = false;
            Vector2 pos = new Vector2(0, 0);
            while (!positionne)
            {
                positionne = true;
                pos = new Vector2(new Random().Next(64, 1550), new Random().Next(64, 1550));
                if (Collision.IsCollision( (ushort)(pos.X / Map._tiledMap.TileWidth), (ushort)(pos.Y / Map._tiledMap.TileWidth))){
                    positionne = false;
                }
                if(Vector2.Distance(pos, Perso._positionPerso) < 16* 10)
                {
                    positionne = false;
                }

            }
            this.Position = pos;
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

        public AnimatedSprite MonstreSprite
        {
            get
            {
                return this._monstre;
            }

            set
            {
                this._monstre = value;
            }
        }

        public double Vitesse
        {
            get
            {
                return this._vitesse;
            }

            set
            {
                this._vitesse = value;
            }
        }

        public int Vie
        {
            get
            {
                return this._vie;
            }

            set
            {
                this._vie = value;
            }
        }

        public bool Hit
        {
            get
            {
                return this._hit;
            }

            set
            {
                this._hit = value;
            }
        }

        public bool Mort
        {
            get
            {
                return _mort;
            }

            set
            {
                _mort = value;
            }
        }

        public void Update(float deltaTime, ContentManager Content)
        {
            this._fumee.Play("fumee");
            this._fumee.Update(deltaTime);


            float distance = Vector2.Distance(this.Position, Perso._positionPerso);
            if (this.Mort)
            {
                this._mortWait += 1 * deltaTime;
                if(this._mortWait >= 0.3)
                {
                    Jeu._listeMonstre.Remove(this);
                    Vague.NewVague(Content);
                }
            }

            if (distance >= 6 && distance < 16 * 10)
            {
                this._deplaceWait = 0;
                Vector2 direction = Vector2.Normalize(Perso._positionPerso - this.Position);
                Vector2 pos = new Vector2(this.Position.X, this.Position.Y);
                ushort x;
                ushort y;
                if (direction.X <= 0) //x gauche
                {
                    x = (ushort)(pos.X / Map._tiledMap.TileWidth - 0.5);
                    y = (ushort)(pos.Y / Map._tiledMap.TileHeight);
                    if (Collision.IsCollision(x, y))
                    {
                        direction.X = 0;
                    }
                }
                if (direction.X > 0) //x droite
                {
                    x = (ushort)(pos.X / Map._tiledMap.TileWidth + 0.5);
                    y = (ushort)(pos.Y / Map._tiledMap.TileHeight);
                    if (Collision.IsCollision(x, y))
                    {
                        direction.X = 0;
                    }
                }
                if (direction.Y <= 0) //y haut
                {
                    x = (ushort)(pos.X / Map._tiledMap.TileWidth);
                    y = (ushort)((pos.Y - 2) / Map._tiledMap.TileHeight);
                    if (Collision.IsCollision(x, y))
                    {
                        direction.Y = 0;
                    }
                }
                if (direction.Y > 0) //y bas
                {
                    x = (ushort)(pos.X / Map._tiledMap.TileWidth);
                    y = (ushort)((pos.Y + 3) / Map._tiledMap.TileHeight);
                    if (Collision.IsCollision(x, y))
                    {
                        direction.Y = 0;
                    }
                }
                this.MonstreSprite.Update(deltaTime);
                double vitesse = this.Vitesse;
                if (this.Hit)
                {
                    vitesse -= 30;
                }
                this.Position += direction * (float)vitesse * deltaTime;
                String animation = "walkSouth";
                this.MonstreSprite.Play(animation);
            }

            if (Vector2.Distance(this.Position, Perso._positionPerso) <= 6)
            {
                if (!Perso._touche)
                {
                    if (!this.Mort)
                    {
                        Perso._touche = true;
                        Perso._viePerso -= 1;
                        Perso._coeurPerdu += 1;
                        ViePerso.Update();
                        _sonHit.Play(Game1._volumeSon, 0, 0);
                    }
  
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch, ContentManager Content)
        {
            Monstre monstre = this;
  
            if (monstre._hit)
            {
                _spriteBatch.Draw(monstre._textureMonstreHit, monstre.Position - new Vector2(8, 8), Color.White);
            }
            else
            {
                if(!monstre.Mort)
                    _spriteBatch.Draw(monstre.MonstreSprite, monstre.Position);
            }
            if (!monstre.Mort)
                _spriteBatch.Draw(Jeu._textureombrePerso, monstre.Position + new Vector2(-16, -13), Color.White);
            if (monstre.Mort)
            {
                _spriteBatch.Draw(monstre._fumee, monstre.Position);
            }

            if (monstre.Vie == 3)
                _spriteBatch.Draw(monstre._texturefullLife, monstre.Position + new Vector2(-12, -12), Color.White);
            else if (monstre.Vie == 2)
                _spriteBatch.Draw(monstre._texturemidLife, monstre.Position + new Vector2(-12, -12), Color.White);
            else if (monstre.Vie == 1)
                _spriteBatch.Draw(monstre._texturelowLife, monstre.Position + new Vector2(-12, -12), Color.White);
            else //vie du monstre = 0
            {
                monstre.Hit = false;
                if(monstre.Mort == false)
                {
                    Jeu._nombreKill += 1;
                    int rnd = new Random().Next(0, 100);
                    if (rnd <= DROP_COEUR_FACILE && Jeu.difficulte != Jeu.NiveauDifficulte.Extreme && Jeu.difficulte == Jeu.NiveauDifficulte.Facile) //25% de chance de drop un coeur
                    {
                        new Coeur(monstre.Position, Content);
                        Message.Display("Oh ! Il y a un coeur", "par terre !", 5);
                    }
                    else if (rnd <= DROP_COEUR_DIFFICILE && Jeu.difficulte != Jeu.NiveauDifficulte.Extreme) //10% de chance de drop un coeur
                    {
                        new Coeur(monstre.Position, Content);
                        Message.Display("Oh ! Il y a un coeur", "par terre !", 5);
                    }
                }

                monstre.Mort = true;

            }
        }
        public static void Touche(Monstre monstre)
        {
            if (Vector2.Distance(monstre.Position, Perso._positionPerso) < 40)
            {
                monstre.Vie -= 1;
                monstre.Hit = true;
                Perso._sonHit.Play(Game1._volumeSon, 0, 0);
            }
        }

    }
}
