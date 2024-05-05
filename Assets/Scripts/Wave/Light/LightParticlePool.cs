using System;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;
using TrapLight.Utilities;
using TrapLight.Wave;

namespace TrapLight.Wave.Light
{
    public class LightParticlePool : GenericObjectPool<LightParticleController>
    {
        private LightParticleView lightPrefab;
        private List<LightParticleSO> lightScriptableObjects;
        private Transform lightContainer;

        public LightParticlePool(WaveSO waveScriptableObject)
        {
            this.lightPrefab = waveScriptableObject.LightPrefab;
            this.lightScriptableObjects = waveScriptableObject.LightScriptableObjects;
            this.lightContainer = new GameObject("Light Container").transform;
        }

        public LightParticleController GetLightParticle(LightParticleType lightType)
        {
            LightParticleController light = GetItem();
            LightParticleSO scriptableObjectToUse = lightScriptableObjects.Find(so => so.Type == lightType);
            light.Init(scriptableObjectToUse);
            return light;
        }

        protected override LightParticleController CreateItem() => new LightParticleController(lightPrefab, lightContainer);

    }
}
