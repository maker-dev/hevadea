﻿using Hevadea.Framework;
using Hevadea;

namespace Hevadea.Registry
{
    public static class REGISTRY
    {
        public static void Initialize()
        {
            Logger.Log("Initializing game registery.");

            TILES.Initialize();
            ENTITIES.Initialize();
            ITEMS.Initialize();
            RECIPIES.InitializeHandCraftedRecipe();
            LEVELS.Initialize();
            GENERATOR.Initialize();
            TILES.AttachRender();
            TILES.AttachTags();
            ITEMS.AttachTags();
        }
    }
}