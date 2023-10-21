using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameModeBase : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] float timeLimit = 90f;
    [SerializeField] TextMeshProUGUI timerDisplay;

    public UnityEvent OnTimerEnd;

    private float timeRemaining;

    private void Start()
    {
        timeRemaining = timeLimit;
        RefreshTimerDisplay();
    }
    private void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            RefreshTimerDisplay();
        }
        else
        {
            if (timeRemaining < 0) timeRemaining = 0;
            RefreshTimerDisplay();
            OnTimerEnd?.Invoke();
        }

    }
    private void RefreshTimerDisplay()
    {
        if(timerDisplay != null)
        {
            timerDisplay.text = Utility.DisplayTimeMinutes(timeRemaining);
        }
    }
}
