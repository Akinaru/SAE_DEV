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
    public class Boss
    {

        private Vector2 position;
        private AnimatedSprite boss;
        private AnimatedSprite fumee;
        private double vitesse;
        private int vie;
        private Texture2D _texturepleinLife;
        private Texture2D _texturevideLife;
        private Texture2D _textureBossHit;
        private bool _hit;
        private bool _mort;
        private float _mortWait;
        private float _deplaceWait;
        private SoundEffect _sonHit;

        private const int DROP_COEUR_FACILE = 25;
        private const int DROP_COEUR_DIFFICILE = 10;



        public Boss(String spritesheet, Vector2 position, ContentManager content)
        {
            this.BossSprite = new AnimatedSprite(content.Load<SpriteSheet>(spritesheet, new JsonContentLoader()));
            this.fumee = new AnimatedSprite(content.Load<SpriteSheet>("fumee.sf", new JsonContentLoader()));
            this.Vitesse = new Random().Next(400, 550) / 10;
            this.Vie = 10;
            this._texturepleinLife = content.Load<Texture2D>("fullLife");
            this._texturevideLife = content.Load<Texture2D>("fullLife");
            this._textureBossHit = content.Load<Texture2D>("bossHit");
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
                if (Collision.IsCollision((ushort)(pos.X / Map._tiledMap.TileWidth), (ushort)(pos.Y / Map._tiledMap.TileWidth)))
                {
                    positionne = false;
                }
                if (Vector2.Distance(pos, Perso._positionPerso) < 16 * 10)
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

        public AnimatedSprite BossSprite
        {
            get
            {
                return this.boss;
            }

            set
            {
                this.boss = value;
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
            this.fumee.Play("fumee");
            this.fumee.Update(deltaTime);


            float distance = Vector2.Distance(this.Position, Perso._positionPerso);
            if (this.Mort)
            {
                this._mortWait += 1 * deltaTime;
                if (this._mortWait >= 0.3)
                {
                    Game1._listeBoss.Remove(this);
                    Vague.NewVague(Content);
                }
            }
            String animation = "idle";

            if (distance >= 6 && distance < 16 * 15)
            {
                this._deplaceWait = 0;
                Vector2 direction = Vector2.Normalize(Perso._positionPerso - this.Position);
                Vector2 pos = new Vector2(this.Position.X, this.Position.Y);
                ushort x;
                ushort y;
                if (direction.X <= 0)
                    animation = "walkWest";
                else
                    animation = "walkEast";
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
                this.BossSprite.Update(deltaTime);
                double vitesse = this.Vitesse;
                if (this.Hit)
                {
                    vitesse -= 30;
                }


                this.Position += direction * (float)vitesse * deltaTime;
            }
            this.BossSprite.Play(animation);


            if (Vector2.Distance(this.Position, Perso._positionPerso) <= 6)
            {
                if (!Perso._touche)
                {
                    if (!this.Mort)
                    {
                        Perso._touche = true;
                        int vieAEnelver = 1;
                        if (Jeu.difficulte == Jeu.NiveauDifficulte.Facile)
                            vieAEnelver = 2;
                        if (Jeu.difficulte == Jeu.NiveauDifficulte.Difficile)
                            vieAEnelver = 4;
                        Perso._viePerso -= vieAEnelver;
                        ViePerso.Update();
                        _sonHit.Play(Game1._volumeSon, 0, 0);
                    }

                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch, ContentManager Content)
        {
            Boss boss = this;

            if (boss._hit)
            {
                _spriteBatch.Draw(boss._textureBossHit, boss.Position - new Vector2(20, 20), Color.White);
            }
            else
            {
                if (!boss.Mort)
                    _spriteBatch.Draw(boss.BossSprite, boss.Position);
            }
            if (!boss.Mort)
                _spriteBatch.Draw(Jeu._textureombrePerso, boss.Position + new Vector2(-16, -10), Color.White);
            if (boss.Mort)
            {
                _spriteBatch.Draw(boss.fumee, boss.Position);
            }
            if(boss.Vie > 0)
                _spriteBatch.Draw(boss._texturepleinLife, boss.Position + new Vector2(-12, -12), Color.White);
            else //vie du monstre = 0
            {
                boss.Hit = false;
                if (boss.Mort == false)
                {
                    Jeu._nombreKill += 1;
                    int rnd = new Random().Next(0, 100);
                    if (rnd <= DROP_COEUR_FACILE && Jeu.difficulte != Jeu.NiveauDifficulte.Extreme && Jeu.difficulte == Jeu.NiveauDifficulte.Facile) //25% de chance de drop un coeur
                    {
                        new Coeur(boss.Position, Content);
                        Message.Display("Oh ! Il y a un coeur", "par terre !", 5);
                    }
                    else if (rnd <= DROP_COEUR_DIFFICILE && Jeu.difficulte != Jeu.NiveauDifficulte.Extreme) //10% de chance de drop un coeur
                    {
                        new Coeur(boss.Position, Content);
                        Message.Display("Oh ! Il y a un coeur", "par terre !", 5);
                    }
                }

                boss.Mort = true;

            }
            
        }
        public static void Touche(Boss boss)
        {
            if (Vector2.Distance(boss.Position, Perso._positionPerso) < 40)
            {
                boss.Vie -= 1;
                boss.Hit = true;
                Perso._sonHit.Play(Game1._volumeSon, 0, 0);
                //Vector2 direction = Vector2.Normalize(monstre.Position - Perso._positionPerso);
                //monstre.Position += direction * 700 * deltaTime;
            }
        }

    }
}
