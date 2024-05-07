using System.Collections.Generic;
using TrapLight.Player.Black;
using TrapLight.Player.Explosion;
using TrapLight.Player.Wall;
using UnityEngine;

namespace TrapLight.Player
{
    public class PlayerService
    {
        private BlackParticleController blackParticle;
        private PlayerSO playerSO;
        private ExplosionPool explosionPool;
        private ExplosionSO explosionSO;
        private WallSO wallSO;
        private WallPool wallPool;
        private List<WallController> allWalls;
        private WallController currentWallController;
        public PlayerService(PlayerSO playerSO, ExplosionSO explosionSO, WallSO wallSO)
        {
            this.playerSO = playerSO;
            this.explosionSO = explosionSO;
            this.wallSO = wallSO;
            blackParticle = new BlackParticleController(playerSO.BlackParticleSO.BlackParticlePrefab );
        }

        public void Init()
        {
            blackParticle.Init(playerSO.BlackParticleSO, this);
            explosionPool = new ExplosionPool(explosionSO.ExplosionViewPrefab);
            wallPool = new WallPool(wallSO.WallView);
            allWalls = new List<WallController>();
        }
        public void ReturnExplsionToPool(ExplosionController explosionController)
        {
            explosionPool.ReturnItem(explosionController);
        }
        public void SetExplosive()
        {
            ExplosionController explosionController = explosionPool.GetExplosion();
            explosionController.SetPosition(blackParticle.Position);
            explosionController.Init(this, explosionSO);
        }

        public void CreateWall()
        {
            currentWallController = wallPool.GetWall();
            currentWallController.Init();
            allWalls.Add(currentWallController);
        }

        public void OnWallDrawStart(Vector3 positionToSet)
        {
            CreateWall();
            currentWallController.OnWallDrawStart(positionToSet);
        }

        public void OnWallDrawing(Vector3 positionToSet)
        {
            currentWallController.OnWallDrawing(positionToSet);
        }

        public void OnWallDrawEnd(Vector3 positionToSet)
        {
            currentWallController.OnWallDrawEnd(positionToSet);
        }
        public void OnWallColliderDraw()
        {
            currentWallController.OnWallColliderDraw();
        }
    }
}
