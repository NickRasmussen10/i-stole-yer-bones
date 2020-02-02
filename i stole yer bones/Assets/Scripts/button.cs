using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    [SerializeField] SpriteRenderer fill = null;
    [SerializeField] Transform snapPos = null;
    public bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "outsideBone")
        {
            fill.size = new Vector2(fill.size.x, 0);
            pressed = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ragdoll")
        {
            Debug.Log(collision.gameObject);
            if (collision.GetComponentInChildren<insideBone>().hasBone)
            {
                pressed = true;
                if (collision.gameObject.transform.position != snapPos.position)
                {
                    if (collision.gameObject.GetComponent<RagdollDrag>()) collision.gameObject.GetComponent<RagdollDrag>().StickTo(snapPos);
                }


                Vector2 size = fill.size;
                size.y += 0.1f;
                if (size.y < 4.5f) size.y = 4.5f;
                fill.size = size;
            }
        }
    }
}
