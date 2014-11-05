using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

    public enum GameMode
    {
        survival,
        arena,
        level1
    };

    private int _newNameZomb, _magic4Helth, _magic4Armour;
    private Rect _helthRect, _accountRect, _armourRect, _leftInfoRect, _rightInfoRect, _deathRect, _pauseButtonRect, _pauseRect, _exitRect, _nvdRect, _helthResetRect, _liveActorRect;
    private Rect _magic1Rect, _magic2Rect, _magic3Rect, _magic4Rect;
    private Rect _arrowLeftWeaponsRect, _arrowRightWeaponsRect, _weaponsRect, _weaponsBulletRect;
    private Transform magic2ParticleNew, magic3Particle, magic4ParticleNew;

    public GameMode gameMode = GameMode.survival;

    public int magic1b, magic2b, magic3b, magic4b;
    public float timeZombie = 1, timeMagic1, timeMagic2, timeMagic3, timeMagic4;
    public Texture2D goal, leftInfo, rightInfo, pauseT, pauseDownT, arrowWeapons, nvdT, nvdCam, helthResetT;
    public Texture2D magic1, magic2, magic3, magic4;
    public bool pause = false, bMagic1, bMagic2, bMagic3, bMagic2Play, bMagic3Play, bMagic4Play, bMagic4, exit = false, helthResetB, nvdEnabled = false;
    public Vector2 mp;
    public Transform Player, magic2Particle, magic3Zone, magic4Particle;
	public string actorName= "", modeGame, currentW;
    public GameObject ZombAll;

    //Beta версия, вывод FPS
    private float time, timeOneS = 0;
    private int frames = 0;
    private float fps;

	void Start () {
        ZombAll = GameObject.Find("ZombieLogic");
        modeGame = Application.loadedLevelName;
		//Screen.showCursor = false;
		Player = GameObject.Find ("Actor").transform;

        magic1b = PlayerPrefs.GetInt("fx106f1");
        magic2b = PlayerPrefs.GetInt("fx106f2");
        magic3b = PlayerPrefs.GetInt("fx106f3");
        magic4b = PlayerPrefs.GetInt("fx106f4");

		_helthRect = new Rect (10, 0, 100, 30);
        _armourRect = new Rect(10, 30, 100, 30);
        _accountRect = new Rect(Screen.width - 100, 0, 100, 30);
		_leftInfoRect = new Rect (0, 0, 125, 123);
		_rightInfoRect = new Rect (Screen.width - 150, 0, 150, 37);
        _deathRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
        _pauseButtonRect = new Rect(5, 70, 44, 43);
        _pauseRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 75, 300, 150);

        _nvdRect = new Rect(10, 130, 25, 20);

        _liveActorRect = new Rect(60, 105, 30, 30);
        _helthResetRect = new Rect(70, 80, 30, 25);

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

    void Update()
    {
        if (Fps() > 55)
            fpsGS.normal.textColor = Color.green;
        if (Fps () < 54 && Fps() > 31)
            fpsGS.normal.textColor = Color.yellow;
        if (Fps() < 30)
            fpsGS.normal.textColor = Color.red;
    }


    void FixedUpdate()
    {

        #region Magic
        switch (gameMode)
        {
            #region survival
            case GameMode.survival:
                //Magic1
                if (bMagic1)
                {
                 ZombAll.GetComponent<ZombieAll>().magic1 = true;
                 timeMagic1 -= Time.deltaTime * 3;
                 if (timeMagic1 <= 0)
                      bMagic1 = false;
             }
             if (!bMagic1)
              {
                 ZombAll.GetComponent<ZombieAll>().magic1 = false;
                 if (timeMagic1 < 30)
                      timeMagic1 += Time.deltaTime;
               }

               //Magic2
                if (bMagic2)
                {
                    if (!bMagic2Play)
                    {
                        magic2ParticleNew = Instantiate(magic2Particle, transform.position, Quaternion.Euler(90, 0, 0)) as Transform;
                        magic2ParticleNew.parent = transform; //Присвоение к актеру
                        Destroy(GameObject.Find("magic2(Clone)"), 2.5f);
                        bMagic2Play = true;
                    }
                 ZombAll.GetComponent<ZombieAll>().magic2 = true;
                 timeMagic2 -= Time.deltaTime * 20;
                 if (timeMagic2 <= 0)
                 {
                     bMagic2Play = false;
                     ZombAll.GetComponent<ZombieAll>().magic2kill = true;
                     bMagic2 = false;
                 }
             }
             if (!bMagic2)
              {
                 ZombAll.GetComponent<ZombieAll>().magic2 = false;
                 if (timeMagic2 < 40)
                     timeMagic2 += Time.deltaTime;
                 if (timeMagic2 > 1)
                    ZombAll.GetComponent<ZombieAll>().magic2kill = false;
               }

             //Magic3
             if (bMagic3)
             {
                 timeMagic3 -= Time.deltaTime * 30;
                 if (!bMagic3Play)
                 {
                     magic3Particle = Instantiate(magic3Zone, transform.position, Quaternion.Euler(0, 0, 0)) as Transform;
                     magic3Particle.parent = transform; //Присвоение к актеру
                     bMagic3Play = true;
                 }
                 if (timeMagic3 <= 0)
                 {
                     bMagic3 = false;
                     bMagic3Play = false;
                 }
             }
             if (!bMagic3)
             {
                 if (timeMagic3 < 50)
                     timeMagic3 += Time.deltaTime;
             }

             //Magic4
             if (bMagic4)
             {
                 if (!bMagic4Play)
                 {
                     magic4ParticleNew = Instantiate(magic4Particle, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.275401f), Quaternion.Euler(0, 0, 0)) as Transform;
                     magic4ParticleNew.parent = transform; //Присвоение к актеру
                     bMagic4Play = true;
                 }
                 Player.GetComponent<Actor>().helth = _magic4Helth;
                 Player.GetComponent<Actor>().armour = _magic4Armour;
                 timeMagic4 -= Time.deltaTime * 6;
                 if (timeMagic4 <= 0)
                 {
                     bMagic4Play = false;
                     Destroy(GameObject.Find("magic4(Clone)"));
                     bMagic4 = false;
                 }
             }
             if (!bMagic4)
             {
                 if (timeMagic4 < 70)
                     timeMagic4 += Time.deltaTime;
             }
             break;
            #endregion
            #region arena
            case GameMode.arena:
             //Magic1
             if (bMagic1)
             {
                 ZombAll.GetComponent<ZombieAll>().magic1 = true;
                 timeMagic1 -= Time.deltaTime * 3;
                 if (timeMagic1 <= 0)
                     bMagic1 = false;
             }
             if (!bMagic1)
             {
                 ZombAll.GetComponent<ZombieAll>().magic1 = false;
                 if (timeMagic1 < 30)
                     timeMagic1 += Time.deltaTime;
             }

             //Magic2
             if (bMagic2)
             {
                 if (!bMagic2Play)
                 {
                     magic2ParticleNew = Instantiate(magic2Particle, new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z), Quaternion.Euler(90, 0, 0)) as Transform;
                     magic2ParticleNew.parent = transform; //Присвоение к актеру
                     Destroy(GameObject.Find("magic2(Clone)"), 2.5f);
                     bMagic2Play = true;
                 }
                 ZombAll.GetComponent<ZombieAll>().magic2 = true;
                 timeMagic2 -= Time.deltaTime * 20;
                 if (timeMagic2 <= 0)
                 {
                     bMagic2Play = false;
                     ZombAll.GetComponent<ZombieAll>().magic2kill = true;
                     bMagic2 = false;
                 }
             }
             if (!bMagic2)
             {
                 ZombAll.GetComponent<ZombieAll>().magic2 = false;
                 if (timeMagic2 < 40)
                     timeMagic2 += Time.deltaTime;
                 if (timeMagic2 > 1)
                     ZombAll.GetComponent<ZombieAll>().magic2kill = false;
             }

             //Magic3
             if (bMagic3)
             {
                 timeMagic3 -= Time.deltaTime * 30;
                 if (!bMagic3Play)
                 {
                     magic3Particle = Instantiate(magic3Zone, new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z), Quaternion.Euler(0, 0, 0)) as Transform;
                     magic3Particle.parent = transform; //Присвоение к актеру
                     bMagic3Play = true;
                 }
                 if (timeMagic3 <= 0)
                 {
                     bMagic3 = false;
                     bMagic3Play = false;
                 }
             }
             if (!bMagic3)
             {
                 if (timeMagic3 < 50)
                     timeMagic3 += Time.deltaTime;
             }
             break;
            #endregion
            #region level1
            case GameMode.level1:
             //Magic1
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
                 if (timeMagic1 < 30)
                     timeMagic1 += Time.deltaTime * 3;
             }

             //Magic2
             if (bMagic2)
             {
                 ZombAll.GetComponent<Level_1>().magic2 = true;
                 timeMagic2 -= Time.deltaTime * 20;
                 if (timeMagic2 <= 0)
                 {
                     ZombAll.GetComponent<Level_1>().magic2kill = true;
                     bMagic2 = false;
                 }
             }
             if (!bMagic2)
             {
                 ZombAll.GetComponent<Level_1>().magic2 = false;
                 if (timeMagic2 < 40)
                     timeMagic2 += Time.deltaTime;
                 if (timeMagic2 > 1)
                     ZombAll.GetComponent<Level_1>().magic2kill = false;
             }

             //Magic3
             if (bMagic3)
             {
                 timeMagic3 -= Time.deltaTime * 30;
                 Instantiate(magic3Zone, transform.position, Quaternion.Euler(0, 0, 0));
                 if (timeMagic3 <= 0)
                     bMagic3 = false;
             }
             if (!bMagic3)
             {
                 if (timeMagic3 < 50)
                     timeMagic3 += Time.deltaTime;
             }

             //Magic4
             if (bMagic4)
             {
                 Player.GetComponent<Actor>().helth = _magic4Helth;
                 Player.GetComponent<Actor>().armour = _magic4Armour;
                 timeMagic4 -= Time.deltaTime * 6;
                 if (timeMagic4 <= 0)
                     bMagic4 = false;
             }
             if (!bMagic4)
             {
                 if (timeMagic4 < 70)
                     timeMagic4 += Time.deltaTime;
             }
             break;
            #endregion
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
        //Beta версия, вывод FPS
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 50, 100, 30), "fps " + fps.ToString(), fpsGS);
        if (gameMode != GameMode.level1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 50, 100, 30), "time: " + ZombAll.GetComponent<ZombieAll>().timeInGame.ToString());
            GUI.Label(new Rect(Screen.width / 2 - 210, Screen.height - 50, 100, 30), "zomb: " + ZombAll.GetComponent<ZombieAll>().accountZombNew.ToString());
        }

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
        if (magic1b == 1 && timeMagic1 >= 30)
        {
            if (GUI.Button(_magic1Rect, magic1))
                bMagic1 = true;
        }

        if (magic2b == 1 && timeMagic2 >= 40)
        {
            if (GUI.Button(_magic2Rect, magic2))
                bMagic2 = true;
        }

        if (magic3b == 1 && timeMagic3 >= 50)
        {
            if (GUI.Button(_magic3Rect, magic3))
                bMagic3 = true;
        }

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
            {
                Player.GetComponent<Weapons>().currentWNum--;
                Player.GetComponent<Weapons>().buttonSwitchOn = true;
            }
            if (GUI.Button(_arrowRightWeaponsRect, arrowWeapons))
            {
                Player.GetComponent<Weapons>().currentWNum++;
                Player.GetComponent<Weapons>().buttonSwitchOn = true;
            }
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
                            if (PlayerPrefs.GetInt("ActorNameSurvivalScore") <= Player.GetComponent<Actor>().count)
                            {
                                PlayerPrefs.SetString("ActorNameSurvival", actorName);
                                PlayerPrefs.SetInt("ActorNameSurvivalScore", Player.GetComponent<Actor>().count);
                            }
                        }
                        else if (modeGame == "arena")
                        {
                            Player.GetComponent<Global>().SaveResultGame("arena");
                            if (PlayerPrefs.GetInt("ActorNameArenaScore") <= Player.GetComponent<Actor>().count)
                            {
                                PlayerPrefs.SetString("ActorNameArena", actorName);
                                PlayerPrefs.SetInt("ActorNameArenaScore", Player.GetComponent<Actor>().count);
                            }
                        }
                        else
                        {
                            Player.GetComponent<Global>().SaveResultGame("company");
                            if (PlayerPrefs.GetInt("ActorNameCompanyScore") <= Player.GetComponent<Actor>().count)
                            {
                                PlayerPrefs.SetString("ActorNameCompany", actorName);
                                PlayerPrefs.SetInt("ActorNameCompanyScore", Player.GetComponent<Actor>().count);
                            }
                        }
					Application.LoadLevel("menu");
			}
			break;
            case 1:
            GUI.Label(new Rect(50, 30, 200, 30), "Громкость звука");
            AudioListener.volume = GUI.HorizontalSlider(new Rect(50, 60, 200, 20), AudioListener.volume, 0, 1);
            if (GUI.Button(new Rect(30, 90, 100, 30), "Продолжить"))
            {
                pause = false;
                Time.timeScale = 1;
            }
            if (GUI.Button(new Rect(170, 90, 100, 30), "Выход"))
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
