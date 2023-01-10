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

        public static bool _frappe;
        public static float _wait;
        public static bool _keyEscape;

        public static void Initialise()
        {
            _keyEscape = false;
        }

        public static void Manage(Vector2 _positionPerso, TiledMap _tiledMap, string animation, float walkSpeed, int _mapWidth, int _mapHeight, GraphicsDeviceManager _graphics, float deltaTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            var dir = Vector2.Zero;
            MouseState mouseState = Mouse.GetState();

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
            if (dir != Vector2.Zero)
                dir.Normalize();
            Perso._positionPerso += dir * walkSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) || mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!_frappe)
                {
                    Perso._animEpee = true;
                    _frappe = true;
                    for (int i = 0; i < Game1._listeMonstre.Count; i++)
                    {
                        Monstre.Touche(Game1._listeMonstre[i]);
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                MapUI._showUI = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Tab))
            {
                MapUI._showUI = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (!_keyEscape)
                {
                    Game1.etat = Game1.Etats.Pause;
                    _keyEscape = true;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (_keyEscape)
                {
                    _keyEscape = false;
                }
            }
        }
    }
}
