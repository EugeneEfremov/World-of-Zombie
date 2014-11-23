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

    public Texture jostRed, jostBlue;
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

        homePosLeft = new Vector2(100, Screen.height - 200);
        homePosRight = new Vector2(Screen.width - 200, Screen.height - 200);

        textureRectLeft = new Rect(homePosLeft.x, homePosLeft.y, 100, 100);
        textureRectRight = new Rect(homePosRight.x, homePosRight.y, 100, 100);

        endPosLeft.x = homePosRight.x;
        endPosLeft.y = homePosRight.y;
    }

    void OnGUI()
    {
        if (controllMode == ControllersMode.moveAndRotation || controllMode == ControllersMode.move)
            GUI.DrawTexture(textureRectLeft, jostBlue);

        if (controllMode == ControllersMode.moveAndShot)
            GUI.DrawTexture(textureRectLeft, jostRed);

        if (controllMode == ControllersMode.moveAndRotation)
            GUI.DrawTexture(textureRectRight, jostRed);

        if (controllMode == ControllersMode.moveAndShot)
            GUI.DrawTexture(textureRectRight, jostBlue);
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
        if (endPosRight != Vector2.zero)
            rotationCC = AngleRight(new Vector2(150, 150), new Vector2(150, 300), new Vector2(Screen.width - endPosRight.x, endPosRight.y));

        touches = Input.touches;
        foreach (Touch t in touches)
        {
            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x > Screen.width - 301 && t.position.y < 301)
                        if (!forward)
                            shot = 1;
                    break;
                case TouchPhase.Moved:
                    if (t.position.x > Screen.width - 301 && t.position.y < 301)
                    {
                        textureRectRight = new Rect(endPosRight.x - 50, Screen.height - endPosRight.y - 50, 100, 100);
                        endPosRight = t.position;
                        if (forward && (endPosRight.x != Screen.width - 150 && endPosRight.y != 150))
                        {
                            //поворот актера
                            transform.eulerAngles = new Vector3(0, AngleRight(new Vector2(150, 150), new Vector2(150, 300), new Vector2(Screen.width - endPosRight.x, endPosRight.y)), 0);

                            //вперед
                            forwardCC = DistanceVector2(new Vector2(150, 150), new Vector2(Screen.width - endPosRight.x, endPosRight.y)) / 100;
                        }
                    }
                    break;
                case TouchPhase.Ended:
                    if (!(t.position.x < 301 && t.position.y < 301))
                        shot = 0;

                    textureRectRight = new Rect(homePosRight.x, homePosRight.y, 100, 100);
                    endPosRight.x = Screen.width - 150;
                    endPosRight.y = 150;

                    if (forward)
                        forwardCC = 0;
                    break;

                case TouchPhase.Canceled:
                    if (t.position.x < 301 && t.position.y < 301)
                        shot = 0;
                break;
            }
        }
    }

    void FixedUpdate()
    {
        if (!deathActor)
        {
            Player.forwardCC = forwardCC * 0.7f;
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
                            if (t.position.x < 301 && t.position.y < 301)
                            {
                                textureRectLeft = new Rect(endPosLeft.x - 50, Screen.height - endPosLeft.y - 50, 100, 100);
                                endPosLeft = t.position;
                                
                                //поворот актера
                                transform.eulerAngles = new Vector3(0, Angle(new Vector2(150, 150), new Vector2(150, 300), new Vector2(endPosLeft.x, endPosLeft.y)), 0);

                                //вперед
                                forwardCC = DistanceVector2(new Vector2(150, 150), new Vector2(endPosLeft.x, endPosLeft.y)) / 100;
                            }
                            break;
                        case TouchPhase.Ended:
                            textureRectLeft = new Rect(homePosLeft.x, homePosLeft.y, 100, 100);
                            endPosLeft.x = 150;
                            endPosLeft.y = 150;
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
                            if (t.position.x < 301 && t.position.y < 301)
                                shot = 1;
                            break;
                        case TouchPhase.Ended:
                            if (t.position.x < 301 && t.position.y < 301)
                                shot = 0;
                            break;
                        case TouchPhase.Canceled:
                            if (t.position.x < 301 && t.position.y < 301)
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
                        if (!(t.position.x < 301 && t.position.y < 301))
                        {
                            if (!invertingMove)
                                topBody.transform.eulerAngles = new Vector3(0, Angle(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(Screen.width / 2, Screen.height), new Vector2(t.position.x, t.position.y)) - 30, 0);

                            if (invertingMove)
                                topBody.transform.eulerAngles = new Vector3(0, Angle(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(Screen.width / 2, Screen.height), new Vector2(t.position.x, t.position.y)) - 210, 0);

                            shot = 1;
                        }
                    }

                    if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                    {
                        if (!(t.position.x < 301 && t.position.y < 301))
                            shot = 0;
                    }
                }
            }
            //КОНЕЦ правый джойстик 
            #endregion
            
        }
    }
}