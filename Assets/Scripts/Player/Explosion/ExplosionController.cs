

using TrapLight.Player.Black;
using TrapLight.Wave.Light;
using UnityEngine;
namespace TrapLight.Player.Explosion
{
    public class ExplosionController
    {
        private ExplosionView explosionView;
        private PlayerService playerService;
        private ExplosionState currentState;
        private ExplosionSO explosionSO;
        public Vector3 Position => explosionView.transform.position;
        public ExplosionController(ExplosionView explosionView, Transform explosionContainer)
        {
            this.explosionView = UnityEngine.Object.Instantiate(explosionView, explosionContainer);
            this.explosionView.SetController(this);
        }
        public void Init( PlayerService playerService, ExplosionSO explosionSO)
        {
            this.playerService = playerService;
            this.explosionSO = explosionSO;
            explosionView.gameObject.SetActive(true);
            currentState = ExplosionState.ACTIVE;
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
            currentState = ExplosionState.DETONATED;
        }
        public void IsGameOver()
        {
            if (playerService.IsExplosionEmpty())
            {
                playerService.OnGameOver();
            }
        }
        public void OnHitLightParticle(LightParticleController controller)
        {
           if(currentState == ExplosionState.ACTIVE)
            {
                controller.TakeDamage(explosionSO.Damage);
            }
        }
        public void OnHitBlackParticle(BlackParticleController blackController)
        {
            if (currentState == ExplosionState.ACTIVE)
            {
                blackController.TakeDamage(explosionSO.Damage);
            }
        }
        public bool IsActive => currentState == ExplosionState.ACTIVE;

        private enum ExplosionState
        {
            ACTIVE,
            DETONATED
        }
    }
}
