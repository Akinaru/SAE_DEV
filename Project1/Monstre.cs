using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
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
        private double vitesse;
        private int vie;
        private Texture2D _texturelowLife;
        private Texture2D _texturemidLife;
        private Texture2D _texturefullLife;
        private Texture2D _textureMonstreHit;
        private bool hit;

        public Monstre(String spritesheet, Vector2 position, ContentManager content)
        {
            this.MonstreSprite = new AnimatedSprite(content.Load<SpriteSheet>(spritesheet, new JsonContentLoader()));
            this.Vitesse = new Random().Next(400,550) / 10;
            this.Vie = 3;
            this._texturelowLife = content.Load<Texture2D>("lowLife");
            this._texturemidLife = content.Load<Texture2D>("midLife");
            this._texturefullLife = content.Load<Texture2D>("fullLife");
            this._textureMonstreHit = content.Load<Texture2D>("monstreHit");
            this.Spawn();
            this.Hit = false;
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
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        public AnimatedSprite MonstreSprite
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

        public double Vitesse
        {
            get
            {
                return this.vitesse;
            }

            set
            {
                this.vitesse = value;
            }
        }

        public int Vie
        {
            get
            {
                return this.vie;
            }

            set
            {
                this.vie = value;
            }
        }

        public bool Hit
        {
            get
            {
                return this.hit;
            }

            set
            {
                this.hit = value;
            }
        }

        public static void Update(float deltaTime)
        {
            for (int i = 0; i < Game1._listeMonstre.Count; i++)
            {
                Monstre monstre = Game1._listeMonstre[i];
                float distance = Vector2.Distance(monstre.Position, Perso._positionPerso);
                if (distance > 6)
                {
                    
                    Vector2 direction = Vector2.Normalize(Perso._positionPerso - monstre.Position);
                    Vector2 pos = new Vector2(monstre.Position.X, monstre.Position.Y);
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
                        y = (ushort)((pos.Y -2 )/ Map._tiledMap.TileHeight);
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
                    String animation = "walkSouth";
                    monstre.MonstreSprite.Play(animation);
                    monstre.MonstreSprite.Update(deltaTime);
                    double vitesse = monstre.Vitesse;
                    if (monstre.Hit)
                    {
                        vitesse -= 30;
                    }
                    monstre.Position += direction * (float)vitesse * deltaTime;

                    if (Vector2.Distance(monstre.Position, Perso._positionPerso) < 6)
                    {
                        if (!Perso._touche)
                        {
                            Perso._touche = true;
                            Game1._viePerso -= 1;
                        }
                    }
                }
            }

        }

        public static void Draw(SpriteBatch _spriteBatch, ContentManager Content)
        {
            for (int i = 0; i < Game1._listeMonstre.Count; i++)
            {
                Monstre monstre = Game1._listeMonstre[i];

                if (monstre.hit)
                {
                    _spriteBatch.Draw(monstre._textureMonstreHit, monstre.Position - new Vector2(8, 8), Color.White);
                }
                else
                {
                    _spriteBatch.Draw(monstre.MonstreSprite, monstre.Position);
                }
                _spriteBatch.Draw(Game1._textureombrePerso, monstre.Position + new Vector2(-16, -13), Color.White);
                
                
                if(monstre.Vie == 3)
                    _spriteBatch.Draw(monstre._texturefullLife, monstre.Position + new Vector2(-12, -12), Color.White);
                else if (monstre.Vie == 2)
                    _spriteBatch.Draw(monstre._texturemidLife, monstre.Position + new Vector2(-12, -12), Color.White);
                else if (monstre.Vie == 1)
                    _spriteBatch.Draw(monstre._texturelowLife, monstre.Position + new Vector2(-12, -12), Color.White);
                else
                {
                    monstre.Hit = false;
                    Game1._listeMonstre.Remove(monstre);
                    if(Game1._listeMonstre.Count == 0)
                    {
                        Monstre.NewVague(Content);
                    }
                }
            }
        }

        public static void NewVague(ContentManager Content)
        {
            Game1._nombreMonstre += 6;
            Game1._vague += 1;
            Message.Display("Bravo ! Tu es a la vague", "numero "+ Game1._vague+ ". Continues !", 5);
            for (int i = 0; i < Game1._nombreMonstre; i++)
            {
                Game1._listeMonstre.Add(new Monstre("monstreAnimation.sf", new Vector2(new Random().Next(0, 1600), new Random().Next(0, 1600)), Content));
            }
            
        }
    }
}
