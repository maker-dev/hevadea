﻿using Microsoft.Xna.Framework;

namespace Hevadea.Utils
{
    public enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }
    
    public static partial class Extensions
    {
        public static Point ToPoint(this Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    return new Point(0, -1);
                case Direction.East:
                    return new Point(1, 0);
                case Direction.South:
                    return new Point(0, 1);
                case Direction.West:
                    return new Point(-1, 0);
                default:
                    return new Point(0, 0);
            }
        }

        public static Vector2 ToVector2(this Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    return new Vector2(0, -1);
                case Direction.East:
                    return new Vector2(1, 0);
                case Direction.South:
                    return new Vector2(0, 1);
                case Direction.West:
                    return new Vector2(-1, 0);
                default:
                    return new Vector2(0, 0);
            }
        }
    }
}