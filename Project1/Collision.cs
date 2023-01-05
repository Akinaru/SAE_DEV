using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Collision
    {
        public static List<string> _mapLayers = new List<string>() { "Batiments", "Batiments2", "Objets", "Objets2" };

        public static bool IsCollision(ushort x, ushort y)
        {
            //gestion des collisions listé dans la liste _mapLayers

            for (int i = 0; i < _mapLayers.Count; i++)
            {
                TiledMapTileLayer _Layer = Game1._tiledMap.GetLayer<TiledMapTileLayer>(_mapLayers[i]);
                TiledMapTile? tile;
                if (_Layer.TryGetTile(x, y, out tile) == false)
                    return false;
                if (!tile.Value.IsBlank)
                    return true;

            }
            return false;

        }
    }
}
