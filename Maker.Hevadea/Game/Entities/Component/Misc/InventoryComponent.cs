﻿using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Storage;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class InventoryComponent : EntityComponent, IDrawableComponent, IUpdatableComponent, ISaveLoadComponent
    {
        public Inventory Inventory { get; private set; }
        public bool AlowPickUp { get; set; } = true;
        private Item lastAdded;
        private readonly Animation anim = new Animation();

        public InventoryComponent(int slotCount)
        {
            Inventory = new Inventory(slotCount);
        }
        
        public bool Pickup(Item item)
        {
            if (AlowPickUp && Inventory.Add(item))
            {
                anim.Reset();
                anim.Show = true;
                anim.Speed = 0.50f;

                lastAdded = item;
                return true;
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            anim.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var v = 1f - anim.SinTwoPhases;
            lastAdded?.GetSprite().Draw(spriteBatch, new Vector2(Owner.X + Owner.Width / 2f - 8 * v, Owner.Y+ Owner.Height / 2 - 24 * v), v, Color.White);
        }

        public void OnSave(EntityStorage store)
        {
            store.Set(nameof(Inventory), Inventory.Items);
        }

        public void OnLoad(EntityStorage store)
        {
            Inventory.Items = store.Get(nameof(Inventory), Inventory.Items);
        }
    }
}