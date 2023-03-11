using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : Photon.MonoBehaviour
{
    // Start is called before the first frame update
    public float HP;
    public Spell spell; 
    public List<Status> statuses = new List<Status>();
    public string interactionType; //ethereal (wave/mist/wind), projectile (fireball, rockshot), static (wall)
    public virtual void OnSpawn()
    {
        if(interactionType == "ethereal")
        {
            foreach(var comp in GetComponents<CircleCollider2D>())
                comp.enabled = false;
            foreach (var comp in GetComponents<BoxCollider2D>())
                comp.enabled = false;


        }
        else if(interactionType == "static")
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public virtual void OnTick()
    {
        foreach (var stat in statuses)
        {
            stat.OnTick(this);
        }
    }

    public virtual void OnHit(Magic hitMagic)
    {
        foreach (var stat in statuses)
        {
            stat.OnHit(this);
        }
    }

    public virtual void OnExpire()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        HP -= damage;
    }

    public virtual void ApplyStatus(Status status)
    {
        statuses.Add(status);
    }

    void Start()
    {
        OnSpawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OnTick();
        if(HP <= 0)
        {
            OnExpire();
        }
    }

    void OnSerializeField()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spell")
            OnHit(collision.gameObject.GetComponent<Magic>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spell" && interactionType == "ethereal")
            OnHit(collision.gameObject.GetComponent<Magic>());
    }


}
 