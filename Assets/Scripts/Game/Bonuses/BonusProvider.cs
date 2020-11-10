using System.Collections.Generic;
using UnityEngine;

namespace Game.Bonuses
{
    public static class BonusProvider
    {
        public static BaseBonus GetRandomBonus()
        {
            var allBonuses = new List<BaseBonus>()
            {
                new ExpandRacketBonus()
            };

            return allBonuses[Random.Range(0, allBonuses.Count)];
        }

        public static BaseBonus TryGetRandomBonus(float probabilityPercent)
        {
            return Random.Range(0f, 1f) <= probabilityPercent ? GetRandomBonus() : null;
        }
    }
}