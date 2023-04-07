using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class ConstructionSite : MonoBehaviour, IDamageable
    {
        public float smallAmountOfHealthBeforeBuild = 50;
        public float timeNeededToBeBuild = 5;
        public float timeBuilding = 0f;
        [SerializeField]
        private GameObject constructionSiteObject;
        [SerializeField]
        private GameObject buildBuildingObject;
        [SerializeField]
        private MonoBehaviour turretShootingScript;
        [SerializeField]
        private SpriteRenderer buildingSpriteRenderer;
        [SerializeField]
        private ProgressBar progressBar;


        private Building building;

        private void Awake()
        {
            if(progressBar == null)
            {
                progressBar = GetComponentInChildren<ProgressBar>();
            }
        }

        public void StartBuilding(Building building)
        {
            this.building = building;
            buildBuildingObject.SetActive(false);
            constructionSiteObject.SetActive(true);
        }

        public void Damage(float damage)
        {
            smallAmountOfHealthBeforeBuild -= damage;
        }

        public bool ProgressWithBuild(float progressTime)
        {
            timeBuilding += progressTime;
            if (timeBuilding < timeNeededToBeBuild) {
                float percentage = timeBuilding / timeNeededToBeBuild;
                progressBar.FillProgressBar(percentage);
                return false;
            }
            BuildingBuild();
            return true;
        }

        public void SetColorToBlueprint(Color color)
        {
            buildingSpriteRenderer.color = color;
        }

        private void BuildingBuild()
        {
            Destroy(constructionSiteObject);
            buildBuildingObject.SetActive(true);
            EnableShooting();
            Destroy(this);
        }

        public void DisableShooting()
        {
            turretShootingScript.enabled = false;
        }

        public void EnableShooting()
        {
            turretShootingScript.enabled = true;
        }
    }
}
