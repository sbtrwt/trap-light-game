using UnityEngine;
namespace TrapLight.Player.Explosion
{
    public class ExplosionView : MonoBehaviour
    {
        private ExplosionController controller;
        public void SetController(ExplosionController controller)
        {
            this.controller = controller;
        }
    }
}
