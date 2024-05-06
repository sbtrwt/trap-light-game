
using UnityEngine;

namespace TrapLight.Wave.Light
{
    public class LightParticleController
    {
        private LightParticleView lightView;
        private LightParticleSO lightScriptableObject;

        //private const float waypointThreshold = 0.1f;
        //private List<Vector3> waypoints;
        private int currentHealth;
        private int currentWaypointIndex;
        private LightParticleState currentState;

        public Vector3 Position => lightView.transform.position;
        public LightParticleController(LightParticleView lightPrefab, Transform lightContainer)
        {
            lightView = Object.Instantiate(lightPrefab, lightContainer);
            lightView.SetController( this);
        }
        public void Init(LightParticleSO lightScriptableObject)
        {
            this.lightScriptableObject = lightScriptableObject;
            InitializeVariables();
            SetState(LightParticleState.ACTIVE);
        }
        private void InitializeVariables()
        {
            lightView.SetRenderer(lightScriptableObject.Sprite);
            currentHealth = lightScriptableObject.Health;
           
        }
        private void SetState(LightParticleState state) => currentState = state;
        private void PopLightParticle()
        {
            SetState(LightParticleState.POPPED);
            //lightView.PopLightParticle();
        }

        public void OnPopAnimationPlayed()
        {
            //if (HasLayeredLights())
            //    SpawnLayeredLights();

            //GameService.Instance.playerService.GetReward(lightScriptableObject.Reward);
            //GameService.Instance.waveService.RemoveBloon(this);
        }

        public void TakeDamage(int damageToTake)
        {
            int reducedHealth = currentHealth - damageToTake;
            currentHealth = reducedHealth <= 0 ? 0 : reducedHealth;

            if (currentHealth <= 0 && currentState == LightParticleState.ACTIVE)
            {
                PopLightParticle();
                //GameService.Instance.soundService.PlaySoundEffects(Sound.SoundType.BloonPop);
            }
        }

        public enum LightParticleState
        {
            ACTIVE,
            POPPED
        }
    }
}
