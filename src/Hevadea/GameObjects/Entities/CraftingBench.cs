﻿using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.States;
using Hevadea.GameObjects.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities
{
    public class CraftingBench : Entity
    {
        private readonly Sprite _sprite;

        public CraftingBench()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 0));

            AddComponent(new Breakable());
            AddComponent(new Burnable(1f));
            AddComponent(new Colider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new CraftingStation(RECIPIES.BenchCrafted));
            AddComponent(new Dropable { Items = { new Drop(ITEMS.CRAFTING_BENCH, 1f, 1, 1) } });
            AddComponent(new Move());
            AddComponent(new Pickupable(_sprite));
            AddComponent(new Pushable());
            AddComponent(new Shadow());
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int)X - 8, (int)Y - 8, 16, 16), Color.White);
        }
    }
}