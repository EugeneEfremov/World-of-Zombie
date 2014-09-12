using UnityEngine;
using System.Collections;

public class Lamp : MonoBehaviour {

	public float timeOut = 2; //Не изменяем лампу

	// Update is called once per frame
	void Update () {
		timeOut -= Time.deltaTime;
		if (timeOut <= 0) {
			gameObject.light.range = Random.Range(10, 12);
			gameObject.light.spotAngle = Random.Range(50, 70);
			timeOut = Random.Range(1,4);
				}
	}
}
