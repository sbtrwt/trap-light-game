using TrapLight.Events;
using TrapLight.Map;
using TrapLight.Wave;
using UnityEngine;

namespace TrapLight
{
    public class GameService : MonoBehaviour
    {
        private EventService eventService;
        private MapService mapService;
        private WaveService waveService;


        [SerializeField] private MapSO mapScriptableObject;
        [SerializeField] private WaveSO waveScriptableObject;
     

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
        }

        private void InjectDependencies()
        {
            mapService.Init(eventService);
          
          
            waveService.Init( mapService,  eventService);
        }

        private void Update()
        {
           
        }
    }
}
