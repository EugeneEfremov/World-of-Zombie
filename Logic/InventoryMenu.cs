using UnityEngine;
using System.Collections;

public class InventoryMenu : MonoBehaviour {


    private GameObject MenuObj;

    private Rect _windowRect, _gameMenuRect, _settingsRect, _continueRect; //Buttons of navigations
    private Rect _nameActorRect, _moneyRect, _helthRect, _strongRect, _speedRect, _accuracyRect; //Value
    private Rect _armourRect, _lanternRect, _nvdRect, _dronRect, _liveRect, _helthResetRect; //Equipment
    private Rect _countHelthMaxRect, _countStrongMaxRect, _countSpeedMaxRect, _countAccuracyMaxRect; //Specifications of Actor
    private Rect _helthMaxButtonsRect, _strongMaxButtonsRect, _speedMaxButtonsRect, _accuracyMaxButtonsRect; //Specifications of Actor Buttons
    private Rect _pistolsRect, _gunRect, _grenadeRect, _minigunRect, _rocketRect, _diskgunRect, _firegunRect, _zeusgunRect, _plasmicgunRect, _gaussgunRect; //Level of Weapons
    private Rect _pistolsBulletRect, _gunBulletRect, _grenadeBulletRect, _minigunBulletRect, _rocketBulletRect, _diskgunBulletRect; //Bullet of Weapons

    public bool newGame = false; //new Game?
    public string nameActor;
    public int money, loadLevel;
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
            PlayerPrefs.SetInt("0x01001", 0);
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
            PlayerPrefs.SetInt("0x01001", 0);
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
        }

        nameActor = PlayerPrefs.GetString("ActorNameCompany").ToString();
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

        _helthRect = new Rect (30, 30, 100, 30);
        _strongRect = new Rect (30, 90, 100, 30);
        _speedRect = new Rect (30, 150, 100, 30);
        _accuracyRect = new Rect (30, 210, 100, 30);

        _countHelthMaxRect = new Rect (145, 30, 100, 30);
        _countStrongMaxRect = new Rect(145, 90, 100, 30);
        _countSpeedMaxRect = new Rect(145, 150, 100, 30);
        _countAccuracyMaxRect = new Rect(145, 210, 100, 30);

        _armourRect = new Rect(235, 30, 40, 30);
        _lanternRect = new Rect (285, 30, 40, 30);
        _nvdRect = new Rect (335, 30, 40, 30);
        _dronRect = new Rect (385, 30, 40, 30);
        _liveRect = new Rect (435, 30, 40, 30);
        _helthResetRect = new Rect(485, 30, 40, 30);

        _pistolsRect = new Rect (250, 90, 70, 40);
        _gunRect = new Rect (350, 90, 70, 40);
        _grenadeRect = new Rect (450, 90, 50, 40);
        _minigunRect = new Rect (550, 90, 70, 40);
        _rocketRect = new Rect (250, 170, 70, 40);
        _diskgunRect = new Rect(350, 170, 70, 40);
        _firegunRect = new Rect(450, 170, 70, 40);
        _zeusgunRect = new Rect(550, 170, 70, 40);
        _plasmicgunRect = new Rect(250, 260, 70, 40);
        _gaussgunRect = new Rect(350, 260, 70, 40);

        _pistolsBulletRect = new Rect(300, 130, 30, 20);
        _gunBulletRect = new Rect(400, 130, 30, 20);
        _grenadeBulletRect = new Rect(500, 130, 30, 20);
        _minigunBulletRect = new Rect(600, 130, 30, 20);
        _rocketBulletRect = new Rect (300, 210, 30, 20);
        _diskgunBulletRect = new Rect(400, 210, 30, 20);

        _gameMenuRect = new Rect(70, 450, 150, 40);
        _settingsRect = new Rect(270, 450, 150, 40);
        _continueRect = new Rect(470, 450, 150, 40);
    }

    void WindowFunction(int id)
    {
        if (id == 0)
        {
            //Specifications of Actor
            GUI.Label(_helthRect, helthMax.ToString());
            GUI.Label(_strongRect, strongMax.ToString());
            GUI.Label(_speedRect, speedMax.ToString());
            GUI.Label(_accuracyRect, accuracyMax.ToString());

            //Upgrade specifications

            GUI.Label(_countHelthMaxRect, countMax.ToString());
            GUI.Label(_countStrongMaxRect, countMax.ToString());
            GUI.Label(_countSpeedMaxRect, countMax.ToString());
            GUI.Label(_countAccuracyMaxRect, countMax.ToString());

            //Equipment
            GUI.Label(_armourRect, armour.ToString());
            GUI.Label(_lanternRect, lantern.ToString());
            GUI.Label(_nvdRect, nvd.ToString());
            GUI.Label(_dronRect, dron.ToString());
            GUI.Label(_liveRect, live.ToString());
            GUI.Label(_helthResetRect, helthReset.ToString());

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
            GUI.Label(_gunBulletRect, gunBullet.ToString());
            GUI.Label(_grenadeBulletRect, grenadeBullet.ToString());
            GUI.Label(_minigunBulletRect, minigunBullet.ToString());
            GUI.Label(_rocketBulletRect, rocketBullet.ToString());
            GUI.Label(_diskgunBulletRect, diskgunBullet.ToString());

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
