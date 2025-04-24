// Variabler start

// Varje karaktär har ett bestämt värde för: HP, Agility och Attack.
Character[] characters = {
    new() {Name = "Riddare", Hp = 30, Agility = 1, Attack = 7},
    new() {Name = "Samurai", Hp = 20, Agility = 2, Attack = 5},
    new() {Name = "Ninja", Hp = 15, Agility = 3, Attack = 4}
};

// Varje monster har samma attributes, men de är medvetet satta till ett betydligt lägre värde för att göra spelet lättare och mer rättvist
Monsters[] Monsters = {
    new() {Name = "Orc", Hp = 10, Agility = 2, Attack = 3},
    new() {Name = "Skelett", Hp = 7, Agility = 3, Attack = 2}
};

// genom en två-dimensionel array 
int[,] map = {
    {0, 0, 1, 0, 0, 0, 0, 0},
    {0, 0, 2, 0, 0, 0, 0, 0},
    {0, 3, 7, 8, 6, 0, 0, 0},
    {0, 2, 4, 6, 2, 0, 0, 0},
    {0, 4, 9, 7, 5, 0, 0, 0},
    {0, 0, 2, 2, 0, 0, 0, 0},
    {0, 0, 4, 10, 0, 0, 0, 0},
    {0, 0, 0, 2, 0, 0, 0, 0}
    };


// Jag valde en array för att innehållet inte kommer att ändras
string[] descriptions = { "Rummet ser öde ut, facklorna har slocknat sedan länge...", "Rummet är förvånansvärt rent, fast en ohygienisk stank lindrar i luften...", "Fukten i luften är noterbar, nästan lite störande..." };

// Samma gäller här som ovan
string[] roomtype = { "▓", "$", "║", "╔", "╚", "╝", "╗", "╬", "═", "╦", "╣" };

// Startposition för karaktären på kartan
int heroX = 7;
int heroY = 3;
bool walkarounddone = false;
int ROUNDS = 0;
int totalgold = 0;
bool insidedunegon = true;
bool treasurechamber = false;
int revivecount = 0;

bool success = false;
bool Mycharacterokay = false;
int Mycharacter = 0;
string thisheroname = "";
int thisherohp = 0;
int thisheroagi = 0;
int thisheroattack = 0;
bool canmeetmonster = false;
string path = "";
// Variabler slut



// Startsida start

// För ascii art
string logo = File.ReadAllText("ascii.txt");
string dragon = File.ReadAllText("dragon.txt");
string skeleton = File.ReadAllText("skeleton2.txt");
string phoenix = File.ReadAllText("phoenix.txt");
say(logo, false);
say("Välkommen till Drakborgen! Målet med detta spel är att navigera en karta för att nå skattkammaren högst upp som representeras av ett '$'.", false);
say("Tryck ENTER för att fortsätta", true);
Console.Clear();



// Skriver ut karaktärernas attributes


// Tar in input och kollar om valet är ok (siffra 0-2)

while (success == false || Mycharacterokay == false)
{
    say(logo, false);
    say("Välj din karaktär nedan, varje karaktär har ett bestämt hp och agility. Dessa kommer att spela roll i kommande strider.", false);
    say("siffrorna 0, 1 och 2 motsvarar en karaktär. Välj en av dem.", false);
    for (int i = 0; i < characters.Length; i++)
    {
        say(i + ": " + characters[i].Name + " Hp: " + characters[i].Hp + " Agility: " + characters[i].Agility, false);
    }
    string klass = Console.ReadLine();
    success = int.TryParse(klass, out Mycharacter);
    if (success)
    {

        if (Mycharacter < characters.Length && Mycharacter >= 0)
        {
            Mycharacterokay = true;
            thisheroname = characters[Mycharacter].Name;
            thisherohp = characters[Mycharacter].Hp;
            thisheroagi = characters[Mycharacter].Agility;
            thisheroattack = characters[Mycharacter].Attack;
            say("Du valde " + thisheroname + ".", false);
            say("Tryck ENTER för att börja spela.", false);
            Console.ReadLine();
        }
        else
        {
            say("Du får bara skriva siffror mellan 0-2!", false);
            say("Tryck ENTER för att försöka igen.", true);
        }

    }
    else
    {
        say("Du får bara skriva siffror mellan 0-2!", false);
        say("Tryck ENTER för att försöka igen.", true);
    }
    Console.Clear();

}
// Input koll slut

