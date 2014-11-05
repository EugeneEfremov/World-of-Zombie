using UnityEngine;
using System.Collections;

public class ShellBullet : MonoBehaviour {
	void Start () {
        Destroy(gameObject, 15);
	}
}
