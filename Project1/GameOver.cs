using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class ScreenGameOver : GameScreen
    {


        public ScreenGameOver(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {

            base.Initialize();
        }
        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Game1.Etat = Game1.Etats.Menu;
            }
        }
        public override void Draw(GameTime gameTime)
        {

        }
    }
}
