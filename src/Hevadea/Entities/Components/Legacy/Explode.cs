﻿using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Utils;
using Hevadea.Tiles;
using Hevadea.Tiles.Components;

namespace Hevadea.Entities.Components
{
    public class Explode : EntityComponent
    {
        float _strenght;
        float _radius;
        bool _hasExplosed;

        public Explode(float strenght = 1f, float radius = 3f)
        {
            _strenght = strenght;
            _radius = radius;
            _hasExplosed = false;
        }

        public void Do()
        {
            if (_hasExplosed) return;
            _hasExplosed = true;

            var entities = Owner.Level.GetEntitiesOnArea(Owner.X, Owner.Y, _radius * Game.Unit);
            Owner.GameState.Camera.Thrauma += 0.2f;

            foreach (var e in entities)
            {
                if (e != Owner)
                {
                    var distance = Mathf.Distance(e.X, e.Y, Owner.X, Owner.Y);
                    e.GetComponent<Health>()?.Hurt(Owner, GetDammage(distance) * Rise.Rnd.NextFloat());

                    if (e == Owner.GameState.LocalPlayer?.Entity)
                    {
                        Owner.GameState.Camera.Thrauma += GetPower(distance);
                    }

                    if (Rise.Rnd.NextFloat() <= 0.3f)
                    {
                        e.GetComponent<Explode>()?.Do();
                        e.GetComponent<Breakable>()?.Break();
                        e.GetComponent<Flammable>()?.SetInFire();
                    }
                }
            }

            var pos = Owner.Coordinates;
            for (int x = -(int)_radius; x <= (int)_radius; x++)
            {
                for (int y = -(int)_radius; y <= (int)_radius; y++)
                {
                    var tilePos = new Coordinates(pos.X + x, pos.Y + y);
                    var tile = Owner.Level.GetTile(tilePos);
                    var distance = Mathf.Distance(tilePos.WorldX, tilePos.WorldY, Owner.X, Owner.Y);

                    if (distance < _radius * Game.Unit)
                    {
                        tile.Tag<DamageTile>()?.Hurt(GetDammage(distance) * Rise.Rnd.NextFloat(), tilePos, Owner.Level);

                        if (Rise.Rnd.NextDouble() * 1.25 < 1f - (distance / (_radius * Game.Unit)))
                        {
                            tile.Tag<BreakableTile>()?.Break(tilePos, Owner.Level);
                        }
                    }
                }
            }
        }

        float GetPower(float distance)
        {
            return (1f - (distance / (_radius * Game.Unit)));
        }

        public float GetDammage(float distance)
        {
            var value = _strenght * GetPower(distance);
            return value;
        }
    }
}