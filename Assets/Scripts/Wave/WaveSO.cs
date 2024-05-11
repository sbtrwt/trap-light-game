using System.Collections.Generic;
using TrapLight.Wave.Light;
using UnityEngine;

namespace TrapLight.Wave
{
    [CreateAssetMenu(fileName = "WaveScriptableObject", menuName = "ScriptableObjects/WaveScriptableObject")]
    public class WaveSO : ScriptableObject
    {
        public float SpawnRate;
        public List<WaveConfiguration> WaveConfigurations;
        public LightParticleView LightPrefab;
        public List<LightParticleSO> LightScriptableObjects;
     
    }
}
