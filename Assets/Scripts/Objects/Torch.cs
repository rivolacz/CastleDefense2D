using Project.Waves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Torch : MonoBehaviour
{
    private readonly int isDayHash = Animator.StringToHash("isDay");
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        WaveManager.OnDayStarted += DayStarted;
        WaveManager.OnNightStarted += NightStarted;
    }

    private void OnDisable()
    {
        WaveManager.OnDayStarted -= DayStarted;
        WaveManager.OnNightStarted -= NightStarted;
    }

    public void DayStarted()
    {
        animator.SetBool(isDayHash, true);
    }

    public void NightStarted()
    {
        animator.SetBool(isDayHash, false);
    }
}
