using TMPro;
using UnityEngine;
using Zenject;

public class CheckPoints : MonoBehaviour
{
    [Inject] private ProgressPlayer player;

    [SerializeField] private TextMeshProUGUI textPoints;
    
    private void Start()
    {
        textPoints.text = "Points: " + player.points;
    }

    public void UpdateTextPoints()
    {
        textPoints.text = "Points: " + player.points;
    }
}
