using TrapLight.Events;
using TrapLight.Map;
using TrapLight.Player;
using TrapLight.Player.Explosion;
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
            playerService = new PlayerService(playerScriptableObject, explosionScriptableObject);
        }

        private void InjectDependencies()
        {
            mapService.Init(eventService);
            waveService.Init( mapService,  eventService);
            playerService.Init();
        }

        private void Update()
        {
           
        }
    }
}
