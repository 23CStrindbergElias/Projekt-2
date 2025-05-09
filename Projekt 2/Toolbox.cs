public class Toolbox
{
    public static void Say(string what2say, bool shouldIwait)
    {
        Console.WriteLine(what2say);
        if (shouldIwait)
        {
            Console.ReadLine();
        }

    }

    public static int monsterattack(Character thisHero, Monsters thisMonster)
    {
        Random rnd = new Random();
        int enemyhitchance = rnd.Next(0, 2 + thisHero.Agility);
        if (enemyhitchance < 2)
        {
            thisHero.Hp -= thisMonster.Attack;
            Toolbox.Say("" + thisMonster.Name + " gjorde " + thisMonster.Attack + " skada på dig!", false);
            Toolbox.Say("Tryck ENTER för att fortsätta striden.", false);
            Console.ReadLine();
            Console.Clear();
        }
        else
        {
            Toolbox.Say("Monstret träffar dig inte.", false);
            Toolbox.Say("Tryck ENTER för att fortsätta striden.", false);
            Console.ReadLine();
            Console.Clear();
        }
        return thisHero.Hp;
    }

    public static Character Choosecharacter(Character[] characters, string logo)
    {
        bool success = false;
        bool Mycharacterokay = false;
        int Mycharacter = 0;
        Character myHero = new();
        while (success == false || Mycharacterokay == false)
        {
            Toolbox.Say(logo, false);
            Toolbox.Say("Välj din karaktär nedan, varje karaktär har ett bestämt hp och agility. Dessa kommer att spela roll i kommande strider.", false);
            Toolbox.Say("siffrorna 0, 1 och 2 motsvarar en karaktär. Välj en av dem.", false);
            for (int i = 0; i < characters.Length; i++)
            {
                Toolbox.Say(i + ": " + characters[i].Name + " Hp: " + characters[i].Hp + " Agility: " + characters[i].Agility, false);
            }
            string klass = Console.ReadLine();
            success = int.TryParse(klass, out Mycharacter);
            if (success)
            {
                if (Mycharacter < characters.Length && Mycharacter >= 0)
                {
                    Mycharacterokay = true;
                    myHero.Name = characters[Mycharacter].Name;
                    myHero.Hp = characters[Mycharacter].Hp;
                    myHero.Agility = characters[Mycharacter].Agility;
                    myHero.Attack = characters[Mycharacter].Attack;
                    Toolbox.Say("Du valde " + myHero.Name + ".", false);
                    Toolbox.Say("Tryck ENTER för att börja spela.", false);
                    Console.ReadLine();
                }
                else
                {
                    Toolbox.Say("Du får bara skriva siffror mellan 0-2!", false);
                    Toolbox.Say("Tryck ENTER för att försöka igen.", true);
                }
            }
            else
            {
                Toolbox.Say("Du får bara skriva siffror mellan 0-2!", false);
                Toolbox.Say("Tryck ENTER för att försöka igen.", true);
            }
            Console.Clear();
        }
        return myHero;
    }



    public static (Monsters, bool) meetmonster(Monsters[] Monsters, bool fightend)
    {
        int randommonster = Random.Shared.Next(0, Monsters.GetLength(0));
        Monsters thisMonster = new();
        thisMonster.Name = Monsters[randommonster].Name;
        // Dessa bestämmer monstrets attributes 
        thisMonster.Hp = Monsters[randommonster].Hp;
        thisMonster.Agility = Monsters[randommonster].Agility;
        thisMonster.Attack = Monsters[randommonster].Attack;

        Toolbox.Say("Du mötte " + Monsters[randommonster].Name + "!", false);
        Toolbox.Say("Tryck på ENTER för att gå in i strid.", false);
        fightend = false;
        Console.ReadLine();
        Console.Clear();
        return (thisMonster, fightend);
    }
}


