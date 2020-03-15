using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_background : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(2 * speed, speed, speed / 2);
    }

}
