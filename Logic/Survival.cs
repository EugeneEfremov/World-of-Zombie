using UnityEngine;
using System.Collections;

public class Survival : MonoBehaviour {

    public GameObject Street_light1;
    public GameObject Street_light2;
    public GameObject Actor;
    float DLight1;

    void Start(){
        Actor = GameObject.Find("Actor");

        DLight1 = GameObject.Find("DirectionalLight1").GetComponent<Light>().intensity = LightIntens();
        GameObject.Find("DirectionalLight2").GetComponent<Light>().intensity = GameObject.Find("DirectionalLight1").GetComponent<Light>().intensity;

        if (DLight1 < 0.2f)
        {
            if (Actor.GetComponent<Global>().lantern == 1)
            {
                GameObject.Find("Lantern").light.enabled = true;
            }
            else if (Actor.GetComponent<Global>().lantern == 0)
                GameObject.Find("Lantern").light.enabled = false;
        }
    }
        public float LightIntens(){
		if (Random.Range (0, 3) == 1)
			return 0f;
		else
			return 0.5f;
	}


}
