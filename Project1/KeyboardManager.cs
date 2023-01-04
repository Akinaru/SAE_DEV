﻿using Microsoft.Xna.Framework;
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

            var dir = Vector2.Zero;
            

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);


                Game1.animation = "walkEast";
                if (!Game1.IsCollision(tx, ty) && _positionPerso.X < _mapWidth)
                {
                    dir.X += 1;
                }

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                Game1.animation = "walkWest";


                if (!Game1.IsCollision(tx, ty) && _positionPerso.X > 0)
                {
                    dir.X -= 1;
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Z))
            {

                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso.Y - 2) / _tiledMap.TileHeight);

                Game1.animation = "walkNorth";
                if (!Game1.IsCollision(tx, ty) && _positionPerso.Y > 0)
                {
                    dir.Y -= 1;
                }
 

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {

                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso.Y + 2) / _tiledMap.TileHeight + 0.5);

                Game1.animation = "walkSouth";
                if (!Game1.IsCollision(tx, ty) && _positionPerso.Y < _mapHeight)
                {
                    dir.Y += 1;
                }

            }
            if(dir != Vector2.Zero)
                dir.Normalize();
            Game1._positionPerso += dir * walkSpeed;

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
                Game1._showUI = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Tab))
            {
                Game1._showUI = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                Game1._vitessePerso = 125;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.LeftShift))
            {
                Game1._vitessePerso = 100;
            }
        }

    }
}
