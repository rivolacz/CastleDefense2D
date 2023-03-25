using Project.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class UnitStorage : MonoBehaviour
    {
        public int MaxStoredUnits = 10;
        public List<Unit> UnitsStored = new List<Unit>();


        public void StoreUnit(Unit unit)
        {
            if (UnitsStored.Count > MaxStoredUnits) return;
            UnitsStored.Add(unit);
            unit.gameObject.SetActive(false);
        }

        public bool CanStoreUnit(Unit unit)
        {
            if(UnitsStored.Count > MaxStoredUnits) return false;
            return true;
        }

        public void ReleaseUnit(Unit unit)
        {
            UnitsStored.Remove(unit);
            unit.gameObject.SetActive(true);
        }
    }
}
