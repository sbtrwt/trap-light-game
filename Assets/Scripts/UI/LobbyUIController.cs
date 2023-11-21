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

        private void Start()
        {
            buttonStart.onClick.AddListener(OnClickStart);
            buttonTutorial.onClick.AddListener(OnClickTutorial);
            buttonExit.onClick.AddListener(OnClickExit);
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
    }
}