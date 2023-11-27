using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProgressEnemy : MonoBehaviour
{
    [Range(0, 3)] public int speedUnit = 0;
    [Range(0, 3)] public int armorUnit = 0;
    [Range(0, 3)] public int damageUnit = 0;

    [Range(0, 3)] public int armorPlanet = 0;
    [Range(0, 3)] public int draftPlanet = 0;
    [Range(0, 3)] public int growthPlanet = 0;

    public void ResetProgress()
    {
        speedUnit = 0;
        armorUnit = 0;
        damageUnit = 0;
        armorPlanet = 0;
        draftPlanet = 0;
        growthPlanet = 0;
    }
}
