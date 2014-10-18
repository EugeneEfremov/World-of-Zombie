using UnityEngine;
using System.Collections;

public class OtherHelth : MonoBehaviour {

    public int helth;
    public GameObject block1, block2, instans;
    public bool inst1 = false, inst2 = false;

    void Start()
    {
        helth = 300;
    }

    void FixedUpdate()
    {
        if (helth < 170 && !inst1)
        {
            instans = Instantiate(block1, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            instans.transform.localScale = new Vector3(70,70,70);
            instans.transform.eulerAngles = new Vector3(270, transform.eulerAngles.y, 0);
            inst1 = true;
            transform.renderer.enabled = false;
        }
        if (helth < 1 && !inst2)
        {
            instans.renderer.enabled = false;
            instans = Instantiate(block2, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            instans.transform.localScale = new Vector3(70, 70, 70);
            instans.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            inst2 = true;
            transform.collider.enabled = false;
        }
    }
}
