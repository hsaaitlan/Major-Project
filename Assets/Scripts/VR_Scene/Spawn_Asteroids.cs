using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Asteroids : MonoBehaviour
{
    public GameObject left_spiked_asteroid;
    public GameObject right_spiked_asteroid;
    public GameObject left_normal_asteroid;
    public GameObject right_normal_asteroid;
    // Start is called before the first frame update
    void Start()
    {
        create_asteroids(3);
    }

    // Update is called once per frame
    void create_asteroids(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Instantiate(left_spiked_asteroid, new Vector3(57.42f + Random.Range(-10, 0), 39.7f + Random.Range(-10, 0), 38.1f + Random.Range(-10, 0)), left_spiked_asteroid.transform.rotation);
        }
        for (int i = 0; i < num; i++)
        {
             Instantiate(left_normal_asteroid, new Vector3(57.42f + Random.Range(-10, 0), 39.7f + Random.Range(-10, 0), 38.1f + Random.Range(-10, 0)), left_normal_asteroid.transform.rotation);
        }
        for (int i = 0; i < num; i++)
        {
            Instantiate(right_normal_asteroid, new Vector3(-57.36f + Random.Range(0, 10), -36 + Random.Range(0, 10), -76 + Random.Range(0, 10)), right_normal_asteroid.transform.rotation);
        }
        for (int i = 0; i < num; i++)
        {
            Instantiate(right_spiked_asteroid, new Vector3(-57.36f + Random.Range(0, 10), -36 + Random.Range(0, 10), -76 + Random.Range(0, 10)), right_spiked_asteroid.transform.rotation);
        }
    }
}
