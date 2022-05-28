using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        float rotation = rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * rotation);
    }
}
