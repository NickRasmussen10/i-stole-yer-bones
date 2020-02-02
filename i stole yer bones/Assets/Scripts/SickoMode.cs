using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickoMode : MonoBehaviour
{
    [SerializeField] UnityEngine.Experimental.Rendering.LWRP.Light2D globalLight;
    [SerializeField] UnityEngine.Experimental.Rendering.LWRP.Light2D[] buttonLights = new UnityEngine.Experimental.Rendering.LWRP.Light2D[4];

    public bool isSicko = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoSickoMode()
    {
        StartCoroutine(SickoModeTime());
    }

    public void RIPSickoMode()
    {
        StartCoroutine(GoodByeSickoMode());
    }

    IEnumerator SickoModeTime()
    {
        isSicko = true;
        float lerpVal = 0.0f;
        while(lerpVal < 1.0f)
        {
            lerpVal += Time.deltaTime;
            globalLight.intensity = Mathf.Lerp(1.0f, 0.5f, lerpVal);
            for(int i = 0; i < buttonLights.Length; i++)
            {
                buttonLights[i].intensity = Mathf.Lerp(0.0f, 1.0f, lerpVal);
            }
            //Camera.main.orthographicSize = Mathf.Lerp(5f, 4.5f, lerpVal);
            yield return null;
        }
    }

    IEnumerator GoodByeSickoMode()
    {
        isSicko = false;
        float globalLightStart = globalLight.intensity;
        float buttonLightStart = buttonLights[0].intensity;
        float lerpVal = 0.0f;

        while(lerpVal < 1.0f)
        {
            lerpVal += Time.deltaTime * 8f;
            globalLight.intensity = Mathf.Lerp(globalLightStart, 1.0f, lerpVal);
            for(int i = 0; i < buttonLights.Length; i++)
            {
                buttonLights[i].intensity = Mathf.Lerp(buttonLightStart, 0.0f, lerpVal);
            }

            //Camera.main.orthographicSize = Mathf.Lerp(4.5f, 5f, lerpVal);
            yield return null;
        }

    }
}
