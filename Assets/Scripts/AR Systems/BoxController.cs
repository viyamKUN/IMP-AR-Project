using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.Rotate(new Vector3(-30, 0 , 0), Space.World);  
    }
}
