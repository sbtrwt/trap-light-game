using TrapLight.Player.Black;
using UnityEngine;

namespace TrapLight.Player
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
    public class PlayerSO : ScriptableObject
    {
        public int ID;
        public BlackParticleSO BlackParticleSO;


    }
}
