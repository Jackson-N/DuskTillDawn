using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DuckHunt : DuckState
{
    public DuckHunt(DuckStateController dsc) : base(dsc) { }

    public override void OnStateEnter()
    {
        //dsc.light;
        //stuff
    }

    public override void CheckTransisions()
    {
        if (dsc.light.intensity > 5.0f)
        {
            dsc.SetState(new DuckRetreat(dsc));
        }
        else if (dsc.light.intensity < 5.0f)
        {
            dsc.SetState(new DuckChase(dsc));
        }
        
    }

    public override void Act()
    {
        dsc.transform.Translate(Vector3.forward * 1.0f * Time.deltaTime);
        Vector3 dir = dsc.player.transform.position - dsc.transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        dsc.transform.rotation = Quaternion.Slerp(dsc.transform.rotation, rot, 0.2f * Time.deltaTime);
    }

    public override void OnStateExit() { }
}
