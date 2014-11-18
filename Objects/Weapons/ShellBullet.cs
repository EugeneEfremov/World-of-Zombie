using UnityEngine;
using System.Collections;

public class ShellBullet : MonoBehaviour {
	void Start () {
        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                Destroy(gameObject, 3);
                break;
            case 1:
                Destroy(gameObject, 6);
                break;
            default:
                Destroy(gameObject, 10);
                break;
        }
	}
}
