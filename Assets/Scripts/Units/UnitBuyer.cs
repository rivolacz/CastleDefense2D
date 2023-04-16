using Project.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    [RequireComponent(typeof(PlayerUnitsManager))]
    public class UnitBuyer : MonoBehaviour
    {
        public UnitStorage UnitStorage;
        private PlayerUnitsManager playerUnitsManager;

        [SerializeField]
        private Button swordsmanBuyButton;
        [SerializeField]
        private Button knightBuyButton;

        private void Awake()
        {
            playerUnitsManager = GetComponent<PlayerUnitsManager>();
            if (!UpgradesManager.Upgrades.KnightUpgrades.KnightResearched)
            {
                knightBuyButton.gameObject.SetActive(false);
            }
            if (!UpgradesManager.Upgrades.SwordsmanUpgrades.Researched)
            {
                swordsmanBuyButton.gameObject.SetActive(false);
            }
        }

        public void BuyUnit(GameObject unitPrefab)
        {
            Unit unit = unitPrefab.GetComponent<Unit>();
            if (GameData.CanAfford(unit.unitStats.BuyCost))
            {
                GameData.Buy(unit.unitStats.BuyCost);
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
