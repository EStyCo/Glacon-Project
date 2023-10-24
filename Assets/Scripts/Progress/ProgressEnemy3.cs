using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressEnemy3 : MonoBehaviour
{
    [Range(0, 3)] public int speedUnit = 0;
    [Range(0, 3)] public int armorUnit = 0;
    [Range(0, 3)] public int damageUnit = 0;

    [Range(0, 3)] public int armorPlanet = 0;
    [Range(0, 3)] public int draftPlanet = 0;
    [Range(0, 3)] public int growthPlanet = 0;
}