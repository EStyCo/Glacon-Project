using UnityEngine;

public class RightTopSM : MonoBehaviour, ICoordinate
{
    public Vector3 position => transform.position;

    public Vector3 GetPosition()
    {
        return position;
    }
}
