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
    private Rect _helthRect, _accountRect, _armourRect, _leftInfoRect, _rightInfoRect, _deathRect, _pauseButtonRect, _pauseRect, _exitRect, _nvdRect, _helthResetRect, _liveActorRect;
    private Rect _magic1Rect, _magic2Rect, _magic3Rect, _magic4Rect;
    private Rect _arrowLeftWeaponsRect, _arrowRightWeaponsRect, _weaponsRect, _weaponsBulletRect;

    public GameMode gameMode = GameMode.survival;

    public float timeZombie = 1, timeMagic1;
    public Texture2D goal, leftInfo, rightInfo, pauseT, pauseDownT, arrowWeapons, nvdT, nvdCam, helthResetT;
    public Texture2D magic1, magic2, magic3, magic4;
    public bool pause = false, bMagic1, bMagic2, bMagic3, bMagic4, exit = false, helthResetB, nvdEnabled = false;
    public Vector2 mp;
	public Transform Player;
	public string actorName= "", modeGame, currentW;
    public GameObject ZombAll;

	void Start () {
        ZombAll = GameObject.Find("ZombieLogic");
        modeGame = Application.loadedLevelName;
		//Screen.showCursor = false;
		Player = GameObject.Find ("Actor").transform;
		_helthRect = new Rect (10, 0, 100, 30);
        _armourRect = new Rect(10, 20, 100, 30);
        _accountRect = new Rect(Screen.width - 100, 0, 100, 30);
		_leftInfoRect = new Rect (0, 0, 100, 98);
		_rightInfoRect = new Rect (Screen.width - 120, 0, 120, 30);
        _deathRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
        _pauseButtonRect = new Rect(5, 55, 34, 33);
        _pauseRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 200);

        _nvdRect = new Rect(10, 100, 25, 20);

        _liveActorRect = new Rect(50, 85, 30, 30);
        _helthResetRect = new Rect(60, 60, 30, 25);

        _arrowRightWeaponsRect = new Rect(Screen.width - 40, 80, 30, 30);
        _arrowLeftWeaponsRect = new Rect(Screen.width - 100, 80, 30, 30);
        _weaponsRect = new Rect(Screen.width - 90, 40, 90, 30);
        _weaponsBulletRect = new Rect(Screen.width - 70, 65, 40, 30);

        _exitRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100);

        if (gameMode == GameMode.arena)
        {

            _magic1Rect = new Rect(Screen.width - 80, 50, 50, 50);
            _magic2Rect = new Rect(Screen.width - 80, 110, 50, 50);
            _magic3Rect = new Rect(Screen.width - 80, 170, 50, 50);
            _magic4Rect = new Rect(Screen.width - 80, 230, 50, 50);

        }
        else
        {
            _magic1Rect = new Rect(Screen.width - 80, 135, 50, 50);
            _magic2Rect = new Rect(Screen.width - 80, 195, 50, 50);
            _magic3Rect = new Rect(Screen.width - 80, 255, 50, 50);
            _magic4Rect = new Rect(Screen.width - 80, 315, 50, 50);
        }

        if (!pause)
                Time.timeScale = 1;
	}

    void FixedUpdate()
    {
        #region Magic
        switch (gameMode){
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
        #endregion

        if (gameMode != GameMode.arena)
            currentW = Player.GetComponent<Weapons>().currentW;

        if (pause)
            Time.timeScale = 0;

        if (Player.GetComponent<Actor>().helthReset > 0)
            helthResetB = true;

        if (Player.GetComponent<Actor>().helthReset == 0)
            helthResetB = false;
    }

	void OnGUI (){
        //Vector2 mp = Event.current.mousePosition;
		GUI.depth = 1;
		GUI.DrawTexture (_leftInfoRect, leftInfo);
		GUI.DrawTexture (_rightInfoRect, rightInfo);
		//GUI.Label (new Rect (mp.x - 12, mp.y - 15, goal.width, goal.height), goal);
		GUI.Label (_helthRect, "hp " + Player.GetComponent<Actor>().helth.ToString());
		GUI.Label (_armourRect, "A " + Player.GetComponent<Actor>().armour.ToString());
        GUI.Label (_accountRect, Player.GetComponent<Actor>().count.ToString());
        GUI.Label(_liveActorRect, Player.GetComponent<Actor>().live.ToString());

        if (GUI.Button(_nvdRect, nvdT))
        {
            if (!nvdEnabled && Player.GetComponent<Actor>().nvd > 0)
            {
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height), nvdCam);
                nvdEnabled = true;
            }
        }

        if (helthResetB)
            GUI.Label (_helthResetRect, helthResetT);

        if (GUI.Button(_pauseButtonRect, pauseT))
            pause = true;

        if (pause)
        {
            GUI.Window(1, _pauseRect, WindowFunction, "Пауза");
        }

        if (exit)
            GUI.Window(11, _exitRect, WindowFunction, "Вы хотите выйти?");

        //Magic
        if (GUI.Button(_magic1Rect, magic1) && timeMagic1 >= 10)
            bMagic1 = true;


        GUI.Button(_magic1Rect, magic1);

        GUI.Button(_magic2Rect, magic2);

        GUI.Button(_magic3Rect, magic3);

        GUI.Button(_magic4Rect, magic4);

        //Magic END        

		if (Player.GetComponent<Actor> ().death) {
            GUI.Window(0, _deathRect, WindowFunction, "Вы погибли");
        }

