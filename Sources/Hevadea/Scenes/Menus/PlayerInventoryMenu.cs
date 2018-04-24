﻿using Hevadea.Craftings;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Menus.Tabs;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Items;
using Hevadea.Registry;

namespace Hevadea.Scenes.Menus
{
    
    public abstract class InventoryTab : Tab
    {
        public GameManager.GameManager Game { get; }

        public InventoryTab(GameManager.GameManager game)
        {
            Game = game;
        }
    }

    public class PlayerInventoryMenu : Menu
    {
        private WidgetItemContainer _inventory;
        private CraftingTab         _crafting;
        private WidgetTabContainer  _sideMenu;

        public PlayerInventoryMenu(GameManager.GameManager game) : base(game)
        {
            InitializeComponents();
        }

        public void InitializeComponents(InventoryTab mainTab = null)
        {
            PauseGame = true;
            
            var r = new List<List<Recipe>>();
            foreach (var e in Game.MainPlayer.Level.GetEntitiesOnRadius(Game.MainPlayer.X, Game.MainPlayer.Y, 16))
            {
                var s = e.GetComponent<CraftingStation>();
                if (s != null)
                {
                    if (!r.Contains(s.Recipies))
                    {
                        r.Add(s.Recipies);
                    }
                }
            }
            
            var recipies = new List<Recipe>();
            recipies.AddRange(RECIPIES.HandCrafted);
            foreach (var i in r)
            {
                recipies.AddRange(i);
            }
            
            _inventory = new WidgetItemContainer(Game.MainPlayer.GetComponent<Inventory>().Content);
            _crafting = new CraftingTab(Game, recipies);

            _inventory.Dock = Dock.Fill;

            _inventory.MouseClick += (sender) =>
            {
                _inventory.HighlightedItem = _inventory.SelectedItem;
                Game.MainPlayer.HoldingItem = _inventory.SelectedItem;
            };

            var inventoryPanel = new WidgetFancyPanel
            {
                Content = new DockContainer
                {
                    Childrens =
                    {
                        new Label {Text = "Inventory", Font = Ressources.FontAlagard, Dock = Dock.Top},
                        _inventory,
                    }
                }
            };

            _sideMenu = new WidgetTabContainer
            {
                Padding = new Padding(16, 16),
                Tabs =
                {
                    _crafting,
                    new ContainerTab()
                    {
                        Icon = new Sprite(Ressources.TileIcons, new Point(0, 2)),
                        Childrens =
                        {
                            new PlayerStatesTab(),
                            new EquipmentTab(),
                        }
                    },
                    new SaveTab(Game),
                }
            };

            if (mainTab != null)
            {
                _sideMenu.Tabs.Insert(0, mainTab);
            }

            Content = new TileContainer
            {
                Flow = FlowDirection.RightToLeft,
                Childrens = { _sideMenu, inventoryPanel }
            };
        }
    }
}