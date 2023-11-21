using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrapLight.Light;
using UnityEngine.SceneManagement;
using System;
using TrapLight.UI;
using TMPro;

namespace TrapLight
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        [SerializeField] private GameObject lightParticlePrefab;
        [SerializeField] private int waveCount = 1;
        [SerializeField] private int lightParticleCount = 3;
        [SerializeField] private List<GameObject> lightParticles;
        [SerializeField] public BlackParticle blackParticle;
        [SerializeField] private int currentLightParticleCount = 3;
        [SerializeField] private int currentExplosionCount = 0;
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private Animator waveTextAnimator;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            //else
            //{
            //    Destroy(gameObject);
            //}
        }
        private void Start()
        {
           StartCoroutine( ResetWave(waveCount));
        }
        public IEnumerator ResetWave(int wave)
        {
            waveCount = wave;
            RefreshWaveText();
            blackParticle.DeleteAllWalls();
            currentExplosionCount = 0;
            DeleteAllLightParticles();

            if (waveTextAnimator)
                waveTextAnimator.SetBool("IsWaveShow", true);

            UIController.Instance.SetGameOverUI(false);
            yield return new WaitForSeconds(4);

           
            InitLightParticle();
            blackParticle.UpgradeLevel(waveCount);

            if (waveTextAnimator)
                waveTextAnimator.SetBool("IsWaveShow", false);
        }
        public void ResetWave()
        {
            StartCoroutine( ResetWave(waveCount));
        }

        private void InitLightParticle()
        {
            lightParticles = new List<GameObject>();
            currentLightParticleCount = lightParticleCount * waveCount;
            for (int i = 0; i < currentLightParticleCount; i++)
            {
                lightParticles.Add(Instantiate(lightParticlePrefab, new Vector3(UnityEngine.Random.Range(GlobalConstant.MIN_WIDTH, GlobalConstant.MAX_WIDTH), UnityEngine.Random.Range(GlobalConstant.MIN_HEIGHT, GlobalConstant.MAX_HEIGHT)), Quaternion.identity));
            }
        }

        public void ValidateGame()
        {
            
            if (blackParticle.GetMaxExplosiveCount() <= currentExplosionCount && blackParticle.GetExplosiveCount() <= 0 && currentLightParticleCount > 0 )
            {
                //Debug.Log("game over");
                blackParticle.DeleteAllWalls();
                UIController.Instance.SetGameOverUI(true);
            }
        }

        public void IncrementExplosionCount() 
        {
            currentExplosionCount++;
        }
        public void DecreaseLightParticleCount()
        {
            currentLightParticleCount--;
            if (currentLightParticleCount <= 0)
            {
              StartCoroutine(  ResetWave(++waveCount));
            }
        }

        public int GetLightParticleCount()
        {
            return lightParticleCount;
        }

        public void DeleteAllLightParticles()
        {
            if (lightParticles != null)
                for (int i = 0; i < lightParticles.Count; i++)
                {
                    Destroy(lightParticles[i]);
                }
        }
        private void RefreshWaveText()
        {
            if (waveText != null)
                waveText.text ="Wave : "+ waveCount.ToString();
        }
    }
}