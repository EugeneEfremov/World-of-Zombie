using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	private CharacterController cc;
	private Vector3 moveDirectionZ, moveDirectionX = Vector3.zero;// Перед назад, лево право
	private int score; //Очки

    public bool death = false;
    public int helth = 100, helthMax, helthReset, armour = 0, armourMax, count = 0, gravity = 80, live = 0;
    public float speed = 8, speedMax;

	void Start(){
		cc = GetComponent<CharacterController> ();

        helth += GetComponent<Global>().helthMax;
        helthMax = 100 + GetComponent<Global>().helthMax;

        live = GetComponent<Global>().live;

        armour = GetComponent<Global>().armour;
        armourMax = GetComponent<Global>().armourMax;

        speedMax = GetComponent<Global>().speedMax * speed;
        speed += speedMax / 2;
	}

	void FixedUpdate () {
        if (helth > helthMax) helth = helthMax;
        if (armour > armourMax) armour = armourMax;
        if (armour < 0) armour = 0;
        helthReset = transform.GetComponent<Global>().helthReset;

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
            Time.timeScale = 0.2f;

		if (helth < 1) {
            if (helthReset > 0)
            {
                helth = helthMax;
                transform.GetComponent<Global>().helthReset--;
            }
            else
            {
                death = true;
                helth = 0;
            }
		}
	}
}
