using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForCube : MonoBehaviour
{
    public GameObject FollowTarget;
    void Start()
    {
        
    }

    void Update()
    {
        transform.RotateAround(FollowTarget.transform.position, Vector3.up, 20 * Time.deltaTime);
        transform.Rotate(45 * Time.deltaTime, 30 * Time.deltaTime, 65 *Time.deltaTime, Space.World);
    }
}
