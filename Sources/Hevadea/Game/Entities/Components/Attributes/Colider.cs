﻿using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Hevadea.Game.Entities.Components.Attributes
{
    public class Colider : EntityComponent, IEntityComponentDrawableOverlay
    {
        public Func<Entity, bool> ColidePredicat = b => { return true;};
        private RectangleF _hitbox;

        public Colider(Rectangle hitbox)
        {
            _hitbox = hitbox;
        }

        public RectangleF GetHitBox()
        {
            return new RectangleF(AttachedEntity.X + _hitbox.X, AttachedEntity.Y + _hitbox.Y, _hitbox.Width, _hitbox.Height);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)(AttachedEntity.X + _hitbox.X), (int)(AttachedEntity.Y + _hitbox.Y), (int)_hitbox.Width, (int)_hitbox.Height);
        }

        public List<Entity> GetTouchingEntities()
        {
            return AttachedEntity.Level.GetEntitiesOnArea(ToRectangle());
        }

        public bool CanCollideWith(Entity e)
        {
            return ColidePredicat(e);
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Rise.DebugUI)
                spriteBatch.DrawRectangle(ToRectangle(), Color.Red, 1 / AttachedEntity.Game.Camera.Zoom);
        }
    }
}