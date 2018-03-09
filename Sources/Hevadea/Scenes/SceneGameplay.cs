﻿using Hevadea.Framework.Scening;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public class SceneGameplay : Scene
    {
        public readonly GameManager Game;

        public SceneGameplay(GameManager game)
        {
            Game = game;
            Game.CurrentMenuChange += Game_CurrentMenuChange;
            Container = new Panel();
        }

        private void Game_CurrentMenuChange(Menu oldMenu, Menu newMenu)
        {
            var p = (Panel) Container;
            p.Content = newMenu;
            Container.RefreshLayout();
        }

        public override void Load()
        {
            Game.Initialize();
        }


        public override void OnUpdate(GameTime gameTime)
        {
            Game.Update(gameTime);
        }

        public override void OnDraw(GameTime gameTime)
        {
            Game.Draw(gameTime);
        }

        public override void Unload()
        {
        }

        public override string GetDebugInfo()
        {
            return
                $@"World time: {Game.World.DayNightCycle.Time}
Time of the day: {Game.World.DayNightCycle.TimeOfTheDay} / {Game.World.DayNightCycle.CycleDuration}
Days : {Game.World.DayNightCycle.DayCount}
Current Stage: {Game.World.DayNightCycle.GetCurrentStage().Name} : {(int)Game.World.DayNightCycle.GetTimeOfTheCurrentStage()}/{(int)Game.World.DayNightCycle.GetCurrentStage().Duration}
Player pos {Game.Player.X} {Game.Player.Y}
";
        }
    }
}