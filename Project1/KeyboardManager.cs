using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class KeyboardManager
    {

        public static bool frappe;
        public static float wait;

        public static void Manage(Vector2 _positionPerso, TiledMap _tiledMap, string animation, float walkSpeed, int _mapWidth, int _mapHeight, GraphicsDeviceManager _graphics, float deltaTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            var dir = Vector2.Zero;
            

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);


                Perso._animation = "walkEast";
                if (!Collision.IsCollision(tx, ty))
                {
                    dir.X += 1;
                }

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                Perso._animation = "walkWest";


                if (!Collision.IsCollision(tx, ty))
                {
                    dir.X -= 1;
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Z))
            {

                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso.Y - 2) / _tiledMap.TileHeight);

                Perso._animation = "walkNorth";
                if (!Collision.IsCollision(tx, ty))
                {
                    dir.Y -= 1;
                }
 

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {

                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso.Y + 3) / _tiledMap.TileHeight + 0.6);

                Perso._animation = "walkSouth";
                if (!Collision.IsCollision(tx, ty))
                {
                    dir.Y += 1;
                }

            }
            if(dir != Vector2.Zero)
                dir.Normalize();
            Perso._positionPerso += dir * walkSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (!frappe)
                {
                    for (int i = 0; i < Game1._listeMonstre.Count; i++)
                    {
                        Monstre monstre = Game1._listeMonstre[i];
                        if (Vector2.Distance(monstre.Position, Perso._positionPerso) < 30)
                        {
                            Game1._listeMonstre[i].Vie -= 1;
                            frappe = true;
                            //Vector2 direction = Vector2.Normalize(monstre.Position - Perso._positionPerso);
                            //monstre.Position += direction * 700 * deltaTime;
                        }
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F3))
            {
                Game1._debugMode = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.F3))
            {
                Game1._debugMode = false;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                MapUI._showUI = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Tab))
            {
                MapUI._showUI = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                Perso._vitessePerso = 125;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.LeftShift))
            {
                Perso._vitessePerso = 100;
            }

        }

    }
}
