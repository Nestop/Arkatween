using System.Collections.Generic;
using Game.Bonuses.Config;
using UnityEngine;

namespace Game.Bonuses.Base
{
    public static class BonusProvider
    {
        public static Bonus GetRandomBonus()
        {
            var allBonuses = new List<Bonus>
            {
                new WidthRacketBonus(new WidthRacketBonusConfig(10f, Color.green)),
                new WidthRacketBonus(new WidthRacketBonusConfig(-10f, Color.red)),
                new SpeedBallBonus(new SpeedBallBonusConfig(0.6f, 5f, Color.cyan)),
                new SpeedBallBonus(new SpeedBallBonusConfig(1.9f, 5f, Color.white)),
                new RageBallBonus(new SpeedBallBonusConfig(1.5f, 6f, Color.red)),
                new CloneBallBonus(new CloneBallBonusConfig(2))
            };

            return allBonuses[Random.Range(0, allBonuses.Count)];
        }

        public static Bonus TryGetRandomBonus(float probabilityPercent)
        {
            return Random.Range(0f, 1f) <= probabilityPercent ? GetRandomBonus() : null;
        }
    }
}