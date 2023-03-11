using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status 
{
    public float duration; //duration in seconds
    public virtual void OnTick(Magic magic)
    {

    }
    public virtual void OnHit(Magic magic)
    {

    }
    public Status()
    {
    }

    

}

public class Burning : Status
{
    float damage;
    public Burning(float duration, float damage)
    {
        this.duration = duration;
        this.damage = damage;
    }

    public override void OnTick(Magic magic)
    {
        base.OnTick(magic);
        magic.TakeDamage(damage);
    }
    public override void OnHit(Magic magic)
    {
        base.OnHit(magic);
        magic.ApplyStatus(new Burning(duration, damage / 2));
    }
}


