using System.Collections;
using System.Collections.Generic;
using System;

// 森へ行き素材を手に入れる
class EnterForestAndPickupGredient: State
{
    override public void Enter(Alchemist alchemist){
        Console.Write("Enter  : ");
        if(alchemist.m_location != Location.Forest){
            alchemist.m_location = Location.Forest;
            Console.WriteLine("森に到着、いい素材あるかな?");
        }
    }
    override public void Execute(Alchemist alchemist){
        alchemist.PickupIngredient();
        Console.Write("Execute: ");
        Console.Write("素材を見つけた! ingredients: " + alchemist.m_ingredientCount);
        alchemist.IncreaseFatigue();
        Console.WriteLine(" iFatigue: "  + alchemist.m_iFatigue);

        if(alchemist.Fatigued()){
            Console.WriteLine("疲れたから家に帰ろう");
            alchemist.ChangeState(new ReturnToHomeAndRest());
            return;
        }
        if (alchemist.PocketsFull()) {
            Console.WriteLine("ポケットがいっぱいになっちゃったから帰ろう");
            alchemist.ChangeState(new GoAtelierAndSynthesis());
            return;
        }
    }
    override public void Exit(Alchemist alchemist){
        Console.WriteLine("Exit   : ");
        // Console.WriteLine("森から出よう");
    }
}

// アトリエに行き調合する
class GoAtelierAndSynthesis: State
{
    override public void Enter(Alchemist alchemist){
        if(alchemist.m_location != Location.Atelier){
            alchemist.m_location = Location.Forest;
            Console.Write("Enter: ");
            Console.WriteLine("アトリエに着いた、さあ調合しよう");
        }
    }
    override public void Execute(Alchemist alchemist){
        Console.Write("Execute: ");
        if(alchemist.EnoughIngredient()){
            alchemist.Synthesis();
            alchemist.IncreaseFatigue();
            Console.WriteLine("良いものができた! item: " + alchemist.m_itemCount + " m_iFatigue: " + alchemist.m_iFatigue);

            if(alchemist.Fatigued()) {
                alchemist.ChangeState(new ReturnToHomeAndRest());
            }

        } else {
            Console.WriteLine("素材が足りなくなっちゃった");
            alchemist.ChangeState(new EnterForestAndPickupGredient());
        }
    }
    override public void Exit(Alchemist alchemist)
    {
        Console.WriteLine("Exit   : ");
        // Console.WriteLine("素材探しに行こうっと");
    }
}

// アトリエで休憩する
class ReturnToHomeAndRest: State
{
    override public void Enter(Alchemist alchemist){
        if(alchemist.m_location != Location.Home){
            alchemist.m_location = Location.Home;
            Console.Write("Enter  : ");
            Console.WriteLine("ただいまー");
        }
    }
    override public void Execute(Alchemist alchemist){
        Console.Write("Execute: ");
        Console.WriteLine("休憩しよう m_iFatigue: " + alchemist.m_iFatigue);
        alchemist.DecreaseFatigue();

        if (alchemist.ImFine()){
            Console.WriteLine("気持ち良いお昼寝だったー");

            // 素材が足りていたら
            if(alchemist.EnoughIngredient() == true)
            alchemist.ChangeState(new GoAtelierAndSynthesis());

            // 素材が足りなかったら
            if(alchemist.EnoughIngredient() == false)
            alchemist.ChangeState(new EnterForestAndPickupGredient());
        }
    }
    override public void Exit(Alchemist alchemist){
        Console.Write("Exit   : ");
        Console.WriteLine("いってきまーす");
    }
}