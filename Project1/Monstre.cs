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
            this.MonstreSprite = new AnimatedSprite(content.Load<SpriteSheet>(spritesheet, new JsonContentLoader()));
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

        public static void Update(float deltaTime)
        {
            for (int i = 0; i < Game1._listeMonstre.Count; i++)
            {
                float distance = Vector2.Distance(Game1._listeMonstre[i].Position, Perso._positionPerso);
                if (distance > 16)
                {
                    
                    Vector2 direction = Vector2.Normalize(Perso._positionPerso - Game1._listeMonstre[i].Position);
                    Vector2 pos = new Vector2(Game1._listeMonstre[i].Position.X, Game1._listeMonstre[i].Position.Y);
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
                    String animation = "walkNorth";
                    if (direction.X >= 0)
                        animation = "walkWest";
                    if (direction.X < 0)
                        animation = "walkEast";
                    if (direction.Y >= 0)
                        animation = "walkSouth";
                    if (direction.Y < 0)
                        animation = "walkNorth";
                    Game1._listeMonstre[i].MonstreSprite.Play(animation);
                    Game1._listeMonstre[i].MonstreSprite.Update(deltaTime);
                    Game1._listeMonstre[i].Position += direction * new Random().Next(45,55) * deltaTime;

                }
            }

        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < Game1._listeMonstre.Count; i++)
            {

                _spriteBatch.Draw(Game1._listeMonstre[i].MonstreSprite, Game1._listeMonstre[i].Position);
                _spriteBatch.Draw(Game1._textureombrePerso, Game1._listeMonstre[i].Position + new Vector2(-16, -12), Color.White);
            }
        }

    }
}
