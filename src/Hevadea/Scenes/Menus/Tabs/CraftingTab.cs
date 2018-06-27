﻿using Hevadea.Craftings;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Scenes.Menus.Tabs
{
    public class CraftingListItem : ListItem
    {
        private Recipe _recipe;
        private ItemStorage _storage;

        public CraftingListItem(Recipe recipe, ItemStorage storage)
        {
            _recipe = recipe;
            _storage = storage;
        }

        public Recipe GetRecipe()
        {
            return _recipe;
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle host, GameTime gameTime)
        {
            Color color = Color.White * (_recipe.CanBeCrafted(_storage) ? 1f : 0.5f);
            _recipe.Result.GetSprite().Draw(spriteBatch, new Margins((int)(8 * Rise.Ui.ScaleFactor)).Apply(new Rectangle(host.Location, new Point(host.Height))), color);
            spriteBatch.DrawString(Ressources.FontRomulus, $"{_recipe.Result.GetName()} x{_recipe.Quantity}", new Margins(0, host.Height / 2, host.Height, 0).Apply(host), DrawText.Alignement.Left, DrawText.TextStyle.DropShadow, color, Rise.Ui.ScaleFactor);

            for (var i = 0; i < _recipe.Costs.Count; i++)
            {
                var c = _recipe.Costs[i];
                var dest = new Rectangle(host.X + host.Height + (host.Height / 2) * i, host.Y + host.Height / 2, host.Height / 2, host.Height / 2);
                c.Item.GetSprite().Draw(spriteBatch, dest, color);
                spriteBatch.DrawString(Ressources.FontRomulus, c.Count.ToString(), dest, DrawText.Alignement.Right, DrawText.TextStyle.DropShadow, color, Rise.Ui.ScaleFactor);
            }
        }
    }

    public class CraftingTab : InventoryTab
    {
        public ListWidget CraftingList { get; }

        public CraftingTab(GameState gameState, List<Recipe> recipies = null) : base(gameState)
        {
            Icon = new Sprite(Ressources.TileIcons, new Point(4, 2));
            CraftingList = new ListWidget
            {
                Dock = Dock.Fill,
                ItemHeight = 64,
            };

            var craftButton = new Button
            {
                Text = "Craft",
                Dock = Dock.Bottom,
            };

            craftButton.MouseClick += Craft;

            foreach (var recipe in recipies ?? RECIPIES.HandCrafted)
            {
                CraftingList.AddItem(new CraftingListItem(recipe, GameState.LocalPlayer.Entity.GetComponent<Inventory>().Content));
            }

            Content = new Container()
            {
                Childrens =
                {
                    new Label {Text = "Crafting", Font = Ressources.FontAlagard, Dock = Dock.Top},
                    craftButton, CraftingList,
                }
            };
        }

        private void Craft(Widget widget)
        {
            if (CraftingList.SelectedItem is CraftingListItem craft)
            {
                craft.GetRecipe().Craft(GameState.LocalPlayer.Entity.GetComponent<Inventory>().Content);
            }
        }
    }
}