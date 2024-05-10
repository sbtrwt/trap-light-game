using System.Collections;
using System.Collections.Generic;
using TMPro;
using TrapLight.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TrapLight.UI
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance { get; private set; }
        [SerializeField] private Button buttonRestart;
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private Button buttonLobby;
        [SerializeField] private Button buttonPause;
        [SerializeField] private Button buttonResume;
        [SerializeField] private Button buttonResumeLobby;
        [SerializeField] private GameObject gamePauseUI;
        [SerializeField] private Button buttonSound;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            
        }
        private void Start()
        {
            if(buttonRestart)
                buttonRestart.onClick.AddListener(OnClickStart);

            if (buttonLobby)
                buttonLobby.onClick.AddListener(OnClickLobby);

            if (buttonPause)
                buttonPause.onClick.AddListener(OnClickPause);

            if (buttonResume)
                buttonResume.onClick.AddListener(OnClickResume);

            if (buttonResumeLobby)
                buttonResumeLobby.onClick.AddListener(OnClickLobby);

            if (buttonSound)
                buttonSound.onClick.AddListener(OnClickSound);

           
        }

        private void OnClickStart()
        {
            Time.timeScale = 1;
            GameController.Instance.ResetWave();
        }

        public void SetGameOverUI(bool flag)
        {
            gameOverUI.SetActive(flag);
        }
        private void OnClickLobby()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(GlobalConstant.LOBBY_INDEX);
        }
        private void OnClickPause()
        {
            Time.timeScale = 0;
            gamePauseUI.SetActive(true);
        }

        private void OnClickResume()
        {
            Time.timeScale = 1;
            gamePauseUI.SetActive(false);
        }

        private void OnClickSound()
        {
            SoundManager.Instance.ToggleMusic();
        }
      
    }
}