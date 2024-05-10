
using System.Collections.Generic;
using UnityEngine;
using TrapLight.Utilities;

namespace TrapLight.Player.Explosion
{
    public class ExplosionPool : GenericObjectPool<ExplosionController>
    {
        ExplosionView explosionView;
        private Transform explosionContainer;

        public ExplosionPool(ExplosionView explosionView)
        {
            this.explosionView = explosionView;
          
            this.explosionContainer = new GameObject("Explosion Container").transform;
        }

        protected override ExplosionController CreateItem() => new ExplosionController(explosionView, explosionContainer);
        public ExplosionController GetExplosion()
        {
            ExplosionController explosion = GetItem();
          
            return explosion;
        }

    }
}


