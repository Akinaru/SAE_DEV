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
    public class Fantome
    {


        private Vector2 position;
        private AnimatedSprite fantome;
        private AnimatedSprite fumee;
        private double vitesse;
        private int vie;
        private Texture2D _texturelowLife;
        private Texture2D _texturemidLife;
        private Texture2D _texturefullLife;
        private Texture2D _textureFantomeHit;
        private bool _hit;
        private bool _mort;
        private float _mortWait;
        private float _deplaceWait;
        private SoundEffect _sonHit;

        private const int DROP_COEUR_FACILE = 25;
        private const int DROP_COEUR_DIFFICILE = 10;



        public Fantome(String spritesheet, Vector2 position, ContentManager content)
        {
            this.FantomeSprite = new AnimatedSprite(content.Load<SpriteSheet>(spritesheet, new JsonContentLoader()));
            this.fumee = new AnimatedSprite(content.Load<SpriteSheet>("fumee.sf", new JsonContentLoader()));
            this.Vitesse = new Random().Next(400, 550) / 10;
            this.Vie = 3;
            this._texturelowLife = content.Load<Texture2D>("lowLife");
            this._texturemidLife = content.Load<Texture2D>("midLife");
            this._texturefullLife = content.Load<Texture2D>("fullLife");
            this._textureFantomeHit = content.Load<Texture2D>("monstreHit");
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

        public AnimatedSprite FantomeSprite
        {
            get
            {
                return this.fantome;
            }

            set
            {
                this.fantome = value;
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
                    Game1._listeFantome.Remove(this);
                    Vague.NewVague(Content);


                }
            }
            this._deplaceWait = 0;
            Vector2 direction = Vector2.Normalize(Perso._positionPerso - this.Position);
            this.FantomeSprite.Update(deltaTime);
            double vitesse = this.Vitesse;
            if (this.Hit)
            {
                vitesse -= 30;
            }
            this.Position += direction * (float)vitesse * deltaTime;
            String animation = "walkSouth";
            this.FantomeSprite.Play(animation);
            

            if (Vector2.Distance(this.Position, Perso._positionPerso) <= 6)
            {
                if (!Perso._touche)
                {
                    if (!this.Mort)
                    {
                        Perso._touche = true;
                        Perso._viePerso -= 1;
                        ViePerso.Update();
                        _sonHit.Play(Game1._volumeSon, 0, 0);
                    }

                }
            }
        }

        public static void Draw(SpriteBatch _spriteBatch, ContentManager Content)
        {
            for (int i = 0; i < Game1._listeFantome.Count; i++)
            {
                Fantome fantome = Game1._listeFantome[i];

                if (fantome._hit)
                {
                    _spriteBatch.Draw(fantome._textureFantomeHit, fantome.Position - new Vector2(8, 8), Color.White);
                }
                else
                {
                    if (!fantome.Mort)
                        _spriteBatch.Draw(fantome.FantomeSprite, fantome.Position);
                }
                if (!fantome.Mort)
                    _spriteBatch.Draw(Jeu._textureombrePerso, fantome.Position + new Vector2(-16, -13), Color.White);
                if (fantome.Mort)
                {
                    _spriteBatch.Draw(fantome.fumee, fantome.Position);
                }

                if (fantome.Vie == 3)
                    _spriteBatch.Draw(fantome._texturefullLife, fantome.Position + new Vector2(-12, -12), Color.White);
                else if (fantome.Vie == 2)
                    _spriteBatch.Draw(fantome._texturemidLife, fantome.Position + new Vector2(-12, -12), Color.White);
                else if (fantome.Vie == 1)
                    _spriteBatch.Draw(fantome._texturelowLife, fantome.Position + new Vector2(-12, -12), Color.White);
                else //vie du monstre = 0
                {
                    fantome.Hit = false;
                    if (fantome.Mort == false)
                    {
                        Jeu._nombreKill += 1;
                    }

                    fantome.Mort = true;

                }
            }
        }
        public static void Touche(Fantome fantome)
        {
            if (Vector2.Distance(fantome.Position, Perso._positionPerso) < 40)
            {
                fantome.Vie -= 1;
                fantome.Hit = true;
                Perso._sonHit.Play(Game1._volumeSon, 0, 0);
                //Vector2 direction = Vector2.Normalize(monstre.Position - Perso._positionPerso);
                //monstre.Position += direction * 700 * deltaTime;
            }
        }

    }
}
