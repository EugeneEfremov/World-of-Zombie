using UnityEngine;
using System.Collections;

public class Destroying : MonoBehaviour {

    public string type;
    public float destroyTime;
    public bool destroy;

    void Start()
    {
        if (type == null || type == "")
            Destroy(gameObject, destroyTime);

        switch (type)
        {
            case "blood":
                if (QualitySettings.GetQualityLevel() == 0)
                    Destroy(gameObject, 30);

                if (QualitySettings.GetQualityLevel() == 1)
                    Destroy(gameObject, 45);

                if (QualitySettings.GetQualityLevel() == 2)
                    Destroy(gameObject, 60);
            break;

            default:
                Destroy(gameObject, destroyTime);
            break;
        }
    }

    void FixedUpdate()
    {
        if (destroy)
            Destroy(gameObject);
    }
}
