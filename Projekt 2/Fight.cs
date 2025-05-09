public class Fight
{
    public static (int, bool, bool, int) Hello(Character thisHero, Monsters thisMonster, bool canmeetmonster, bool fightend)
    {
        int hitchance = Random.Shared.Next(0, 1 + thisHero.Agility);
        if (hitchance < 2)
        {

            thisMonster.Hp -= thisHero.Attack;
            Toolbox.Say("Du gjorde " + thisHero.Attack + " skada!", false);
            Toolbox.Say("Tryck ENTER för att fortsätta striden.", false);
            Console.ReadLine();
        }
        if (hitchance >= 2)
        {
            Toolbox.Say("Du missade din attack!", false);
            Toolbox.Say("Tryck ENTER för att fortsätta striden.", false);
            Console.ReadLine();

        }
        if (thisMonster.Hp <= 0)
        {
            canmeetmonster = false;
            thisMonster.Hp = 0;
            Toolbox.Say("Monstret dog!", false);
            Toolbox.Say("Tryck ENTER för att återgå till att röra dig fritt i slottet igen.", false);
            fightend = true;
        }
        // Så länge monstret lever så har den en chans att slå tillbaks
        if (thisMonster.Hp > 0)
        {
            // Räknar ut nytt Hero-HP efter fienden slagit tillbaks.
            Toolbox.monsterattack(thisHero, thisMonster);
        }
        return (thisMonster.Hp, canmeetmonster, fightend, thisHero.Hp);
    }

    public static (bool, bool, bool) HeroDead(string logo, bool fightend, bool walkarounddone, bool insidedunegon, int thisherohp)
    {
        // Om du dör:
        if (thisherohp == 0)
        {
            Console.Clear();
            Toolbox.Say(logo, false);
            Toolbox.Say("Du dog!", false);
            fightend = true;
            walkarounddone = true;
            insidedunegon = false;
            Toolbox.Say("Tryck på ENTER för att avsluta:", false);
        }
        return (fightend, walkarounddone, insidedunegon);
    }

    public static (bool, bool, int) Flee(Character thisHero, Monsters thisMonster, bool canmeetmonster, bool fightend)
    {
        int fleechance = Random.Shared.Next(0, thisMonster.Agility);
        if (fleechance == 0)
        {
            Toolbox.Say("Du lyckades fly!", false);
            canmeetmonster = false;
            fightend = true;
        }
        else
        {
            Toolbox.Say("Din flykt misslyckades!", false);
            // Monster attack nedan: Finns till för att monstret ska kunna attackera när du försöker att fly
            thisHero.Hp = Toolbox.monsterattack(thisHero, thisMonster);
        }
        return (canmeetmonster, fightend, thisHero.Hp);
    }


}
