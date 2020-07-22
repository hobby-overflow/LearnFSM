using System.Collections;
using System.Collections.Generic;
using System;

class State
{
    public virtual void Enter(Alchemist alchemist){}
    public virtual void Execute(Alchemist alchemist){}
    public virtual void Exit(Alchemist alchemist){}
}