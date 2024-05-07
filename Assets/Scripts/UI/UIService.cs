using TrapLight.Events;
using TrapLight.Wave;
using UnityEngine;
using UnityEngine.UI;

namespace TrapLight.UI
{
    public class UIService : MonoBehaviour
    {
        private EventService eventService;
        private WaveService waveService;

        [Header("Level Selection Panel")]
        [SerializeField] private GameObject levelSelectionPanel;
        [SerializeField] private Button Map1Button;
        [SerializeField] private MapButton mapButton;

        private void Start()
        {
           
            levelSelectionPanel.SetActive(true);

        }

        public void Init(EventService eventService, WaveService waveService)
        {
            this.eventService = eventService;
            this.waveService = waveService;
            
            mapButton.Init(eventService);
            SubscribeToEvents();
        }

        public void SubscribeToEvents() => eventService.OnMapSelected.AddListener(OnMapSelected);

        public void OnMapSelected(int mapID)
        {
            levelSelectionPanel.SetActive(false);
            waveService.StarNextWave();
        }

      
    }
}
