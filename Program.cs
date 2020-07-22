using System;

namespace LearnFSMwithAlchemist
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Alchemist maruru = new Alchemist(new GoAtelierAndSynthesis());
            maruru.m_location = Location.Atelier;
            for(int i=0;i<20;i++){
                System.Threading.Thread.Sleep(500);
                maruru.Update();
            }
        }
    }
}
