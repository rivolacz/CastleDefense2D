using Project.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Project
{
    public class PlayerUnitsManager : MonoBehaviour
    {
        public List<Unit> AllPlayerUnits = new List<Unit>();

        public void NewUnit(Unit unit)
        {
            AllPlayerUnits.Add(unit);
        }

        public void LostUnit(Unit unit)
        {
            AllPlayerUnits.Remove(unit);
        }
    }
}
