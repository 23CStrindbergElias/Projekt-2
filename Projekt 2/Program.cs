// Pseudo kod för drakborgen (Draken och skattkammaren)
// Börja med att dra ett kort från skatt-korten
// Därefter får man reda på vilket föremål man drog samt värdet på föremålet i valutan "coins"
// Sedan ska ett kort dras från en annan hög, som då till en början innehåller 12 kort, där en av dessa kort är draken
// Om du drar draken så dör du och du tappar alla värdeföremål, du förlorar alltså
// Om du lyckas undvika att väcka draken så ska ett kort dras från den här högen, det kan inte vara kortet där draken vaknar
// Efter varje runda så ska man ha möjligheten att bege sig ut från skattkammaren och så får man reda på värdet av alla sin skatter


// Pesudo kod för vägen till skattkammaren
// Börja med att välja din karaktär
// Sedan har man möjligheten att gå till höger, vänster eller frammåt
// I varje rum ska det finnas en viss chans att det spawnas ett monster, kanske ett skelett eller en orc






// Variabler start

// Varje karaktär har ett bestämt värde för: HP, Agility och Attack.
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

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
int Mycharacter = 0;
Character myHero = new();
bool canmeetmonster = false;
string path = "";
bool fightend = true;
int makeitwork;


int x_position;
int Y_position;
// Variabler slut



// Startsida start

// För ascii art
string logo = File.ReadAllText("ascii.txt");
string dragon = File.ReadAllText("dragon.txt");
string skeleton = File.ReadAllText("skeleton2.txt");
string phoenix = File.ReadAllText("phoenix.txt");
Toolbox.Say(logo, false);
Toolbox.Say("Välkommen till Drakborgen! Målet med detta spel är att navigera en karta för att nå skattkammaren högst upp som representeras av ett '$'.", false);
Toolbox.Say("Tryck ENTER för att fortsätta", true);
Console.Clear();

myHero = Toolbox.Choosecharacter(characters, logo);


// walkaround start

