using TrapLight.Wave.Light;
using UnityEngine;

namespace TrapLight.Player.Wall
{
    public class WallController
    {
        private WallView wallView;
        private Vector3 startPosition;
        private Vector3 endPosition;
        public WallController(WallView explosionView, Transform explosionContainer)
        {
            this.wallView = UnityEngine.Object.Instantiate(explosionView, explosionContainer);
            this.wallView.SetController(this);
        }
        public void Init()
        {
            wallView.Init();
        }
        public void OnWallDrawStart(Vector3 positionToSet)
        {
            startPosition = positionToSet;
            wallView.SetLineRendererPosition(0, positionToSet);
            wallView.SetLineRendererPosition(1, positionToSet);
        }

        public void OnWallDrawing(Vector3 positionToSet)
        {
            wallView.SetLineRendererPosition(1, positionToSet);
        }

        public void OnWallDrawEnd(Vector3 positionToSet)
        {
            endPosition = positionToSet;
            wallView.SetLineRendererPosition(1, positionToSet);
        }
        public void OnWallColliderDraw()
        {
            wallView.SetCollider2D();
        }
        public void AddColliderToLine(BoxCollider2D collider2D)
        {
           
            float lineLength = Vector3.Distance(startPosition, endPosition); 

            collider2D.size = new Vector3(lineLength, 0.1f, 1f); 
            Vector3 midPoint = (startPosition + endPosition) / 2;
            collider2D.transform.position = midPoint; 
                                              
            float angle = (Mathf.Abs(startPosition.y - endPosition.y) / Mathf.Abs(startPosition.x - endPosition.x));
            if ((startPosition.y < endPosition.y && startPosition.x > endPosition.x) || (endPosition.y < startPosition.y && endPosition.x > startPosition.x))
            {
                angle *= -1;
            }
            angle = Mathf.Rad2Deg * Mathf.Atan(angle);

            if (collider2D != null && !float.IsNaN(angle))
            {
                collider2D.transform.Rotate(0, 0, angle);
              
            }

        }
    }
}
