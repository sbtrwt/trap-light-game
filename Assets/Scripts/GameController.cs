using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrapLight.Light;
using UnityEngine.SceneManagement;

namespace TrapLight {
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        [SerializeField] private GameObject lightParticlePrefab;
        [SerializeField] private int waveCount = 1;
        [SerializeField] private int lightParticleCount = 5;
        [SerializeField] private List<GameObject> lightParticles;
        [SerializeField] public BlackParticle blackParticle;
        [SerializeField] private int currentLightParticleCount = 5;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            ResetWave(waveCount);
        }
        public void ResetWave(int wave)
        {
            waveCount = wave;
            InitLightParticle();
            blackParticle.UpgradeLevel(waveCount);
            blackParticle.DeleteAllWalls();
        }

        private void InitLightParticle()
        {
            lightParticles = new List<GameObject>();
            currentLightParticleCount = lightParticleCount * waveCount;
            for (int i = 0; i < currentLightParticleCount; i++)
            {
                lightParticles.Add(Instantiate(lightParticlePrefab, new Vector3(Random.Range(GlobalConstant.MIN_WIDTH, GlobalConstant.MAX_WIDTH), Random.Range(GlobalConstant.MIN_HEIGHT, GlobalConstant.MAX_HEIGHT)), Quaternion.identity));
            }
        }

        public void DecreaseLightParticleCount()
        {
            currentLightParticleCount--;
            if(currentLightParticleCount <= 0)
            {
                ResetWave(++waveCount);
            }
        }

        public int GetLightParticleCount() 
        {
            return lightParticleCount;
        }
      
    }
}