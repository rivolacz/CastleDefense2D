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
            if (PlayerResources.CanBuyForAmount(unit.unitStats.BuyCost))
            {
                Vector2 RandomOffset = Random.onUnitSphere * 6;
                Instantiate(unitPrefab, UnitStorage.transform.position + (Vector3)RandomOffset, Quaternion.identity);
                playerUnitsManager.NewUnit(unit);
                UnitStorage.StoreUnit(unit);
                UnitStorage.ReleaseUnit(unit);
            }
            Debug.Log("Buying " + unitPrefab);
        }
    }
}
