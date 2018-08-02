﻿using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.Renderer;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public class Torch : Entity
    {
        public Torch()
        {
            AddComponent(new Breakable());
            AddComponent(new Dropable { Items = { new Drop(ITEMS.TORCH, 1f, 1, 1) } });
            AddComponent(new LightSource { IsOn = true, Color = Color.LightGoldenrodYellow * 0.75f, Power = 72 });
			AddComponent(new SpriteRenderer(new Sprite(Ressources.TileEntities, new Point(4, 0))));
        }
    }
}