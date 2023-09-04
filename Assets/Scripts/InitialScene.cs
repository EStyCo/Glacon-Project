using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitialScene : MonoBehaviour
{
    public TextMeshProUGUI textGame;

    void Start()
    {
        textGame.text = "Моя игра";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
