using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private Rect _newGameRect, _continueGameRect, _inventoryRect, _settingsRect, _recordsRect, _authorsRect, _exitRect;
    private bool _newGame, _newNameActor, _newGameClear, _settings, _records, _authors, _switchLevel, _creatingShellBllet, _creatingBlood, _invertingMove;
    private int _modeController;
    private float _graphicQuality;
    private GameObject MenuObj;
	public Texture2D background;
    public string actorName = "", qualityGraphics;
	public AudioClip Audio;

	void Start(){
        _graphicQuality = PlayerPrefs.GetInt("MyGraphicsQuality") + 1;
        PlayerPrefs.SetInt("UnityGraphicsQuality", (int)_graphicQuality);

        MenuObj = GameObject.Find("Menu");
		GetComponent<AudioSource> ().PlayOneShot (Audio);
		Screen.showCursor = true;
		_newGameRect = new Rect (Screen.width - 190, Screen.height - 300, 170, 35);
        _continueGameRect = new Rect(Screen.width - 190, Screen.height - 260, 170, 35);
        _inventoryRect = new Rect(Screen.width - 190, Screen.height - 220, 170, 35);
		_settingsRect = new Rect (Screen.width - 190, Screen.height - 180, 170, 35);
		_recordsRect = new Rect (Screen.width - 190, Screen.height - 140, 170, 35);
		_authorsRect = new Rect (Screen.width - 190, Screen.height - 100, 170, 35);
		_exitRect = new Rect (40, Screen.height - 80, 90, 35);

        if (PlayerPrefs.GetInt("shellBullet") != 0)
            _creatingShellBllet = true;
        else
            _creatingShellBllet = false;

        if (PlayerPrefs.GetInt("blood") != 0)
            _creatingBlood = true;
        else
            _creatingBlood = false;

        if (PlayerPrefs.GetInt("invertingMove") != 0)
            _invertingMove = true;
        else
            _invertingMove = false;
	}

	void Window(int id){
		switch (id) {
            //New game
			case 1:
                if (GUI.Button(new Rect(15, 20, 150, 30), "Компания"))
                {
                    _newGameClear = true;
                    _newGame = false;
                }
				if (GUI.Button(new Rect (15, 60, 150, 30), "Выживание")) Application.LoadLevel("survival");
                if (GUI.Button(new Rect(15, 100, 150, 30), "Арена")) Application.LoadLevel("arena");
				if (GUI.Button(new Rect (20, 170, 145, 30), "Закрыть"))_newGame = false;
			break;
            //New game confirmation clear
            case 11:
                GUI.Label(new Rect(45, 20, 260, 25), "Это сотрет все ваши достижения");
                if (GUI.Button(new Rect(75, 60, 50, 30), "Да"))
                {
                    _newNameActor = true;
                }
                if (GUI.Button(new Rect(175, 60, 50, 30), "Нет"))
                {
                    _newGameClear = false;
                    _newGame = true;
                }
            break;
            //Enter new Name of Actor
            case 112:
                 _newGameClear = false;
                actorName = GUI.TextArea(new Rect(30, 30, 140, 25), actorName, 15);
                if (GUI.Button(new Rect(20, 60, 70, 30), "Сохранить"))
                {
                    PlayerPrefs.SetString("ActorNameCompany", actorName);
                    _newGameClear = false;
                    _newGame = false;
                    MenuObj.GetComponent<InventoryMenu>().nameActor = actorName;
                    MenuObj.GetComponent<InventoryMenu>().enabled = true;
                    MenuObj.GetComponent<InventoryMenu>().newGame = true;
                    MenuObj.GetComponent<Menu>().enabled = false;
                }
                if (GUI.Button(new Rect(110, 60, 70, 30), "Отмена"))
                {
                    _newNameActor = false;
                    _newGame = true;
                }
            break;
            case 4:
                GUI.Label(new Rect(500, 30, 105, 30), "Громкость");
                AudioListener.volume = GUI.HorizontalSlider (new Rect(400, 60, 275, 30), AudioListener.volume, 0, 1);
                GUI.Label(new Rect(505, 90, 105, 30), "Графика");
                _graphicQuality = GUI.HorizontalSlider (new Rect (400, 130, 275, 30), _graphicQuality, 0, 3);
                if (_graphicQuality <= 1)
                {
                    QualitySettings.SetQualityLevel(0, true);
                    PlayerPrefs.SetInt("UnityGraphicsQuality", 0);
                    PlayerPrefs.SetInt("MyGraphicsQuality", 0);
                    qualityGraphics = "Плохая";
                }
                if (_graphicQuality > 1 && _graphicQuality <= 2)
                {
                    QualitySettings.SetQualityLevel(1, true);
                    PlayerPrefs.SetInt("UnityGraphicsQuality", 1);
                    PlayerPrefs.SetInt("MyGraphicsQuality", 1);
                    qualityGraphics = "Нормальная";
                }
                if (_graphicQuality > 2 && _graphicQuality <= 3)
                {
                    QualitySettings.SetQualityLevel(2, true);
                    PlayerPrefs.SetInt("UnityGraphicsQuality", 2);
                    PlayerPrefs.SetInt("MyGraphicsQuality", 2);
                    qualityGraphics = "Хорошая";
                }

                GUI.Label(new Rect(505, 150, 100, 30), qualityGraphics);

                //Режим управления
                if (GUI.Button(new Rect(25, 25, 350, 100), "Передвижение и поворот"))
                    PlayerPrefs.SetInt("Controllers", 1);
                if (GUI.Button(new Rect (25, 150, 350, 100), "Передвижение"))
                    PlayerPrefs.SetInt("Controllers", 2);
                if (GUI.Button(new Rect (25, 275 , 350, 100), "Передвижение/поворот и стрельба"))
                    PlayerPrefs.SetInt("Controllers", 3);

                //Доп. настройки
                //Кровь
                GUI.Toggle(new Rect(420, 200, 100, 30), _creatingBlood, " Кровь");
                if (GUI.Button(new Rect(400, 230, 40, 30), " Да"))
                {
                        _creatingBlood = true;
                        PlayerPrefs.SetInt("blood", 1);
                }
                if (GUI.Button(new Rect(450, 230, 40, 30), " Нет"))
                {
                    _creatingBlood = false;
                    PlayerPrefs.SetInt("blood", 0);
                }

                //Гильзы
                GUI.Toggle(new Rect(530, 200, 100, 30), _creatingShellBllet, " Гильзы");
                if (GUI.Button(new Rect(520, 230, 40, 30), " Да"))
                {
                    _creatingShellBllet = true;
                    PlayerPrefs.SetInt("shellBullet", 1);
                }
                if (GUI.Button(new Rect(570, 230, 40, 30), " Нет"))
                {
                    _creatingShellBllet = false;
                    PlayerPrefs.SetInt("shellBullet", 0);
                }

                //Инвертация в Move
                GUI.Toggle(new Rect(400, 265, 320, 30), _invertingMove, " Инвертировать прицел в передвижени");
                if (GUI.Button(new Rect(480, 295, 40, 30), " Да"))
                {
                    _invertingMove = true;
                    PlayerPrefs.SetInt("invertingMove", 1);
                }
                if (GUI.Button(new Rect(530, 295, 40, 30), " Нет"))
                {
                    _invertingMove = false;
                    PlayerPrefs.SetInt("invertingMove", 0);
                }

                GUI.Label(new Rect(400, 325, 320, 30), "*Влияет на производительность системы");

                if (GUI.Button(new Rect(577, 350, 100, 30), "Закрыть")) _settings = false;
            break;
			case 5:
				GUI.Label (new Rect (90, 20, 105, 30), "Компания");
				GUI.Label (new Rect (20, 45, 105, 30), PlayerPrefs.GetString("ActorNameCompany").ToString());
				GUI.Label (new Rect (125, 45, 105, 30), PlayerPrefs.GetInt("ActorNameCompanyScore").ToString());
				GUI.Label (new Rect (90, 70, 105, 30), "Выживание");
				GUI.Label (new Rect (20, 95, 105, 30), PlayerPrefs.GetString("ActorNameSurvival").ToString());
				GUI.Label (new Rect (125, 95, 105, 30), PlayerPrefs.GetInt("ActorNameSurvivalScore").ToString());
				GUI.Label (new Rect (110, 120, 105, 30), "Арена");
				GUI.Label (new Rect (20, 145, 105, 30), PlayerPrefs.GetString("ActorNameArena").ToString());
				GUI.Label (new Rect (125, 145, 105, 30), PlayerPrefs.GetInt("ActorNameArenaScore").ToString());
				if (GUI.Button (new Rect (45, 190, 145, 30), "Закрыть")) _records = false;
			break;
		}
	}

	void OnGUI(){
		GUI.DrawTexture( new Rect (0, 0, Screen.width, Screen.height), background);
		if (GUI.Button (_newGameRect, "Новая игра")) _newGame = true;
        if (GUI.Button(_continueGameRect, "Продолжить"))
        {
            MenuObj.GetComponent<Menu>().enabled = false;
            MenuObj.GetComponent<SwitchLevel>().enabled = true;
        }
        if (GUI.Button(_inventoryRect, "Инвентарь"))
        {
            MenuObj.GetComponent<Menu>().enabled = false;
            MenuObj.GetComponent<InventoryMenu>().enabled = true;
        }

        if (GUI.Button(_settingsRect, "Настройки")) _settings = true;
		if (GUI.Button (_recordsRect, "Рекорды")) _records = true;
		GUI.Button (_authorsRect, "Авторы");
		if(GUI.Button (_exitRect, "Выход"))Application.Quit();
	
		if (_newGame) 
			GUI.Window(1, new Rect (Screen.width/2 - 100, Screen.height/2 - 100, 180, 215), Window, "Веберите режим");
        if (_newGameClear)
            GUI.Window(11, new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100), Window, "Вы уверены, что хотите начать новую игру?");
        if (_newNameActor)
            GUI.Window(112, new Rect(Screen.width / 2 - 90, Screen.height / 2 - 50, 200, 100), Window, "Введите ваше имя");
        if (_settings)
            GUI.Window(4, new Rect(50, 50, 700, 400), Window, "Настройки");
		if (_records) 
			GUI.Window(5, new Rect (Screen.width/2 - 120, Screen.height/2 - 120, 240, 240), Window, "Рекорды");
		}
}
