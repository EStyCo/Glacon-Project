using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIProgressButton : MonoBehaviour
{
    [Inject] private UIProgress uIProgress;

    [SerializeField] int value;
    [SerializeField] string param;

    public void SendValue()
    {
        if (uIProgress.SetNewValue(param, value))
        { 
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponent<Image>().color = new Color(0.5754717f, 0.5754717f, 0.5754717f, 1f);
        }
    }
}
