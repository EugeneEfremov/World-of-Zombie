using UnityEngine;
using System.Collections;

public class Level_1 : MonoBehaviour {

    private Transform Player;

    public Transform zombie, rat, dog, solders, grenade, bigZ, instans; //Объекты
    public bool magic1;

	void Start () {
        Player = GameObject.FindWithTag("Player").transform;
	}
}
