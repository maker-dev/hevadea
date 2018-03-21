﻿using Hevadea.Framework.Graphic.SpriteAtlas;

using Hevadea.Game.Entities.Components;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;

using Hevadea.Game.Items;
using Hevadea.Game.Registry;

using Hevadea.Scenes.Menus;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities
{
    public class EntityCraftingBench : Entity
    {
        private readonly Sprite _sprite;

        public EntityCraftingBench()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 0));

            Attachs(
                new Breakable(),
                new Dropable {Items = {new Drop(ITEMS.CRAFTING_BENCH, 1f, 1, 1)}},
                new Pushable(){ CanBePushByAnything = true },
                new Move(),
                new Colider(new Rectangle(-6, -2, 12, 8)),
                Attach(new Burnable(1f))
            );

            Attach(new Interactable()).Interacted += OnInteracted;
      
            Attach(new Pickupable(_sprite));
        }

        private void OnInteracted(object sander, InteractEventArg e)
        {
            if (e.Entity.Has<Inventory>())
                Game.CurrentMenu = new MenuPlayerInventory(e.Entity, RECIPIES.BenchCrafted, Game);
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 8, (int) Y - 8, 16, 16), Color.White);
        }
    }
}