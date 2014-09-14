 using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

	private int _newNameZomb;
	private Rect _helthRect, _accountRect, _armourRect, _infoRect, _countRect, _pauseRect;

	public float timeZombie = 1;
	public Texture2D goal, info;
	public Transform Player;
	public string actorName= "";

	void Start () {
		Screen.showCursor = false;
		Player = GameObject.Find ("Actor").transform;
		GameObject.Find ("Directional light").GetComponent<Light> ().intensity = LightIntens ();
		_helthRect = new Rect (Screen.width - 40, 0, 30, 30);
		_armourRect = new Rect (Screen.width - 80, 0, 30, 30);
		_accountRect = new Rect (10, 0, 50, 30);
		_infoRect = new Rect (Screen.width - 130, 0, 130, 28);
		_countRect = new Rect (130, 0, -130, 28);
		_pauseRect = new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
	}

	public float LightIntens(){
		if (Random.Range (0, 3) == 1)
			return 0f;
		else
			return 0.5f;
	}

	void OnGUI (){
		Vector2 mp = Event.current.mousePosition;
		GUI.depth = 0;
		GUI.DrawTexture (_infoRect, info);
		GUI.DrawTexture (_countRect, info);
		GUI.Label (new Rect (mp.x - 12, mp.y - 15, goal.width, goal.height), goal);
		GUI.Label (_helthRect, Player.GetComponent<Actor>().helth.ToString());
		GUI.Label (_armourRect, Player.GetComponent<Actor>().armour.ToString());
		GUI.Label (_accountRect, Player.GetComponent<Actor>().count.ToString());
		if (Player.GetComponent<Actor> ().pause) {
			GUI.Window(0, _pauseRect, WindowFunction, "лол");
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
						PlayerPrefs.SetString("ActorNameSurvival", actorName);
						PlayerPrefs.SetInt("ActorNameSurvivalScore", Player.GetComponent<Actor>().count);
					Application.LoadLevel("menu");
			}
			break;
		}
	}
}
