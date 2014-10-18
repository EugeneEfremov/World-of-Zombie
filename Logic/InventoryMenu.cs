using UnityEngine;
using System.Collections;

public class InventoryMenu : MonoBehaviour {


    private GameObject MenuObj;

    private Rect _windowRect, _gameMenuRect, _settingsRect, _continueRect; //Buttons of navigations
    private Rect _nameActorRect, _actorCompanyScore, _moneyRect, _helthRect, _strongRect, _speedRect, _accuracyRect; //Value
    private Rect _armourRect1, _armourRect2, _armourRect3, _lanternRect, _nvdRect, _dronRect, _liveRect, _helthResetRect; //Equipment
    private Rect _countHelthMaxRect, _countStrongMaxRect, _countSpeedMaxRect, _countAccuracyMaxRect; //Specifications of Actor
    private Rect _helthMaxButtonsRect, _strongMaxButtonsRect, _speedMaxButtonsRect, _accuracyMaxButtonsRect; //Specifications of Actor Buttons
    private Rect _pistolsRect, _gunRect, _grenadeRect, _minigunRect, _rocketRect, _diskgunRect, _firegunRect, _zeusgunRect, _plasmicgunRect, _gaussgunRect; //Level of Weapons
    private Rect _pistolsBulletRect, _gunBulletRect, _grenadeBulletRect, _minigunBulletRect, _rocketBulletRect, _diskgunBulletRect; //Bullet of Weapons

    public bool newGame = false; //new Game?
    public string nameActor;
    public int money, actorCompanyScore, loadLevel;
    public int armour, lantern, nvd, dron, live, helthReset; //Equipment
    public int helthMax, strongMax, speedMax, accuracyMax, countMax; //Specifications of Actor
    public int pistolsLvl, gunLvl, grenadeLvl, minigunLvl, rocketLvl, diskgunLvl, firegunLvl, zeusgunLvl, plasmicgunLvl, gaussgunLvl; //Level of Weapons
    public int gunBullet, grenadeBullet, minigunBullet, rocketBullet, diskgunBullet; //Bullet of Weapons

