using TrapLight.Player.Black;
using TrapLight.Player.Explosion;

namespace TrapLight.Player
{
    public class PlayerService
    {
        private BlackParticleController blackParticle;
        private PlayerSO playerSO;
        private ExplosionPool explosionPool;
        public PlayerService(PlayerSO playerSO)
        {
            this.playerSO = playerSO;
            blackParticle = new BlackParticleController(playerSO.BlackParticleSO.BlackParticlePrefab );
        }

        public void Init()
        {
            blackParticle.Init(playerSO.BlackParticleSO, this);
            explosionPool = new ExplosionPool(playerSO.BlackParticleSO.ExplosionViewPrefab);
        }
        public void ReturnExplsionToPool(ExplosionController explosionController)
        {
            explosionPool.ReturnItem(explosionController);
        }
        public void SetExplosive()
        {
            ExplosionController explosionController = explosionPool.GetExplosion();
            explosionController.SetPosition(blackParticle.Position);
            explosionController.Init(this);
        }
    }
}