// Här börjar koden där man går omkring i själva slottet, spelets kod.
while (insidedunegon == true)
{
    while (walkarounddone == false)
    {
        Console.Clear();
        Toolbox.Say(logo, false);
        // Ritar kartan
        Toolbox.DrawMap(map, heroX, heroY, roomtype);
        // beskriver rummet med en slumpmässig beskrivning som ligger i en array, beskrivningen bestäms av ett slumpat tal, talet som bestäms är lika med en plats i en array som innehåller en beskrivning.
        Random rnd = new Random();
        int describe = rnd.Next(0, descriptions.GetLength(0));
        Toolbox.Say(descriptions[describe], false);

        int random = 0;
        if (canmeetmonster == true)
        {
            // TIPS, om du vill göra det lättare att möta monster så kan du ändra fyran till tex en tvåa. Då slumpas ett tal mellan 0-1 istället för 0-3.
            random = Random.Shared.Next(4);
        }

        // Om du inte mött ett monster så frågas du vilket håll du vill gå åt.
        if (random != 1)
        {
            Toolbox.Say("Skriv vilket håll du vill gå åt: (up, down, left or right)", false);
        }



        // Om variablen är lika med 1 så slummpas ett monster fram. Talet ger återigen en plats i en array med mtvå monster och deras attributes
        if (random == 1)
        {

            (Monsters thisMonster, fightend) = Toolbox.meetmonster(Monsters, fightend);

            // Denna while loop innefattar fight-sekvensen
            while (fightend == false)
            {
                Console.Clear();
                Toolbox.Say(logo, false);
                if (thisMonster.Name == "Skelett")
                {
                    Toolbox.Say(skeleton, false);
                }
                string action = "";

                Toolbox.Say("Vad vill du göra? (Skriv 'Attack' för att försöka göra skada på monstret, eller skriv 'Flee' för att försöka fly.)", false);
                Toolbox.Say("Du har " + myHero.Hp + " HP och " + thisMonster.Name + " har " + thisMonster.Hp + " HP!", false);
                action = Console.ReadLine();
                // Action motsvarar ditt val, väljer du att attackera eller fly?
                switch (action)
                {
                    case "Attack":

                        // När du attackerar ska även monstret kunna slå tillbaka
                        (thisMonster.Hp, canmeetmonster, fightend, myHero.Hp) = Fight.Hello(myHero, thisMonster, canmeetmonster, fightend);

                        // Om du dör


                        break;
                    case "Flee":

                        // Om du försöker fly
                        (canmeetmonster, fightend, myHero.Hp) = Fight.Flee(myHero, thisMonster, canmeetmonster, fightend);



                        break;
                    default:
                        break;
                }
                if (myHero.Hp == 0)
                {
                    (fightend, walkarounddone, insidedunegon) = Fight.HeroDead(logo, fightend, walkarounddone, insidedunegon, myHero.Hp);
                }
            }
        }


        path = Console.ReadLine();

        // Olika "utfall", vad som ska hända om "path" är up, down, left eller right
        switch (path)
        {
            case "up":

                x_position = 1;
                Y_position = 0;
                makeitwork = 1;

                var vilkethåll = Toolbox.direction(map, heroX, heroY, canmeetmonster, x_position, Y_position, makeitwork);
                canmeetmonster = vilkethåll.Item1;
                heroX = vilkethåll.Item2;
                heroY = vilkethåll.Item3;
                map = vilkethåll.Item4;

                break;
            case "left":

                x_position = 0;
                Y_position = 1;
                makeitwork = 2;

                vilkethåll = Toolbox.direction(map, heroX, heroY, canmeetmonster, x_position, Y_position, makeitwork);
                canmeetmonster = vilkethåll.Item1;
                heroX = vilkethåll.Item2;
                heroY = vilkethåll.Item3;
                map = vilkethåll.Item4;

                break;
            case "right":

                x_position = 0;
                Y_position = 1;
                makeitwork = 3;

                vilkethåll = Toolbox.direction(map, heroX, heroY, canmeetmonster, x_position, Y_position, makeitwork);
                canmeetmonster = vilkethåll.Item1;
                heroX = vilkethåll.Item2;
                heroY = vilkethåll.Item3;
                map = vilkethåll.Item4;

                break;
            case "down":

                x_position = 1;
                Y_position = 0;
                makeitwork = 4;

                vilkethåll = Toolbox.direction(map, heroX, heroY, canmeetmonster, x_position, Y_position, makeitwork);
                canmeetmonster = vilkethåll.Item1;
                heroX = vilkethåll.Item2;
                heroY = vilkethåll.Item3;
                map = vilkethåll.Item4;

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
                    Toolbox.Say("Du fick totalt " + totalgold + " värt av coins!", false);
                    Toolbox.Say("Tryck på RETURN för att avsluta", true);
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
        int hasshowedintro = 0;
        int cardsleft = 12;
        while (treasurechamber == true)
        {
            if (hasshowedintro == 0)
            {
                Toolbox.Say("Välkommen till skattkammaren. Här kommer du att dra så kallade skattkort i hopp om att samla på dig rikedommar.", false);
                Toolbox.Say("Dock bör du vara varsam. En drake sover i närheten, och med varje skattkort som dras ökar chansen att draken vaknar och dödar dig!", false);
                Toolbox.Say("Tryck på ENTER för att börja dra skattkort.", true);
                hasshowedintro++;

            }
            Toolbox.Say("Tryck 1 för att dra ett skatt-kort, tryck 2 för att gå ut ur skattkammaren.", false);
            string s = Console.ReadLine();

            bool lyckad = int.TryParse(s, out int i);
            if (i == 1)
            {
                var temp = Toolbox.Drakkort(ROUNDS, cardsleft);
                int Alive = temp.Item1;
                cardsleft = temp.Item2;

                totalgold += Toolbox.Skattkort(ROUNDS);

                temp = Toolbox.hero_alive(Alive, totalgold, ROUNDS);
                totalgold = temp.Item1;
                ROUNDS = temp.Item2;

                var temp2 = Toolbox.hero_dead(Alive, dragon, revivecount, phoenix, ROUNDS, treasurechamber, insidedunegon, walkarounddone);
                treasurechamber = temp2.Item1;
                insidedunegon = temp2.Item2;
                walkarounddone = temp2.Item3;
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


// Klasser början

public class Character
{
    public string Name;
    public int Hp;
    public int Agility;
    public int Attack;
}

public class Monsters
{
    public string Name;
    public int Hp;
    public int Agility;
    public int Attack;
}

// Klasser slut