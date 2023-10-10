using UnityEngine;

public class CanvasParent : MonoBehaviour,ICoordinate
{
    public Vector3 position => gameObject.transform.position;

    public Vector3 GetPosition()
    {
        return position;
    }
}
