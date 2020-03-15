using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public int speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
        }
    }

}
