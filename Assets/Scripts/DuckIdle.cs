using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckIdle : DuckState
{

    public DuckIdle(DuckStateController dsc) : base(dsc) { }

    public override void OnStateEnter()
    {
        
    }

    public override void CheckTransisions()
    {
        float dist = Vector3.Distance(dsc.transform.position, dsc.player.transform.position);

        if (dist > 7.5f)
        {
            dsc.SetState(new DuckFollow(dsc));
        }
    }

    public override void Act() { }
    public override void OnStateExit() { }
}
