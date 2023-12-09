using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStatsProfile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cooldownDisplay;
    [SerializeField] private TextMeshProUGUI speedDisplay;
    [SerializeField] private TextMeshProUGUI rangeDisplay;

    private void Start()
    {
        //Debug.LogWarning("Apply Profile");
        //Debug.LogFormat("[Profile] C:{0} S:{1} R:{2}", GameInstance.Profile.cooldownTime, GameInstance.Profile.playerSpeed, GameInstance.Profile.collectionRange);
    }
    private void Update()
    {
        //StatsProfile copiedProfile = GameInstance.Profile;

        if(cooldownDisplay != null)
            if(cooldownDisplay.text != GameInstance.Profile.cooldownTime.ToString()) cooldownDisplay.text = GameInstance.Profile.cooldownTime.ToString();
        
        if(speedDisplay != null)
            if(speedDisplay.text != GameInstance.Profile.playerSpeed.ToString()) speedDisplay.text = GameInstance.Profile.playerSpeed.ToString();

        if(rangeDisplay != null)
            if(rangeDisplay.text != GameInstance.Profile.collectionRange.ToString()) rangeDisplay.text = GameInstance.Profile.collectionRange.ToString();
    }
}
