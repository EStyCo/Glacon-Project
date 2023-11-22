using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using Zenject;

public class UIProgressButton : MonoBehaviour
{
    [Inject] private UIProgress uIProgress;

    [SerializeField] int value;
    [SerializeField] string param;
    [SerializeField] SpriteResolver substrateResolver;
    [SerializeField] SpriteRenderer substrateRenderer;
    [SerializeField] Color originalColor;
    [SerializeField] Color selectColor;

    private bool isChosen = false;

    public void Upgrade()
    {
        if (uIProgress.SetNewValue(param, value))
        {
            SelectButton(true);
            ChangeSprite(true);
        }
        else SelectButton(false);
    }

    public void SelectButton(bool isSelected)
    {
        if (isSelected)
        {
            substrateRenderer.color = selectColor;
        }
        else
        { 
            substrateRenderer.color = originalColor;
        }
    }

    public void ChangeSprite(bool state)
    {
        SpriteResolver spriteResolver = gameObject.GetComponent<SpriteResolver>();

        if (state)
        {
            spriteResolver.SetCategoryAndLabel("Button", "Chosen");
            substrateResolver.SetCategoryAndLabel("Substrate", "Chosen");
            isChosen = true;
        }
        else
        {
            spriteResolver.SetCategoryAndLabel("Button", "UnChosen");
            substrateResolver.SetCategoryAndLabel("Substrate", "UnChosen");
            isChosen = false;
        }

    }
}
