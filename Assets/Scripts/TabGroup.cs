using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class TabGroup : MonoBehaviour
    {
        public List<GameObject> Panels;
        [SerializeField]
        private bool deselectAll;
        private GameObject CurrentPanel;


        private void OnEnable()
        {
            SelectPanel(0);
        }

        public void SelectPanel(int index)
        {
            if (index < 0 || index >= Panels.Count) return;
            if (CurrentPanel != null) CurrentPanel.SetActive(false);
            DeselectAll(Panels);
            CurrentPanel = Panels[index];
            CurrentPanel.SetActive(true);
        }

        public void DeselectAll()
        {
            if (!deselectAll) return;
            foreach (GameObject panel in Panels)
            {
                var tabGroup = panel.GetComponent<TabGroup>();
                if (tabGroup == null) continue;
                tabGroup.Panels.ForEach(panel => panel.SetActive(false));
                panel.SetActive(false);
            }
        }

        public void DeselectAll(List<GameObject> Panels)
        {
            if (!deselectAll) return;
            foreach (GameObject panel in Panels)
            {
                var tabGroup = panel.GetComponent<TabGroup>();
                if (tabGroup == null) continue;
                DeselectAll(tabGroup.Panels);
                tabGroup.Panels.ForEach(panel => panel.SetActive(false));
                panel.SetActive(false);
            }
        }
    }
}
