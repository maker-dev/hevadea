﻿using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hevadea.Framework.UI
{
    public enum Dock
    {
        Top, Right, Bottom, Left, Fill, None
    }

    public enum MouseState
    {
        Over, Down, None
    }

    public class Widget
    {
        bool _enabled = true;

        public bool Enabled { get { return _enabled; } set { Rise.Ui.RefreshLayout(); _enabled = value; } }
        public bool Disabled { get => !Enabled; set => Enabled = !value; }
        public bool Focused { get => Rise.Ui.FocusWidget == this; }
        public bool CanGetFocus { get; set; }

        public Margins Padding { get; set; } = new Margins(0);
        public Margins Margin { get; set; } = new Margins(0);

        public Rectangle UnitBound { get; set; } = new Rectangle(0, 0, 64, 64);
        public Rectangle UnitHost { get => Padding.Apply(UnitBound); }
        public Point UnitOffset { get; set; } = Point.Zero;

        protected Rectangle Bound => Scale(UnitBound);
        protected Rectangle Host => Scale(UnitBound);
        protected Point Offset => Scale(UnitOffset);

        public Dock Dock { get; set; } = Dock.None;
        public Anchor Anchor { get; set; } = Anchor.TopLeft;
        public Anchor Origine { get; set; } = Anchor.TopLeft;
        public MouseState MouseState { get; set; } = MouseState.None;

        public delegate void WidgetEventHandler(Widget sender);
        public event WidgetEventHandler MouseClick;
        public event WidgetEventHandler MouseHold;

        public virtual void RefreshLayout()
        {}

        public virtual void Update(GameTime gameTime)
        {}

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {}

        public static float Scale(float v) => v * Rise.Ui.ScaleFactor;
        public static int Scale(int v) => (int)(v * Rise.Ui.ScaleFactor);
        public static Point Scale(Point p) => new Point(Scale(p.X), Scale(p.Y));
        public static Rectangle Scale(Rectangle rect) => new Rectangle(Scale(rect.Location), Scale(rect.Size));

        public void Enable() => Enabled = true;
        public void Disable() => Disabled = true;
        public void Toggle() => Enabled = !Enabled;

        public void UpdateInternal(GameTime gameTime)
        {
            if (Enabled)
            {
                HandleInput();
                Update(gameTime);
            }
        }

        public void DrawIternal(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Enabled)
            {
                Draw(spriteBatch, gameTime);
                if (Rise.Debug.UI)
                {
                    spriteBatch.DrawRectangle(Host, Color.Cyan);
                    spriteBatch.DrawRectangle(Bound, Color.Black);
                }
            }
        }

        void HandleInput()
        {
            if (Rise.Pointing.AreaOver(Bound))
            {
                MouseState = MouseState.Over;
                if (Rise.Pointing.AreaDown(Bound))
                {
                    if (CanGetFocus == true)
                    {
                        Rise.Ui.FocusWidget = this;
                    }

                    MouseState = MouseState.Down;
                    MouseHold?.Invoke(this);
                }

                if (Rise.Pointing.AreaClick(Bound))
                {
                    Logger.Log<Widget>(this.ToString() + "clicked!");
                    MouseClick?.Invoke(this);
                }
            }
            else
            {
                MouseState = MouseState.None;
            }
        }

        public Widget RegisterMouseClickEvent(WidgetEventHandler func)
        {
            MouseClick += func;
            return this;
        }

        public Widget RegisterMouseClickEvent(Action func)
        {
            MouseClick += (sender) => { func(); };
            return this;
        }
    }
}