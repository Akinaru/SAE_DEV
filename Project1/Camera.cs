﻿using Microsoft.Xna.Framework;
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
        public static float _positionCameraX;
        public static float _positionCameraY;

        public static void Initialise(BoxingViewportAdapter viewportadapter)
        {
            _positionCameraX = Game1._positionPerso.X;
            _positionCameraY = Game1._positionPerso.Y;

            _camera = new OrthographicCamera(viewportadapter);
            _cameraPosition = new Vector2(Game1._screenWidth, Game1._screenHeight);
            _camera.ZoomIn(1.5f);
        }

        public static void Update()
        {
            //ici on evite a la camera de sortie de la map et d'afficher une zone "morte" qui ne contient pas de tuile

            _positionCameraX = Game1._positionPerso.X;
            _positionCameraY = Game1._positionPerso.Y;

            //si le personnage arrive dans l'angle a gauche, on place la camera
            if (Game1._positionPerso.X < Game1._screenWidth / 5)
                _positionCameraX = Game1._screenWidth / 5;
            //si le personnage arrive dans l'angle a droite, on place la camera
            if (Game1._positionPerso.X > (Game1._mapWidth - Game1._screenWidth / 5))
                _positionCameraX = (Game1._mapWidth - Game1._screenWidth / 5);
            //si le personnage arrive dans l'angle en haut , on place la camera
            if (Game1._positionPerso.Y < Game1._screenHeight / 5)
                _positionCameraY = Game1._screenHeight / 5;
            //si le personnage arrive dans l'angle en bas, on place la camera
            if (Game1._positionPerso.Y > (Game1._mapHeight - Game1._screenHeight / 5))
                _positionCameraY = (Game1._mapHeight - Game1._screenHeight / 5);

            _camera.LookAt(new Vector2(_positionCameraX, _positionCameraY));

        }

    }
}
