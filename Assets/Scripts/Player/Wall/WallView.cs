using UnityEngine;

namespace TrapLight.Player.Wall
{
    public class WallView : MonoBehaviour
    {
        private WallController controller;
        private LineRenderer lineRenderer;
        private BoxCollider2D boxCollider2D;
        private void Start()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            lineRenderer = GetComponent<LineRenderer>();
        }
        public void SetController(WallController controller)
        {
            this.controller = controller;
        }
    }
}
