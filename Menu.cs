using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private Rect _newGameRect, _continueGameRect, _settingsRect, _recordsRect, _authorsRect, _exitRect;
	private bool _newGame, _continue, _settings, _records, _authors; 

	public Texture2D background;

	void Start(){
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
			break;
		}
	}

	void OnGUI(){
		GUI.DrawTexture( new Rect (0, 0, Screen.width, Screen.height), background);
		if (GUI.Button (_newGameRect, "Новая игра")) _newGame = true;
		GUI.Button (_continueGameRect, "Продолжить игру");
		GUI.Button (_settingsRect, "Настройки");
		GUI.Button (_recordsRect, "Рекорды");
		GUI.Button (_authorsRect, "Авторы");
		if(GUI.Button (_exitRect, "Выход"))Application.Quit();
	
		if (_newGame) 
			GUI.Window(1, new Rect (Screen.width/2 - 100, Screen.height/2 - 50, 180, 140), Window, "Веберите режим");
		}
}
