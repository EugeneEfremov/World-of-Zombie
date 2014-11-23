using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

    public enum GameMode
    {
        survival,
        arena,
        level1
    };

    private int _magic1b, _magic2b, _magic3b, _magic4b;
    private float _timeLogBonus, _timeZombie, _timeAction, _timeMagic1, _timeMagic2, _timeMagic3, _timeMagic4;
    private int _newNameZomb;
    private Rect _helthRect, _accountRect, _armourRect, _leftInfoRect, _rightInfoRect, _deathRect, _pauseButtonRect, _pauseRect, _exitRect, _lanternRect, _helthResetRect, _liveActorRect;
    private Rect _magic1Rect, _magic2Rect, _magic3Rect, _magic4Rect;
    private Rect _arrowLeftWeaponsRect, _arrowRightWeaponsRect, _weaponsRect, _weaponsBulletRect;
    private Rect _logBonus;
    private Magic magicClass;

    public GameMode gameMode = GameMode.survival;

    public Texture2D goal, leftInfo, rightInfo, pauseT, pauseDownT, arrowWeapons, lanternT, helthResetT;
    public Texture2D magic1, magic2, magic3, magic4;
    public bool pause = false, exit = false, helthResetB, lanternEnabled = false;
    public Vector2 mp;
	public string actorName= "", modeGame, currentW, logBonusString;
    public GameObject ZombAll, lanternObj;

    //Beta версия, вывод FPS
    private float time, timeOneS = 0;
    private int frames = 0;
    private float fps;

	void Start () {
        ZombAll = GameObject.Find("ZombieLogic");
        modeGame = Application.loadedLevelName;
        magicClass = ZombAll.GetComponent<Magic>();
        lanternObj = GameObject.Find("Lantern");

		_helthRect = new Rect (10, 0, 190, 50);
        _armourRect = new Rect(10, 40, 190, 50);
        _accountRect = new Rect(Screen.width - 160, 0, 180, 30);
		_leftInfoRect = new Rect (0, 0, 195, 193);
		_rightInfoRect = new Rect (Screen.width - 188, 0, 188, 48);
        _deathRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
        _pauseButtonRect = new Rect(10, 100, 70, 72);
        _pauseRect = new Rect(Screen.width / 2 - 250, Screen.height / 2 - 150, 500, 300);
        _logBonus = new Rect(170, 0, 200, 200);

        _lanternRect = new Rect(10, 200, 50, 40);

        _liveActorRect = new Rect(105, 110, 40, 40);
        _helthResetRect = new Rect(75, 140, 40, 35);


        _weaponsRect = new Rect(Screen.width - 100, 50, 100, 40);
        _weaponsBulletRect = new Rect(Screen.width - 90, 80, 40, 30);
        _arrowRightWeaponsRect = new Rect(Screen.width - 70, 100, 40, 40);
        _arrowLeftWeaponsRect = new Rect(Screen.width - 130, 100, 40, 40);

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

    //Beta версия, вывод FPS, помещать в Update()
    public float Fps()
    {
        timeOneS += Time.deltaTime;
        if (timeOneS <= 1)
        {
            time += Time.deltaTime;
            frames++;
            fps = frames / time;
        }
        else
        {
            time = 0;
            frames = 0;
            fps = 0;
            timeOneS = 0;
        }
        return fps;
    }

    GUIStyle fpsGS = new GUIStyle();

    void FixedUpdate()
    {
        #region Бета FPS
        if (Fps() >= 24)
            fpsGS.normal.textColor = Color.green;
        if (Fps() <= 23 && Fps() >= 20)
            fpsGS.normal.textColor = Color.yellow;
        if (Fps() < 20)
            fpsGS.normal.textColor = Color.red;
        #endregion

        _magic1b = magicClass.magic1b;
        _magic2b = magicClass.magic2b;
        _magic3b = magicClass.magic3b;
        _magic4b = magicClass.magic4b;

        _timeMagic1 = magicClass.timeMagic1;
        _timeMagic2 = magicClass.timeMagic2;
        _timeMagic3 = magicClass.timeMagic3;
        _timeMagic4 = magicClass.timeMagic4;

        //Вывод в лог подобраных бонусов
        if (logBonusString != "")
        {
            _timeLogBonus += Time.deltaTime;
            if (_timeLogBonus > 7)
            {
                _timeLogBonus = 0;
                logBonusString = "";
            }
        }

        //Для GUI
        _timeAction += Time.deltaTime;

        if (gameMode != GameMode.arena)
            currentW = transform.GetComponent<Weapons>().currentW;

        if (pause)
            Time.timeScale = 0;

        if (transform.GetComponent<Actor>().helthReset > 0)
            helthResetB = true;

        if (transform.GetComponent<Actor>().helthReset == 0)
            helthResetB = false;
    }

	void OnGUI (){
        //Beta версия, вывод FPS
        if (gameMode != GameMode.level1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 50, 100, 30), "fps " + ((int)fps).ToString() + "  g: " + QualitySettings.GetQualityLevel().ToString(), fpsGS);
            if (gameMode != GameMode.level1)
            {
                GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 50, 100, 30), "time: " + ((int)ZombAll.GetComponent<ZombieAll>().timeInGame).ToString());
                GUI.Label(new Rect(Screen.width / 2 - 210, Screen.height - 50, 100, 30), "zomb: " + ZombAll.GetComponent<ZombieAll>().accountZombNew.ToString());
            }
        }
        
		GUI.depth = 1;
		GUI.DrawTexture (_leftInfoRect, leftInfo);
		GUI.DrawTexture (_rightInfoRect, rightInfo);
		GUI.Label (_helthRect, "hp " + transform.GetComponent<Actor>().helth.ToString());
        GUI.Label(_armourRect, "A " + transform.GetComponent<Actor>().armour.ToString());
        GUI.Label(_accountRect, transform.GetComponent<Actor>().count.ToString());
        GUI.Label(_liveActorRect, transform.GetComponent<Actor>().live.ToString());
        GUI.Label (_logBonus, logBonusString, fpsGS);

        #region Lantern
        if (GUI.Button(_lanternRect, "Ф"))
        {
            if (!lanternEnabled && _timeAction > 1)
            {
                lanternObj.light.enabled = true;
                lanternEnabled = true;
                _timeAction = 0;
            }
            if (lanternEnabled && _timeAction > 1)
            {
                lanternObj.light.enabled = false;
                lanternEnabled = false;
                _timeAction = 0;
            }
        }
        #endregion

        #region Helth Reset
        if (helthResetB)
            GUI.Label (_helthResetRect, helthResetT);
        #endregion

        #region Pause Button
        if (GUI.Button(_pauseButtonRect, pauseT))
            pause = true;
        #endregion

        #region Window Pause
        if (pause)
            GUI.Window(1, _pauseRect, WindowFunction, "Пауза");
        #endregion

        #region Window Exit
        if (exit)
            GUI.Window(11, _exitRect, WindowFunction, "Вы хотите выйти?");
        #endregion

        #region MAGIC
        if (_magic1b == 1 && _timeMagic1 >= 30)
        {
            if (GUI.Button(_magic1Rect, magic1))
                magicClass.bMagic1 = true;
        }

        if (_magic2b == 1 && _timeMagic2 >= 40)
        {
            if (GUI.Button(_magic2Rect, magic2))
                magicClass.bMagic2 = true;
        }

        if (_magic3b == 1 && _timeMagic3 >= 50)
        {
            if (GUI.Button(_magic3Rect, magic3))
                magicClass.bMagic3 = true;
        }

        if (_magic4b == 1 && _timeMagic4 >= 70)
        {
            if (GUI.Button(_magic4Rect, magic4))
                magicClass.bMagic4 = true;
        }
        #endregion     

        #region Window Message Death
        if (transform.GetComponent<Actor> ().death) {
            GUI.Window(0, _deathRect, WindowFunction, "Вы погибли");
        }
        #endregion

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
                    GUI.Label(_weaponsBulletRect, transform.GetComponent<Weapons>().gunBullet.ToString());
                    break;
                case "grenade(Clone)":
                    GUI.Label(_weaponsRect, "grenade");
                    GUI.Label(_weaponsBulletRect, transform.GetComponent<Weapons>().grenadeBullet.ToString());
                    break;
                case "minigun(Clone)":
                    GUI.Label(_weaponsRect, "minigun");
                    GUI.Label(_weaponsBulletRect, transform.GetComponent<Weapons>().minigunBullet.ToString());
                    break;
                case "rocket(Clone)":
                    GUI.Label(_weaponsRect, "rocket");
                    GUI.Label(_weaponsBulletRect, transform.GetComponent<Weapons>().rocketBullet.ToString());
                    break;
                case "diskgun(Clone)":
                    GUI.Label(_weaponsRect, "diskgun");
                    GUI.Label(_weaponsBulletRect, transform.GetComponent<Weapons>().diskgunBullet.ToString());
                    break;
                case "firegun(Clone)":
                    GUI.Label(_weaponsRect, "firegun");
                    GUI.Label(_weaponsBulletRect, ((int) transform.GetComponent<Weapons>().firegunBullet).ToString());
                    break;
                case "zeusgun(Clone)":
                    GUI.Label(_weaponsRect, "zeusgun");
                    GUI.Label(_weaponsBulletRect, ((int) transform.GetComponent<Weapons>().zeusgunBullet).ToString());
                    break;
                case "plasmicgun(Clone)":
                    GUI.Label(_weaponsRect, "plasmicgun");
                    GUI.Label(_weaponsBulletRect, ((int) transform.GetComponent<Weapons>().plasmicgunBullet).ToString());
                    break;
                case "gaussgun(Clone)":
                    GUI.Label(_weaponsRect, "gaussgun");
                    GUI.Label(_weaponsBulletRect, transform.GetComponent<Weapons>().gaussgunBullet.ToString());
                    break;
            }

        //Смена оружия
            if (GUI.Button(_arrowLeftWeaponsRect, arrowWeapons))
                transform.GetComponent<Weapons>().currentWNum--;

            if (GUI.Button(_arrowRightWeaponsRect, arrowWeapons))
                transform.GetComponent<Weapons>().currentWNum++;
        }

