
using UnityEngine;
namespace TrapLight.Player.Explosion
{
    public class ExplosionController
    {
        private ExplosionView explosionView;
        public ExplosionController(ExplosionView explosionView, Transform explosionContainer)
        {
            explosionView = Object.Instantiate(explosionView, explosionContainer);
            explosionView.SetController(this);
        }

    }
}
