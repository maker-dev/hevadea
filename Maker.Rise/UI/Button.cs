﻿using Maker.Rise.Enum;
using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Rise.UI
{
    public class Button : Control
    {
        public string Text { get; set; } = "Button";
        public Texture2D Icon { get; set; } = null;

        public bool Dancing { get; set; } = true;
        public Animation animation = new Animation();
        public Animation clickAnimation = new Animation();
        private Point OnMousClickPosition = Point.Zero;

        public Button()
        {
            animation.Speed = 1f;
            clickAnimation.Speed = 2f;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var invClickanim = (1f - clickAnimation.TwoPhases);
            
            var width = (int) (Bound.Width + 8f * animation.SinTwoPhases - 16f);
            var height = (int) (Bound.Height + 8f * animation.SinTwoPhases - 16f);

            var rectX = Bound.X + Bound.Width / 2 - width / 2;
            var rectY = Bound.Y + Bound.Height / 2 - height / 2;
            var rect = new Rectangle(rectX, rectY, width, height);

            var clickRectWidth =  (int)(width * clickAnimation.SinTwoPhases);
            var clickRectHeight = (int)(height* clickAnimation.SinTwoPhases);

            var clickRect = new Rectangle(rectX + (int)Math.Min(Math.Max(OnMousClickPosition.X - clickRectWidth / 2, 0f), width - clickRectWidth),
                                          rectY + (int)Math.Min(Math.Max(OnMousClickPosition.Y - clickRectHeight / 2, 0f), height - clickRectHeight),
                
                                          clickRectWidth, clickRectHeight);

            spriteBatch.FillRectangle(new Rectangle(rectX + 4, rectY + 4, width, height), Color.Black * 0.25f);
            
            spriteBatch.FillRectangle(rect, new Color(0x54, 0x52, 0x9b));
            spriteBatch.FillRectangle(rect, new Color(0x34, 0x33, 0x60) * animation.Linear);

            spriteBatch.FillRectangle(clickRect, Color.White * invClickanim);

            if (Icon != null)
            {
                var iconY = (Bound.Height / 2 - Icon.Height / 2);

                spriteBatch.Draw(Icon, new Vector2(Bound.X + iconY, Bound.Y + iconY), Color.White);
                spriteBatch.DrawString(EngineRessources.FontBebas, Text, Bound, Alignement.Center, TextStyle.DropShadow,
                    Color.White);
            }
            else
            {
                spriteBatch.DrawString(EngineRessources.FontBebas, Text, Bound, Alignement.Center, TextStyle.DropShadow,
                    Color.White, 1f + animation.TwoPhases / 3f);
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            if (OldMouseState == MouseState.None && MouseState == MouseState.Over)
            {
                Engine.Audio.PlaySoundEffect(EngineRessources.MenuPick);
            }

            animation.Show = MouseState == MouseState.Over || MouseState == MouseState.Down;

            if (Engine.Input.MouseLeftClick && Bound.Contains(Engine.Input.MousePosition))
            {
                clickAnimation.Reset();
                clickAnimation.Show = true;
                OnMousClickPosition = new Point(Engine.Input.MousePosition.X - Bound.X, Engine.Input.MousePosition.Y - Bound.Y);
                Engine.Audio.PlaySoundEffect(EngineRessources.MenuSelect);
            }

            if (clickAnimation.TwoPhases == 1f)
            {
                clickAnimation.Reset();
                clickAnimation.Show = false;
            }

            animation.Update(gameTime);
            clickAnimation.Update(gameTime);
        }
    }
}