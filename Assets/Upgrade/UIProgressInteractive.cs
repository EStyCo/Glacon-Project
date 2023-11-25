using UnityEngine;

public class UIProgressInteractive : MonoBehaviour
{
    [SerializeField] LayerMask layer;

    public UIProgressButton selectButton;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DisableButton();

            RaycastHit2D hit = RaycastHit();

            TryUpgrade(hit);

            selectButton = hit.collider?.GetComponent<UIProgressButton>();
            selectButton?.SelectButton(true);
        }
    }

    private void TryUpgrade(RaycastHit2D hit)
    {
        if (selectButton != null && selectButton == hit.collider?.GetComponent<UIProgressButton>())
        {
            selectButton.Upgrade();
            selectButton = null;
        }
    }

    private RaycastHit2D RaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layer);
        return hit;
    }

    private void DisableButton() => selectButton?.SelectButton(false);
}
