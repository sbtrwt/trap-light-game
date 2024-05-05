using UnityEngine;

namespace TrapLight.Black
{
    public class BlackParticleController
    {
        private BlackParticleView blackView;
        private BlackParticleSO blackScriptableObject;

        private int currentHealth;

        public Vector3 Position => blackView.transform.position;

        public BlackParticleController(BlackParticleView blackPrefab, Transform blackContainer)
        {
            blackView = Object.Instantiate(blackPrefab, blackContainer);
            blackView.SetController(this);
        }

        public void Init(BlackParticleSO blackScriptableObject)
        {
            this.blackScriptableObject = blackScriptableObject;
            InitializeVariables();
            //SetState(LightState.ACTIVE);
        }

        private void InitializeVariables()
        {
            blackView.SetRenderer(blackScriptableObject.Sprite);
            currentHealth = blackScriptableObject.Health;

        }
        public void OnPopAnimationPlayed()
        { }
    }
}
