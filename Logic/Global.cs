using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

    public int money, loadLevel;
    public int armour, armourMax, lantern, nvd, dron, live, helthReset; //Equipment
    public int helthMax, strongMax, speedMax, accuracyMax, countMax; //Specifications of Actor
    public int pistolsLvl, gunLvl, grenadeLvl, minigunLvl, rocketLvl, diskgunLvl, firegunLvl, zeusgunLvl, plasmicgunLvl, gaussgunLvl; //Level of Weapons
    public int gunBullet, grenadeBullet, minigunBullet, rocketBullet, diskgunBullet; //Bullet of Weapons
    public GameObject Player;

    void Start(){
        Player = GameObject.Find("Actor");

        money = PlayerPrefs.GetInt("0x01001");
        loadLevel = PlayerPrefs.GetInt("0xffaa01");
        //Исключение ошибки нулевого уровня
        if (loadLevel < 1) loadLevel = 1;

        armour = PlayerPrefs.GetInt("0x01002");
        armourMax = PlayerPrefs.GetInt("0x02001");
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
    }

    public void SaveResultGame(string gameMode)
    {
        PlayerPrefs.SetInt("0x01001", money + Player.GetComponent<Actor>().count / 70);

        PlayerPrefs.SetInt("0x01002", Player.GetComponent<Actor>().armour);
        PlayerPrefs.SetInt("0x02001", Player.GetComponent<Actor>().armourMax);
        PlayerPrefs.SetInt("0x01003", lantern);
        PlayerPrefs.SetInt("0x01004", nvd);
        PlayerPrefs.SetInt("0x01005", dron);
        PlayerPrefs.SetInt("0x01006", live);
        PlayerPrefs.SetInt("0x01007", helthReset);

        PlayerPrefs.SetInt("fx10ab0", pistolsLvl);
        PlayerPrefs.SetInt("fx10ab1", gunLvl);
        PlayerPrefs.SetInt("fx10ab2", grenadeLvl);
        PlayerPrefs.SetInt("fx10ab3", minigunLvl);
        PlayerPrefs.SetInt("fx10ab4", rocketLvl);
        PlayerPrefs.SetInt("fx10ab5", diskgunLvl);
        PlayerPrefs.SetInt("fx01e01", firegunLvl);
        PlayerPrefs.SetInt("fx01e03", zeusgunLvl);
        PlayerPrefs.SetInt("fx01e05", plasmicgunLvl);
        PlayerPrefs.SetInt("fx01e07", gaussgunLvl);

        if (gameMode != "arena")
        {

            PlayerPrefs.SetInt("ax90ab1", Player.GetComponent<Weapons>().gunBullet);
            PlayerPrefs.SetInt("ax90ab2", Player.GetComponent<Weapons>().grenadeBullet);
            PlayerPrefs.SetInt("ax90ab3", Player.GetComponent<Weapons>().minigunBullet);
            PlayerPrefs.SetInt("ax90ab4", Player.GetComponent<Weapons>().rocketBullet);
            PlayerPrefs.SetInt("ax90ab5", Player.GetComponent<Weapons>().diskgunBullet);
        }

        if (gameMode == "company")
        {
            PlayerPrefs.SetInt("0xffaa01", loadLevel);

            PlayerPrefs.SetInt("0x01f01", helthMax / 40);
            PlayerPrefs.SetInt("0x01f02", strongMax * 2);
            PlayerPrefs.SetInt("0x01f03", speedMax * 10);
            PlayerPrefs.SetInt("0x01f04", accuracyMax);
        }
    }
}
