using UnityEngine;
using System.Collections;

public class ZombieMove : MonoBehaviour {

	private Transform Player, newbloodObj;
	private CharacterController cc;
    private GameObject ZombieAll;
    public float timeInGame, instNewWeapTime, timeDamage = 1.2f, bloodY = 1f; //время в игре, время до создания нового оружия, переодичность нанесения урона, смещение текстуры
    private int blood; //Создавать ли кровь?

	Vector3 moveDirection = Vector3.zero;

    public Animation anim;
    public AnimationClip steps, attack;
	public string typeZomb, gameMode;
    public bool shoting = false, magic1, magic1enabled, magic2, magic2kill; //Применена ли магия
    public Transform ice, blod, helth20, armour100, armour200, armour300, gunBul50, grenadeBul50, minigunBul500, rocketBul50, diskgunBul50, firegunBul50, zeusgunBul50, plasmicgunBul50, gaussgunBul100, gun, grenage, minigun, rocket, diskgun, firegun, zeusgun, plasmicgun, gaussgun, accuracyMaxBonus, strongMaxBonus, speedMaxBonus, helthMaxBonus;
    public Transform BanditBullet, ForesterBullet, BulletSpawn;
    public int helth = 100, done = 2; //жизнь, урон
    public float speed = 3, speedMax = 3;
	public AudioClip[] ratA;

