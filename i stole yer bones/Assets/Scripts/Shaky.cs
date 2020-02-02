using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaky : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartShake(float intensity, float duration)
    {
        StartCoroutine(Shake(intensity, duration));
    }

    IEnumerator Shake(float intensity, float duration)
    {
        float time = 0;
        Vector3 origin = new Vector3(0.0f, 0.0f, -10.0f);
        Vector3 newPos = transform.position;
        while(time < duration)
        {
            time += Time.deltaTime;
            newPos.x += Random.Range(-intensity, intensity);
            newPos.y += Random.Range(-intensity, intensity);
            transform.position = newPos;
            yield return null;
        }
        transform.position = origin;
        yield return null;
    }
}
