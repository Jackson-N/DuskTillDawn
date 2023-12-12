using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckStalk : DuckState
{
    public DuckStalk(DuckStateController dsc) : base(dsc) { }

    public override void OnStateEnter()
    {
        
    }

    public override void CheckTransisions()
    {
        float dist = Vector3.Distance(dsc.transform.position, dsc.player.transform.position);

        if (dist < 15.0f)
        {
            dsc.SetState(new DuckHunt(dsc));
        }
        
    }

    public override void Act()
    {
       
    }

    public override void OnStateExit() { }
}