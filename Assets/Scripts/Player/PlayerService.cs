using System;
using System.Collections.Generic;
using TrapLight.Events;
using TrapLight.Player.Black;
using TrapLight.Player.Explosion;
using TrapLight.Player.Wall;
using TrapLight.UI;
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
        private List<ExplosionController> allUsedExplosions;
        private WallController currentWallController;
        private UIService uiService;
        private EventService eventService;
        public PlayerService(PlayerSO playerSO, ExplosionSO explosionSO, WallSO wallSO)
        {
            this.playerSO = playerSO;
            this.explosionSO = explosionSO;
            this.wallSO = wallSO;
            blackParticle = new BlackParticleController(playerSO.BlackParticleSO.BlackParticlePrefab );
           
        }
        private void SubscribeToEvents() 
        { 
            eventService.OnWaveStart.AddListener(OnWaveStart); 
        }
        public void Init(UIService uiService, EventService eventService)
        {
            this.eventService = eventService;
            this.uiService = uiService;
            blackParticle.Init(playerSO.BlackParticleSO, this, uiService);
            explosionPool = new ExplosionPool(explosionSO.ExplosionViewPrefab);
            wallPool = new WallPool(wallSO.WallView);
            allWalls = new List<WallController>();
            allUsedExplosions = new List<ExplosionController>();
            SubscribeToEvents();
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
            allUsedExplosions.Add(explosionController);
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

        public void OnGameOver()
        {
            //uiService.SetGameOver(true);
            eventService.OnGameOver.InvokeEvent(true);
        }
        public bool IsExplosionEmpty()
        {
            return blackParticle.IsExplosionEmpty && allUsedExplosions.Find(x=> x.IsActive) == null;
            //return blackParticle.IsExplosionEmpty;
        }
        public void SetExplosionCount(int explosionCount)
        {
            allUsedExplosions.Clear();
            blackParticle.SetExplosionCount(explosionCount);
        }

        public void ClearWalls()
        { 
            foreach(var wall in allWalls)
            {
                wall.RemoveWall();
                wallPool.ReturnItem(wall);
            }
        }

        public void OnWaveStart(int waveID)
        {
            ClearWalls();
            blackParticle.ResetBlackParticle();
        }
       
        ~ PlayerService()
        {
            eventService.OnWaveStart.RemoveListener(OnWaveStart);
        }
    }
}
