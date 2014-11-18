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
    private bool invertingMove;
    private Rect textureRectLeft, textureRectRight;
    private Touch[] touches;
    private Vector2 endPosLeft, homePosLeft, endPosRight, homePosRight;
    private Actor Player;
    private Transform topBody;

    public Texture jost;
    public bool deathActor = false;
    public float rotationCC, forwardCC;
    public int shot;

    void Start()
    {
        Player = GetComponent<Actor>();
        topBody = GameObject.Find("topBody").transform;

        if (PlayerPrefs.GetInt("invertingMove") == 1)
            invertingMove = true;
        if (PlayerPrefs.GetInt("invertingMove") == 0)
            invertingMove = false;

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

    //Длина вектора
    static float DistanceVector2(Vector2 Start, Vector2 End)
    {
        return Mathf.Sqrt(Mathf.Pow((Start.x - End.x), 2) + Mathf.Pow((Start.y - End.y), 2));
    }

    //Просчет угла
    static float Angle(Vector2 Start, Vector2 aEnd, Vector2 bEnd)
    {
        Vector2 a = new Vector2 (aEnd.x - Start.x, aEnd.y - Start.y);
        Vector2 b = new Vector2 (bEnd.x - Start.x, bEnd.y - Start.y);

        if (bEnd.x < Start.x)
            return -Vector2.Angle(a, b);
        else
            return Vector2.Angle(a, b);
    }

    //Просчет угла для правого джойстика
    static float AngleRight(Vector2 Start, Vector2 aEnd, Vector2 bEnd)
    {
        Vector2 a = new Vector2 (aEnd.x - Start.x, aEnd.y - Start.y);
        Vector2 b = new Vector2 (bEnd.x - Start.x, bEnd.y - Start.y);

        if (bEnd.x < Start.x)
            return Vector2.Angle(a, b);
        else
            return -Vector2.Angle(a, b);
    }

    void RotationCC(bool forward)
    {
        rotationCC = AngleRight(new Vector2(75, 75), new Vector2(75, 80), new Vector2(Screen.width - endPosRight.x, endPosRight.y));

        touches = Input.touches;
        foreach (Touch t in touches)
        {
            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x > Screen.width - 151 && t.position.y < 151)
                        if (!forward)
                        shot = 1;
                    break;
                case TouchPhase.Moved:
                    if (t.position.x > Screen.width - 151 && t.position.y < 151)
                    {
                        textureRectRight = new Rect(endPosRight.x - 25, Screen.height - endPosRight.y - 25, 50, 50);
                        endPosRight = t.position;
                        if (forward)
                        {
                            //поворот актера
                            transform.eulerAngles = new Vector3(0, AngleRight(new Vector2(75, 75), new Vector2(75, 150), new Vector2(Screen.width - endPosRight.x, endPosRight.y)), 0);

                            //вперед
                            forwardCC = DistanceVector2(new Vector2(75, 75), new Vector2(Screen.width - endPosRight.x, endPosRight.y)) / 100;
                        }
                    }
                    break;
                case TouchPhase.Ended:
                    shot = 0;

                    textureRectRight = new Rect(homePosRight.x, homePosRight.y, 50, 50);
                    endPosRight.x = Screen.width - 50;
                    endPosRight.y = 75;

                    if (forward)
                    {
                        forwardCC = 0;
                    }
                    break;
                case TouchPhase.Canceled:
                    shot = 0;
                break;
            }
        }
    }

    void FixedUpdate()
    {
        if (!deathActor)
        {
            Player.forwardCC = forwardCC;
            topBody.transform.localEulerAngles = new Vector3(0,  -transform.localEulerAngles.y + rotationCC, 0);
            
            #region Left
            //Левый джойстик

            if (controllMode == ControllersMode.moveAndRotation || controllMode == ControllersMode.move)
            {
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
                                
                                //поворот актера
                                transform.eulerAngles = new Vector3(0, Angle(new Vector2(75, 75), new Vector2(75, 150), new Vector2(endPosLeft.x, endPosLeft.y)), 0);

                                //вперед
                                forwardCC = DistanceVector2(new Vector2(75, 75), new Vector2(endPosLeft.x, endPosLeft.y)) / 100;
                            }
                            break;
                        case TouchPhase.Ended:
                            textureRectLeft = new Rect(homePosLeft.x, homePosLeft.y, 50, 50);
                            endPosLeft.x = 50;
                            endPosLeft.y = 75;
                            forwardCC = 0;
                            break;
                    }
                }
            }

            //Стрельба
            if (controllMode == ControllersMode.moveAndShot)
            {
                touches = Input.touches;
                foreach (Touch t in touches)
                {
                    switch (t.phase)
                    {
                        case TouchPhase.Began:
                            if (t.position.x < 149 && t.position.y < 149)
                                shot = 1;
                            break;
                        case TouchPhase.Ended:
                            shot = 0;
                            break;
                        case TouchPhase.Canceled:
                            shot = 0;
                            break;
                    }
                }
            }
            //КОНЕЦ левый джойстик
            #endregion

            #region Right
            //Правый джостик

            if (controllMode == ControllersMode.moveAndRotation)
                RotationCC(false);

            if (controllMode == ControllersMode.moveAndShot)
                RotationCC(true);

            if (controllMode == ControllersMode.move)
            {
                //Стрельба
                touches = Input.touches;
                foreach (Touch t in touches)
                {
                    if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
                    {
                        if (!(t.position.x < 149 && t.position.y < 149))
                        {
                            if (!invertingMove)
                                topBody.transform.eulerAngles = new Vector3(0, Angle(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(Screen.width / 2, Screen.height), new Vector2(t.position.x, t.position.y)) - 30, 0);

                            if (invertingMove)
                                topBody.transform.eulerAngles = new Vector3(0, Angle(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(Screen.width / 2, Screen.height), new Vector2(t.position.x, t.position.y)) - 210, 0);

                            shot = 1;
                        }
                    }

                    if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                        shot = 0;
                }
            }
            //КОНЕЦ правый джойстик 
            #endregion
            
        }
    }
}