using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIREFIRE : Magic
{
    public override void OnExpire()
    {
        base.OnExpire();
        PhotonNetwork.Instantiate("AOE", transform.position, Quaternion.identity, 0)
            .GetComponent<AOE>()
            .SetValues(spell.power2, spell.power2, spell.power1, new Status[] { new Burning(spell.power2, spell.power2)});
    }

    public override void OnHit(Magic hitMagic)
    {
        base.OnHit(hitMagic);
        hitMagic.ApplyStatus(new Burning(spell.power2, spell.power2));
        hitMagic.TakeDamage(spell.power1 * 50);
        OnExpire();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        HP = spell.power1;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DefaultSprites/Circle");
       
        
    }


}
