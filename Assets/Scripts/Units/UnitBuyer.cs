using Project.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(PlayerUnitsManager))]
    public class UnitBuyer : MonoBehaviour
    {
        public UnitStorage UnitStorage;
        private PlayerUnitsManager playerUnitsManager;

        private void Awake()
        {
            playerUnitsManager = GetComponent<PlayerUnitsManager>();
        }

        public void BuyUnit(GameObject unitPrefab)
        {
            Unit unit = unitPrefab.GetComponent<Unit>();
            if(UnitStorage.CanStoreUnit(unit) && PlayerResources.CanBuyForAmount(unit.unitStats.BuyCost))
            {
                if (PlayerResources.FoodProduction >= playerUnitsManager.GetUnitsFoodUpkeep() + unit.unitStats.FoodUpkeep)
                {
                    Vector2 RandomOffset = Random.onUnitSphere * 5;
                    Instantiate(unitPrefab, UnitStorage.transform.position + (Vector3)RandomOffset, Quaternion.identity);
                    playerUnitsManager.NewUnit(unit);
                    UnitStorage.StoreUnit(unit);
                    UnitStorage.ReleaseUnit(unit);
                }
            }
            Debug.Log("Buying " + unitPrefab);
        }
    }
}
