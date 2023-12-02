using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] protected SelectManager selectManager;

    public List<GameObject> listPlanet = new List<GameObject>();

    protected abstract void CreateGameState();

    public void GetPlanet(GameObject planet)
    { 
        listPlanet.Add(planet);
    }

    public abstract void StartScript();
}
