using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Message
    {
        public static SpriteFont _police;
        public static Vector2 _positionMessageTexte;
        public static Vector2 _positionMessageBox;
        public static Texture2D _textureMessageBox;
        public static Texture2D _textureNavi;

        public static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            _police = Content.Load<SpriteFont>("font");
            _positionMessageBox = new Vector2(930, 530);
            _positionMessageTexte = _positionMessageBox + new Vector2(50,20);
            _textureMessageBox = Content.Load<Texture2D>("message");
            _textureNavi = Content.Load<Texture2D>("navi_grand");
        }

        public static void Draw(SpriteBatch _spriteBatch, String message, String message2)
        {
            _spriteBatch.Draw(_textureMessageBox, _positionMessageBox, Color.White);
            _spriteBatch.DrawString(Message._police, message, _positionMessageTexte, Color.Black);
            _spriteBatch.DrawString(Message._police, message2, _positionMessageTexte + new Vector2(0, 20), Color.Black);
            _spriteBatch.Draw(_textureNavi, _positionMessageBox+ new Vector2(260,70), Color.White);

        }

    }
}
