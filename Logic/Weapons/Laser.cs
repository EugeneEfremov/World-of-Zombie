using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    private LineRenderer lineRender;
    private RaycastHit hit;

    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        lineRender.SetWidth(0.3f, 0.3f);
    }

	void FixedUpdate () {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            lineRender.SetPosition(1, new Vector3(0, 0, hit.distance));
        }
	}
}