	void Start () {
        magic1enabled = false;
		Player = GameObject.FindWithTag ("Player").transform;
		cc = GetComponent<CharacterController> ();
        ZombieAll = GameObject.Find("ZombieLogic");
        gameMode = Player.GetComponent<Info>().gameMode.ToString();

        anim.AddClip(steps, "Steps");

		if (typeZomb == "Zombie") {
            speed = 1.5f;
            speedMax = 1.5f;
			helth = 70;
			done = 6;
            bloodY = 0.6f;
            shoting = false;
            anim["Steps"].speed = 1.5f;
		}
		if (typeZomb == "Rat") {
			speed = 4;
            speedMax = 4;
			helth = 30;
			done = 2;
            bloodY = 0.1f;
            shoting = false;
            anim["Steps"].speed = 3f;
		}
		if (typeZomb == "Dog") {
			speed = 3.5f;
            speedMax = 3.5f;
			helth = 70;
			done = 8;
            bloodY = 0.1f;
            shoting = false;
            anim["Steps"].speed = 1.5f;
            anim.AddClip(attack, "Attack");
		}
		if (typeZomb == "Bandit") {
			speed = 2.5f;
            speedMax = 2.5f;
			helth = 150;
			done = 9;
            bloodY = 0.9f;
            shoting = true;
            anim["Steps"].speed = 1.5f;
		}
		if (typeZomb == "Forester") {
			speed = 2.5f;
            speedMax = 2.5f;
			helth = 300;
			done = 15;
            bloodY = 0.9f;
            shoting = true;
            anim["Steps"].speed = 1.5f;
		}
		if (typeZomb == "bZomb") {
			speed = 4;
            speedMax = 4;
			helth = 350;
			done = 30;
            bloodY = 0.9f;
            shoting = false;
            anim["Steps"].speed = 1.5f;
		}

        blood = PlayerPrefs.GetInt("blood");
	}

//Оружие в бонус
    //Кидать на оружие колайдер и скрипт бонуса и анимацию
	void GunBonus(){
        if (timeInGame > 15 && ZombieAll.GetComponent<Survival>().gunBonus == false && instNewWeapTime <= 0)
        {
				Instantiate(gun, transform.position, Quaternion.Euler(0,0,0));
                ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
		}
        if (timeInGame > 60 && ZombieAll.GetComponent<Survival>().grenadeBonus == false && instNewWeapTime <= 0)
        {
				Instantiate(grenage, transform.position, Quaternion.Euler(0,0,0));
                ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
		}
        if (timeInGame > 90 && ZombieAll.GetComponent<Survival>().minigunBonus == false && instNewWeapTime <= 0)
        {
				Instantiate(minigun, transform.position, Quaternion.Euler(0,0,0));
                ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
		}
        if (timeInGame > 120 && ZombieAll.GetComponent<Survival>().rocketBonus == false && instNewWeapTime <= 0)
        {
				Instantiate(rocket, transform.position, Quaternion.Euler(0,0,0));
                ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
		}
        if (timeInGame > 150 && ZombieAll.GetComponent<Survival>().diskgunBonus == false && instNewWeapTime <= 0)
        {
				Instantiate(diskgun, transform.position, Quaternion.Euler(0,0,0));
                ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
		}
        if (timeInGame > 180 && ZombieAll.GetComponent<Survival>().gaussgunBonus == false && instNewWeapTime <= 0)
        {
            Instantiate(gaussgun, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
        }
        if (timeInGame > 210 && ZombieAll.GetComponent<Survival>().firegunBonus == false && instNewWeapTime <= 0)
        {
            Instantiate(firegun, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
        }
        if (timeInGame > 240 && ZombieAll.GetComponent<Survival>().zeusgunBonus == false && instNewWeapTime <= 0)
        {
            Instantiate(zeusgun, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
        }
        if (timeInGame > 290 && ZombieAll.GetComponent<Survival>().plasmicgunBonus == false && instNewWeapTime <= 0)
        {
            Instantiate(plasmicgun, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
        }
	}

//Бонусы
	void BonusRandom(){
		int rand = Random.Range (0, 400);
		if (rand <= 10){
			Instantiate(helth20, transform.position, Quaternion.Euler(0,0,0));
		}
		if (rand > 10 && rand <= 15) {
			Instantiate(gunBul50, transform.position, Quaternion.Euler(0,0,0));
		}
        if (rand > 30 && rand <= 35)
        {
            Instantiate(grenadeBul50, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 50 && rand <= 55)
        {
            Instantiate(minigunBul500, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 70 && rand <= 75)
        {
            Instantiate(rocketBul50, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 90 && rand <= 95)
        {
            Instantiate(diskgunBul50, transform.position, Quaternion.Euler(0, 0, 0));
        }

        if (rand > 110 && rand <= 115)
        {
            Instantiate(gaussgunBul100, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 130 && rand <= 135)
        {
            Instantiate(firegunBul50, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 150 && rand <= 155)
        {
            Instantiate(zeusgunBul50, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 170 && rand <= 175)
        {
            Instantiate(plasmicgunBul50, transform.position, Quaternion.Euler(0, 0, 0));
        }


        if (rand > 190 && rand <= 195)
        {
            Instantiate(armour100, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 210 && rand <= 215)
        {
            Instantiate(armour200, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 230 && rand <= 235)
        {
            Instantiate(armour300, transform.position, Quaternion.Euler(0, 0, 0));
        }

        //Способности
        if (rand > 250 && rand <= 255 && ZombieAll.GetComponent<ZombieAll>().accuracyMax < 5)
        {
            Instantiate(accuracyMaxBonus, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().accuracyMax += 1;
        }
        if (rand > 280 && rand <= 285 && ZombieAll.GetComponent<ZombieAll>().strongMax < 5)
        {
            Instantiate(strongMaxBonus, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().strongMax += 1;
        }
        if (rand > 310 && rand <= 315 && ZombieAll.GetComponent<ZombieAll>().speedMax < 5)
        {
            Instantiate(speedMaxBonus, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().speedMax += 1;
        }
        if (rand > 340 && rand <= 345 && ZombieAll.GetComponent<ZombieAll>().helthMax < 5)
        {
            Instantiate(helthMaxBonus, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().helthMax += 1;
        }
	}

    RaycastHit Hit; //Что перед зомби
    RaycastHit HitRight; //Справа от зомби

    void FixedUpdate()
    {
        #region Animation

        anim.CrossFade("Steps");
        #endregion

        timeInGame = GameObject.Find("ZombieLogic").GetComponent<ZombieAll>().timeInGame;

        instNewWeapTime = ZombieAll.GetComponent<ZombieAll>().instNewWeapTime;
        magic1 = ZombieAll.GetComponent<Magic>().magic1;
        magic2 = ZombieAll.GetComponent<Magic>().magic2;
        magic2kill = ZombieAll.GetComponent<Magic>().magic2kill;

        //Физика движения зомби
        //Луч вперед
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out Hit, 100f))
        {
            if (Hit.transform.tag == "Limiter")
                Physics.IgnoreCollision(transform.collider, Hit.transform.collider);
            if (Hit.transform.tag == "Player" && !magic1 && !magic2)
                speed = speedMax;
            if (Hit.transform.tag == "Zombie")
                speed = speedMax / 2;

            if (magic1 && !magic1enabled)
            {
                speed /= 2;
                magic1enabled = true;
            }
            if (!magic1 && magic1enabled)
            {
                speed = speedMax;
                magic1enabled = false;
            }

            if (magic2)
            {
                speed = 0;
                ice.renderer.enabled = true;
            }
            if (magic2kill)
                helth = 0;
        }

        #region Rotation
        Vector3 relativePos = Player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        if (!magic2)
            transform.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
        #endregion

        #region Moved
        moveDirection = new Vector3(0, 0, 1);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        moveDirection.y -= 150 * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
        #endregion

        #region Death Zombie
        if (transform.position.y < -2)
            Destroy(gameObject);

        if (helth < 1)
        {
            if (blood != 0)
            {
                newbloodObj = Instantiate(blod, new Vector3(transform.position.x, transform.position.y - bloodY, transform.position.z), Quaternion.Euler(-90, transform.rotation.y, transform.rotation.z)) as Transform;
                newbloodObj.GetComponent<Blood>().typeZombie = typeZomb;
            }

            if (gameMode != "arena")
            {
                BonusRandom();
                GunBonus();
            }

            ZombieAll.GetComponent<ZombieAll>().accountZombNew--;
            Destroy(gameObject);
        }
        #endregion

        #region Damage
        RaycastHit HitZomb;

        //Урон

        timeDamage -= Time.deltaTime;

        if (Physics.Raycast(transform.position, transform.forward, out HitZomb, 2.5f))
        {
            if (HitZomb.transform.tag == "OtherHelth")
            {
                if (timeDamage <= 0)
                {
                    HitZomb.transform.GetComponent<OtherHelth>().helth -= done;
                    timeDamage = 1.2f;
                }
            }

            if (HitZomb.transform.name == "Actor")
            {
                if (typeZomb == "Dog")
                    anim.CrossFade("Attack");

                if (timeDamage <= 0)
                {
                    if (Player.GetComponent<Actor>().armour > 0)
                    {
                        Player.GetComponent<Actor>().helth -= (int)(done / 2f);
                        Player.GetComponent<Actor>().armour -= (int)(done / 2f);
                    }
                    if (Player.GetComponent<Actor>().armour <= 0)
                        Player.GetComponent<Actor>().helth -= done;

                    timeDamage = 1.2f;
                }
            }

            if (HitZomb.transform.name == "Bmp")
            {
                if (typeZomb == "Dog")
                    anim.CrossFade("Attack");

                if (timeDamage <= 0)
                {
                    if (Player.GetComponent<Actor>().armour > 0)
                    {
                        Player.GetComponent<Actor>().helth -= done;
                        Player.GetComponent<Actor>().armour -= done;
                    }
                    if (Player.GetComponent<Actor>().armour <= 0)
                        Player.GetComponent<Actor>().helth -= done * 2;

                    timeDamage = 1.2f;
                }
            }
        }
        #endregion

        #region Shoting
        if (shoting && timeDamage <= 0)
        {
            switch (typeZomb)
            {
                case "Bandit":
                    Instantiate(BanditBullet, BulletSpawn.position, transform.rotation);
                    timeDamage = 1f;
                    break;
                case "Forester":
                    Instantiate(ForesterBullet, BulletSpawn.position, transform.rotation);
                    timeDamage = 2f;
                    break;
            }
        }
        #endregion
    }
}
