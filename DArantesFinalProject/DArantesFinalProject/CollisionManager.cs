/*
 * Neko Runner Game Application
 * Daiana Arantes Dec 2018
 * Final Project
 * Revision history
 * Class CollisionManager
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DArantesFinalProject
{
    class CollisionManager
    {

        public CollisionManager()
        {
        }

        public bool hasObstacleCollision(ActionScene actionScene, Cat cat)
        {
            Rectangle catRect = cat.getBounds();

            foreach (var item in actionScene.Components)
            {
                if (item is Obstacle)
                {
                    Rectangle obsRect = ((Obstacle)item).getBounds();
                    if (catRect.Intersects(obsRect))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool hasCoinCollision(ActionScene actionScene, Cat cat)
        {
            Rectangle catRect = cat.getBounds();

            foreach (var item in actionScene.Components)
            {
                if (item is Coin)
                {
                    Rectangle coinRect = ((Coin)item).getBounds();
                    if (catRect.Intersects(coinRect))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
