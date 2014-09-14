using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private Rect _newGameRect, _continueGameRect, _settingsRect, _recordsRect, _authorsRect, _exitRect;
	private bool _newGame, _continue, _settings, _records, _authors; 

	public Texture2D background;

	public AudioClip Audio;

	void Start(){
		GetComponent<AudioSource> ().PlayOneShot (Audio);
		Screen.showCursor = true;
		_newGameRect = new Rect (Screen.width - 190, Screen.height - 300, 170, 35);
		_continueGameRect = new Rect (Screen.width - 190, Screen.height - 260, 170, 35);
		_settingsRect = new Rect (Screen.width - 190, Screen.height - 220, 170, 35);
		_recordsRect = new Rect (Screen.width - 190, Screen.height - 180, 170, 35);
		_authorsRect = new Rect (Screen.width - 190, Screen.height - 140, 170, 35);
		_exitRect = new Rect (40, Screen.height - 80, 90, 35);
	}

	void Window(int id){
		switch (id) {
			case 1:
				GUI.Button(new Rect (15, 20, 150, 30), "Компания");
				if (GUI.Button(new Rect (15, 60, 150, 30), "Выживание")) Application.LoadLevel("survival");
				GUI.Button(new Rect (15, 100, 150, 30), "Тир");
				if (GUI.Button(new Rect (20, 170, 145, 30), "Закрыть"))_newGame = false;
			break;
			case 4:
				GUI.Label (new Rect (90, 20, 105, 30), "Компания");
				GUI.Label (new Rect (20, 45, 105, 30), PlayerPrefs.GetString("ActorNameCompany").ToString());
				GUI.Label (new Rect (125, 45, 105, 30), PlayerPrefs.GetInt("ActorNameCompanyScore").ToString());
				GUI.Label (new Rect (90, 70, 105, 30), "Выживание");
				GUI.Label (new Rect (20, 95, 105, 30), PlayerPrefs.GetString("ActorNameSurvival").ToString());
				GUI.Label (new Rect (125, 95, 105, 30), PlayerPrefs.GetInt("ActorNameSurvivalScore").ToString());
				GUI.Label (new Rect (110, 120, 105, 30), "Тир");
				GUI.Label (new Rect (20, 145, 105, 30), PlayerPrefs.GetString("ActorNameTir").ToString());
				GUI.Label (new Rect (125, 145, 105, 30), PlayerPrefs.GetInt("ActorNameTirScore").ToString());
				if (GUI.Button (new Rect (45, 190, 145, 30), "Закрыть")) _records = false;
			break;
		}
	}

	void OnGUI(){
		GUI.DrawTexture( new Rect (0, 0, Screen.width, Screen.height), background);
		if (GUI.Button (_newGameRect, "Новая игра")) _newGame = true;
		GUI.Button (_continueGameRect, "Продолжить игру");
		GUI.Button (_settingsRect, "Настройки");
		if (GUI.Button (_recordsRect, "Рекорды")) _records = true;
		GUI.Button (_authorsRect, "Авторы");
		if(GUI.Button (_exitRect, "Выход"))Application.Quit();
	
		if (_newGame) 
			GUI.Window(1, new Rect (Screen.width/2 - 100, Screen.height/2 - 100, 180, 215), Window, "Веберите режим");
		if (_records) 
			GUI.Window(4, new Rect (Screen.width/2 - 120, Screen.height/2 - 120, 240, 240), Window, "Рекорды");
		}
}
