using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupilFollow : MonoBehaviour
{
    Vector3 centerofEye;
    // Start is called before the first frame update
    void Start()
    {
        centerofEye = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 eyeDirection = mousePos - (transform.parent.transform.position + centerofEye);
        eyeDirection = eyeDirection.normalized;
        transform.localPosition = centerofEye + (eyeDirection * 0.5f);

    }
}
