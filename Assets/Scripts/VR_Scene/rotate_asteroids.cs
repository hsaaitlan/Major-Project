using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_asteroids : MonoBehaviour
{
    public float speed;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * 2, speed, speed / 2);

    }
}
