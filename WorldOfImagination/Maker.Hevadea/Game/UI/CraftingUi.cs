﻿using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Registry;
using Maker.Rise;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.UI
{
    public class CraftingUi : Control
    {
        Inventory Inventory;
        public CraftingUi(Inventory i)
        {
            Inventory = i;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var index = 0;
            var maxY = Bound.Height / 64;
            var maxX = Bound.Width / 64;

            foreach (var c in RECIPIES.HAND_CRAFTED)
            {
                if (c != null)
                {
                    bool canBeCrafted = c.CanBeCrafted(Inventory);

                    if (canBeCrafted)
                    {
                        var sx = index % maxX;
                        var sy = index / maxX;

                        var rect = new Rectangle(Bound.X + sx * 64, Bound.Y + sy * 64, 48, 48);
                        spriteBatch.FillRectangle(rect, Color.Black * 0.25f);
                        c.Result.GetSprite().Draw(spriteBatch, rect, Color.White);
                        spriteBatch.DrawString(Ressources.font_romulus, $"x{c.Quantity}", new Vector2(rect.X + 32, rect.Y + 32), Color.White);

                        if (rect.Contains(Engine.Input.MousePosition) && Engine.Input.MouseLeftClick)
                        {
                            c.Craft(Inventory);
                        }

                        index++;
                    }
                }
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {

        }
    }
}