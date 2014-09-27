using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

    public enum GameMode
    {
        survival,
        arena,
        level1
    };

	private int _newNameZomb;
	private Rect _helthRect, _accountRect, _armourRect, _infoRect, _countRect, _deathRect, _pauseButtonRect, _continueButtonRect;
    private Rect _magic1Rect;

    public GameMode gameMode = GameMode.survival;

    public float timeZombie = 1, timeMagic1;
	public Texture2D goal, info, pauseT, pauseDownT, continueT;
    public Texture2D magic1;
    public bool pause = false, bMagic1;
    public Vector2 mp;
	public Transform Player;
	public string actorName= "", modeGame;
    public GameObject ZombAll;

	void Start () {
        ZombAll = GameObject.Find("ZombieLogic");
        modeGame = Application.loadedLevelName;
		//Screen.showCursor = false;
		Player = GameObject.Find ("Actor").transform;
		_helthRect = new Rect (Screen.width - 40, 0, 30, 30);
		_armourRect = new Rect (Screen.width - 80, 0, 30, 30);
		_accountRect = new Rect (10, 0, 50, 30);
		_infoRect = new Rect (Screen.width - 130, 0, 130, 28);
		_countRect = new Rect (130, 0, -130, 28);
        _deathRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
        _pauseButtonRect = new Rect(10, 34, 34, 33);
        _continueButtonRect = new Rect(Screen.width / 2 - 75, Screen.height - (Screen.height / 3), 300, 70);

        _magic1Rect = new Rect(Screen.width - 64 - 30, Screen.height / 4, 64, 72);
	}

    void FixedUpdate()
    {
        //Magic
        switch(gameMode){
            case GameMode.survival:
                if (bMagic1)
                {
                 ZombAll.GetComponent<ZombieAll>().magic1 = true;
                 timeMagic1 -= Time.deltaTime;
                 if (timeMagic1 <= 0)
                      bMagic1 = false;
             }
             if (!bMagic1)
              {
                 ZombAll.GetComponent<ZombieAll>().magic1 = false;
                 if (timeMagic1 < 10)
                      timeMagic1 += Time.deltaTime;
               }
             break;
            case GameMode.arena:
             if (bMagic1)
             {
                 ZombAll.GetComponent<ZombieAll>().magic1 = true;
                 timeMagic1 -= Time.deltaTime;
                 if (timeMagic1 <= 0)
                     bMagic1 = false;
             }
             if (!bMagic1)
             {
                 ZombAll.GetComponent<ZombieAll>().magic1 = false;
                 if (timeMagic1 < 10)
                     timeMagic1 += Time.deltaTime;
             }
             break;
            case GameMode.level1:
             if (bMagic1)
             {
                 ZombAll.GetComponent<Level_1>().magic1 = true;
                 timeMagic1 -= Time.deltaTime;
                 if (timeMagic1 <= 0)
                     bMagic1 = false;
             }
             if (!bMagic1)
             {
                 ZombAll.GetComponent<Level_1>().magic1 = false;
                 if (timeMagic1 < 10)
                     timeMagic1 += Time.deltaTime;
             }
             break;
         }
        //Magic END
    }

	void OnGUI (){
        Vector2 mp = Event.current.mousePosition;
		GUI.depth = 1;
		GUI.DrawTexture (_infoRect, info);
		GUI.DrawTexture (_countRect, info);
		//GUI.Label (new Rect (mp.x - 12, mp.y - 15, goal.width, goal.height), goal);
		GUI.Label (_helthRect, Player.GetComponent<Actor>().helth.ToString());
		GUI.Label (_armourRect, Player.GetComponent<Actor>().armour.ToString());
        GUI.Label(_accountRect, Player.GetComponent<Actor>().count.ToString());

        //Magic
        if (GUI.Button(_magic1Rect, magic1) && timeMagic1 >= 10)
            bMagic1 = true;

        //Magic END

            if (!pause)
            {
                if (Player.GetComponent<Actor>().death == false && !pause)
                {
                    Time.timeScale = 1;
                }
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
                if (modeGame == "survival" || modeGame ==  "arena")
                {
                    GUI.Label(new Rect(40, 60, 140, 30), "Введите Ваше имя");
                    actorName = GUI.TextArea(new Rect(30, 90, 140, 25), actorName, 15);
                }
                else
                {
                    actorName = PlayerPrefs.GetString("ActorNameCompany");
                    GUI.Label(new Rect(40, 60, 140, 30), "Ваше имя");
                    GUI.Label(new Rect(30, 90, 140, 25), actorName);
                }
				if(GUI.Button (new Rect(30, 155, 140, 30), "Сохранить")){
                        PlayerPrefs.SetInt("Money", Player.GetComponent<Actor>().count / 70);
                        if (modeGame == "survival")
                        {
                            Player.GetComponent<Global>().SaveResultGame();
                            PlayerPrefs.SetString("ActorNameSurvival", actorName);
                            PlayerPrefs.SetInt("ActorNameSurvivalScore", Player.GetComponent<Actor>().count);
                        }
                        else if (modeGame == "arena")
                        {
                            PlayerPrefs.SetString("ActorNameArena", actorName);
                            PlayerPrefs.SetInt("ActorNameArenaScore", Player.GetComponent<Actor>().count);
                        }
                        else
                        {
                            Player.GetComponent<Global>().SaveResultGame();
                            PlayerPrefs.SetString("ActorNameCompany", actorName);
                            PlayerPrefs.SetInt("ActorNameCompanyScore", Player.GetComponent<Actor>().count);
                        }
					Application.LoadLevel("menu");
			}
			break;
		}
	}
}
