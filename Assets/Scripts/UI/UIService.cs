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
        //[SerializeField] private Button map1Button;
        [SerializeField] private MapButton mapButton1;
        //[SerializeField] private Button map2Button;
        [SerializeField] private MapButton mapButton2;
        [SerializeField] private MapButton mapButton3;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button helpButton;
        [SerializeField] private Button selectionLobbyButton;

        [Header("Wave Start Panel")]
        [SerializeField] private GameObject waveStartPanel;
        [SerializeField] private Button nextWaveButton;
        [SerializeField] private TMP_Text waveStartText;

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

        [Header("GameOver Panel")]
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Button gameOverRestartButton;
        [SerializeField] private Button gameOverLobbyButton;
        [SerializeField] private TMP_Text gameOvertext;

        private void Start()
        {
            levelSelectionPanel.SetActive(true);
        }

        public void Init(EventService eventService, WaveService waveService)
        {
            this.eventService = eventService;
            this.waveService = waveService;

            mapButton1.Init(eventService);
            mapButton2.Init(eventService);
            mapButton3.Init(eventService);

            nextWaveButton.onClick.AddListener(OnStartNextWave);
            lobbyButton.onClick.AddListener(OnClickLobby);
            resumeLobbyButton.onClick.AddListener(OnClickLobby);
            pauseButton.onClick.AddListener(OnPauseClick);
            resumeButton.onClick.AddListener(OnResumeClick);
            gameOverLobbyButton.onClick.AddListener(OnClickLobby);
            gameOverRestartButton.onClick.AddListener(OnRestartClick);
            selectionLobbyButton.onClick.AddListener(OnClickLobby);

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
        private void OnClickLobby()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(GlobalConstant.LOBBY_INDEX);
        }

        private void OnResumeClick()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
        private void OnPauseClick()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }

        public void SetExplosionCountText(int explosionCount)
        {
            explosionCountText.text = explosionCount.ToString();
        }
        public void SetWaveText(int waveID)
        {
            waveStartText.text = $"Wave : {waveID + 1}";
            waveText.text = $"Wave : {waveID}";

        }

        public void SetGameOver(bool isShow)
        {
            gameOverPanel.SetActive(isShow);
        }
        private void OnRestartClick()
        {
            gameOverPanel.SetActive(false);
            waveService.StartCurrentWave();
        }
    }
}
