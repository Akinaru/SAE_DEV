using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;

namespace Project1
{
    public class ScreenJeu : GameScreen
    {


        public ScreenJeu(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {

    
        }
        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Game1._gameBegin)
            {
                if (Game1._wait < 4)
                    Game1._wait += deltaTime;
                else
                    Game1._gameBegin = false;
            }

            if (KeyboardManager.frappe)
            {
                KeyboardManager.wait += 2 * deltaTime;
                if (KeyboardManager.wait >= 0.5)
                {
                    for (int i = 0; i < Game1._listeMonstre.Count; i++)
                    {
                        Monstre monstre = Game1._listeMonstre[i];
                        if (monstre.Hit)
                        {
                            monstre.Hit = false;
                        }
                    }
                }
                if (KeyboardManager.wait >= 1)
                {
                    KeyboardManager.frappe = false;
                    Perso._animEpee = false;
                    Perso._epee.Play("fight");
                    Perso._epee.Update(deltaTime);
                    KeyboardManager.wait = 0;
                }
            }
            if (Perso._touche)
            {
                Perso._waitBouclier += 1 * deltaTime;
                if (Math.Round(Perso._waitBouclier, 0) == 3)
                {
                    Perso._touche = false;
                    Perso._waitBouclier = 0;
                }

            }

            float walkSpeed = deltaTime * Perso._vitessePerso;
            Perso.Update(deltaTime);
            Fee.Update();
            ViePerso.Update();
            KeyboardManager.Manage(Perso._positionPerso, Map._tiledMap, Perso._animation, walkSpeed, Map._mapWidth, Map._mapHeight, Game1._graphics, deltaTime);
            Camera.Update();
            MapUI.Update();
            Zone.Update();
            Game1._positionObscurite = new Vector2(Perso._positionPerso.X - 1080 / 2, Perso._positionPerso.Y - 720 / 2);
            Monstre.Update(deltaTime);
            Perso._perso.Play(Perso._animation);
            Perso._perso.Update(deltaTime);
            Map.Update(gameTime);
            
        }
        public override void Draw(GameTime gameTime)
        {
            var transformMatrix = Camera._camera.GetViewMatrix();

            //affichage de la map et des sprites en fonction de la matrice créée depuis la caméra actuelle.
            Game1._spriteBatch.Begin(transformMatrix: transformMatrix);

            Map.Draw(transformMatrix);

            Monstre.Draw(Game1._spriteBatch, Content);
            Perso.Draw(Game1._spriteBatch);

            ViePerso.Draw(Game1._spriteBatch);
            Fee.Draw(Game1._spriteBatch);

            if (!Game1._debugMode)
                Game1._spriteBatch.Draw(Game1._textureObscurite, Game1._positionObscurite, Color.White);
            Game1._spriteBatch.End();




            Game1._spriteBatch.Begin();
            MapUI.Draw(Game1._spriteBatch);
            HUD.Draw(Game1._spriteBatch);
            Message.Draw(Game1._spriteBatch);
            Game1._spriteBatch.End();
        }
    }
}
