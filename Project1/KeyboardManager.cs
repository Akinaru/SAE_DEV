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


        public static void Manage(Vector2 _positionPerso, TiledMap _tiledMap, string animation, float walkSpeed, int _mapWidth, int _mapHeight, GraphicsDeviceManager _graphics)
        {
            KeyboardState keyboardState = Keyboard.GetState();


            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.2);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);


                Game1.animation = "walkEast";
                if (!Game1.IsCollision(tx, ty) && _positionPerso.X < _mapWidth)
                {
                    Game1._positionPerso.X += walkSpeed;
                }

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.2);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                Game1.animation = "walkWest";


                if (!Game1.IsCollision(tx, ty) && _positionPerso.X > 0)
                {
                    Game1._positionPerso.X -= walkSpeed;
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {

                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso.Y - 2) / _tiledMap.TileHeight - 0.2);

                Game1.animation = "walkNorth";
                if (!Game1.IsCollision(tx, ty) && _positionPerso.Y > 0)
                {

                    Game1._positionPerso.Y -= walkSpeed;
                }
 

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {

                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso.Y + 2) / _tiledMap.TileHeight + 0.2);

                Game1.animation = "walkSouth";
                if (!Game1.IsCollision(tx, ty) && _positionPerso.Y < _mapHeight)
                {

                    Game1._positionPerso.Y += walkSpeed;
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Game1.animation = "idle";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Game1.animation = "idle";
            }


            if (Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                Game1._vitessePerso = 1000;
            }
        }

    }
}
