using TrapLight.Wave.Light;
using UnityEngine;

namespace TrapLight.Player.Wall
{
    public class WallController
    {
        private WallView wallView;

        public WallController(WallView explosionView, Transform explosionContainer)
        {
            this.wallView = UnityEngine.Object.Instantiate(explosionView, explosionContainer);
            this.wallView.SetController(this);
        }
    }
}
