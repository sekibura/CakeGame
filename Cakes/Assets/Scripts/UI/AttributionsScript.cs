using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributionsScript : MonoBehaviour
{
    public float minimum = 0.0F;
    public float maximum = 1.0F;

    // starting value for the Lerp
    static float t = 0.0f;
    ScrollRect scrollRect;
    bool done = false;

    void Start()
    {
         scrollRect = GetComponent<ScrollRect>();

    }

    private void Update()
    {
        if (done)
            return;

         scrollRect.verticalNormalizedPosition = Mathf.Lerp(minimum, maximum, t);

        // .. and increase the t interpolater
        t += 0.8f * Time.deltaTime;

        if (t > 1.0f)
        {
            done = true;
        }
    }

}
