using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AOE : MonoBehaviour
{
    public float maxSize;
    public float duration;
    public Ease ease1;
    public Ease ease2;
    public float damage;
    public Status[] statusesToApply;
    // Update is called once per frame
    private void Start()
    {
        transform.DOScale(maxSize, duration / 2)
            .SetEase(ease1)
            .onComplete = StartSecondTween;
    }

    void StartSecondTween()
    {
        transform.DOScale(0, duration / 2)
            .SetEase(ease2)
            .onComplete = End;
    }

    void End()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Spell")
        {
            collision.gameObject.GetComponent<Magic>().TakeDamage(damage);
            foreach (Status stat in statusesToApply)
                collision.gameObject.GetComponent<Magic>().ApplyStatus(stat);
        }
    }

    public void SetValues(float MaxSize, float Duration, float Damage, Status[] StatusesToApply)
    {
        maxSize = MaxSize;
        duration = Duration;    
        damage = Damage;
        statusesToApply = StatusesToApply;  
    }

}
