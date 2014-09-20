using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

	private int _newNameZomb;
	private Rect _helthRect, _accountRect, _armourRect, _infoRect, _countRect, _deathRect, _pauseButtonRect, _continueButtonRect;

	public float timeZombie = 1;
	public Texture2D goal, info, pauseT, pauseDownT, continueT;
    public bool pause = false;
    public Vector2 mp;
	public Transform Player;
	public string actorName= "";

	void Start () {
		Screen.showCursor = false;
		Player = GameObject.Find ("Actor").transform;
		GameObject.Find ("Directional light").GetComponent<Light> ().intensity = LightIntens ();
        //Фонарь
        if (GameObject.Find("Directional light").GetComponent<Light>().light.intensity < 0.3f && GameObject.Find("Actor").GetComponent<Global>().lantern == 1)
            GameObject.Find("Lantern").GetComponent<Light>().light.enabled = true;
		_helthRect = new Rect (Screen.width - 40, 0, 30, 30);
		_armourRect = new Rect (Screen.width - 80, 0, 30, 30);
		_accountRect = new Rect (10, 0, 50, 30);
		_infoRect = new Rect (Screen.width - 130, 0, 130, 28);
		_countRect = new Rect (130, 0, -130, 28);
        _deathRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
        _pauseButtonRect = new Rect(10, 34, 34, 33);
        _continueButtonRect = new Rect(Screen.width / 2 - 75, Screen.height - (Screen.height / 3), 300, 70);
	}

    public float LightIntens(){
		if (Random.Range (0, 3) == 1)
			return 0f;
		else
			return 0.5f;
	}

	void OnGUI (){
        Vector2 mp = Event.current.mousePosition;
		GUI.depth = 1;
		GUI.DrawTexture (_infoRect, info);
		GUI.DrawTexture (_countRect, info);
		GUI.Label (new Rect (mp.x - 12, mp.y - 15, goal.width, goal.height), goal);
		GUI.Label (_helthRect, Player.GetComponent<Actor>().helth.ToString());
		GUI.Label (_armourRect, Player.GetComponent<Actor>().armour.ToString());
        GUI.Label(_accountRect, Player.GetComponent<Actor>().count.ToString());
            if (!pause)
            {
                GUI.Label(_pauseButtonRect, pauseT);
                if (mp.x < 37 && mp.y < 62 && Input.GetMouseButtonUp(0))
                {
                    Time.timeScale = 0;
                    pause = true;
                }
            }
            if (pause)
            {
                GUI.Label(_pauseButtonRect, pauseDownT);
                GUI.Label(_continueButtonRect, continueT);
                if (mp.x > _continueButtonRect.xMin && mp.x < _continueButtonRect.xMax && mp.y > _continueButtonRect.yMin &&  mp.y < _continueButtonRect.yMax && Input.GetMouseButtonUp(0))
                {
                    Time.timeScale = 1;
                    pause = false;
                }
            }

        //Инфо об оружии
		if (Player.GetComponent<Actor> ().death) {
            GUI.Window(0, _deathRect, WindowFunction, "Вы погибли");
		}
	}

	void WindowFunction(int id){
		switch (id) {
			case 0:
				GUI.Label ( new Rect(20, 30, 80, 80), "Ваш счет: ");
				GUI.Label ( new Rect(95, 30, 80, 80), Player.GetComponent<Actor>().count.ToString());
				GUI.Label (new Rect(40, 60, 140, 30), "Введите Ваше имя");
				actorName = GUI.TextArea (new Rect (30, 90, 140, 25), actorName, 15);
				if(GUI.Button (new Rect(30, 155, 140, 30), "Сохранить")){
                        PlayerPrefs.SetInt("Money", Player.GetComponent<Actor>().count / 70);
						PlayerPrefs.SetString("ActorNameSurvival", actorName);
						PlayerPrefs.SetInt("ActorNameSurvivalScore", Player.GetComponent<Actor>().count);
					Application.LoadLevel("menu");
			}
			break;
		}
	}
}
