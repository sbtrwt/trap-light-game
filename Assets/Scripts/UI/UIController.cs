using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            buttonRestart.onClick.AddListener(OnClickStart);
            buttonLobby.onClick.AddListener(OnClickLobby);
        }

        private void OnClickStart()
        {
            GameController.Instance.ResetWave();
        }

        public void SetGameOverUI(bool flag)
        {
            gameOverUI.SetActive(flag);
        }
        private void OnClickLobby()
        {
            SceneManager.LoadScene(GlobalConstant.LOBBY_INDEX);
        }
    }
}