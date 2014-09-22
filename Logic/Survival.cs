using UnityEngine;
using System.Collections;

public class Survival : MonoBehaviour {

    public GameObject Street_light1;
    public GameObject Street_light2;

    void Start(){
        GameObject.Find("Directional light1").GetComponent<Light>().intensity = LightIntens();
        GameObject.Find("Directional light2").GetComponent<Light>().intensity = GameObject.Find("Directional light1").GetComponent<Light>().intensity;

        //Фонарь
        if (GameObject.Find("Directional light1").GetComponent<Light>().light.intensity < 0.3f)
        {
            Street_light1.GetComponent<Light>().light.enabled = true;
            Street_light2.GetComponent<Light>().light.enabled = true;

            if (GameObject.Find("Actor").GetComponent<Global>().lantern == 1)
                GameObject.Find("Lantern").GetComponent<Light>().light.enabled = true;
        }
    }
        public float LightIntens(){
		if (Random.Range (0, 3) == 1)
			return 0f;
		else
			return 0.5f;
	}


}
