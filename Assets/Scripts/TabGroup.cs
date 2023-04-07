using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> Panels;
        private GameObject CurrentPanel;

        public void SelectPanel(int index)
        {
            if (index < 0 || index >= Panels.Count) return;
            if (CurrentPanel != null) CurrentPanel.SetActive(false);
            CurrentPanel = Panels[index];
            CurrentPanel.SetActive(true);
        }
    }
}
