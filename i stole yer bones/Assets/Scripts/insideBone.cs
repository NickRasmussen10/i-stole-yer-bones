using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insideBone : MonoBehaviour
{
    [SerializeField] GameObject pref_outsideBone = null;
    public bool hasBone;
    SpriteRenderer spriteRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBone && spriteRenderer.color != Color.white)
        {
            spriteRenderer.color = Color.white;
        }
        else if(!hasBone && spriteRenderer.color != Color.black)
        {
            spriteRenderer.color = Color.black;
        }
    }

    public void Explode()
    {
        StartCoroutine(HandleExplosion());
    }

    IEnumerator HandleExplosion()
    {
        hasBone = false;
        Vector2 explosionForce = Vector2.zero;
        GameObject newBone = Instantiate(pref_outsideBone, transform.position, Quaternion.identity);
        newBone.GetComponent<MoveableObject>().collisionEventsEnabled = false;
        int explosion = Random.Range(0, 2);
        switch (explosion)
        {
            case 0:
                explosionForce = new Vector2(50, 50);
                break;
            case 1:
                explosionForce = new Vector2(-50, 50);
                break;
        }
        newBone.GetComponent<Rigidbody2D>().AddForce(explosionForce, ForceMode2D.Impulse);
        Collider2D[] effeced = Physics2D.OverlapCircleAll(transform.position, 1.0f);
        foreach (Collider2D c in effeced)
        {
            Rigidbody2D r = c.gameObject.GetComponent<Rigidbody2D>();
            if (r) r.AddForce((Vector2)transform.position - r.position, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(1.0f);
        newBone.GetComponent<MoveableObject>().collisionEventsEnabled = true;
        yield return null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Input.GetButton("Fire1"))
        {
            MoveableObject bone = collision.gameObject.GetComponent<MoveableObject>();
            if (bone && !bone.isHeld && bone.collisionEventsEnabled)
            {
                if (!hasBone && collision.gameObject.tag == "outsideBone" && collision.gameObject.transform.parent == null)
                {
                    Destroy(collision.gameObject);
                    hasBone = true;
                }
            }
        }
    }
}
