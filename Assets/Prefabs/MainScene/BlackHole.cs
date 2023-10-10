using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private Transform transformBlackHole;
    public float rotationSpeed = 1f;

    private void Start()
    {
        transformBlackHole = GetComponent<Transform>();
    }

    void Update()
    {
        float rotationAngle = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationAngle);
    }
}
