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
        alchemist.IncreaseFatigue();

        if(alchemist.Fatigued()){
            Console.WriteLine("疲れたから家に帰ろう");
            alchemist.ChangeState(new ReturnToHomeAndRest());
        }
        else if (alchemist.PocketsFull()) {
            Console.WriteLine("ポケットがいっぱいになっちゃったから帰ろう");
            alchemist.ChangeState(new GoAtelierAndSynthesis());
        }
    }
    override public void Exit(Alchemist alchemist){
        // Console.WriteLine("森から出よう");
    }
}

// アトリエに行き調合する
class GoAtelierAndSynthesis: State
{
    override public void Enter(Alchemist alchemist){
        if(alchemist.m_location != Location.Atelier){
            alchemist.m_location = Location.Forest;
            Console.WriteLine("アトリエに着いた、さあ調合しよう");
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
class ReturnToHomeAndRest: State
{
    override public void Enter(Alchemist alchemist){
        if(alchemist.m_location != Location.Home){
            alchemist.m_location = Location.Home;
            Console.WriteLine("ただいまー");
        }
    }
    override public void Execute(Alchemist alchemist){
        Console.WriteLine("休憩しよう m_iFatigue: " + alchemist.m_iFatigue);
        alchemist.DecreaseFatigue();

        if (alchemist.ImFine()){
            Console.WriteLine("気持ち良いお昼寝だったー");

            // 素材が足りていたら
            if(alchemist.EnoughGredient() == true)
            alchemist.ChangeState(new GoAtelierAndSynthesis());

            // 素材が足りなかったら
            if(alchemist.EnoughGredient() == false)
            alchemist.ChangeState(new EnterForestAndPickupGredient());
        }
    }
    override public void Exit(Alchemist alchemist){
        Console.WriteLine("いってきまーす");
    }
}
