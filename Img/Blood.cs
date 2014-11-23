using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {

    public string typeZombie;
    public float timeToDestroyParticle;
    public Material ratBlood, zombieBlood, dogBlood, banditBlood, foresterBlood;
    public GameObject ratParticle, zombieParticle;

	void Start () {
        #region material 
            switch (typeZombie){
                case "Rat":
                    transform.renderer.material = ratBlood;
                    ratParticle.SetActive(true);
                    break;
                case "Zombie":
                    transform.renderer.material = zombieBlood;
                    zombieParticle.SetActive(true);
                    break;
                case "Dog":
                    transform.renderer.material = dogBlood;
                    ratParticle.SetActive(true);
                    break;
                case "Bandit":
                    transform.renderer.material = banditBlood;
                    zombieParticle.SetActive(true);
                    break;
                case "Forester":
                    transform.renderer.material = foresterBlood;
                    zombieParticle.SetActive(true);
                    break;
            }
        #endregion
    }

    void FixedUpdate()
    {
        //Отключение эффекта крови
        timeToDestroyParticle -= Time.deltaTime;

        if (timeToDestroyParticle < 0)
        {
            switch (typeZombie)
            {
                case "Rat":
                    ratParticle.SetActive(false);
                    break;
                case "Dog":
                    ratParticle.SetActive(false);
                    break;
                default:
                    zombieParticle.SetActive(false);
                    break;
            }
        }
    }
}
