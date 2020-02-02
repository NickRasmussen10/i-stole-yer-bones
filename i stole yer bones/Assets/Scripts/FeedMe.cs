using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedMe : MonoBehaviour
{
    GameObject mouse;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        mouse = GameObject.Find("mouse");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.transform.childCount > 0)
        {
            //FEED ME
            rb.AddForce((mouse.transform.GetChild(0).transform.position - transform.position) * 0.5f, ForceMode2D.Impulse);
        }
    }
}
