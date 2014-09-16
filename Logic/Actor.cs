using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	private CharacterController cc;
	private Vector3 moveDirectionZ, moveDirectionX = Vector3.zero;// Перед назад, лево право
	private int score; //Очки

	public bool pause = false;// Пауза
	public int helth = 100, maxHelth = 120, armour = 0, count = 0, speed = 8, gravity = 80;

	void Start(){
		cc = GetComponent<CharacterController> ();
	}

	void Update () {
        if (helth > maxHelth) helth = maxHelth;
		if (!pause) {
						//Вперед
						moveDirectionZ = new Vector3 (0, 0, Input.GetAxis ("Vertical"));
						moveDirectionZ = transform.TransformDirection (moveDirectionZ);
						moveDirectionZ *= speed;

						//В сторны
						moveDirectionX = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
						moveDirectionX = transform.TransformDirection (moveDirectionX);
						moveDirectionX *= speed;

						moveDirectionZ.y -= gravity * speed * Time.deltaTime;

						cc.Move (moveDirectionZ * Time.deltaTime);
						cc.Move (moveDirectionX * Time.deltaTime);
				}
		if (pause) {
						Time.timeScale = 0.2f;
						GameObject.Find ("Camera").GetComponent<SepiaToneEffect> ().enabled = true;
				}
		if (helth < 1) {
			pause = true;
			helth = 0;
		}
	}
}
