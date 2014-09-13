 using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

	private int _newNameZomb;
	private Rect _helthRect, _accountRect, _armourRect, _infoRect, _countRect, _pauseRect;

	public float timeZombie = 1;
	public Texture2D goal, info;
	public Transform Player;

	void Start () {
		Screen.showCursor = false;
		Player = GameObject.Find ("Actor").transform;
		_helthRect = new Rect (Screen.width - 40, 0, 30, 30);
		_armourRect = new Rect (Screen.width - 80, 0, 30, 30);
		_accountRect = new Rect (10, 0, 50, 30);
		_infoRect = new Rect (Screen.width - 130, 0, 130, 28);
		_countRect = new Rect (130, 0, -130, 28);
		_pauseRect = new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
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
				GUI.Label ( new Rect(20, 30, 80, 80), "Ваш счет");
				GUI.Label ( new Rect(30, 60, 80, 80), Player.GetComponent<Actor>().count.ToString());
			if(GUI.Button (new Rect(30, 140, 140, 30), "В меню")){
				Application.LoadLevel("menu");
			}
			break;
		}
	}
}
