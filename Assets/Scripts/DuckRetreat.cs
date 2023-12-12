using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckRetreat : DuckState
{
    public DuckRetreat(DuckStateController dsc) : base(dsc) { }

    public override void OnStateEnter()
    {
    }

    public override void CheckTransisions()
    {

        //dsc.sanity;
        //dsc.light;
        //dsc.currentSanity;

        if(dsc.currentSanity > dsc.sanity - 5.5f)
        {
            //chase
            dsc.SetState(new DuckChase(dsc));
        }
        else if ((dsc.currentSanity == (dsc.sanity + 5.0f)) || (dsc.currentSanity == dsc.sanity - 5.0f))
        {
            //hunt
            dsc.SetState(new DuckHunt(dsc));
        }
        else if(dsc.currentSanity < dsc.sanity + 5.5f)
        {
            //stalk
            dsc.SetState(new DuckStalk(dsc));
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