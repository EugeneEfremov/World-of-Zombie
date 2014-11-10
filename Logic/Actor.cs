using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	private CharacterController cc;
	private Vector3 moveDirectionZ, moveDirectionX = Vector3.zero;// Перед назад, лево право
	private int score; //Очки
    private float _forwardCC, _rightCC;
    public bool death = false;
    public int helth = 100, helthMax, helthReset, armour = 0, armourMax, count = 0, gravity = 80, live = 0, nvd = 0;
    public float speed = 8, speedMax;
    public string gameMode;
    public Animation animTop, animDown;
    public AnimationClip TopSteps, TopRepose, TopRight, TopLeft, DownForward, DownRepose, DownRightLeft, DownBackward;
    public float TopStepsSpeed = 1f, TopReposeSpeed = 1f, TopRightSpeed = 1f, TopLeftSpeed = 1f, DownForwardSpeed = 1f, DownReposeSpeed = 1f, DownRightLeftSpeed = 1f, DownBackwardSpeed = 1f;

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

        if (gameMode != "arena")
        {
            //Top
            animTop.AddClip(TopSteps, "TopSteps");
            animTop["TopSteps"].speed = TopStepsSpeed;
            animTop.AddClip(TopRepose, "TopRepose");
            animTop["TopRepose"].speed = TopReposeSpeed;
            animTop.AddClip(TopRight, "TopRight");
            animTop["TopRight"].speed = TopRightSpeed;
            animTop.AddClip(TopLeft, "TopLeft");
            animTop["TopLeft"].speed = TopLeftSpeed;

            //Down
            animDown.AddClip(DownForward, "DownForward");
            animDown["DownForward"].speed = DownForwardSpeed;
            animDown.AddClip(DownRepose, "DownRepose");
            animDown["DownRepose"].speed = DownReposeSpeed;
            animDown.AddClip(DownRightLeft, "DownRightLeft");
            animDown["DownRightLeft"].speed = DownRightLeftSpeed;
            animDown.AddClip(DownBackward, "DownBackward");
            animDown["DownBackward"].speed = DownBackwardSpeed;
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

            //Если не двигаемся - покой
            if (_forwardCC == 0 && _rightCC == 0)
            {
                animTop.CrossFade("TopRepose");
                animDown.CrossFade("DownRepose");
            }

            //Если не идем - отключить шаги
            if (_forwardCC == 0)
            {
                animTop.Stop("TopSteps");
                animDown.Stop("DownForward");
                animDown.Stop("DownBackward");
            }

            //Если идем - включить шаги
            if (_forwardCC == 1)
            {
                animTop.CrossFade("TopSteps");
                animDown.CrossFade("DownForward");
            }

            //Если идем назад - включить шаги
            if (_forwardCC == -1)
            {
                animTop.CrossFade("TopSteps");
                animDown.CrossFade("DownBackward");
            }

            //Если не идем в стороны
            if (_rightCC == 0)
            {
                animTop.Stop("TopRight");
                animTop.Stop("TopsLeft");
                animDown.Stop("DownRightLeft");
            }

            if (_rightCC == 1)
            {
                animTop.CrossFade("TopRight");
                animDown.CrossFade("DownRightLeft");
            }

            if (_rightCC == -1)
            {
                animTop.CrossFade("TopLeft");
                animDown.CrossFade("DownRightLeft");
            }
						//Вперед
            moveDirectionZ = new Vector3( -_forwardCC, 0, 0);
						moveDirectionZ = transform.TransformDirection (moveDirectionZ);
						moveDirectionZ *= speed;

						//В сторны
						moveDirectionX = new Vector3 (0, 0, _rightCC);
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
