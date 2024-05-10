using TrapLight.Player.Explosion;
using UnityEngine;

namespace TrapLight.Player.Explosion
{
    [CreateAssetMenu(fileName = "ExplosionScriptableObject", menuName = "ScriptableObjects/ExplosionScriptableObject")]

    public class ExplosionSO : ScriptableObject
    {
        public int Damage;
        public float Radius;
        public ExplosionView ExplosionViewPrefab;
        public ParticleSystem ExplosionEffectPrefab;
    }
}
