using System.Collections.Generic;
using TrapLight.Wave;
using UnityEngine;
namespace TrapLight.Map
{
    [CreateAssetMenu(fileName = "MapScriptableObject", menuName = "ScriptableObjects/MapScriptableObject")]
    public class MapSO : ScriptableObject
    {
        public List<MapData> MapDatas;
        public GameObject TileOverlay;
        public Color DefaultTileColor;
        public Color SpawnableTileColor;
        public Color NonSpawnableTileColor;
    }

    [System.Serializable]
    public struct MapData
    {
        public int MapID;
       
    }
}
