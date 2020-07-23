using System.Collections;
using System.Collections.Generic;
using System;

class Alchemist
{
    public int maxItem = 3;
    public int maxFatigue = 5;
    public int m_itemCount;
    public int m_gredientCount;
    public int m_iFatigue;
    public Location m_location;

    private State m_currentState;
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
    public void IncreaseFatigue()
    {
        m_iFatigue = Math.Clamp(m_iFatigue+1, 0, maxFatigue);
    }
    public void DecreaseFatigue()
    {
        m_iFatigue = Math.Clamp(m_iFatigue-2, 0, maxFatigue);
    }
    public bool Fatigued()
    {
        return m_iFatigue >= 5;
    }
    public bool ImFine()
    {
        return m_iFatigue == 0;
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
        Console.WriteLine("--------------------");
        if(m_currentState != null)
        {
            m_currentState.Execute(this);
        }
    }
}