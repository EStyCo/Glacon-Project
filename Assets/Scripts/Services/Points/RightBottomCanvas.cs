using UnityEngine;

public class RightBottomCanvas : MonoBehaviour, ICoordinate
{
    public Vector3 position => transform.localPosition;

    public Vector3 GetPosition()
    {
        return position;
    }
}
