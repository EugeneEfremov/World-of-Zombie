using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	private CharacterController cc;
	private Vector3 moveDirectionZ, moveDirectionX = Vector3.zero;// Перед назад, лево право
	private int score; //Очки
    private Transform topBody, downBody, myCam;

    public float forwardCC;
    public bool death = false;
    public int helth = 100, helthMax, helthReset, armour = 0, armourMax, count = 0, gravity = 80, live = 0, nvd = 0;
    public float speed = 8, speedMax;
    public string gameMode;
    public Animation animTop, animDown;
    public AnimationClip TopSteps, TopRepose, TopRight, TopLeft, DownForward, DownRepose, DownRightLeft, DownBackward;
    public float TopStepsSpeed = 2f, TopReposeSpeed = 2f, TopRightSpeed = 2f, TopLeftSpeed = 2f, DownForwardSpeed = 2f, DownReposeSpeed = 2f, DownRightLeftSpeed = 2f, DownBackwardSpeed = 2f;

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

        myCam = GameObject.Find("Camera").transform;
        topBody = GameObject.Find("topBody").transform;
        downBody = GameObject.Find("downBody").transform;

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

        //Слежка камеры
        myCam.transform.position = new Vector3(transform.position.x + 9, transform.position.y + 15, transform.position.z + 4);

        if (!death && gameMode != "arena")
        {
            //Если не идем - отключить шаги
            if (forwardCC == 0)
            {
                animTop.Stop("TopSteps");
                animTop.Stop("TopRight");
                animTop.Stop("TopLeft");

                animDown.Stop("DownForward");
                animDown.Stop("DownBackward");
                animDown.Stop("DownRightLeft");

                animTop.CrossFade("TopRepose");
                animDown.CrossFade("DownRepose");
            }

            //Если идем
                if (topBody.transform.localEulerAngles.y > 315 || topBody.transform.localEulerAngles.y < 45)
                {
                    downBody.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                    if (forwardCC > 0)
                    {
                        animTop.CrossFade("TopSteps");
                        animDown.CrossFade("DownForward");
                    }
                }

                if (topBody.transform.localEulerAngles.y >= 45 && topBody.transform.localEulerAngles.y <= 135)
                {
                    downBody.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
                    if (forwardCC > 0)
                    {
                        animTop.CrossFade("TopLeft");
                        animDown.CrossFade("DownRightLeft");
                    }
                }

                if (topBody.transform.localEulerAngles.y <= 225 && topBody.transform.localEulerAngles.y > 135)
                {
                    downBody.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
                    if (forwardCC > 0)
                    {
                        animTop.CrossFade("TopSteps");
                        animDown.CrossFade("DownBackward");
                    }
                }

                if (topBody.transform.localEulerAngles.y > 225 && topBody.transform.localEulerAngles.y <= 315)
                {
                    downBody.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90, 0);
                    if (forwardCC > 0)
                    {
                        animTop.CrossFade("TopRight");
                        animDown.CrossFade("DownRightLeft");
                    }
                }

						//Вперед
                        moveDirectionZ = new Vector3( -forwardCC, 0, 0);
						moveDirectionZ = transform.TransformDirection (moveDirectionZ);
						moveDirectionZ *= speed;

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
                transform.GetComponent<Controllers>().deathActor = true;
                death = true;
                helth = 0;
            }
		}
	}
}