    void Start()
    {
        MenuObj = GameObject.Find("Menu");

        if (newGame) //Clear all values
        {
            loadLevel = 1;

            PlayerPrefs.SetString("ActorNameCompany", nameActor);
            PlayerPrefs.SetInt("0x01001", 700);
            PlayerPrefs.SetInt("0xffaa01", 1);
            PlayerPrefs.SetInt("0x01002", 0);
            PlayerPrefs.SetInt("0x02001", 0);
            PlayerPrefs.SetInt("0x01003", 0);
            PlayerPrefs.SetInt("0x01004", 0);
            PlayerPrefs.SetInt("0x01005", 0);
            PlayerPrefs.SetInt("0x01006", 0);
            PlayerPrefs.SetInt("0x01007", 0);
            PlayerPrefs.SetInt("0x01f01", 0);
            PlayerPrefs.SetInt("0x01f02", 0);
            PlayerPrefs.SetInt("0x01f03", 1);
            PlayerPrefs.SetInt("0x01f04", 1);
            PlayerPrefs.SetInt("fx10ab0", 1);
            PlayerPrefs.SetInt("fx10ab1", 0);
            PlayerPrefs.SetInt("fx10ab2", 0);
            PlayerPrefs.SetInt("fx10ab3", 0);
            PlayerPrefs.SetInt("fx10ab4", 0);
            PlayerPrefs.SetInt("fx10ab5", 0);
            PlayerPrefs.SetInt("fx01e01", 0);
            PlayerPrefs.SetInt("fx01e03", 0);
            PlayerPrefs.SetInt("fx01e05", 0);
            PlayerPrefs.SetInt("fx01e07", 0);
            PlayerPrefs.SetInt("ax90ab1", 0);
            PlayerPrefs.SetInt("ax90ab2", 0);
            PlayerPrefs.SetInt("ax90ab3", 0);
            PlayerPrefs.SetInt("ax90ab4", 0);
            PlayerPrefs.SetInt("ax90ab5", 0);
            PlayerPrefs.SetInt("0x910fa", 0);
        }

        nameActor = PlayerPrefs.GetString("ActorNameCompany").ToString();
        actorCompanyScore = PlayerPrefs.GetInt("ActorNameCompanyScore");
        money = PlayerPrefs.GetInt("0x01001");
        loadLevel = PlayerPrefs.GetInt("0xffaa01");
        //Исключение ошибки нулевого уровня
        if (loadLevel < 1) loadLevel = 1;

        armour = PlayerPrefs.GetInt("0x01002");
        lantern = PlayerPrefs.GetInt("0x01003");
        nvd = PlayerPrefs.GetInt("0x01004");
        dron = PlayerPrefs.GetInt("0x01005");
        live = PlayerPrefs.GetInt("0x01006");
        helthReset = PlayerPrefs.GetInt("0x01007");

        helthMax = PlayerPrefs.GetInt("0x01f01") * 40;
        strongMax = PlayerPrefs.GetInt("0x01f02") / 2;
        speedMax = PlayerPrefs.GetInt("0x01f03") / 10;
        //Исключение ошибки деления на нуль
        if (PlayerPrefs.GetInt("0x01f04") == 0) PlayerPrefs.SetInt("0x01f04", 1);
        accuracyMax = 1 / PlayerPrefs.GetInt("0x01f04");

        countMax = PlayerPrefs.GetInt("0x910fa");

        pistolsLvl = PlayerPrefs.GetInt("fx10ab0");
        gunLvl = PlayerPrefs.GetInt("fx10ab1");
        grenadeLvl = PlayerPrefs.GetInt("fx10ab2");
        minigunLvl = PlayerPrefs.GetInt("fx10ab3");
        rocketLvl = PlayerPrefs.GetInt("fx10ab4");
        diskgunLvl = PlayerPrefs.GetInt("fx10ab5");
        firegunLvl = PlayerPrefs.GetInt("fx01e01");
        zeusgunLvl = PlayerPrefs.GetInt("fx01e03");
        plasmicgunLvl = PlayerPrefs.GetInt("fx01e05");
        gaussgunLvl = PlayerPrefs.GetInt("fx01e07");

        gunBullet = PlayerPrefs.GetInt("ax90ab1");
        grenadeBullet = PlayerPrefs.GetInt("ax90ab2");
        minigunBullet = PlayerPrefs.GetInt("ax90ab3");
        rocketBullet = PlayerPrefs.GetInt("ax90ab4");
        diskgunBullet = PlayerPrefs.GetInt("ax90ab5");


//Rect
        _windowRect = new Rect (Screen.width / 2 - 350, Screen.height / 2 - 257, 700, 515);

        _nameActorRect = new Rect(140, 30, 150, 30);
        _actorCompanyScore = new Rect(310, 30, 50, 30);
        _moneyRect = new Rect(560, 30, 100, 30);

        _helthRect = new Rect (30, 90, 100, 30);
        _strongRect = new Rect (30, 150, 100, 30);
        _speedRect = new Rect (30, 210, 100, 30);
        _accuracyRect = new Rect (30, 270, 100, 30);

        _countHelthMaxRect = new Rect (145, 90, 30, 30);
        _countStrongMaxRect = new Rect(145, 150, 30, 30);
        _countSpeedMaxRect = new Rect(145, 210, 30, 30);
        _countAccuracyMaxRect = new Rect(145, 270, 30, 30);

        _armourRect1 = new Rect(235, 90, 40, 30);
        _armourRect2 = new Rect(285, 90, 40, 30);
        _armourRect3 = new Rect(335, 90, 40, 30);
        _lanternRect = new Rect (435, 90, 40, 30);
        _nvdRect = new Rect (485, 90, 40, 30);
        _dronRect = new Rect (535, 90, 40, 30);
        _liveRect = new Rect (585, 90, 40, 30);
        _helthResetRect = new Rect(635, 90, 40, 30);

        _pistolsRect = new Rect (250, 150, 70, 40);
        _gunRect = new Rect (350, 150, 70, 40);
        _grenadeRect = new Rect (450, 150, 50, 40);
        _minigunRect = new Rect (550, 150, 70, 40);
        _rocketRect = new Rect (250, 240, 70, 40);
        _diskgunRect = new Rect(350, 240, 70, 40);
        _firegunRect = new Rect(450, 240, 70, 40);
        _zeusgunRect = new Rect(550, 240, 70, 40);
        _plasmicgunRect = new Rect(250, 330, 70, 40);
        _gaussgunRect = new Rect(350, 330, 70, 40);

        _pistolsBulletRect = new Rect(300, 190, 30, 20);
        _gunBulletRect = new Rect(380, 190, 50, 20);
        _grenadeBulletRect = new Rect(480, 190, 50, 20);
        _minigunBulletRect = new Rect(580, 190, 50, 20);
        _rocketBulletRect = new Rect (300, 280, 50, 20);
        _diskgunBulletRect = new Rect(380, 280, 50, 20);

        _gameMenuRect = new Rect(70, 450, 150, 40);
        _settingsRect = new Rect(270, 450, 150, 40);
        _continueRect = new Rect(470, 450, 150, 40);
    }

