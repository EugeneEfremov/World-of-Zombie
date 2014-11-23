using UnityEngine;
using System.Collections;

public class Survival : MonoBehaviour {

    public GameObject Actor;
    //Было ли получено оружие в бонус?
    public bool day, gunBonus = false, grenadeBonus = false, minigunBonus = false, rocketBonus = false, diskgunBonus = false, gaussgunBonus = false, firegunBonus = false, zeusgunBonus = false, plasmicgunBonus = false;
    float DLight1;

    //Рандом для день/ночь
    public float LightIntens()
    {
        if (Random.Range(0, 3) == 1)
            return 0.1f;
        else
            return 0.5f;
    }

    void Start(){
        day = true;
        Actor = GameObject.Find("Actor");

        //Рандомный день/ночь
        DLight1 = GameObject.Find("DirectionalLight1").GetComponent<Light>().intensity = LightIntens();
        GameObject.Find("DirectionalLight2").GetComponent<Light>().intensity = GameObject.Find("DirectionalLight1").GetComponent<Light>().intensity;
    }
}
