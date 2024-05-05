using System.Collections.Generic;
using UnityEngine;

namespace TrapLight.Light
{
    [CreateAssetMenu(fileName = "LightScriptableObject", menuName = "ScriptableObjects/LightScriptableObject")]
    public class LightParticleSO : ScriptableObject
    {
        public LightParticleType Type;
        public int Health;
        public int Damage;
        public int Reward;
        public float Speed;
        public Sprite Sprite;
        public List<LightParticleType> LayeredLight;
        public float LayerLightSpawnRate;
    }
}
