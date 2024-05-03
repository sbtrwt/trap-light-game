using System.Collections.Generic;
using TrapLight.Light;
using UnityEngine;

namespace TrapLight.Wave
{
    [CreateAssetMenu(fileName = "WaveScriptableObject", menuName = "ScriptableObjects/WaveScriptableObject")]
    public class WaveSO : ScriptableObject
    {
        public float SpawnRate;
        public List<WaveConfiguration> WaveConfigurations;
        public LightParticleView LightPrefab;
        public List<LightSO> LightScriptableObjects;
    }
}
