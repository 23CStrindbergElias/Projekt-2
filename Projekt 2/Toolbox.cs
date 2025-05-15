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

    public static void DrawMap(int[,] map, int heroX, int heroY, string[] roomtype)
    {
        int slask = 0;
        for (int x = 0; x < map.GetLength(1); x++)
        {
            Toolbox.Say("", false);
            for (int y = 0; y < map.GetLength(0); y++)
            {
                slask = map[x, y];
                if (x == heroX && y == heroY)
                {
                    // Ritar ut hjälterutan
                    Console.Write("×");
                }
                else
                {
                    Console.Write(roomtype[slask]);
                }
            }
        }
        Console.WriteLine();
    }

    public static (int, int) Drakkort(int ROUNDS, int totalcards)
    {

        int Alive;
        Random rnd = new Random();
        int drawncard = rnd.Next(1, totalcards);

        if (drawncard == 1)
        {
            // Draken vaknar
            Alive = 0;
        }
        else
        {
            Toolbox.Say("Draken fortsätter att sova.", false);
            Alive = 1;
        }
        int cardsleft = totalcards - 1;
        return (Alive, cardsleft);
    }

    public static int Skattkort(int ROUNDS)
    {
        int i = Random.Shared.Next(1, 21);
        string thisthing = "";
        int thisvalue = 0;
        if (i < 16)
        {
            // Får bara mynt
            thisvalue = Random.Shared.Next(1, 11) * 10; thisthing = "Mynt";
        }
        else
        {
            // Vi får ett föremål. Jag valde en Array för att innehållet aldrig ändras
            string[,] Skatter = { { "Rubin", "50" }, { "Diamant", "70" }, { "Spira", "90" }, { "Krona", "110" }, { "Guldäpple", "30" } };
            int trash = Random.Shared.Next(0, Skatter.GetLength(0));
            bool success = int.TryParse(Skatter[trash, 1], out thisvalue);
            thisthing = Skatter[trash, 0];
        }
        Toolbox.Say("Du hittade " + thisthing + " till ett värde av " + thisvalue + " coins.", false);
        return thisvalue;
    }

    public static (int, int) hero_alive(int Alive, int totalgold, int ROUNDS)
    {
        if (Alive != 0)
        {
            // Lägger ihop totala värdet av allt du snattat åt dig, värdet sparas även om du går in och ut ur skattkammaren genom att klicka 2

            Toolbox.Say("Det du samlat på dig har ett värd på totalt " + totalgold + " coins", false);
            ROUNDS++;
            Toolbox.Say("Du har varit i skattkammaren i " + ROUNDS + " rundor.", false);
            Toolbox.Say("Tryck på ENTER för att fortsätta.", true);
            if (ROUNDS == 11)
            {
                // Liten rekommendation, fast det händer så sällan att det nästan bör räknas som ett easter egg
                Toolbox.Say("Jag skulle gå ut om jag vore du...", false);
            }
            Console.Clear();
        }
        return (totalgold, ROUNDS);
    }


    public static (bool, bool, bool) hero_dead(int alive, string dragon, int revivecount, string phoenix, int ROUNDS, bool treasurechamber, bool insidedunegon, bool walkarounddone)
    {
        if (alive == 0)
        {
            Console.Clear();
            Console.WriteLine("Du dog, draken vaknade!");
            Console.WriteLine(dragon);
            Toolbox.Say("Tryck på RETURN för att avsluta", false);
            Console.ReadLine();
            // Detta förhindrar fenix-fenomenet att hända mer än 1 gång
            if (revivecount == 0)
            {
                int revive = Random.Shared.Next(0, 10);
                if (revive == 9)
                {
                    Console.Clear();
                    Toolbox.Say("...", false);
                    Console.ReadLine();
                    Toolbox.Say("...", false);
                    Console.ReadLine();
                    Toolbox.Say(phoenix, false);
                    Toolbox.Say("En fågel Fenix återupplivar dig! Du har en ny chans, slösa inte bort den.", false);
                    Toolbox.Say("Tryck ENTER för att fortsätta", false);
                    ROUNDS = 0;
                    Console.ReadLine();
                    Console.Clear();
                    revivecount++;
                }
                else
                {
                    treasurechamber = false;
                    insidedunegon = false;
                    walkarounddone = true;
                }
            }
            else
            {
                treasurechamber = false;
                insidedunegon = false;
                walkarounddone = true;
            }
        }
        return (treasurechamber, insidedunegon, walkarounddone);
    }















}


