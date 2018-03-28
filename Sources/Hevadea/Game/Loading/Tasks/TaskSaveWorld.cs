﻿using Hevadea.Framework.Utils;
using Hevadea.Framework.Utils.Json;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Hevadea.Game.Worlds;
using System.Collections.Generic;
using System.IO;
using Hevadea.Framework;
using Hevadea.Framework.Threading;

namespace Hevadea.Game.Loading.Tasks
{
    public class TaskSaveWorld : LoadingTask
    {
        public override void Task(GameManager game)
        {
            SetStatus("Saving world...");

            var levelsName = new List<string>();

            Directory.CreateDirectory(game.SavePath);
            
            // Saves levels
            
            foreach (var level in game.World.Levels)
            {
                SetStatus($"Saving the '{level.Name}'...");
                var path = game.GetLevelSavePath(level);
                var levelJson = SaveLevel(level);
                
                SetStatus($"Writing the '{level.Name}' to disk...");
                File.WriteAllText(path, levelJson);
                
                SetStatus($"Writing the '{level.Name}' minimap to disk...");
                
                var task = new AsyncTask(() =>
                {
                    var fs = new FileStream(game.GetLevelMinimapSavePath(level), FileMode.OpenOrCreate);
                    level.Map.SaveAsPng(fs, level.Width, level.Height);
                    fs.Close();                    
                });
                
                Rise.AsyncTasks.Add(task);

                while (!task.Done){}
                
                levelsName.Add(level.Name);
                
                Logger.Log<TaskSaveWorld>($"Level saved to '{path}'.");
            }
            
            // Save world

            var worldStorage = new WorldStorage()
            {
                Time =  game.World.DayNightCycle.Time,
                PlayerSpawnLevel = game.World.PlayerSpawnLevel,
                Levels = levelsName
            };
            
            Directory.CreateDirectory(game.GetRemotePlayerPath());
            
            File.WriteAllText(game.GetPlayerSaveFile(), game.MainPlayer.Save().ToJson());
            File.WriteAllText(game.GetGameSaveFile(),   worldStorage.ToJson());
        }


        public static string SaveLevel(Level level)
        {
            var storage = new LevelStorage()
            {
                Id = level.Id,
                Name = level.Name,
                Type = level.Properties.Name,
                    
                Width = level.Width,
                Height = level.Height,
                    
                Tiles = level.Tiles,
                TilesData = level.TilesData
            };

            foreach (var e in level.Entities)
            {
                if (!ENTITIES.SaveExluded.Contains(e.Blueprint))
                {
                    storage.Entities.Add(e.Save());
                }
            }
            
            return storage.ToJson();
        }
    }
}
