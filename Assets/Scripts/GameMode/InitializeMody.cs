using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InitializeMody : MonoBehaviour
{
    [Inject] GameModeManager gameModeManager;

    [SerializeField] private GameObject blackHoleComponent;

    void Start()
    {
        ClearMody();
        GetMody();
    }

    private void ClearMody()
    { 
        blackHoleComponent.SetActive(false);
    }

    private void GetMody()
    {
        if (gameModeManager.blackHoleIsOn == true)
        { 
            blackHoleComponent.SetActive (true);
        }
        
    }
}
