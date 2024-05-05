using TrapLight.Player.Black;

namespace TrapLight.Player
{
    public class PlayerService
    {
        private BlackParticleController blackParticle;
        private PlayerSO playerSO;
        
        public PlayerService(PlayerSO playerSO)
        {
            this.playerSO = playerSO;
            blackParticle = new BlackParticleController(playerSO.BlackParticleSO.BlackParticlePrefab );
        }

        public void Init()
        {
            blackParticle.Init(playerSO.BlackParticleSO);
        }
    }
}
