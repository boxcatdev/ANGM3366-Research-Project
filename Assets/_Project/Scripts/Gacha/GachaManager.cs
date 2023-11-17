using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] GameObject rollCanvas;

    private void Start()
    {
        SwitchToUpgradeCanvas();
    }
    public void SwitchToRollCanvas()
    {
        if(upgradeCanvas != null && rollCanvas != null)
        {
            upgradeCanvas.SetActive(false);
            rollCanvas.SetActive(true);
        }
    }
    public void SwitchToUpgradeCanvas()
    {
        if (upgradeCanvas != null && rollCanvas != null)
        {
            upgradeCanvas.SetActive(true);
            rollCanvas.SetActive(false);
        }
    }
}
