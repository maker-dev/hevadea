﻿using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
    public class UiManager
    {
        private float _scaleFactor = 1.0f;

        public bool Enabled { get; set; } = true;

        public SpriteFont DefaultFont { get; set; }
        public SpriteFont DebugFont { get; set; }
        public float ScaleFactor { get => _scaleFactor * Rise.Platform.GetSceenScaling(); set => _scaleFactor = value; }
        public Widget FocusWidget { get; set; }

        public void RefreshLayout()
        {
            Rise.Scene.GetCurrentScene().RefreshLayout();
        }
    }
}