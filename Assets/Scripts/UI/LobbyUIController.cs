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
        private void Start()
        {
            buttonStart.onClick.AddListener(OnClickStart);
            buttonTutorial.onClick.AddListener(OnClickTutorial);
            buttonExit.onClick.AddListener(OnClickExit);
            buttonSound.onClick.AddListener(OnClickSound);
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
    }
}