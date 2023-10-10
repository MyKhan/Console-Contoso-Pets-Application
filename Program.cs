// the ourAnimals array will store the following: 
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

string animalSpecies = "";
string animalID = "";
string animalAge = "";
string animalPhysicalDescription = "";
string animalPersonalityDescription = "";
string animalNickname = "";
string suggestedDonation = "";

// variables that support data entry
int maxPets = 8;
string? readResult;
string menuSelection = "";
decimal decimalDonation = 0.00m;

// array used to store runtime data, there is no persisted data
string[,] ourAnimals = new string[maxPets, 7];

// TODO: Convert the if-elseif-else construct to a switch statement

// create some initial ourAnimals array entries
for (int i = 0; i < maxPets; i++)
{
    switch (i)
    {
        case 0:
            animalSpecies = "dog";
            animalID = "d1";
            animalAge = "2";
            animalPhysicalDescription = "medium sized cream colored female golden retriever weighing about 65 pounds. housebroken.";
            animalPersonalityDescription = "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.";
            animalNickname = "lola";
            suggestedDonation = "85.00";
            break;

        case 1:
            animalSpecies = "dog";
            animalID = "d2";
            animalAge = "9";
            animalPhysicalDescription = "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.";
            animalPersonalityDescription = "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.";
            animalNickname = "loki";
            suggestedDonation = "49.99";
            break;

        case 2:
            animalSpecies = "cat";
            animalID = "c3";
            animalAge = "1";
            animalPhysicalDescription = "small white female weighing about 8 pounds. litter box trained.";
            animalPersonalityDescription = "friendly";
            animalNickname = "Puss";
            suggestedDonation = "40.00";
            break;

        case 3:
            animalSpecies = "cat";
            animalID = "c4";
            animalAge = "?";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "";
            suggestedDonation = "";
            break;

        default:
            animalSpecies = "";
            animalID = "";
            animalAge = "";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "";
            suggestedDonation = "";
            break;
    }

    ourAnimals[i, 0] = "ID #: " + animalID;
    ourAnimals[i, 1] = "Species: " + animalSpecies;
    ourAnimals[i, 2] = "Age: " + animalAge;
    ourAnimals[i, 3] = "Nickname: " + animalNickname;
    ourAnimals[i, 4] = "Physical description: " + animalPhysicalDescription;
    ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;

    if (!decimal.TryParse(suggestedDonation, out decimalDonation))
    {
        decimalDonation = 45.00m; // if suggestedDonation NOT a number, default to 45.00
    }
    ourAnimals[i, 6] = $"Suggested Donation: {decimalDonation:C2}";
}

