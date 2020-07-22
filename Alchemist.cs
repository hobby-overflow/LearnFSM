using System.Collections;
using System.Collections.Generic;
using System;

class Alchemist
{
    private State m_currentState;
    public int m_itemCount;
    public int m_gredientCount;

    public int maxItem = 3;
    public Location m_location;

    public Alchemist(State state){
        this.m_currentState = state;
    }
    public void ChangeState(State newState){
        if(m_currentState == null && newState == null)
        System.Console.Out.WriteLine("state is null");

        m_currentState.Exit(this);
        m_currentState = newState;
        m_currentState.Enter(this);
    }
    public void PickupGredient()
    {
        m_gredientCount++;
        if(m_gredientCount < 0) m_gredientCount = 0;
    }
    public bool PocketsFull()
    {
        return m_gredientCount >= maxItem;
    }
    public bool EnoughGredient()
    {
        return m_gredientCount > 0;
    }
    public void Synthesis()
    {
        m_gredientCount -= 1;
        m_itemCount += 1;
    }

    public void Update(){
        if(m_currentState != null)
        {
            m_currentState.Execute(this);
        }
    }
}