
using UnityEngine;

namespace TrapLight.Player.Wall
{
    [CreateAssetMenu(fileName = "WallScriptableObject", menuName = "ScriptableObjects/WallScriptableObject")]

    public class WallSO : ScriptableObject
    {
        public float Width;
        public WallView WallView;
    }
}
