﻿using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.States;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public class SwimmingEffect : EntityDrawSystem
    {
        public static readonly Point Size = new Point(16, 8);

        public SwimmingEffect()
        {
            Filter.AllOf(typeof(Swim));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            if (entity.IsSwimming() && entity.GetComponent<Swim>().ShowAnimation)
            {
                var frame = (int)(gameTime.TotalGameTime.TotalSeconds * 4) % 4;

                Rectangle source = new Rectangle(new Point(16 * frame, 0), Size);
                Vector2 position = entity.Position - Size.ToVector2() / 2;

                pool.Tiles.Draw(Ressources.ImgWater, position, source, Color.White);
            }
        }
    }
}
