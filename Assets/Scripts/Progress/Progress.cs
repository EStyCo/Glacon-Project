using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    private int unitSpeed = 0;
    private int unitArmor = 0;
    private int unitDamage = 0;

    private int planetArmor = 0;
    private int planetDraft = 0;
    private int planetSpeed = 0;


    private void Start()
    {
        Sum();
        Debug.Log(unitSpeed);
    }
    private void Sum()
    {
        unitSpeed = 5;
    }

}
