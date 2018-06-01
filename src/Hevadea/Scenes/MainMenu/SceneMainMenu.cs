﻿using Hevadea.Framework;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Platform;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Framework.Utils;
using Hevadea.Loading;
using Hevadea.Registry;
using Hevadea.Scenes.MainMenu.Tabs;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace Hevadea.Scenes.MainMenu
{
    public class SceneMainMenu : Scene
    {
        public override void Load()
        {
            Rise.Scene.SetBackground(new Random().NextValue(Ressources.ParalaxeForest, Ressources.ParalaxeMontain));

            var hevadeaLogo = new Label { Text = "Hevadea", Anchor = Anchor.Center, Origine = Anchor.Bottom, Font = Ressources.FontAlagardBig, TextSize = 1.5f };
            var creators = new Label { Text = "© 2017-2018 MAKER", Anchor = Anchor.Bottom, Origine = Anchor.Bottom, Font = Ressources.FontRomulus, TextSize = 1f };
            var continueButton = new Button
            {
                Text = "Continue",
                Anchor = Anchor.Center,
                Origine = Anchor.Top,
                UnitBound = new Rectangle(0, 0, 256, 64),
                UnitOffset = new Point(0, 64)
            }
                .RegisterMouseClickEvent(ContinueLastGame);
            var menu = new WidgetTabContainer
            {
                Anchor = Anchor.Center,
                Origine = Anchor.Center,
                UnitBound = new Rectangle(0, 0, 600, 720),
                TabAnchore = Rise.Platform.Family == PlatformFamily.Mobile ? TabAnchore.Bottom : TabAnchore.Left,
                Tabs =
                {
                    new Tab
                    {
                        Icon = new Sprite(Ressources.TileIcons, new Point(0,4)),
                        Content = new Container()
                        {
                            Childrens =
                            {
                                hevadeaLogo,
                                creators,
                            }
                        }
                    },

                    new TabNewWorld(),
                    new TabLoadWorld(),
                    new TabMultiplayerConnect(),
                    new TabOption(),
                }
            };

            var container = (Container)menu.Tabs[0].Content;

            if (File.Exists(Rise.Platform.GetStorageFolder() + "/.lastgame"))
            {
                container.Childrens.Add(continueButton);
            }

            if (Rise.Platform.Family == PlatformFamily.Mobile)
            {
                var generateButton = new Button
                {
                    Text = "New",
                    Dock = Dock.None,
                    UnitBound = new Rectangle(0, 0, 256, 64),
                    UnitOffset = new Point(0, 128 + 8),
                    Anchor = Anchor.Center,
                    Origine = Anchor.Top,
                }
                .RegisterMouseClickEvent((sender) =>
				{
				var job = Jobs.GenerateWorld;
					job.SetArguments(new Jobs.WorldGeneratorInfo(Game.GetSaveFolder() + $"world/", Rise.Rnd.NextInt(), GENERATOR.DEFAULT));
                    
                    job.Finish += (s, e) =>
                    {
                        Game game = (Game)((Job)s).Result;
                        game.Initialize();
                        Rise.Scene.Switch(new SceneGameplay(game));
                    };
                    Rise.Scene.Switch(new LoadingScene(job));
                });
                container.Childrens.Add(generateButton);

                Container = container;
            }
            else
            {
                Container = new Container
                {
                    Childrens = { menu, new Label { UnitBound = new Rectangle(0, 0, 256, 64), Text = "v0.1.0", Anchor = Anchor.BottomRight, Origine = Anchor.BottomRight, Font = Ressources.FontRomulus } },
                };
            }
        }

        void ContinueLastGame(Widget sender)
        {
			var lastGame = Game.GetLastGame();

            if (lastGame != null)
            {
				var job = Jobs.LoadWorld.SetArguments(new Jobs.WorldLoadInfo(lastGame));
                job.Finish += (task, e) => Rise.Scene.Switch(new SceneGameplay((Game)((Job)task).Result));
                Rise.Scene.Switch(new LoadingScene(job));
            }
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
        }

        public override void OnDraw(GameTime gameTime)
        {
        }
    }
}