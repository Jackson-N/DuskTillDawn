using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Start is called before the first frame update
public abstract class DuckState
{
    protected DuckStateController dsc;
    
    public abstract void CheckTransisions();

    public abstract void Act();

    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }

    public DuckState(DuckStateController dsc)
    {
        this.dsc = dsc; 
    }
}