do
{
    // display the top-level menu options

    Console.Clear();

    Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
    Console.WriteLine(" 1. List all of our current pet information");
    Console.WriteLine(" 2. Add a new animal friend to the ourAnimals array");
    Console.WriteLine(" 3. Ensure animal ages and physical descriptions are complete");
    Console.WriteLine(" 4. Ensure animal nicknames and personality descriptions are complete");
    Console.WriteLine(" 5. Edit an animal’s age");
    Console.WriteLine(" 6. Edit an animal’s personality description");
    Console.WriteLine(" 7. Display all cats with a specified characteristic");
    Console.WriteLine(" 8. Display all dogs with a specified characteristic");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

    readResult = Console.ReadLine();

    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }

    //Console.WriteLine($"You selected menu option {menuSelection}.");
    //Console.WriteLine("Press the Enter key to continue");

    // pause code execution
    //readResult = Console.ReadLine();

    switch (menuSelection)
    {
        case "1":
            // List all of our current pet information

            for (int animal = 0; animal < maxPets; animal++)
            {
                if (ourAnimals[animal, 0] != "ID #: ")
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Console.WriteLine(ourAnimals[animal, j]);
                    }
                }
            }

            Console.WriteLine("Press the Enter key to continue.");
            readResult = Console.ReadLine();
            break;

        case "2":
            // Add a new animal friend to the ourAnimals array

            string anotherPet = "y";
            int petCount = 0;

            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 0] != "ID #: ")
                {
                    petCount++;
                }
            }

            if (petCount < maxPets)
                Console.WriteLine($"We currently have {petCount} pets that need homes. We can manage {(maxPets - petCount)} more.");

            while (anotherPet == "y" && petCount < maxPets)
            {
                bool validEntry = false;

                // get species (cat or dog) - string animalSpecies is a required field 
                do
                {
                    Console.WriteLine("\n\rEnter 'dog' or 'cat' to begin a new entry:");
                    readResult = Console.ReadLine();
                    if (readResult != null)
                    {
                        animalSpecies = readResult.ToLower();
                        if (animalSpecies != "dog" && animalSpecies != "cat")
                        {
                            validEntry = false;
                        }
                        else
                        {
                            validEntry = true;
                        }
                    }
                } while (validEntry == false);

                // build the animal the ID number - for example C1, C2, D3 (for Cat 1, Cat 2, Dog 3)
                animalID = animalSpecies.Substring(0, 1) + (petCount + 1).ToString();

                // get the pet's age. can be ? at initial entry. 
                do
                {
                    int petAge;
                    Console.WriteLine("Enter pet's age or enter ? if unknown");
                    readResult = Console.ReadLine();
                    if (readResult != null)
                    {
                        animalAge = readResult;
                        if (animalAge != "?")
                        {
                            validEntry = int.TryParse(animalAge, out petAge);
                        }
                        else
                        {
                            validEntry = true;
                        }
                    }
                } while (validEntry == false);

                // get a description of the pet's physical appearance/condition - animalPhysicalDescription can be blank.
                do
                {
                    Console.WriteLine("Enter a physical description of the pet (size, color, gender, weight, housebroken)");
                    readResult = Console.ReadLine();
                    if (readResult != null)
                    {
                        animalPhysicalDescription = readResult.ToLower();
                        if (animalPhysicalDescription == "")
                        {
                            animalPhysicalDescription = "tbd";
                        }
                    }
                } while (animalPhysicalDescription == "");

                // get a description of the pet's personality - animalPersonalityDescription can be blank.
                do
                {
                    Console.WriteLine("Enter a description of the pet's personality (likes or dislikes, tricks, energy level)");
                    readResult = Console.ReadLine();
                    if (readResult != null)
                    {
                        animalPersonalityDescription = readResult.ToLower();
                        if (animalPersonalityDescription == "")
                        {
                            animalPersonalityDescription = "tbd";
                        }
                    }
                } while (animalPersonalityDescription == "");

                // get the pet's nickname. animalNickname can be blank.
                do
                {
                    Console.WriteLine("Enter a nickname for the pet");
                    readResult = Console.ReadLine();
                    if (readResult != null)
                    {
                        animalNickname = readResult.ToLower();
                        if (animalNickname == "")
                        {
                            animalNickname = "tbd";
                        }
                    }
                } while (animalNickname == "");

                // store the pet information in the ourAnimals array (zero based)
                ourAnimals[petCount, 0] = "ID #: " + animalID;
                ourAnimals[petCount, 1] = "Species: " + animalSpecies;
                ourAnimals[petCount, 2] = "Age: " + animalAge;
                ourAnimals[petCount, 3] = "Nickname: " + animalNickname;
                ourAnimals[petCount, 4] = "Physical description: " + animalPhysicalDescription;
                ourAnimals[petCount, 5] = "Personality: " + animalPersonalityDescription;

                // increment petCount (the array is zero-based, so we increment the counter after adding to the array)
                petCount = petCount + 1;
                // check maxPet limit
                if (petCount < maxPets)
                {
                    // another pet?
                    Console.WriteLine("Do you want to enter info for another pet (y/n)");
                    do
                    {
                        readResult = Console.ReadLine();
                        if (readResult != null)
                        {
                            anotherPet = readResult.ToLower();
                        }
                    } while (anotherPet != "y" && anotherPet != "n");
                }
            }

            if (petCount >= maxPets)
            {
                Console.WriteLine("We have reached our limit on the number of pets that we can manage.");
                Console.WriteLine("Press the Enter key to continue.");
                readResult = Console.ReadLine();
            }

            break;

        case "3":
            // Ensure animal ages and physical descriptions are complete
            /* ourAnimals[petCount, 0] = "ID #: " + animalID;
            ourAnimals[petCount, 1] = "Species: " + animalSpecies;
            ourAnimals[petCount, 2] = "Age: " + animalAge;
            ourAnimals[petCount, 3] = "Nickname: " + animalNickname;
            ourAnimals[petCount, 4] = "Physical description: " + animalPhysicalDescription;
            ourAnimals[petCount, 5] = "Personality: " + animalPersonalityDescription; */

            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 0] != "ID #: ")
                {
                    //Fix Age
                    if (ourAnimals[i, 2] == "Age: ?")
                    {
                        bool validEntry = false;
                        do
                        {
                            Console.WriteLine($"Enter Age for {ourAnimals[i, 0]}");
                            readResult = Console.ReadLine();

                            if (readResult != "")
                            {
                                validEntry = int.TryParse(readResult, out int petAge);
                                if (validEntry)
                                {
                                    ourAnimals[i, 2] = "Age: " + petAge;
                                }
                            }
                        } while (!validEntry);
                    }

                    // Fix Physical Description
                    if (ourAnimals[i, 4] == "Physical description: tbd" || ourAnimals[i, 4] == "Physical description: ")
                    {
                        bool validEntry = false;
                        do
                        {
                            Console.WriteLine($"Enter Description] for {ourAnimals[i, 0]}");
                            readResult = Console.ReadLine();

                            if (readResult != null && readResult != "")
                            {
                                string petPhysicalDescription = readResult;
                                validEntry = true;
                                ourAnimals[i, 4] = "Physical description: " + petPhysicalDescription;
                            }
                        } while (!validEntry);
                    }
                }
            }


            Console.WriteLine("Press the Enter key to continue.");
            readResult = Console.ReadLine();
            break;

        case "4":
            // Ensure animal nicknames and personality descriptions are complete

            for (int i = 0; i < maxPets; i++)
            {

                //Fix Age
                if (ourAnimals[i, 0] != "ID #: ")
                {
                    if (ourAnimals[i, 3] == "Nickname: ")
                    {
                        bool validEntry = false;
                        do
                        {
                            Console.WriteLine($"Enter nickname for {ourAnimals[i, 0]}");
                            readResult = Console.ReadLine();
                            if (readResult != null && readResult != "")
                            {
                                animalNickname = readResult;
                                ourAnimals[i, 3] = "Nickname: " + animalNickname;
                                validEntry = true;
                            }
                        } while (!validEntry);
                    }
                }

                //Fix Personality Descriptions
                if (ourAnimals[i, 0] != "ID #: ")
                {
                    if (ourAnimals[i, 5] == "Personality: ")
                    {
                        bool validEntry = false;
                        do
                        {
                            Console.WriteLine($"Enter personality description for {ourAnimals[i, 0]}");
                            readResult = Console.ReadLine();
                            if (readResult != null && readResult != "")
                            {
                                animalPersonalityDescription = readResult;
                                ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;
                                validEntry = true;
                            }
                        } while (!validEntry);
                    }
                }
            }

            Console.WriteLine("Press the Enter key to continue.");
            readResult = Console.ReadLine();
            break;

        case "5":
            // Edit an animal’s age
            Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
            Console.WriteLine("Press the Enter key to continue.");
            readResult = Console.ReadLine();
            break;

        case "6":
            // Edit an animal’s personality description
            Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
            Console.WriteLine("Press the Enter key to continue.");
            readResult = Console.ReadLine();
            break;

        case "7":
            // Display all cats with a specified characteristic
            Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
            Console.WriteLine("Press the Enter key to continue.");
            readResult = Console.ReadLine();
            break;

        case "8":
            // Display all dogs with a multiple search characteristics
            string dogCharacteristic = "";

            while (dogCharacteristic == "")
            {
                // Have user enter multiple comma separated characteristics to search for
                Console.WriteLine($"\r\nEnter one or more desired dog characteristic to search for separated by commas");
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    dogCharacteristic = readResult.ToLower().Trim();
                    Console.WriteLine();
                }
            }
            
            string[] dogCharacteristics = dogCharacteristic.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(dogCharacteristics);
            dogCharacteristics = Array.ConvertAll(dogCharacteristics, d => d.ToLower());

            bool noMatchesDog = true;
            string dogDescription = "";
            
            // Update to "rotating" animation with countdown
            string[] searchingIcons = {"--", "\\ ", "| ", "/ "};

            // Loop ourAnimals array to search for matching animals
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 1].Contains("dog"))
                {
                    // Search combined descriptions and report results
                    dogDescription = ourAnimals[i, 4] + "\r\n" + ourAnimals[i, 5];
                    
                    // Searching Animation loop
                    for (int j = 2; j > -1 ; j--)
                    {
                    // Update "searching" message to show countdown 
                        foreach (string icon in searchingIcons)
                        {
                            Console.Write($"\rsearching our dog {ourAnimals[i, 3]} for {dogCharacteristic} {icon} {j}");
                            Thread.Sleep(250);
                        }
                        
                        Console.Write($"\r{new String(' ', Console.BufferWidth)}");
                    }
                    
                    // Console.WriteLine(string.Join("\n", dogCharacteristics));

                    // Iterate submitted characteristic terms and search description for each term

                    bool currentDogMatch = false;
                    for (int j = 0; j < dogCharacteristics.Length; j++) {
                        if (dogDescription.Contains(dogCharacteristics[j]))
                        {
                            // #3b update message to reflect term
                            Console.WriteLine($"Searching {dogCharacteristics[j]}"); 
                            // #3c set a flag "this dog" is a match
                            currentDogMatch = true;
                            noMatchesDog = false;
                            Console.WriteLine($"\nOur dog {ourAnimals[i, 3]} is a match for your search for {dogCharacteristics[j]}!");
                        }
                    }

                    // If "this dog" is match write match message + dog description
                    if (currentDogMatch) {
                        Console.WriteLine($"{ourAnimals[i,3]} ({ourAnimals[i,0]})");
                        Console.WriteLine($"{dogDescription}");
                    }

                }
            }

            if (noMatchesDog)
            {
                Console.WriteLine("None of our dogs are a match found for: " + dogCharacteristic);
            }

            Console.WriteLine("\n\rPress the Enter key to continue");
            readResult = Console.ReadLine();

            break;

        default:
            // Nothing
            Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
            Console.WriteLine("Press the Enter key to continue.");
            readResult = Console.ReadLine();
            break;
    }

} while (menuSelection != "exit");