    void WindowFunction(int id)
    {
        if (id == 0)
        {
            GUI.Label(new Rect(40, 30, 110, 30), "Имя игрока: ");
            GUI.Label(_nameActorRect, nameActor);
            GUI.Label(new Rect(240, 30, 60, 30), "Очки: ");
            GUI.Label(_actorCompanyScore, actorCompanyScore.ToString());
            GUI.Label(new Rect(480, 30, 70, 30), "Деньги: ");
            GUI.Label(_moneyRect, money.ToString());
            //Specifications of Actor
            GUI.Label(_helthRect, PlayerPrefs.GetInt("0x01f01").ToString());
            GUI.Label(_strongRect, PlayerPrefs.GetInt("0x01f02").ToString());
            GUI.Label(_speedRect, PlayerPrefs.GetInt("0x01f03").ToString());
            GUI.Label(_accuracyRect, PlayerPrefs.GetInt("0x01f04").ToString());

            //Upgrade specifications

            if (GUI.Button(_countHelthMaxRect, countMax.ToString()) && countMax > 0)
            {
                if (PlayerPrefs.GetInt("0x01f01") < 10)
                {
                    countMax--;
                    helthMax++;
                    PlayerPrefs.SetInt("0x910fa", countMax);
                    PlayerPrefs.SetInt("0x01f01", helthMax);
                }
            }

            if (GUI.Button(_countStrongMaxRect, countMax.ToString()) && countMax > 0)
            {
                if (PlayerPrefs.GetInt("0x01f02") < 10)
                {
                    countMax--;
                    strongMax++;
                    PlayerPrefs.SetInt("0x910fa", countMax);
                    PlayerPrefs.SetInt("0x01f02", strongMax);
                }
            }

            if (GUI.Button(_countSpeedMaxRect, countMax.ToString()) && countMax > 0)
            {
                if (PlayerPrefs.GetInt("0x01f03") < 10)
                {
                    countMax--;
                    speedMax++;
                    PlayerPrefs.SetInt("0x910fa", countMax);
                    PlayerPrefs.SetInt("0x01f03", speedMax);
                }
            }

            if (GUI.Button(_countAccuracyMaxRect, countMax.ToString()) && countMax > 0)
            {
                if (PlayerPrefs.GetInt("0x01f04") < 10)
                {
                    countMax--;
                    accuracyMax++;
                    PlayerPrefs.SetInt("0x910fa", countMax);
                    PlayerPrefs.SetInt("0x01f04", accuracyMax);
                }
            }

            //Equipment
            if (GUI.Button(_armourRect1, armour.ToString()) && armour < 100)
            {
                if (money >= 600)
                {
                    money -= 600;
                    armour = 100;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("0x02001", 100);
                    PlayerPrefs.SetInt("0x01002", 100);
                }
            }

            if (GUI.Button(_armourRect2, armour.ToString()) && armour < 200)
            {
                if (money >= 1100)
                {
                    money -= 1100;
                    armour = 200;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("0x02001", 200);
                    PlayerPrefs.SetInt("0x01002", 200);

                }
            }

            if (GUI.Button(_armourRect3, armour.ToString()) && armour < 300)
            {
                if (money >= 1500)
                {
                    money -= 1500;
                    armour = 300;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("0x02001", 300);
                    PlayerPrefs.SetInt("0x01002", 300);

                }
            }

            if (GUI.Button(_lanternRect, "Ф") && lantern != 1)
            {
                if (money >= 200)
                {
                    money -= 200;
                    lantern = 1;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("0x01003", 1);
                }
            }

            if (GUI.Button(_nvdRect, "пнв") && nvd != 1)
            {
                if (money >= 200)
                {
                    money -= 200;
                    nvd = 1;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("0x01004", 1);
                }
            }

            GUI.Label(_dronRect, dron.ToString());

            if (GUI.Button(_liveRect, "live") && live <= 5)
            {
                if (money >= 2000)
                {
                    money -= 2000;
                    live += 1;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("0x01006", live++);
                }
            }

            if (GUI.Button(_helthResetRect, "hR") && helthReset != 1)
            {
                if (money >= 1000)
                {
                    money -= 1000;
                    helthReset = 1;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("0x01007", 1);
                }
            }

            //Weapons
            GUI.Label(_pistolsRect, pistolsLvl.ToString());
            GUI.Label(_gunRect, gunLvl.ToString());
            GUI.Label(_grenadeRect, grenadeLvl.ToString());
            GUI.Label(_minigunRect, minigunLvl.ToString());
            GUI.Label(_rocketRect, rocketLvl.ToString());
            GUI.Label(_diskgunRect, diskgunLvl.ToString());
            GUI.Label(_firegunRect, firegunLvl.ToString());
            GUI.Label(_zeusgunRect, zeusgunLvl.ToString());
            GUI.Label(_plasmicgunRect, plasmicgunLvl.ToString());
            GUI.Label(_gaussgunRect, gaussgunLvl.ToString());

            //Bullet
            GUI.Label(_pistolsBulletRect, 999999.ToString());

            if (GUI.Button(_gunBulletRect,"50$" + gunBullet.ToString()) && gunBullet < 100 + 100 * strongMax){
                if (money >= 30)
                {
                    money -= 30;
                    gunBullet += 50;
                    if (gunBullet > 100 + gunBullet * strongMax)
                        gunBullet = 100 + gunBullet * strongMax;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("ax90ab1", gunBullet);
                }
            }

            if (GUI.Button(_grenadeBulletRect, "50$" + grenadeBullet.ToString()) && grenadeBullet < 100 + 100 * strongMax)
            {
                if (money >= 50)
                {
                    money -= 50;
                    grenadeBullet += 40;
                    if (grenadeBullet > 100 + grenadeBullet * strongMax)
                        grenadeBullet = 100 + grenadeBullet * strongMax;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("ax90ab2", grenadeBullet);
                }
            }

            if (GUI.Button(_minigunBulletRect, "100$" + minigunBullet.ToString()) && minigunBullet < 1000 + 350 * strongMax)
            {
                if (money >= 100)
                {
                    money -= 100;
                    minigunBullet += 100;
                    if (minigunBullet > 100 + minigunBullet * strongMax)
                        minigunBullet = 100 + minigunBullet * strongMax;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("ax90ab3", minigunBullet);
                }
            }

            if (GUI.Button(_rocketBulletRect, "150$" + rocketBullet.ToString()) && rocketBullet < 100 + 100 * strongMax)
            {
                if (money >= 150)
                {
                    money -= 150;
                    rocketBullet += 10;
                    if (rocketBullet > 100 + rocketBullet * strongMax)
                        rocketBullet = 100 + rocketBullet * strongMax;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("ax90ab4", rocketBullet);
                }
            }

            if (GUI.Button(_diskgunBulletRect, "150$" + diskgunBullet.ToString()) && diskgunBullet < 100 + 100 * strongMax)
            {
                if (money >= 190)
                {
                    money -= 190;
                    diskgunBullet += 40;
                    if (diskgunBullet > 100 + diskgunBullet * strongMax)
                        diskgunBullet = 100 + diskgunBullet * strongMax;
                    PlayerPrefs.SetInt("0x01001", money);
                    PlayerPrefs.SetInt("ax90ab5", diskgunBullet);
                }
            }

            //Buttons of navigations
            if (GUI.Button(_gameMenuRect, "Выход в меню"))
            {
                MenuObj.GetComponent<InventoryMenu>().enabled = false;
                MenuObj.GetComponent<Menu>().enabled = true;
            }
            GUI.Button(_settingsRect, "Настройки");
            if (GUI.Button(_continueRect, "Продолжить"))
            {
                Application.LoadLevel("level_" + loadLevel);
            }
        }
        else
            print("error: InventoryMenu.cs -> WindowFunction, id != 0");
    }

    void OnGUI()
    {
        GUI.Window(0, _windowRect, WindowFunction, "");
    }
}
