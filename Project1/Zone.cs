using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Zone
    {

        public static String _zone;

        public static void Initialise()
        {
            _zone = "Zone Redisentielle";
        }


        //Zone Rédisentielle : 1;1 75;1 1;32 75;32 
        //
        //Marcher : 76;1 98;1 76;32 98;32
        //
        //Parking : 1;33 34;33 1;61 34;61
        //
        //Magasin : 35;33 53;33 35;61 53;61
        //
        //Lycée : 56;33 75;33 56;61 75;61
        //
        //Garade : 76;33 98;33 76;61 98;61
        //
        //Parc : 1;62 34;62 1;99 34;99 
        //
        //Résidence Etudiante : 35;62 53;62 35;99 53;99
        //
        //Hôpital : 56;62 75;62 56;99 75;99
        //
        //Mairie : 76;62 98;62 76;99 98;99
        public static void Update()
        {
            _zone = getZone();
        }


        public static String getZone()
        {
            String zone = "Zone Residentielle";
            Vector2 pos = Perso._positionPerso;
            if (pos.X <= (75 * 16) && pos.Y <= (32 * 16))
            {
                zone = "Zone Redisentielle";
            }
            else if (pos.X >= 75 * 16 && pos.Y <= 32 * 16)
            {
                zone = "Marcher";
            }


            else if (pos.X <= 34*16 && pos.Y <= 61 * 16)
            {
                zone = "Parking";
            }
            else if (pos.X <= 53*16 && pos.Y <= 61* 16)
            {
                zone = "Magasin";
            }
            else if (pos.X <= 75*16 && pos.Y <= 61 * 16)
            {
                zone = "Lycee";
            }
            else if (pos.X <= 98*16 && pos.Y <= 61 * 16)
            {
                zone = "Garage";
            }


            else if (pos.X <= 34 * 16 && pos.Y <= 99 * 16)
            {
                zone = "Parc";
            }
            else if (pos.X <= 53 * 16 && pos.Y <= 99 * 16)
            {
                zone = "Residence Etudiante";
            }
            else if (pos.X <= 75 * 16 && pos.Y <= 99 * 16)
            {
                zone = "Hopital";
            }
            else if (pos.X <= 98 * 16 && pos.Y <= 99 * 16)
            {
                zone = "Mairie";
            }

            else
            {
                zone = "Inconnue";
            }
            return zone;
        }
    }
}
