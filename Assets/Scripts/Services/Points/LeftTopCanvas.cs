using UnityEngine;

public class LeftTopCanvas : MonoBehaviour, ICoordinate
{
    public Vector3 position => transform.localPosition;

    public Vector3 GetPosition()
    {
        return position;
    }
}

