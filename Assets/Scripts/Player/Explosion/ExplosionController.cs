
using UnityEngine;
namespace TrapLight.Player.Explosion
{
    public class ExplosionController
    {
        private ExplosionView explosionView;
        private PlayerService playerService;
        public Vector3 Position => explosionView.transform.position;
        public ExplosionController(ExplosionView explosionView, Transform explosionContainer)
        {
            this.explosionView = Object.Instantiate(explosionView, explosionContainer);
            this.explosionView.SetController(this);
        }
        public void Init( PlayerService playerService)
        {
            this.playerService = playerService;
            explosionView.gameObject.SetActive(true);
            explosionView.Init();
        }
        public void SetPosition(Vector3 positionToSet)
        {
            explosionView.transform.position = positionToSet;
        }

        public void ResetExplosion()
        {
            explosionView.gameObject.SetActive(false);
            playerService.ReturnExplsionToPool(this);
        }
    }
}
