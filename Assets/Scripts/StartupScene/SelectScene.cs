using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectScene : MonoBehaviour
{
    public Image img;
    public float tot_Time = 1f;
    float gvrTimer = 0f;
    bool gvrstat;
    public int distanceOfRay = 10;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gvrstat)
        {
            img.fillAmount = gvrTimer / tot_Time;
            gvrTimer += Time.deltaTime;
        }
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, distanceOfRay))
        {
            if (img.fillAmount == 1)
            {
                if (hit.transform.CompareTag("SpaceScene"))
                    sceneUpdate(1);
                else if (hit.transform.CompareTag("SecondScene"))
                    sceneUpdate(2);
            }
        }
    }

    public void GVROn()
    {
        gvrstat = true;
    }

    public void GVROff()
    {
        gvrstat = false;
        gvrTimer = 0;
        img.fillAmount = 0;
    }

    public void sceneUpdate(int number)
    {
        SceneManager.LoadScene(number);
    }
}
