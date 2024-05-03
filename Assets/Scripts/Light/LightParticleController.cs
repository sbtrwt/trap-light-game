
using UnityEngine;

namespace TrapLight.Light
{
    public class LightParticleController
    {
        private LightParticleView lightView;
        private LightSO lightScriptableObject;

        //private const float waypointThreshold = 0.1f;
        //private List<Vector3> waypoints;
        private int currentHealth;
        private int currentWaypointIndex;
        private LightState currentState;

        public Vector3 Position => lightView.transform.position;
        public LightParticleController(LightParticleView lightPrefab, Transform lightContainer)
        {


            lightView = Object.Instantiate(lightPrefab, lightContainer);
            lightView.SetController( this);
        }
        public void Init(LightSO lightScriptableObject)
        {
            this.lightScriptableObject = lightScriptableObject;
            InitializeVariables();
            SetState(LightState.ACTIVE);
        }
        private void InitializeVariables()
        {
            lightView.SetRenderer(lightScriptableObject.Sprite);
            currentHealth = lightScriptableObject.Health;
            //waypoints = new List<Vector3>();
        }
        private void SetState(LightState state) => currentState = state;
        private void PopLigth()
        {
            SetState(LightState.POPPED);
            lightView.PopLight();
        }

        public void OnPopAnimationPlayed()
        {
            //if (HasLayeredLights())
            //    SpawnLayeredLights();

            //GameService.Instance.playerService.GetReward(lightScriptableObject.Reward);
            //GameService.Instance.waveService.RemoveBloon(this);
        }

        //private bool HasLayeredLights() => lightScriptableObject.LayeredBloons.Count > 0;
        //private void SpawnLayeredLights() => GameService.Instance.waveService.SpawnLights(lightScriptableObject.LayeredLights,
        //lightView.transform.position,
        //currentWaypointIndex,
        //lightScriptableObject.LayerLightSpawnRate);
        public enum LightState
        {
            ACTIVE,
            POPPED
        }
    }
}
