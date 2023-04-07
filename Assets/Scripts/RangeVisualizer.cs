using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class RangeVisualizer : MonoBehaviour
    {
        [SerializeField]
        private float Range = 10;

        private void OnEnable()
        {
            transform.localScale = new Vector3 (Range, Range);
        }

        public void SetRange(float range)
        {
            Range = range;
            transform.localScale = new Vector3(range, range);
        }
    }
}
