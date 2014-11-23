using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

	public AudioClip boomA;
    public GameObject Particle;
    public bool damageOfZombie = true;
    public float timeDestroyParticle = 2.9f, timeDeactivateObject = 0.7f, timeDestrooyObject = 3.1f;
    public Quaternion rotationParticle = Quaternion.Euler(270, 0, 0);

    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(boomA);
        Particle = Instantiate(Particle, transform.position, rotationParticle) as GameObject;
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(0.1f, 0, 0.1f);

        timeDestroyParticle -= Time.deltaTime;
        timeDeactivateObject -= Time.deltaTime;
        timeDestrooyObject -= Time.deltaTime;

        if (timeDeactivateObject <= 0)
            damageOfZombie = false;

        if (timeDestroyParticle <= 0)
            Destroy(Particle);

        if (timeDestrooyObject <= 0)
            Destroy(gameObject);
    }

	void OnTriggerStay (Collider other){
        if (other.transform.tag == "Zombie" && damageOfZombie)
        {
            GameObject.Find(other.transform.name).GetComponent<ZombieMove>().helth -= 70;
            GameObject.Find("Actor").GetComponent<Actor>().count += 50;
        }
        if (other.transform.tag == "Barel")
        {
            other.transform.GetComponent<Barel>().helth -= 70;
            GameObject.Find("Actor").GetComponent<Actor>().count += 30;
        }
	}
}
