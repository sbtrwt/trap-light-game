using System;
using System.Collections;
using System.Collections.Generic;
using TrapLight.Events;
using TrapLight.Map;
using TrapLight.Player;
using TrapLight.UI;
using TrapLight.Wave.Light;
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
        private UIService uiService;
        private PlayerService playerService;
        private WaveData currentWaveData;
        public WaveService(WaveSO waveScriptableObject)
        {
            this.waveScriptableObject = waveScriptableObject;
            InitializeLights();
        }
        public void Init( MapService mapService,  EventService eventService, UIService uiService, PlayerService playerService)
        {
            this.mapService = mapService;
            this.eventService = eventService;
            this.uiService = uiService;
            this.playerService = playerService;

            InitializeLights();
            SubscribeToEvents();
        }
        private void InitializeLights()
        {
            lightPool = new LightParticlePool(waveScriptableObject, this);
            activeLights = new List<LightParticleController>();
        }

        private void SubscribeToEvents() => eventService.OnMapSelected.AddListener(LoadWaveDataForMap);

        private void LoadWaveDataForMap(int mapId)
        {
            currentWaveId = 0;
            waveDatas = waveScriptableObject.WaveConfigurations.Find(config => config.MapID == mapId).WaveDatas;
        }

        public void StarNextWave()
        {
            currentWaveId++;
            StartCurrentWave();
        }
        public void StartCurrentWave()
        {
            eventService.OnWaveStart.InvokeEvent(currentWaveId);
            //activeLights = new List<LightParticleController>();
            SetCurrenctWaveData();
            SpawnLights(currentWaveData.ListOfLights);
            playerService.SetExplosionCount(currentWaveData.ExplosionCount);
            uiService.SetWaveText(currentWaveId);
        }
        public  void SpawnLights(List<LightParticleType> lightsToSpawn)
        {
            if(lightsToSpawn != null)
            foreach (LightParticleType lightType in lightsToSpawn)
            {
                LightParticleController light = lightPool.GetLightParticle(lightType);
                AddLight(light);
            }
        }

       

        private void AddLight(LightParticleController lightToAdd)
        {
            activeLights.Add(lightToAdd);
        }

        public void RemoveLightParticle(LightParticleController light)
        {
            lightPool.ReturnItem(light);
            activeLights.Remove(light);
            Debug.Log("Light count : " + activeLights.Count);
            if (HasCurrentWaveEnded())
            {
                if (IsLevelWon())
                {
                    uiService.ShowNotificationPanel(true, "Conguratulation!!! completed this map.");
                }
                else
                {
                    uiService.ShowNextWavePanel(true);
                }
            }
        }

        private List<LightParticleType> GetLightsForCurrentWave() => waveDatas.Find(waveData => waveData.WaveID == currentWaveId).ListOfLights;
        private void SetCurrenctWaveData()
        {
            currentWaveData = waveDatas.Find(waveData => waveData.WaveID == currentWaveId);
        }
        private bool HasCurrentWaveEnded() => activeLights.Count == 0;

        private bool IsLevelWon() => currentWaveId >= waveDatas.Count;

        ~WaveService()
        {
            eventService.OnMapSelected.RemoveListener(LoadWaveDataForMap);
        }
    }
}