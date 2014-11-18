using UnityEngine;
using System.Collections;

public class shotFireEffect : MonoBehaviour {

    void FixedUpdate()
    {
        transform.localScale = Vector3.one * Random.Range(0.5f, 1f);
    }
}
