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
        public static float _time;
        public static bool _messageDraw;
        public static String _message;
        public static String _message2;

        public static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            _messageDraw = false;
            _police = Content.Load<SpriteFont>("font");
            _positionMessageBox = new Vector2(930, 530);
            _positionMessageTexte = _positionMessageBox + new Vector2(40,32);
            _textureMessageBox = Content.Load<Texture2D>("Message/message");
            _textureNavi = Content.Load<Texture2D>("Message/navi_grand");
        }

        public static void Update(float deltaTime)
        {
            if (_messageDraw)
            {
                _time -= 1 * deltaTime;
                if(_time <= 0)
                {
                    _messageDraw = false;
                    _time = 0;
                }
            }
        }

        public static void Display(String message, String message2, int time)
        {
            _messageDraw = true;
            _time = time;
            _message = message;
            _message2 = message2;
        }

        public static void Draw(SpriteBatch _spriteBatch)
        {
            if (_messageDraw)
            {
                _spriteBatch.Draw(_textureMessageBox, _positionMessageBox, Color.White);
                _spriteBatch.DrawString(Message._police, "Fee: ", _positionMessageTexte + new Vector2(0, -18), Color.Red);
                _spriteBatch.DrawString(Message._police, _message, _positionMessageTexte, Color.Black);
                _spriteBatch.DrawString(Message._police, _message2, _positionMessageTexte + new Vector2(0, 18), Color.Black);
                _spriteBatch.Draw(_textureNavi, _positionMessageBox + new Vector2(260, 70), Color.White);
            }
        }

    }
}
