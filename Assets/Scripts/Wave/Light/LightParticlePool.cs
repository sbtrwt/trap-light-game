using System.Collections.Generic;
using TrapLight.Utilities;
using UnityEngine;

namespace TrapLight.Wave.Light
{
    public class LightParticlePool : GenericObjectPool<LightParticleController>
    {
        private LightParticleView lightPrefab;
        private List<LightParticleSO> lightScriptableObjects;
        private Transform lightContainer;
        private WaveService waveService;
        public LightParticlePool(WaveSO waveScriptableObject, WaveService waveService)
        {
            this.lightPrefab = waveScriptableObject.LightPrefab;
            this.lightScriptableObjects = waveScriptableObject.LightScriptableObjects;
            this.lightContainer = new GameObject("Light Container").transform;
            this.waveService = waveService;
        }

        public LightParticleController GetLightParticle(LightParticleType lightType)
        {
            LightParticleController light = GetItem();
            LightParticleSO scriptableObjectToUse = lightScriptableObjects.Find(so => so.Type == lightType);
            light.Init(scriptableObjectToUse, waveService);
            return light;
        }

        protected override LightParticleController CreateItem() => new LightParticleController(lightPrefab, lightContainer);

    }
}
