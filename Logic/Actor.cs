using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	private CharacterController cc;
	private Vector3 moveDirectionZ, moveDirectionX = Vector3.zero;// Перед назад, лево право
	private int score; //Очки

    public bool death = false;
	public int helth = 100, maxHelth, armour = 0, armourMax, count = 0, speed = 8, gravity = 80;

	void Start(){
		cc = GetComponent<CharacterController> ();

        helth += GetComponent<Global>().helthMax;
        maxHelth = 100 + GetComponent<Global>().helthMax;

        armour = 100 * GetComponent<Global>().armour;
        armourMax = 100 * GetComponent<Global>().armour;

        speed += (GetComponent<Global>().speedMax * speed) / 2;
	}

	void Update () {
        if (helth > maxHelth) helth = maxHelth;
        if (armour > armourMax) armour = armourMax;
        if (armour < 0) armour = 0;

        if (!death)
        {
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
        if (death)
        {
						Time.timeScale = 0.2f;
				}
		if (helth < 1) {
            death = true;
			helth = 0;
		}
	}
}
