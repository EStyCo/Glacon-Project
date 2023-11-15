using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class UIMain : MonoBehaviour
{
    [Inject] GameManager gameManager;

    [SerializeField] private TextMeshProUGUI pointsText;
    
    void Start()
    {
        UpdateData();
    }

    public void UpdateData()
    {
        pointsText.text = "points: " + gameManager.points;
    }
}
