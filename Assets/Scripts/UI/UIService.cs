using TMPro;
using TrapLight.Events;
using TrapLight.Wave;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TrapLight.UI
{
    public class UIService : MonoBehaviour
    {
        private EventService eventService;
        private WaveService waveService;

        [Header("Level Selection Panel")]
        [SerializeField] private GameObject levelSelectionPanel;
        [SerializeField] private Button map1Button;
        [SerializeField] private MapButton mapButton;

        [Header("Wave Start Panel")]
        [SerializeField] private GameObject waveStartPanel;
        [SerializeField] private Button nextWaveButton;

        [Header("Notification Panel")]
        [SerializeField] private GameObject notificationPanel;
        [SerializeField] private Button lobbyButton;
        [SerializeField] private TMP_Text textMessage;

        [Header("Game State Panel")]
        [SerializeField] private GameObject gameStatePanel;
        [SerializeField] private Button pauseButton;
        [SerializeField] private TMP_Text waveText;
        [SerializeField] private TMP_Text explosionCountText;

        [Header("Pause Panel")]
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button resumeLobbyButton;
        private void Start()
        {
           
            levelSelectionPanel.SetActive(true);

        }

        public void Init(EventService eventService, WaveService waveService)
        {
            this.eventService = eventService;
            this.waveService = waveService;
            
            mapButton.Init(eventService);
            nextWaveButton.onClick.AddListener(OnStartNextWave);
            lobbyButton.onClick.AddListener(OnClickLobby);
            resumeLobbyButton.onClick.AddListener(OnClickLobby);
            pauseButton.onClick.AddListener(OnPauseClick);
            resumeButton.onClick.AddListener(OnResumeClick);

            SubscribeToEvents();
        }

        public void SubscribeToEvents() => eventService.OnMapSelected.AddListener(OnMapSelected);

        public void OnMapSelected(int mapID)
        {
            levelSelectionPanel.SetActive(false);
            waveStartPanel.SetActive(true);
           
        }
        public void OnStartNextWave()
        {
            waveStartPanel.SetActive(false);
            gameStatePanel.SetActive(true);
            waveService.StarNextWave();
        }
        public void ShowNextWavePanel(bool isShow) => waveStartPanel.SetActive(isShow);
        public void ShowNotificationPanel(bool isShow, string textToShow) 
        {
            notificationPanel.SetActive(isShow);
            textMessage.text = textToShow;
        }
        public void OnClickLobby()
        {
            SceneManager.LoadScene(GlobalConstant.LOBBY_INDEX);
        }

        public void OnResumeClick()
        {
            pausePanel.SetActive(false);
        }
        public void OnPauseClick()
        {
            pausePanel.SetActive(true);
        }

        public void SetExplosionCountText(int explosionCount)
        {
            explosionCountText.text = explosionCount.ToString();
        }
        public void SetWaveText(int waveID)
        {
            waveText.text = $"wave:{waveID}" ;
        }
    }
}
