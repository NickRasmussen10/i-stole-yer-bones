using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    GameObject mouse;
    [SerializeField] insideBone bone;

    new Collider2D collider;

    Rigidbody2D rb = null;
    public bool isHeld = false; //is the mouse holding this objct?
    public bool collisionEventsEnabled = true;

    Vector3 scaleNormal;

    Vector3 previousMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        mouse = GameObject.Find("mouse");
        rb = GetComponent<Rigidbody2D>();
        if (GetComponent<BoxCollider2D>()) collider = GetComponent<BoxCollider2D>();
        else if (GetComponent<CapsuleCollider2D>()) collider = GetComponent<CapsuleCollider2D>();
        scaleNormal = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButton("Fire1")) isHeld = false;
        if (!isHeld)
        {
            mouse.transform.eulerAngles = Vector3.zero;
            if (!ParentIsInsideBone()) transform.parent = null;
            collider.isTrigger = true;
        }

        if(!isHeld && collider.isTrigger)
        {
            collider.isTrigger = false;
        }


        if (isHeld)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.localScale = scaleNormal * 1.25f;
            mouse.transform.eulerAngles = new Vector3 (0, 0, EasingFunction.EaseInElastic( mouse.transform.eulerAngles.z, 90.01f, 0.39f ) );
        }
        else if(rb.bodyType != RigidbodyType2D.Dynamic && !ParentIsInsideBone())
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            transform.localScale = scaleNormal;

            //oh gee howdy do I love throwin' bones with some M A G I C N U M B E R S
            rb.AddForce((mouse.transform.position - previousMousePosition) * 50.0f, ForceMode2D.Impulse) ;
        }

        previousMousePosition = mouse.transform.position;

        if (ParentIsInsideBone())
        {
            collider.isTrigger = true;
        }
    }


    private void OnMouseOver()
    {
        if (Input.GetButton("Fire1") && !ParentIsInsideBone())
        {
            if (!isHeld)
            {
                FindObjectOfType<AudioManager>().Play("bone pop");
                transform.parent = mouse.transform;
                collider.isTrigger = true;
                isHeld = true;
            }
        }
    }

    bool ParentIsInsideBone()
    {
        return transform.parent != null && transform.parent.gameObject.tag == "insideBone";
    }
}
