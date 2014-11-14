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

    private int _controllers;
    private Rect textureRectLeft, textureRectRight;
    private Touch[] touches;
    private Vector2 endPosLeft, homePosLeft, endPosRight, homePosRight;

    public Texture jost;
    public float rotationCC, forwardCC, rightCC;
    public int shot;

    void Start()
    {
        //Выбор режима управления
        _controllers = PlayerPrefs.GetInt("Controllers");

        switch (_controllers)
        {
            case 1:
                controllMode = ControllersMode.moveAndRotation;
                break;
            case 2:
                controllMode = ControllersMode.move;
                break;
            case 3:
                controllMode = ControllersMode.moveAndShot;
                break;
        }
        //Конец выбор режима управления

        homePosLeft = new Vector2(50, Screen.height - 100);
        homePosRight = new Vector2(Screen.width - 100, Screen.height - 100);
        textureRectLeft = new Rect(homePosLeft.x, homePosLeft.y, 50, 50);
        textureRectRight = new Rect(homePosRight.x, homePosRight.y, 50, 50);

        endPosLeft.x = homePosRight.x;
        endPosLeft.y = homePosRight.y;
    }

    void OnGUI()
    {
            GUI.DrawTexture(textureRectLeft, jost);
            if (controllMode == ControllersMode.moveAndRotation || controllMode == ControllersMode.moveAndShot)
                GUI.DrawTexture(textureRectRight, jost);
    }

    //Просчет угла
    static float Angle(Vector2 Start, Vector2 aEnd, Vector2 bEnd)
    {
        Vector2 a = new Vector2 (aEnd.x - Start.x, aEnd.y - Start.y);
        Vector2 b = new Vector2 (bEnd.x - Start.x, bEnd.y - Start.y);

        if (bEnd.x > Start.x)
            return -Vector2.Angle(a, b);
        else
            return Vector2.Angle(a, b);
    }

    void RotationCC(bool forward)
    {
        rotationCC = Angle(new Vector2(75, 75), new Vector2(75, 80), new Vector2(Screen.width - endPosRight.x, endPosRight.y));

        touches = Input.touches;
        foreach (Touch t in touches)
        {
            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x > Screen.width - 151 && t.position.y < 151)
                        shot = 1;
                    break;
                case TouchPhase.Moved:
                    if (t.position.x > Screen.width - 151 && t.position.y < 151)
                    {
                        textureRectRight = new Rect(endPosRight.x - 25, Screen.height - endPosRight.y - 25, 50, 50);
                        endPosRight = t.position;
                        if (forward)
                            forwardCC = 1;
                    }
                    break;
                case TouchPhase.Ended:
                    shot = 0;
                    textureRectRight = new Rect(homePosRight.x, homePosRight.y, 50, 50);
                    endPosRight.x = Screen.width - 50;
                    endPosRight.y = 75;
                    if (forward)
                        forwardCC = 0;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        #region Left
        //Левый джойстик

        touches = Input.touches;
        foreach (Touch t in touches)
        {
            switch (t.phase)
            {
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

        if (controllMode == ControllersMode.moveAndRotation || controllMode == ControllersMode.move)
        {
            //Центр
            if (endPosLeft.x > 49 && endPosLeft.x < 101 && endPosLeft.y > 49 && endPosLeft.y < 101)
            {
                forwardCC = 0;
                rightCC = 0;
            }
            //Верх
            if (endPosLeft.x > 49 && endPosLeft.x < 101 && endPosLeft.y > 99 && endPosLeft.y < 151)
                forwardCC = 1;
            //Верх лево
            if (endPosLeft.x < 51 && endPosLeft.y < 151 && endPosLeft.y > 99){
                forwardCC = 0.5f;
                rightCC = -0.5f;
            }
            //Лево
            if (endPosLeft.x < 50 && endPosLeft.y > 49 && endPosLeft.y < 101)
                rightCC = -1;
            //Верх право
            if (endPosLeft.x > 99 && endPosLeft.x < 151 && endPosLeft.y < 151 && endPosLeft.y > 99)
            {
                forwardCC = 0.5f;
                rightCC = 0.5f;
            }
            //Право
            if (endPosLeft.x > 99 && endPosLeft.x < 151 && endPosLeft.y > 49 && endPosLeft.y < 101)
                rightCC = 1;
            //Низ лево
            if (endPosLeft.x < 51 && endPosLeft.y < 51)
            {
                forwardCC = -0.5f;
                rightCC = -0.5f;
            }
            //Низ право
            if (endPosLeft.x > 99 && endPosLeft.x < 151 && endPosLeft.y < 51)
            {
                forwardCC = -0.5f;
                rightCC = 0.5f;
            }
            //Низ
            if (endPosLeft.x > 49 && endPosLeft.x < 101 && endPosLeft.y < 51)
                forwardCC = -1;
        }

        //Поворот и вперед
        if (controllMode == ControllersMode.moveAndShot)
        {
            //rotationCC = Angle(new Vector2(75, 75), new Vector2(75, 80), new Vector2(endPosLeft.x, endPosLeft.y));          

            touches = Input.touches;
            foreach (Touch t in touches)
            {
                switch (t.phase)
                {
                    case TouchPhase.Moved:
                        if (t.position.x < 151 && t.position.y < 151)
                        {
                            endPosLeft = t.position;
                            textureRectLeft = new Rect(endPosLeft.x - 25, Screen.height - endPosLeft.y - 25, 50, 50);

                            //Движение в зависимости от поворота
                            if (rotationCC > -45 && rotationCC < 45)
                            {
                                forwardCC = 1;
                                rightCC = 0;
                            }
                            if (rotationCC > 45 && rotationCC < 135)
                            {
                                rightCC = -1;
                                forwardCC = 0;
                            }
                            if (rotationCC > 135 && rotationCC < 180 || rotationCC < -135 && rotationCC > -179)
                            {
                                forwardCC = -1;
                                rightCC = 0;
                            }
                            if (rotationCC > -135 && rotationCC < -45)
                            {
                                rightCC = 1;
                                forwardCC = 0;
                            }
                        }
                        break;
                    case TouchPhase.Ended:
                        rightCC = 0;
                        forwardCC = 0;
                        textureRectLeft = new Rect(homePosLeft.x, homePosLeft.y, 50, 50);
                        endPosLeft.x = 50;
                        endPosLeft.y = 75;
                        break;
                }
            }
        }
            //КОНЕЦ левый джойстик
        #endregion

        #region Right
            //Правый джостик
            if (controllMode == ControllersMode.moveAndRotation)
                //RotationCC(false);

            //Выстрел
            if (controllMode == ControllersMode.moveAndShot ){
                //Стрельба
                touches = Input.touches;
                foreach (Touch t in touches)
                {
                    switch (t.phase)
                    {
                        case TouchPhase.Began:
                            if (t.position.x > Screen.width - 151 && t.position.y < 151)
                                shot = 1;
                            break;
                        case TouchPhase.Ended:
                            shot = 0;
                            break;
                    }
                }
            }

            if (controllMode == ControllersMode.move)
            {
                //Стрельба
                touches = Input.touches;
                foreach (Touch t in touches)
                {
                    switch (t.phase)
                    {
                        case TouchPhase.Began:
                            if (!(t.position.x < 149 && t.position.y < 149))
                                shot = 1;
                            break;
                        case TouchPhase.Ended:
                            shot = 0;
                            break;
                    }
                }
            }
        //КОНЕЦ правый джойстик 
            #endregion
    }
}