using System.Collections;
using System.Collections.Generic;
using TrapLight.Events;
using TrapLight.Light;
using TrapLight.Map;
using UnityEngine;

namespace TrapLight.Wave
{
    public class WaveService
    {
        private MapService mapService;
        private EventService eventService;
       
        [SerializeField] private WaveSO waveScriptableObject;

        private LightParticlePool lightPool;

        private int currentWaveId;
        private List<WaveData> waveDatas;
        private List<LightParticleController> activeLights;

        public WaveService(WaveSO waveScriptableObject)
        {
           
            this.waveScriptableObject = waveScriptableObject;
            InitializeLights();
            
        }
        public void Init( MapService mapService,  EventService eventService)
        {
            this.mapService = mapService;
            this.eventService = eventService;
            InitializeLights();
            SubscribeToEvents();
        }
        private void InitializeLights()
        {
            lightPool = new LightParticlePool(waveScriptableObject);
            activeLights = new List<LightParticleController>();
        }

        private void SubscribeToEvents() => eventService.OnMapSelected.AddListener(LoadWaveDataForMap);

        private void LoadWaveDataForMap(int mapId)
        {
            currentWaveId = 0;
            waveDatas = waveScriptableObject.WaveConfigurations.Find(config => config.MapID == mapId).WaveDatas;
            //GameService.Instance.UIService.UpdateWaveProgressUI(currentWaveId, waveDatas.Count);
        }

        public void StarNextWave()
        {
            currentWaveId++;
            var lightsToSpawn = GetLightsForCurrentWave();
            //var spawnPosition = GameService.Instance.MapService.GetLightSpawnPositionForCurrentMap();
            //SpawnLights(lightsToSpawn, spawnPosition, 0, waveScriptableObject.SpawnRate);
        }

        public  void SpawnLights(List<LightParticleType> lightsToSpawn, Vector3 spawnPosition, int startingWaypointIndex, float spawnRate)
        {
            foreach (LightParticleType lightType in lightsToSpawn)
            {
                LightParticleController light = lightPool.GetLightParticle(lightType);
                //light.SetPosition(spawnPosition);
                //light.SetWayPoints(GameService.Instance.MapService.GetWayPointsForCurrentMap(), startingWaypointIndex);

                AddLight(light);
                
            }
        }

        private void AddLight(LightParticleController lightToAdd)
        {
            activeLights.Add(lightToAdd);
            //lightToAdd.SetOrderInLayer(-activeLights.Count);
        }

        public void RemoveLight(LightParticleController light)
        {
            lightPool.ReturnItem(light);
            activeLights.Remove(light);
            if (HasCurrentWaveEnded())
            {
                //GameService.Instance.soundService.PlaySoundEffects(Sound.SoundType.WaveComplete);
                //GameService.Instance.UIService.UpdateWaveProgressUI(currentWaveId, waveDatas.Count);

                //if (IsLevelWon())
                //    GameService.Instance.UIService.UpdateGameEndUI(true);
                //else
                //    GameService.Instance.UIService.SetNextWaveButton(true);
            }
        }

        private List<LightParticleType> GetLightsForCurrentWave() => waveDatas.Find(waveData => waveData.WaveID == currentWaveId).ListOfLights;

        private bool HasCurrentWaveEnded() => activeLights.Count == 0;

        private bool IsLevelWon() => currentWaveId >= waveDatas.Count;
    }
}