using System.Collections.Generic;
using UnityEngine;
namespace TrapLight.Black
{
    [CreateAssetMenu(fileName = "BlackScriptableObject", menuName = "ScriptableObjects/BlackScriptableObject")]

    public class BlackParticleSO : ScriptableObject
    {
        public int Health;
        public int Damage;
        public int DefaultExplosion;
        public Sprite Sprite;
    }
}
