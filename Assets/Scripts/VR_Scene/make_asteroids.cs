using UnityEngine;

public class make_asteroids : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject spiked;
    public int no_of_asteroids;
    public int rad;
    private Vector3 pos;
    private bool todo;
    private float pi = 3.14f;
    // Start is called before the first frame update
    void Start()
    {
        todo = true;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (todo)
        {
            float act_rad = Mathf.Sqrt(2 * rad * rad);
            float total_parameter = 2 * pi * act_rad;
            int dis = (int)(total_parameter / no_of_asteroids);
            int[] arr_x = new int[no_of_asteroids / 2 + 1];
            int[] arr_z = new int[no_of_asteroids / 2 + 1];
            for (int i = 0; i < arr_x.Length; i++)
            {
                if (i == 0)
                    arr_x[i] = - rad;
                else
                    arr_x[i] = arr_x[i - 1] + dis;
            }
            for (int i = 0; i < arr_z.Length; i++)
            {
                arr_z[i] = (int)Mathf.Sqrt( act_rad * act_rad - arr_x[i] * arr_x[i] );
            }

            for (int i = 0; i < no_of_asteroids / 2 + 1; i++)
            {
                int ch = 0; 
                Instantiate(asteroid, new Vector3(arr_x[i], Random.Range(-10, 10), pos.z + arr_z[i]), asteroid.transform.rotation);
                Instantiate(spiked, new Vector3(arr_x[i], Random.Range(-10, 10), pos.z - arr_z[i]), spiked.transform.rotation);
            }
            todo = false;
        }
    }
}
