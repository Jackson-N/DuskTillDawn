using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckChase : DuckState
{
    public DuckChase(DuckStateController dsc) : base(dsc) { }

    public override void OnStateEnter()
    {
        //dsc.sanity;
        //dsc.currentSanity;
    }

    public override void CheckTransisions()
    {
        if(dsc.sanity < 10.0f)
        {
            dsc.SetState(new DuckKill(dsc));
        }
        else
        {
            //currentSanity = sanity;
            dsc.SetState(new DuckRetreat(dsc));
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
