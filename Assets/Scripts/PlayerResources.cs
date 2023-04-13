using Project.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public static class PlayerResources
    {
        public static float Money { get; private set; } = 100;
        public static int FoodProduction { get; private set; } = 20;

        public static void GainMoney(float amount)
        {
            Money += amount;
        }

        public static bool CanBuyForAmount(float amount)
        {
            if (Money >= amount) return true;
            return false;
        }

        public static void SubtractMoney(float amount)
        {
            if(Money < amount) return;
            Money -= amount;
        }

        public static bool CanBuyUnit(Unit unit)
        {
            if (!CanBuyForAmount(unit.unitStats.BuyCost)) return false;
            return true;
        }
    }
}
