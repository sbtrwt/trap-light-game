using TrapLight.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TrapLight.UI
{
    public class LobbyUIController : MonoBehaviour
    {
        [SerializeField] private Button buttonStart;
        [SerializeField] private Button buttonTutorial;
        [SerializeField] private Button buttonExit;
        [SerializeField] private Button buttonSound;
        [SerializeField] private Button buttonMap;
        private void Start()
        {
            buttonStart.onClick.AddListener(OnClickStart);
            buttonTutorial.onClick.AddListener(OnClickTutorial);
            buttonExit.onClick.AddListener(OnClickExit);
            buttonSound.onClick.AddListener(OnClickSound);
            buttonMap.onClick.AddListener(OnClickMap);
        }

        private void OnClickStart()
        {
            SceneManager.LoadScene(GlobalConstant.MAIN_INDEX);
        }
        private void OnClickTutorial()
        {
            SceneManager.LoadScene(GlobalConstant.TUTORIAL_INDEX);
        }
        private void OnClickExit()
        {
            Application.Quit();
        }
        private void OnClickSound()
        {
            SoundManager.Instance.ToggleMusic();
        }
        private void OnClickMap()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(GlobalConstant.MAP_INDEX);
        }
    }
}