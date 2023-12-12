using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckFollow : DuckState
{
    public DuckFollow(DuckStateController dsc) : base(dsc) { }

    public override void OnStateEnter()
    {
        dsc.AnimationControl("DuckFlightAnimation.Glide");
    }

    public override void CheckTransisions()
    {
        float dist = Vector3.Distance(dsc.transform.position, dsc.player.transform.position);

        if (dist < 0.5f)
        {
            dsc.SetState(new DuckIdle(dsc));
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
