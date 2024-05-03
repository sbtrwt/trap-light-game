using System.Collections.Generic;
using TrapLight.Events;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TrapLight.Map
{
    public class MapService
    {
        private EventService eventService;
        private MapSO mapScriptableObject;

        private Grid currentGrid;
        private Tilemap currentTileMap;
        private MapData currentMapData;
        private SpriteRenderer tileOverlay;

        public MapService(MapSO mapScriptableObject)
        {
            this.mapScriptableObject = mapScriptableObject;
            tileOverlay = Object.Instantiate(mapScriptableObject.TileOverlay).GetComponent<SpriteRenderer>();
           
        }

        public void Init(EventService eventService)
        {
            this.eventService = eventService;
            SubscribeToEvents();
        }
        private void SubscribeToEvents() => eventService.OnMapSelected.AddListener(LoadMap);

        private void LoadMap(int mapId)
        {
            currentMapData = mapScriptableObject.MapDatas.Find(mapData => mapData.MapID == mapId);
            currentGrid = Object.Instantiate(currentMapData.MapPrefab);
            currentTileMap = currentGrid.GetComponentInChildren<Tilemap>();
        }
    }
}
