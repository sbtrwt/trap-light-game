using System;
using UnityEngine;
using TrapLight.Utilities;

namespace TrapLight.Player.Wall
{
    public class WallPool : GenericObjectPool<WallController>
    {
        WallView wallView;
        private Transform wallContainer;

        public WallPool(WallView wallView)
        {
            this.wallView = wallView;

            this.wallContainer = new GameObject("Wall Container").transform;
        }

        protected override WallController CreateItem() => new WallController(wallView, wallContainer);
        public WallController GetWall()
        {
            WallController wall = GetItem();

            return wall;
        }

    }
}
