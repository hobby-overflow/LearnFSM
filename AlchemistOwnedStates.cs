using System.Collections;
using System.Collections.Generic;
using System;

// 森へ行き素材を手に入れる
class EnterForestAndPickupGredient: State
{
    override public void Enter(Alchemist alchemist){
        if(alchemist.m_location != Location.Forest){
            alchemist.m_location = Location.Forest;
            Console.WriteLine("森に到着、いい素材あるかな?");
        }
    }
    override public void Execute(Alchemist alchemist){
        alchemist.PickupGredient();
        Console.WriteLine("素材を見つけた! gredients: " + alchemist.m_gredientCount);
        if (alchemist.PocketsFull()) {
            alchemist.ChangeState(new GoAtelierAndSynthesis());
        }
    }
    override public void Exit(Alchemist alchemist){
        Console.WriteLine("いっぱいになったから帰ろう");
    }
}

// アトリエに行き調合する
class GoAtelierAndSynthesis: State
{
    override public void Enter(Alchemist alchemist){
        if(alchemist.m_location != Location.Atelier){
            alchemist.m_location = Location.Forest;
            Console.WriteLine("アトリエに着いた、調合しよう");
        }
    }
    override public void Execute(Alchemist alchemist){
        if(alchemist.EnoughGredient()){
            alchemist.Synthesis();
            Console.WriteLine("良いものができた! item: " + alchemist.m_itemCount);
        } else {
            Console.WriteLine("素材が足りなくなっちゃった");
            alchemist.ChangeState(new EnterForestAndPickupGredient());
        }
    }
    override public void Exit(Alchemist alchemist)
    {
        Console.WriteLine("素材探しに行こうっと");
    }
}

// アトリエで休憩する