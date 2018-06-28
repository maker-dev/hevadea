﻿using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Framework.Graphic
{
    public static class Primitives
    {
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, float x, float y, float width, float height, Color color, float angle = 0.0f)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), null, color, angle, Vector2.Zero, new Vector2(width, height) * new Vector2(1f / texture.Width, 1f / texture.Height), SpriteEffects.None, 0);
        }

        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 location, Vector2 size, Color color, float angle = 0.0f)
        {
            spriteBatch.Draw(texture, location, null, color, angle, Vector2.Zero, size * new Vector2(1f / texture.Width, 1f / texture.Height), SpriteEffects.None, 0);
        }

        public static void PutPixel(this SpriteBatch spriteBatch, float x, float y, Color color)
        {
            PutPixel(spriteBatch, new Vector2(x, y), color);
        }

        public static void PutPixel(this SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), position, color);
        }

        public static void PutPixel(this SpriteBatch spriteBatch, Vector2 position, Color color, float size)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), position, null, color, 0, Vector2.Zero, new Vector2(size, size), SpriteEffects.None, 0);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color,
            float thickness)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color)
        {
            DrawLine(spriteBatch, point1, point2, color, 1.0f);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = Mathf.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color)
        {
            DrawLine(spriteBatch, point, length, angle, color, 1.0f);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), point, null, color, angle, Vector2.Zero, new Vector2(length, thickness), SpriteEffects.None, 0);
        }

        private static void DrawPolygon(SpriteBatch spriteBatch, Vector2 position, List<Vector2> points, Color color,
            float thickness)
        {
            if (points.Count < 2)
                return;

            for (int i = 1; i < points.Count; i++)
            {
                DrawLine(spriteBatch, points[i - 1] + position, points[i] + position, color, thickness);
            }
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color)
        {
            DrawPolygon(spriteBatch, center, Geometry.CreateCircle(radius, sides), color, 1.0f);
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color, float thickness)
        {
            DrawPolygon(spriteBatch, center, Geometry.CreateCircle(radius, sides), color, thickness);
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, float x, float y, float radius, int sides, Color color)
        {
            DrawPolygon(spriteBatch, new Vector2(x, y), Geometry.CreateCircle(radius, sides), color, 1.0f);
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, float x, float y, float radius, int sides, Color color, float thickness)
        {
            DrawPolygon(spriteBatch, new Vector2(x, y), Geometry.CreateCircle(radius, sides), color, thickness);
        }

        public static void DrawArc(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color)
        {
            DrawArc(spriteBatch, center, radius, sides, startingAngle, radians, color, 1.0f);
        }

        public static void DrawArc(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color, float thickness)
        {
            var arc = Geometry.CreateArc(radius, sides, startingAngle, radians);
            DrawPolygon(spriteBatch, center, arc, color, thickness);
        }

        public static void DrawRectangle(this SpriteBatch sb, int x, int y, int w, int h, Color color, float thickness = 1.0f)
        {
            DrawRectangle(sb, new Rectangle(x, y, w, h), color, thickness);
        }

        public static void DrawRectangle(this SpriteBatch sb, Point begin, Point end, Color color, float thickness = 1.0f)
        {
            DrawLine(sb, new Vector2(begin.X, begin.Y), new Vector2(end.X, begin.Y), color, thickness); // Right
            DrawLine(sb, new Vector2(begin.X + thickness, begin.Y + thickness), new Vector2(begin.X + thickness, end.Y - thickness), color, thickness); // left
            DrawLine(sb, new Vector2(begin.X, end.Y - thickness), new Vector2(end.X, end.Y - thickness), color, thickness); // bottom
            DrawLine(sb, new Vector2(end.X, begin.Y + thickness), new Vector2(end.X, end.Y - thickness), color, thickness); // right
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float thickness = 1.0f)
        {
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness); // Right
            DrawLine(spriteBatch, new Vector2(rect.X + thickness, rect.Y + thickness), new Vector2(rect.X + thickness, rect.Bottom - thickness), color, thickness); // left
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Bottom - thickness), new Vector2(rect.Right, rect.Bottom - thickness), color, thickness); // bottom
            DrawLine(spriteBatch, new Vector2(rect.Right, rect.Y + thickness), new Vector2(rect.Right, rect.Bottom - thickness), color, thickness); // right
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, RectangleF rect, Color color, float thickness = 1.0f)
        {
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness); // Right
            DrawLine(spriteBatch, new Vector2(rect.X + thickness, rect.Y + thickness), new Vector2(rect.X + thickness, rect.Bottom - thickness), color, thickness); // left
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Bottom - thickness), new Vector2(rect.Right, rect.Bottom - thickness), color, thickness); // bottom
            DrawLine(spriteBatch, new Vector2(rect.Right, rect.Y + thickness), new Vector2(rect.Right, rect.Bottom - thickness), color, thickness); // right
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color)
        {
            DrawRectangle(spriteBatch, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, 1.0f);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float thickness)
        {
            DrawRectangle(spriteBatch, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y),
                color, thickness);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), rect, color);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float angle, Vector2 origine)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), rect, null, color, angle, origine, SpriteEffects.None, 0);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float angle = 0.0f)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), location, null, color, angle, Vector2.Zero, size, SpriteEffects.None, 0);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float angle, Vector2 origin)
        {
            spriteBatch.Draw(Rise.Graphic.GetPixel(), location, null, color, angle, origin, size, SpriteEffects.None, 0);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color)
        {
            FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, 0.0f);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color, float angle)
        {
            FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, angle);
        }

        public static void FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color, float angle, float ox, float oy)
        {
            FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, angle, new Vector2(ox, oy));
        }
    }
}