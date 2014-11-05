using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	private CharacterController cc;
	private Vector3 moveDirectionZ, moveDirectionX = Vector3.zero;// Перед назад, лево право
	private int score, _forwardCC, _rightCC; //Очки
    public bool death = false;
    public int helth = 100, helthMax, helthReset, armour = 0, armourMax, count = 0, gravity = 80, live = 0, nvd = 0;
    public float speed = 8, speedMax;
    public string gameMode;

	void Start(){
        cc = GetComponent<CharacterController>();
        gameMode = transform.GetComponent<Info>().gameMode.ToString();
        helth += GetComponent<Global>().helthMax;
        helthMax = 100 + GetComponent<Global>().helthMax;
        live = GetComponent<Global>().live;
        nvd = GetComponent<Global>().nvd;
        armour = GetComponent<Global>().armour;
        armourMax = GetComponent<Global>().armourMax;
        speedMax = GetComponent<Global>().speedMax * speed;
        speed += speedMax / 2;

        if (gameMode == "arena")
        {
            helth *= 7;
            helthMax *= 7;
        }
	}

	void FixedUpdate () {

        if (helth > helthMax) helth = helthMax;
        if (armour > armourMax) armour = armourMax;
        if (armour < 0) armour = 0;
        helthReset = transform.GetComponent<Global>().helthReset;

        if (!death && gameMode != "arena")
        {
            //Move
            _forwardCC = transform.GetComponent<Controllers>().forwardCC;
            _rightCC = transform.GetComponent<Controllers>().rightCC;

						//Вперед
                        moveDirectionZ = new Vector3(0, 0, _forwardCC /*Input.GetAxis("Vertical")*/);
						moveDirectionZ = transform.TransformDirection (moveDirectionZ);
						moveDirectionZ *= speed;

						//В сторны
						moveDirectionX = new Vector3 (_rightCC /*Input.GetAxis ("Horizontal")*/, 0, 0);
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
