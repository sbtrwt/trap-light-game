using TrapLight.Player.Explosion;
using UnityEngine;
namespace TrapLight.Player.Black
{
    [CreateAssetMenu(fileName = "BlackScriptableObject", menuName = "ScriptableObjects/BlackScriptableObject")]

    public class BlackParticleSO : ScriptableObject
    {
        public int Health;
        public int Damage;
      
        public int Speed;
        public BlackParticleView BlackParticlePrefab;
        public Vector2 Position;
    }
}
