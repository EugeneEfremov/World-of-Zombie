using UnityEngine;
using System.Collections;

public class Controllers : MonoBehaviour
{
    public enum ControllersMode
    {
        moveAndRotation,
        move,
        moveAndShot
    };

    public ControllersMode controllMode = ControllersMode.moveAndRotation;

    Transform Actor;
    Rect textureRectLeft, textureRectRight;
    Touch[] touches;
    Vector2 startPosLeft, endPosLeft, homePosLeft, startPosRight, endPosRight, homePosRight;


    public Texture jost;
    public int forwardCC, rightCC;
    public float rotationCC;

    void Start()
    {
        Actor = GameObject.Find("Actor").transform;

        homePosLeft = new Vector2(50, Screen.height - 100);
        homePosRight = new Vector2(Screen.width - 100, Screen.height - 100);
        textureRectLeft = new Rect(homePosLeft.x, homePosLeft.y, 50, 50);
        textureRectRight = new Rect(homePosRight.x, homePosRight.y, 50, 50);
    }

    void OnGUI()
    {
        if (controllMode == ControllersMode.moveAndRotation)
        {
            GUI.DrawTexture(textureRectLeft, jost);
            GUI.DrawTexture(textureRectRight, jost);
        }
    }

    //Просчет угла
    static float Angle(Vector2 start, Vector2 end)
    {
        var x = end.x - start.x;
        var y = end.y - start.y;
        var summ = x * (90 / (Mathf.Abs(x) + Mathf.Abs(y)));
        return x >= 0 ? y >= 0 ? summ : 180 - summ : y >= 0 ? 360 + summ : 180 + summ;
    }

    void FixedUpdate()
    {
        if (controllMode == ControllersMode.moveAndRotation)
        {
            //Левый джойстик
            if (endPosLeft.x > 49 && endPosLeft.x < 101 && endPosLeft.y > 49 && endPosLeft.y < 101)
            {
                forwardCC = 0;
                rightCC = 0;
            }
            if (endPosLeft.x < 50 && endPosLeft.y > 49 && endPosLeft.y < 101)
                rightCC = -1;
            if (endPosLeft.x > 99 && endPosLeft.x < 151 && endPosLeft.y > 49 && endPosLeft.y < 101)
                rightCC = 1;
            if (endPosLeft.x > 49 && endPosLeft.x < 101 && endPosLeft.y < 51)
                forwardCC = -1;
            if (endPosLeft.x > 49 && endPosLeft.x < 101 && endPosLeft.y > 99 && endPosLeft.y < 151)
                forwardCC = 1;

            touches = Input.touches;
            foreach (Touch t in touches)
            {
                switch (t.phase)
                {
                    case TouchPhase.Began:
                        startPosLeft = t.position;
                        break;
                    case TouchPhase.Moved:
                        if (t.position.x < 151 && t.position.y < 151)
                        {
                            textureRectLeft = new Rect(endPosLeft.x - 25, Screen.height - endPosLeft.y - 25, 50, 50);
                            endPosLeft = t.position;
                        }
                        break;
                    case TouchPhase.Ended:
                        textureRectLeft = new Rect(homePosLeft.x, homePosLeft.y, 50, 50);
                        endPosLeft.x = 50;
                        endPosLeft.y = 75;
                        break;
                }
            }
            //КОНЕЦ левый джойстик

            //Правый джостик
            rotationCC = Angle(new Vector2(75, 75), new Vector2(Screen.width - endPosRight.x, endPosRight.y));

            touches = Input.touches;
            foreach (Touch t in touches)
            {
                switch (t.phase)
                {
                    case TouchPhase.Began:
                        startPosRight = t.position;
                        break;
                    case TouchPhase.Moved:
                        if (t.position.x > Screen.width - 151 && t.position.y < 151)
                        {
                            textureRectRight = new Rect(endPosRight.x - 25, Screen.height - endPosRight.y - 25, 50, 50);
                            endPosRight = t.position;
                        }
                        break;
                    case TouchPhase.Ended:
                        textureRectRight = new Rect(homePosRight.x, homePosRight.y, 50, 50);
                        endPosRight.x = Screen.width - 50;
                        endPosRight.y = 75;
                        break;
                }
            }
            //КОНЕЦ правый джойстик 
        }
    }
}