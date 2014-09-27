using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

    public int money; //Money
    public int armour, lantern, nvd, dron, live, helthReset; //Equipment
    public int helthMax, strongMax, speedMax, accuracyMax; //Specifications of Actor

    void Start(){
        money = PlayerPrefs.GetInt("0x01001");

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
    }
}
