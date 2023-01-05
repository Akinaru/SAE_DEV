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

        public static void Update(float deltaTime)
        {
            for (int i = 0; i < Game1._listeMonstre.Count; i++)
            {
                float distance = Vector2.Distance(Game1._listeMonstre[i].Position, Game1._positionPerso);
                if (distance > 5)
                {
                    
                    Vector2 direction = Vector2.Normalize(Game1._positionPerso - Game1._listeMonstre[i].Position);
                    Vector2 pos = new Vector2(Game1._listeMonstre[i].Position.X, Game1._listeMonstre[i].Position.Y);
                    int dirX;
                    int dirY;
                    ushort x;
                    ushort y;
                    if (direction.X <= 0) //x gauche
                    {
                        x = (ushort)(pos.X / Game1._tiledMap.TileWidth - 0.5);
                        y = (ushort)(pos.Y / Game1._tiledMap.TileHeight);
                        if (Collision.IsCollision(x, y))
                        {
                            direction.X = 0;
                        }
                    }
                    if (direction.X <= 0) //x droite
                    {
                        x = (ushort)(pos.X / Game1._tiledMap.TileWidth + 0.5);
                        y = (ushort)(pos.Y / Game1._tiledMap.TileHeight);
                        if (Collision.IsCollision(x, y))
                        {
                            direction.X = 0;
                        }
                    }

                    Game1._listeMonstre[i].Position += direction * 50 * deltaTime;
                }
            }

        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < Game1._listeMonstre.Count; i++)
            {
                _spriteBatch.Draw(Game1._listeMonstre[i].monstre, Game1._listeMonstre[i].Position);

            }
        }

    }
}
