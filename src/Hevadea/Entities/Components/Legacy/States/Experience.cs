﻿namespace Hevadea.Entities.Components.States
{
    public class Experience : EntityComponent
    {
        public int XP { get; private set; } = 0;

        public void TakeXP(int points)
        {
            XP += points;
        }

        public void TakeXP(XpOrb orb)
        {
            TakeXP(orb.Value);
        }
    }
}