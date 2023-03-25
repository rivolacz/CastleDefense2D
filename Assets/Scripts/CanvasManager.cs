using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class CanvasManager : MonoBehaviour
    {
        public List<Canvas> Canvases;
        private Canvas currentCanvas;

        public void EnableCanvas(Canvas canvas)
        {
            if (currentCanvas == canvas) return;
            Canvases.ForEach(canvas => canvas.enabled = false);
            canvas.enabled = true;
            currentCanvas = canvas;
        }

        public void ReenableCanvas(Canvas canvas)
        {
            if (currentCanvas == canvas)
            {
                canvas.enabled = !canvas.enabled;
            }
            else
            {
                canvas.enabled = true;
                currentCanvas = canvas;
            }
        }
    }
}
