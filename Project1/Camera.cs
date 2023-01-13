using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Camera
    {
        public static OrthographicCamera _camera;
        public static Vector2 _cameraPosition;

        public static void Initialise(BoxingViewportAdapter viewportadapter)
        {
            _camera = new OrthographicCamera(viewportadapter);
            _cameraPosition = new Vector2(Perso._positionPerso.X, Perso._positionPerso.Y);
            _camera.ZoomIn(1.5f);
        }

        public static void Update()
        {
            _cameraPosition = new Vector2(Perso._positionPerso.X, Perso._positionPerso.Y);

            //on divise la taille de l'écran par 5 et non par 2 car la camera est zoomé
            //si le personnage arrive dans l'angle a gauche, on place la camera
            if (Perso._positionPerso.X < Game1._largeurEcran / 5)
                _cameraPosition.X = Game1._largeurEcran / 5;
            //si le personnage arrive dans l'angle a droite, on place la camera
            if (Perso._positionPerso.X > (Map._mapWidth - Game1._largeurEcran / 5))
                _cameraPosition.X = (Map._mapWidth - Game1._largeurEcran / 5);
            //si le personnage arrive dans l'angle en haut , on place la camera
            if (Perso._positionPerso.Y < Game1._hauteurEcran / 5)
                _cameraPosition.Y = Game1._hauteurEcran / 5;
            //si le personnage arrive dans l'angle en bas, on place la camera
            if (Perso._positionPerso.Y > (Map._mapHeight - Game1._hauteurEcran / 5))
                _cameraPosition.Y = (Map._mapHeight - Game1._hauteurEcran / 5);

            _camera.LookAt(_cameraPosition);
        }

    }
}
