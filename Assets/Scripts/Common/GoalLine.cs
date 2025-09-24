
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLine : MonoBehaviour
{
    public RaceManager raceManager;
    public float resetDistance = 5f;

    private void OnTriggerEnter(Collider other)
    {
        KartController kart = other.GetComponent<KartController>();
        if (kart != null)
        {
            if (!kart.passedGoal)
            {
                kart.AddLap();
                kart.passedGoal = true;
                kart.lastGoalPosition = kart.transform.position;

                Racer racer = raceManager.racers.Find(r => r.kartTransform == kart.transform);
                if (racer != null)
                {
                    racer.lapCount = kart.lapCount;
                }

                raceManager.CheckRaceEnd();
            }
        }

        CPULapController cpu = other.GetComponent<CPULapController>();
        if (cpu != null)
        {
            if (!cpu.passedGoal)
            {
                cpu.AddLap();
                cpu.passedGoal = true;
                cpu.lastGoalPosition = cpu.transform.position;

                Racer racer = raceManager.racers.Find(r => r.kartTransform == cpu.transform);
                if (racer != null)
                {
                    racer.lapCount = cpu.lapCount;
                }

                raceManager.CheckRaceEnd();
            }
        }
    }
}