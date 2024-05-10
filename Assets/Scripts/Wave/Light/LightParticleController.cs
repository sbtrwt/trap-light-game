
using System;
using TrapLight.Player.Black;
using UnityEngine;

namespace TrapLight.Wave.Light
{
    public class LightParticleController
    {
        private LightParticleView lightView;
        private LightParticleSO lightScriptableObject;
        private WaveService waveService;
        //private const float waypointThreshold = 0.1f;
        //private List<Vector3> waypoints;
        private int currentHealth;
        private int currentWaypointIndex;
        private LightParticleState currentState;

        public Vector3 Position => lightView.transform.position;
        public LightParticleController(LightParticleView lightPrefab, Transform lightContainer)
        {
            lightView = UnityEngine.Object.Instantiate(lightPrefab, lightContainer);
            lightView.SetController( this);
        }
        public void Init(LightParticleSO lightScriptableObject, WaveService waveService)
        {
            this.lightScriptableObject = lightScriptableObject;
            this.waveService = waveService;
            InitializeVariables();
            SetState(LightParticleState.ACTIVE);
            lightView.gameObject.SetActive(true);
            lightView.SetHealth(lightScriptableObject.Health.ToString());
        }
        private void InitializeVariables()
        {
            lightView.SetRenderer(lightScriptableObject.Sprite);
            currentHealth = lightScriptableObject.Health;
            lightView.SetColor(lightScriptableObject.ParticleColor);
           
        }
        private void SetState(LightParticleState state) => currentState = state;
        private void PopLightParticle()
        {
            SetState(LightParticleState.POPPED);
            //lightView.PopLightParticle();
        }

        public void OnHitBlackParticle(BlackParticleController blackController)
        {
            blackController.TakeDamage(lightScriptableObject.Damage);
        }

        public void OnPopAnimationPlayed()
        {
            
        }

        public void TakeDamage(int damageToTake)
        {
            int reducedHealth = currentHealth - damageToTake;
            currentHealth = reducedHealth <= 0 ? 0 : reducedHealth;
            lightView.SetHealth(currentHealth.ToString());
            if (currentHealth <= 0 && currentState == LightParticleState.ACTIVE)
            {
                PopLightParticle();
                ResetLightParticle();
            }
        }

        private void ResetLightParticle()
        {
            waveService.RemoveLightParticle(this);
            lightView.gameObject.SetActive(false);
           
        }
        public int GiveDamage() =>  lightScriptableObject.Damage;
        public void SetPosition(Vector3 spawnPosition)
        {
            lightView.transform.position = spawnPosition;
        }
        public enum LightParticleState
        {
            ACTIVE,
            POPPED
        }
    }
}
