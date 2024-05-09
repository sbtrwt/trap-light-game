﻿using TrapLight.Events;
using TrapLight.Map;
using TrapLight.Player;
using TrapLight.Player.Explosion;
using TrapLight.Player.Wall;
using TrapLight.UI;
using TrapLight.Wave;
using UnityEngine;

namespace TrapLight
{
    public class GameService : MonoBehaviour
    {
        private EventService eventService;
        private MapService mapService;
        private WaveService waveService;
        private PlayerService playerService;


        [SerializeField] private MapSO mapScriptableObject;
        [SerializeField] private WaveSO waveScriptableObject;
        [SerializeField] private PlayerSO playerScriptableObject;
        [SerializeField] private ExplosionSO explosionScriptableObject;
        [SerializeField] private WallSO wallScriptableObject;

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;
        // Scene References:

        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgMusicSource;
        private void Start()
        {
            InitializeServices();
            InjectDependencies();
        }

        private void InitializeServices()
        {
            eventService = new EventService();
            mapService = new MapService(mapScriptableObject);
            waveService = new WaveService(waveScriptableObject);
            playerService = new PlayerService(playerScriptableObject, explosionScriptableObject, wallScriptableObject);
        
        }

        private void InjectDependencies()
        {
            mapService.Init(eventService);
            waveService.Init( mapService,  eventService, uiService, playerService);
            playerService.Init(uiService, eventService);
            UIService.Init(eventService, waveService);
            uiService.SubscribeToEvents();
        }

        private void Update()
        {
           
        }
    }
}
