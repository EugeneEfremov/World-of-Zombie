using UnityEngine;
using System.Collections;

public class Magic : MonoBehaviour {

    private Transform magic2ParticleNew, magic3Particle, magic4ParticleNew;
    private int _magic4Helth, _magic4Armour;

    public Transform Actor, magic2Particle, magic3Zone, magic4Particle;
    public bool magic1 = false, magic2 = false, magic2kill = false;
    public bool bMagic1, bMagic2, bMagic3, bMagic2Play, bMagic3Play, bMagic4Play, bMagic4;

    //Используется ли магия
    public int magic1b, magic2b, magic3b, magic4b;

    //Время использования магии
    public float timeZombie = 1, timeMagic1, timeMagic2, timeMagic3, timeMagic4;

	void Start () {
        magic1b = PlayerPrefs.GetInt("fx106f1");
        magic2b = PlayerPrefs.GetInt("fx106f2");
        magic3b = PlayerPrefs.GetInt("fx106f3");
        magic4b = PlayerPrefs.GetInt("fx106f4");

        Actor = GameObject.Find("Actor").transform;
	}
	
	
	void FixedUpdate ()
    {
                #region Magic1
        if (bMagic1)
                {
                    magic1 = true;
                    timeMagic1 -= Time.deltaTime * 3;
                    if (timeMagic1 <= 0)
                        bMagic1 = false;
                }
                if (!bMagic1)
                {
                    magic1 = false;
                    if (timeMagic1 < 30)
                        timeMagic1 += Time.deltaTime;
                }
                #endregion

                #region Magic2
                if (bMagic2)
                {
                    if (!bMagic2Play)
                    {
                        magic2ParticleNew = Instantiate(magic2Particle, Actor.transform.position, Quaternion.Euler(90, 0, 0)) as Transform;
                        magic2ParticleNew.parent = Actor; //Присвоение к актеру
                        Destroy(GameObject.Find("magic2(Clone)"), 4.5f);
                        bMagic2Play = true;
                    }
                    magic2 = true;
                    timeMagic2 -= Time.deltaTime * 20;
                    if (timeMagic2 <= 0)
                    {
                        bMagic2Play = false;
                        magic2kill = true;
                        bMagic2 = false;
                    }
                }
                if (!bMagic2)
                {
                    magic2 = false;
                    if (timeMagic2 < 40)
                        timeMagic2 += Time.deltaTime;
                    if (timeMagic2 > 1)
                        magic2kill = false;
                }
                #endregion

                #region Magic3
                if (bMagic3)
                {
                    timeMagic3 -= Time.deltaTime * 30;
                    if (!bMagic3Play)
                    {
                        magic3Particle = Instantiate(magic3Zone, Actor.transform.position, Quaternion.Euler(0, 0, 0)) as Transform;
                        magic3Particle.parent = Actor; //Присвоение к актеру
                        bMagic3Play = true;
                    }
                    if (timeMagic3 <= 0)
                    {
                        bMagic3 = false;
                        bMagic3Play = false;
                    }
                }
                if (!bMagic3)
                {
                    if (timeMagic3 < 50)
                        timeMagic3 += Time.deltaTime;
                }
                #endregion

                #region Magic4
                if (bMagic4)
                {
                    if (!bMagic4Play)
                    {
                        magic4ParticleNew = Instantiate(magic4Particle, Actor.transform.position, Quaternion.Euler(-90, 0, 0)) as Transform;
                        magic4ParticleNew.parent = Actor.transform; //Присвоение к актеру
                        bMagic4Play = true;
                        _magic4Helth = Actor.transform.GetComponent<Actor>().helth;
                        _magic4Armour = Actor.transform.GetComponent<Actor>().armour;
                    }
                    Actor.transform.GetComponent<Actor>().helth = _magic4Helth;
                    Actor.transform.GetComponent<Actor>().armour = _magic4Armour;
                    timeMagic4 -= Time.deltaTime * 6;
                    if (timeMagic4 <= 0)
                    {
                        bMagic4Play = false;
                        Destroy(GameObject.Find("magic4(Clone)"));
                        bMagic4 = false;
                    }
                }
                if (!bMagic4)
                {
                    if (timeMagic4 < 70)
                        timeMagic4 += Time.deltaTime;
                }
                #endregion
    }
}
