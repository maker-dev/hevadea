﻿using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Components;
using Hevadea.Game.Entities.Components.Attributes;
using Hevadea.Game.Entities.Components.Interaction;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.Game.Entities
{
    public class EntityChest : Entity
    {
        private readonly Sprite _sprite;

        public EntityChest()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(0, 1));

            AddComponent(new Move());
            AddComponent(new Inventory(128));
            AddComponent(new Pickupable(_sprite));
            AddComponent(new Colider(new Rectangle(-6, -2, 12, 8)));
            AddComponent(new Pushable() {CanBePushByAnything = true});
            AddComponent(new Health(10)).Killed += EntityDie;
            AddComponent(new Interactable()).Interacted += EntityInteracte;
            AddComponent(new Pickupable(_sprite));
            AddComponent(new Burnable(1f));
        }

        private void EntityInteracte(object sender, InteractEventArg args)
        {
            if (args.Entity.HasComponent<Inventory>())
                Game.CurrentMenu = new MenuItemContainer(args.Entity, this, Game);
        }

        private void EntityDie(object sender, EventArgs args)
        {
            GetComponent<Inventory>().Content.DropOnGround(Level, X, Y);
            ITEMS.CHEST.Drop(Level, X, Y, 1);
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 8, (int) Y - 8, 16, 16), Color.White);
        }
    }
}