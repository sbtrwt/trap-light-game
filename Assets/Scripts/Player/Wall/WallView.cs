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
            Init();
        }
        public void Init()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            lineRenderer = GetComponent<LineRenderer>();
            transform.rotation = new Quaternion();
        }
        public void SetController(WallController controller)
        {
            this.controller = controller;
        }

        public void SetLineRendererPosition(int index, Vector3 positionToSet)
        {
            lineRenderer.SetPosition(index, positionToSet);
        }

        public void SetCollider2D()
        {
            controller.AddColliderToLine(boxCollider2D);
        }
    }
}
