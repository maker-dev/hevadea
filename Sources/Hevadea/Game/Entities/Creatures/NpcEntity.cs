﻿using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Render;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Creatures
{
    public class NpcEntity : Entity
    {
        public NpcEntity()
        {
            Components.Adds
            (
                new NpcRender(new Sprite(Ressources.TileCreatures, 2, new Point(16, 32))),
                new Interact(),
                new Attack(),
                new Target()
            );
        }
    }
}