#region Swith Weapons Views

        if (gameMode != GameMode.arena)
        {
            switch (currentW)
            {
                case "pistols(Clone)":
                    GUI.Label(_weaponsRect, "pistols");
                    GUI.Label(_weaponsBulletRect, "~~~~");
                    break;
                case "gun(Clone)":
                    GUI.Label(_weaponsRect, "gun");
                    GUI.Label(_weaponsBulletRect, Player.GetComponent<Weapons>().gunBullet.ToString());
                    break;
                case "grenade(Clone)":
                    GUI.Label(_weaponsRect, "grenade");
                    GUI.Label(_weaponsBulletRect, Player.GetComponent<Weapons>().grenadeBullet.ToString());
                    break;
                case "minigun(Clone)":
                    GUI.Label(_weaponsRect, "minigun");
                    GUI.Label(_weaponsBulletRect, Player.GetComponent<Weapons>().minigunBullet.ToString());
                    break;
                case "rocket(Clone)":
                    GUI.Label(_weaponsRect, "rocket");
                    GUI.Label(_weaponsBulletRect, Player.GetComponent<Weapons>().rocketBullet.ToString());
                    break;
                case "diskgun(Clone)":
                    GUI.Label(_weaponsRect, "diskgun");
                    GUI.Label(_weaponsBulletRect, Player.GetComponent<Weapons>().diskgunBullet.ToString());
                    break;
                case "firegun(Clone)":
                    GUI.Label(_weaponsRect, "firegun");
                    GUI.Label(_weaponsBulletRect, Player.GetComponent<Weapons>().firegunBullet.ToString());
                    break;
                case "zeusgun(Clone)":
                    GUI.Label(_weaponsRect, "zeusgun");
                    GUI.Label(_weaponsBulletRect, Player.GetComponent<Weapons>().zeusgunBullet.ToString());
                    break;
                case "plasmicgun(Clone)":
                    GUI.Label(_weaponsRect, "plasmicgun");
                    GUI.Label(_weaponsBulletRect, Player.GetComponent<Weapons>().plasmicgunBullet.ToString());
                    break;
                case "gaussgun(Clone)":
                    GUI.Label(_weaponsRect, "gaussgun");
                    GUI.Label(_weaponsBulletRect, Player.GetComponent<Weapons>().gaussgunBullet.ToString());
                    break;
            }

        //Смена оружия
        if (GUI.Button(_arrowLeftWeaponsRect, arrowWeapons))
            Player.GetComponent<Weapons>().currentWNum--;
        if(GUI.Button(_arrowRightWeaponsRect, arrowWeapons))
            Player.GetComponent<Weapons>().currentWNum++;
        }

#endregion

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
                        if (modeGame == "survival")
                        {
                            Player.GetComponent<Global>().SaveResultGame("sirvival");
                            PlayerPrefs.SetString("ActorNameSurvival", actorName);
                            PlayerPrefs.SetInt("ActorNameSurvivalScore", Player.GetComponent<Actor>().count);
                        }
                        else if (modeGame == "arena")
                        {
                            Player.GetComponent<Global>().SaveResultGame("arena");
                            PlayerPrefs.SetString("ActorNameArena", actorName);
                            PlayerPrefs.SetInt("ActorNameArenaScore", Player.GetComponent<Actor>().count);
                        }
                        else
                        {
                            Player.GetComponent<Global>().SaveResultGame("company");
                            PlayerPrefs.SetString("ActorNameCompany", actorName);
                            PlayerPrefs.SetInt("ActorNameCompanyScore", Player.GetComponent<Actor>().count);
                        }
					Application.LoadLevel("menu");
			}
			break;
            case 1:
            GUI.Label(new Rect(50, 30, 200, 30), "Громкость звука");
            AudioListener.volume = GUI.HorizontalSlider(new Rect(50, 60, 200, 20), AudioListener.volume, 0, 1);
            if (GUI.Button(new Rect(75, 90, 150, 30), "Продолжить"))
            {
                pause = false;
                Time.timeScale = 1;
            }
            if (GUI.Button(new Rect(30, 140, 100, 30), "Настройки"))
            {
                pause = false;
                Time.timeScale = 1;
            }
            if (GUI.Button(new Rect(170, 140, 100, 30), "Выход"))
                exit = true;
            break;
            case 11:
                    if (GUI.Button(new Rect(30, 40, 50, 30), "Да"))
                         Application.LoadLevel("menu");
                    if (GUI.Button(new Rect(120, 40, 50, 30), "Нет"))
                        exit = false;
            break;
		}
	}
}
