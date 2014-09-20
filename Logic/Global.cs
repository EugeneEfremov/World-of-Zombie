using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

    //Money
    public int money;
    //Equipment
    public int armour, lantern, nvd, dron, live, helthReset;
    //Ability
    public int helthMax, strongMax, speedMax, accuracyMax;

    void Start(){
        money = PlayerPrefs.GetInt("Money");

        armour = PlayerPrefs.GetInt("Armour");
        lantern = PlayerPrefs.GetInt("Lantern");
        nvd = PlayerPrefs.GetInt("nvd");
        dron = PlayerPrefs.GetInt("Dron");
        live = PlayerPrefs.GetInt("Live");
        helthReset = PlayerPrefs.GetInt("HelthReset");

        helthMax = PlayerPrefs.GetInt("HelthMax") * 40;
        strongMax = PlayerPrefs.GetInt("StrongMax") / 2;
        speedMax = PlayerPrefs.GetInt("SpeedMax") / 10 ;
        //Исключение ошибки деления на нуль
        if (PlayerPrefs.GetInt("AccuracyMax") == 0) PlayerPrefs.SetInt("AccuracyMax", 1);
        accuracyMax = 1 / PlayerPrefs.GetInt("AccuracyMax");
    }

    void Update()
    {
        
    }
}
