
using UnityEngine;
using TrapLight.Light;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TrapLight
{
    public class TutorialController : MonoBehaviour
    {
      
        [SerializeField] private GameObject lightParticlePrefab;

        [SerializeField] private GameObject fillRightArrow;
        [SerializeField] private GameObject fillDownArrow;
        [SerializeField] private GameObject fillUpArrow;
        [SerializeField] private GameObject fillLeftArrow;

        [SerializeField] private GameObject fillCtrlKey;
        [SerializeField] private GameObject fillSpaceKey;

        //[SerializeField] private GameObject movementContainer;
        //[SerializeField] private GameObject explosiveContainer;
        //[SerializeField] private GameObject wallContianer;

        [SerializeField] private GameObject[] tutorialContainers;


        [SerializeField] private Button buttonStart;
        [SerializeField] private Button buttonNext;
        [SerializeField] private int currentIndex;

        private void Start()
        {
            currentIndex = 0;
            buttonStart.onClick.AddListener(OnClickStart);
            buttonNext.onClick.AddListener(OnClickNext);
            SetCurrentTutorial();
        }
        private void Update()
        {
            HandleMovement();
            HandleExplosive();
            HandleDrawWall();
        }

        private void HandleMovement()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                fillRightArrow.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                fillLeftArrow.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                fillUpArrow.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                fillDownArrow.SetActive(true);
            }

        }
        private void HandleExplosive()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                fillCtrlKey.SetActive(true);
            }

        }

        private void HandleDrawWall()
        {
            if (Input.GetKeyDown(KeyCode.Space) )
            {
                fillSpaceKey.SetActive(true);
            }

        }
        private void OnClickStart()
        {
            SceneManager.LoadScene(GlobalConstant.LOBBY_INDEX);
        }
        private void OnClickNext()
        {
            currentIndex++;
            if(tutorialContainers.Length <= currentIndex)
            {
                currentIndex = 0;
            }
            SetCurrentTutorial();
            //EventSystemManager.currentSystem.SetSelectedGameObject(null, null);
            GUI.FocusControl(null);
        }

        private void SetCurrentTutorial() 
        {
            if (tutorialContainers == null) return;

            foreach (var c in tutorialContainers)
            {
                c.SetActive(false);
            }
            tutorialContainers[currentIndex].SetActive(true);
        }
    }
}