#endregion

    }

	void WindowFunction(int id){
		switch (id)
        {
            #region Death
            case 0:
				GUI.Label ( new Rect(20, 30, 80, 80), "Ваш счет: ");
				GUI.Label ( new Rect(95, 30, 80, 80), transform.GetComponent<Actor>().count.ToString());

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
                            if (ZombAll.GetComponent<Survival>().day)
                                transform.GetComponent<Global>().SaveResultGame("sirvival", true);

                            if (!ZombAll.GetComponent<Survival>().day)
                                transform.GetComponent<Global>().SaveResultGame("sirvival", false);

                            if (PlayerPrefs.GetInt("ActorNameSurvivalScore") <= transform.GetComponent<Actor>().count)
                            {
                                PlayerPrefs.SetString("ActorNameSurvival", actorName);
                                PlayerPrefs.SetInt("ActorNameSurvivalScore", transform.GetComponent<Actor>().count);
                            }
                        }

                        else if (modeGame == "arena")
                        {
                            transform.GetComponent<Global>().SaveResultGame("arena", false);
                            if (PlayerPrefs.GetInt("ActorNameArenaScore") <= transform.GetComponent<Actor>().count)
                            {
                                PlayerPrefs.SetString("ActorNameArena", actorName);
                                PlayerPrefs.SetInt("ActorNameArenaScore", transform.GetComponent<Actor>().count);
                            }
                        }

                        else
                        {
                            transform.GetComponent<Global>().SaveResultGame("company", false);
                            if (PlayerPrefs.GetInt("ActorNameCompanyScore") <= transform.GetComponent<Actor>().count)
                            {
                                PlayerPrefs.SetString("ActorNameCompany", actorName);
                                PlayerPrefs.SetInt("ActorNameCompanyScore", transform.GetComponent<Actor>().count);
                            }
                        }
					Application.LoadLevel("menu");
			}
			break;
            #endregion

            #region Pause
            case 1:
            GUI.Label(new Rect(50, 30, 200, 30), "Громкость звука");
            AudioListener.volume = GUI.HorizontalSlider(new Rect(50, 60, 200, 20), AudioListener.volume, 0, 1);

            //BETA
            GUI.Label(new Rect(100, 90, 200, 30), "КОЛ-ВО ЗОМБИ (МАКСИМУМ)");
            GUI.TextArea(new Rect(50, 90, 40, 20), ZombAll.GetComponent<ZombieAll>()._accountZombMax.ToString());
            if (GUI.Button(new Rect(50, 140, 50, 30), "21"))
            {
                ZombAll.GetComponent<ZombieAll>().timeInGame = 301;
                ZombAll.GetComponent<ZombieAll>()._accountZombMax = 20;
                ZombAll.GetComponent<ZombieAll>()._countTypeZombieInGame = 4;
            }
            if (GUI.Button(new Rect(120, 140, 50, 30), "26"))
            {
                ZombAll.GetComponent<ZombieAll>().timeInGame = 301;
                ZombAll.GetComponent<ZombieAll>()._accountZombMax = 25;
                ZombAll.GetComponent<ZombieAll>()._countTypeZombieInGame = 4;
            }
            if (GUI.Button(new Rect(190, 140, 50, 30), "31"))
            {
                ZombAll.GetComponent<ZombieAll>().timeInGame = 301;
                ZombAll.GetComponent<ZombieAll>()._accountZombMax = 30;
                ZombAll.GetComponent<ZombieAll>()._countTypeZombieInGame = 4;
            }
            //END BETA

            if (GUI.Button(new Rect(30, 230, 150, 50), "Продолжить"))
            {
                pause = false;
                Time.timeScale = 1;
            }

            if (GUI.Button(new Rect(325, 230, 150, 50), "Выход"))
                exit = true;
            break;
            #endregion

            #region Exit
            case 11:
                    if (GUI.Button(new Rect(30, 40, 50, 30), "Да"))
                         Application.LoadLevel("menu");
                    if (GUI.Button(new Rect(120, 40, 50, 30), "Нет"))
                        exit = false;
            break;
            #endregion
        }
	}
}
