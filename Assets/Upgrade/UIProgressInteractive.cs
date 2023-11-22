using Unity.Burst.CompilerServices;
using UnityEngine;

public class UIProgressInteractive : MonoBehaviour
{
    [SerializeField] LayerMask layer;

    public UIProgressButton selectButton;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectButton != null) selectButton.SelectButton(false);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layer);

            if (IsUpgrade(hit))
            {
                selectButton.Upgrade();
                selectButton = null;
            }

            selectButton = hit.collider?.GetComponent<UIProgressButton>();

            if (selectButton != null) selectButton.SelectButton(true);
        }
    }

    private bool IsUpgrade(RaycastHit2D hit)
    {
        return selectButton != null && selectButton == hit.collider?.GetComponent<UIProgressButton>();
    }

    private void Upgrade(UIProgressButton button)
    { 
        
    }
}
