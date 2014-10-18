using UnityEngine;
using System.Collections;

public class SwitchLevel : MonoBehaviour {

    public int ScreenWidth, ScreenHeight, LevScore1, LevScore2, LevScore3, LevScore4, LevScore5, LevScore6, LevScore7, LevScore8, LevScore9, LevScore10;
    public Rect _lev1, _levScore1, _lev2, _levScore2, _lev3, _levScore3, _lev4, _levScore4, _lev5, _levScore5, _lev6, _levScore6, _lev7, _levScore7, _lev8, _levScore8, _lev9, _levScore9, _lev10, _levScore10;

	void Start () {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;

        _lev1 = new Rect (ScreenWidth * 0.05f, ScreenHeight * 0.05f, ScreenWidth * 0.14f, ScreenHeight * 0.29f);
        _lev2 = new Rect(ScreenWidth * 0.24f, ScreenHeight * 0.05f, ScreenWidth * 0.14f, ScreenHeight * 0.29f);
        _lev3 = new Rect(ScreenWidth * 0.43f, ScreenHeight * 0.05f, ScreenWidth * 0.14f, ScreenHeight * 0.29f);
        _lev4 = new Rect(ScreenWidth * 0.62f, ScreenHeight * 0.05f, ScreenWidth * 0.14f, ScreenHeight * 0.29f);
        _lev5 = new Rect(ScreenWidth * 0.81f, ScreenHeight * 0.05f, ScreenWidth * 0.14f, ScreenHeight * 0.29f);
        _lev6 = new Rect(ScreenWidth * 0.05f, ScreenHeight * 0.40f, ScreenWidth * 0.14f, ScreenHeight * 0.29f);
        _lev7 = new Rect(ScreenWidth * 0.24f, ScreenHeight * 0.40f, ScreenWidth * 0.14f, ScreenHeight * 0.29f);
        _lev8 = new Rect(ScreenWidth * 0.43f, ScreenHeight * 0.40f, ScreenWidth * 0.14f, ScreenHeight * 0.29f);
        _lev9 = new Rect(ScreenWidth * 0.62f, ScreenHeight * 0.40f, ScreenWidth * 0.14f, ScreenHeight * 0.29f);
        _lev10 = new Rect(ScreenWidth * 0.81f, ScreenHeight * 0.40f, ScreenWidth * 0.14f, ScreenHeight * 0.29f);
	}
	
	void Update () {
	    
	}

    void OnGUI()
    {
        GUI.Button(_lev1, "lvl 1");
        GUI.Button(_lev2, "lvl 2");
        GUI.Button(_lev3, "lvl 3");
        GUI.Button(_lev4, "lvl 4");
        GUI.Button(_lev5, "lvl 5");
        GUI.Button(_lev6, "lvl 6");
        GUI.Button(_lev7, "lvl 7");
        GUI.Button(_lev8, "lvl 8");
        GUI.Button(_lev9, "lvl 9");
        GUI.Button(_lev10, "lvl 10");
    }
}
