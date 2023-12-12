using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckKill : DuckState
{
    public DuckKill(DuckStateController dsc) : base(dsc) { }

    public override void OnStateEnter()
    {
        dsc.isTouchingPlayer = false;
    }

    public override void CheckTransisions() { }

    public override void Act()
    {
        dsc.transform.Translate(Vector3.forward * 1.0f * Time.deltaTime);
        Vector3 dir = dsc.player.transform.position - dsc.transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        dsc.transform.rotation = Quaternion.Slerp(dsc.transform.rotation, rot, 0.2f * Time.deltaTime);
        dsc.isTouchingPlayer = true;
        
    }

    public override void OnStateExit() { }
}