// Startsida slut


// walkaround start


// KOM IHÅG: NÄR MAN SKRIVER NÅGOT ANNAT NÄR MAN GÅR OMKRING KRASCHAR SPELET. FIXA!!



// Här börjar koden där man går omkring i själva slottet, spelets kod.
while (insidedunegon == true)
{
    while (walkarounddone == false)
    {
        Console.Clear();
        say(logo, false);
        // Ritar kartan
        DrawMap(map, heroX, heroY, roomtype);
        // beskriver rummet med en slumpmässig beskrivning som ligger i en array, beskrivningen bestäms av ett slumpat tal, talet som bestäms är lika med en plats i en array som innehåller en beskrivning.
        Random rnd = new Random();
        int describe = rnd.Next(0, descriptions.GetLength(0));
        say(descriptions[describe], false);



        int random = 0;
        if (canmeetmonster == true)
        {
            // TIPS, om du vill göra det lättare att möta monster så kan du ändra fyran till tex en tvåa. Då slumpas ett tal mellan 0-1 istället för 0-3.
            random = Random.Shared.Next(4);
        }

        // Om du inte mött ett monster så frågas du vilket håll du vill gå åt.
        if (random != 1)
        {
            say("Skriv vilket håll du vill gå åt: (up, down, left or right)", false);
        }

        // Om variablen är lika med 1 så slummpas ett monster fram. Talet ger återigen en plats i en array med mtvå monster och deras attributes
        if (random == 1)
        {
            int randommonster = Random.Shared.Next(0, Monsters.GetLength(0));
            string thismonstername = Monsters[randommonster].Name;
            // Dessa bestämmer monstrets attributes 
            int thismonsterhp = Monsters[randommonster].Hp;
            int thismonsteragi = Monsters[randommonster].Agility;
            int thismonsterattack = Monsters[randommonster].Attack;

            say("Du mötte " + Monsters[randommonster].Name + "!", false);
            say("Tryck på ENTER för att gå in i strid.", false);

            Console.ReadLine();
            Console.Clear();

            bool fightend = false;

            // Denna while loop innefattar fight-sekvensen
            while (fightend == false)
            {
                Console.Clear();
                say(logo, false);
                if (thismonstername == "Skelett")
                {
                    say(skeleton, false);
                }
                string action = "";

                say("Vad vill du göra? (Skriv 'Attack' för att försöka göra skada på monstret, eller skriv 'Flee' för att försöka fly.)", false);
                say("Du har " + thisherohp + " HP och " + thismonstername + " har " + thismonsterhp + " HP!", false);
                action = Console.ReadLine();
                // Action motsvarar ditt val, väljer du att attackera eller fly?
                switch (action)
                {
                    case "Attack":

                        // 50/50 om du träffar motståndaren eller inte
                        int hitchance = rnd.Next(0, 1 + thismonsteragi);
                        if (hitchance < 2)
                        {

                            thismonsterhp -= thisheroattack;
                            say("Du gjorde " + thisheroattack + " skada!", false);
                            say("Tryck ENTER för att fortsätta striden.", false);
                            Console.ReadLine();
                        }
                        if (hitchance >= 2)
                        {
                            say("Du missade din attack!", false);
                            say("Tryck ENTER för att fortsätta striden.", false);
                            Console.ReadLine();

                        }
                        if (thismonsterhp <= 0)
                        {
                            canmeetmonster = false;
                            thismonsterhp = 0;
                            say("Monstret dog!", false);
                            say("Tryck ENTER för att återgå till att röra dig fritt i slottet igen.", false);
                            fightend = true;
                        }
                        // Så länge monstret lever så har den en chans att slå tillbaks
                        if (thismonsterhp > 0)
                        {
                            // Räknar ut nytt Hero-HP efter fienden slagit tillbaks.
                            thisherohp = monsterattack(thisheroagi, thisherohp, thismonsterattack, thismonstername);
                        }
                        // Om du dör:
                        if (thisherohp < 0)
                        {
                            say(logo, false);
                            say("Du dog!", false);
                            fightend = true;
                            walkarounddone = true;
                            insidedunegon = false;
                            say("Tryck på ENTER för att avsluta:", false);
                        }
                        break;

                    // Om du försöker fly
                    case "Flee":
                        int fleechance = rnd.Next(0, thismonsteragi);
                        if (fleechance == 0)
                        {
                            say("Du lyckades fly!", false);
                            canmeetmonster = false;
                            fightend = true;
                        }
                        else
                        {
                            say("Din flykt misslyckades!", false);
                            // Monster attack nedan: Finns till för att monstret ska kunna attackera när du försöker att fly
                            thisherohp = monsterattack(thisheroagi, thisherohp, thismonsterattack, thismonstername);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        path = Console.ReadLine();

        // Olika "utfall", vad som ska hända om "path" är up, down, left eller right
        switch (path)
        {
            case "up":
                // int next = map[heroX - 1, heroY];
                if (map[heroX - 1, heroY] != 0 && map[heroX - 1, heroY] != 8 && map[heroX - 1, heroY] != 4)
                {
                    heroX--;
                    canmeetmonster = true;
                    say("Du gick upp.", false);
                    say("Tryck ENTER för att gå in i rummet.", true);
                }
                else
                {
                    say("Du kan inte gå åt det hållet, testa ett annat!", true);
                }
                break;
            case "left":

                // next = map[heroX, heroY - 1];
                if (map[heroX, heroY - 1] != 0 && map[heroX, heroY - 1] != 6 && map[heroX, heroY - 1] != 2)
                {
                    heroY--;
                    canmeetmonster = true;
                    say("Du gick till vänster.", false);
                    say("Tryck ENTER för att gå in i rummet.", true);
                }
                else
                {
                    say("Du kan inte gå åt det hållet, testa ett annat!", true);
                }
                break;
            case "right":
                if (map[heroX, heroY + 1] != 0 && map[heroX, heroY + 1] != 4 && map[heroX, heroY + 1] != 2)
                {
                    heroY++;
                    canmeetmonster = true;
                    say("Du gick till höger.", false);
                    say("Tryck ENTER för att gå in i rummet.", true);
                }
                else
                {
                    say("Du kan inte gå åt det hållet, testa ett annat!", true);
                }
                break;
            case "down":
                // För att koden inte ska krascha om man skriver down i början så existerar "(heroX < map.GetLength(1)-1"
                if (heroX < map.GetLength(1) - 1 && map[heroX + 1, heroY] != 0 && map[heroX + 1, heroY] != 6 && map[heroX + 1, heroY] != 9)
                {
                    heroX++;
                    canmeetmonster = true;
                    say("Du gick nedåt.", false);
                    say("Tryck ENTER för att gå in i rummet.", true);
                }
                else
                {
                    say("Du kan inte gå åt det hållet, testa ett annat!", false);
                    say("Tryck ENTER för att försöka igen.", true);
                }
                break;
            // Om man inte orkar gå manuellt till skattkammaren så kan man skriva in "skip" som input in i stringen "path" för att direkt nå slutet
            case "skip":
                heroX = 0;
                heroY = 2;
                break;
            // Om man vill bege sig ut ur slottet så kan man skriva in "exit" vid kordinaten där man börjar för att kliva ur slottet och avsluta spelet
            case "exit":
                if (heroX == 7 && heroY == 3)
                {
                    walkarounddone = true;
                    insidedunegon = false;
                    Console.Clear();
                    say("Tryck på RETURN för att avsluta", true);
                }
                break;
            default:
                break;
        }

        // Kollar om man befinner sig på skattkammarens kordinater, isåfall så går man in i skattkammaren
        if (heroX == 0 && heroY == 2)
        {
            treasurechamber = true;
            walkarounddone = true;
            canmeetmonster = false;
            Console.Clear();
        }

        // Koden för självaste skattkammaren

        Console.Clear();
        int hasshowedintro =0;
        while (treasurechamber == true)
        {
            if (hasshowedintro == 0)
            {
                say("Välkommen till skattkammaren. Här kommer du att dra så kallade skattkort i hopp om att samla på dig rikedommar.", false);
                say("Dock bör du vara varsam. En drake sover i närheten, och med varje skattkort som dras ökar chansen att draken vaknar och dödar dig!", false);
                say("Tryck på ENTER för att börja dra skattkort.", true);
                hasshowedintro++;
            }
            say("Tryck 1 för att dra ett skatt-kort, tryck 2 för att gå ut ur skattkammaren.", false);
            string s = Console.ReadLine();

            bool lyckad = int.TryParse(s, out int i);
            if (i == 1)
            {
                int alive = Drakkort(ROUNDS, 12);

                if (alive != 0)
                {
                    // Lägger ihop totala värdet av allt du snattat åt dig, värdet sparas även om du går in och ut ur skattkammaren genom att klicka 2
                    totalgold += Skattkort();
                    say("Det du samlat på dig har ett värd på totalt " + totalgold + " coins", false);
                    ROUNDS++;
                    say("Du har varit i skattkammaren i " + ROUNDS + " rundor.", false);
                    say("Tryck på ENTER för att fortsätta.", true);
                    if (ROUNDS == 11)
                    {
                        // Liten rekommendation, fast det händer så sällan att det nästan bör räknas som ett easter egg
                        say("Jag skulle gå ut om jag vore du...", false);
                    }
                    Console.Clear();
                }
                if (alive == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Du dog, draken vaknade!");
                    Console.WriteLine(dragon);
                    say("Tryck på RETURN för att avsluta", false);
                    Console.ReadLine();
                    // Detta förhindrar fenix-fenomenet att hända mer än 1 gång
                    if (revivecount == 0)
                    {
                        int revive = Random.Shared.Next(0, 10);
                        if (revive == 9)
                        {
                            Console.Clear();
                            say("...", false);
                            Console.ReadLine();
                            say("...", false);
                            Console.ReadLine();
                            say(phoenix, false);
                            say("En fågel Fenix återupplivar dig! Du har en ny chans, slösa inte bort den.", false);
                            say("Tryck ENTER för att fortsätta", false);
                            ROUNDS = 0;
                            Console.ReadLine();
                            Console.Clear();
                            revivecount++;
                        }
                        else
                        {
                            treasurechamber = false;
                            insidedunegon = false;
                        }
                    }
                    else
                    {
                        treasurechamber = false;
                        insidedunegon = false;
                    }
                }
            }
            if (i == 2)
            {
                Console.Clear();
                walkarounddone = false;
                heroX = 1;
                heroY = 2;
                treasurechamber = false;
            }
        }
    }
}



// Walkaround slut



// Metoder början

static int monsterattack(int thisheroagi, int thisherohp, int thismonsterattack, string thismonstername)
{
    Random rnd = new Random();
    int enemyhitchance = rnd.Next(0, 2 + thisheroagi);
    if (enemyhitchance < 2)
    {
        thisherohp -= thismonsterattack;
        say("" + thismonstername + " gjorde " + thismonsterattack + " skada på dig!", false);
        say("Tryck ENTER för att fortsätta striden.", false);
        Console.ReadLine();
        Console.Clear();
    }
    else
    {
        say("Monstret träffar dig inte.", false);
        say("Tryck ENTER för att fortsätta striden.", false);
        Console.ReadLine();
        Console.Clear();
    }
    return thisherohp;

}

static void say(string what2say, bool shouldIwait)
{
    Console.WriteLine(what2say);
    if (shouldIwait)
    {
        Console.ReadLine();
    }

}

static void DrawMap(int[,] map, int heroX, int heroY, string[] roomtype)
{
    int slask = 0;
    for (int x = 0; x < map.GetLength(1); x++)
    {
        say("", false);
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

static int Skattkort()
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
    say("Du hittade " + thisthing + " till ett värde av " + thisvalue + " coins.", false);
    return thisvalue;
}

static int Drakkort(int Rundor, int totalcards)
{
    int Alive;
    int cardsleft = totalcards - Rundor;
    int i = Random.Shared.Next(1, cardsleft);
    if (i == 1)
    {
        // Draken vaknar
        Alive = 0;
    }
    else
    {
        say("Draken fortsätter att sova.", false);
        Alive = 1;
    }
    return Alive;
}

// Metoder slut


// Klasser början

class Character
{
    public string Name;
    public int Hp;
    public int Agility;
    public int Attack;
}

class Monsters
{
    public string Name;
    public int Hp;
    public int Agility;
    public int Attack;
}

// Klasser slut