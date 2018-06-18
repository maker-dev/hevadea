﻿using Hevadea.Framework;
using Hevadea.GameObjects.Items;
using System.Collections.Generic;

namespace Hevadea.GameObjects.Entities.Components
{
    public class Dropable : EntityComponent
    {
        public List<Drop> Items { get; set; } = new List<Drop>();

        public void Drop()
        {
            var pos = Owner.Coordinates;

            foreach (var d in Items)
                if (Rise.Rnd.NextDouble() < d.Chance) d.Item.Drop(Owner.Level, pos, Rise.Rnd.Next(d.Min, d.Max));
        }
    }
}