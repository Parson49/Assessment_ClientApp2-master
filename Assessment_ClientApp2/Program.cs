using System;
using System.Collections.Generic;
using System.IO;

namespace Assessment_ClientApp2
{

    // **************************************************
    //
    // Assessment: Client App 2.0
    // Author: Anna Parsons
    // Dated: 11/25/19
    // Level (Novice, Apprentice, or Master): 
    //
    // **************************************************    

    class Program
    {
        /// <summary>
        /// Main method - app starts here
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //
            // initialize monster list from method
            //
            //List<Monster> monsters = InitializeMonsterList();
            List<Monster> monsters = ReadFromDataFile();

            //
            // read monsters from data file
            //
            //List<Monster> monsters = ReadFromDataFile();

            //
            // application flow
            //
            DisplayWelcomeScreen();
            DisplayMenuScreen(monsters);
            DisplayClosingScreen();
        }

        #region UTILITY METHODS

        /// <summary>
        /// initialize a list of monsters
        /// </summary>
        /// <returns>list of monsters</returns>
        //static List<Monster> InitializeMonsterList()
        //{
        //    //
        //    // create a list of monsters
        //    // note: list and object initializers used
        //    //
        //    List<Monster> monsters = new List<Monster>()
        //    {

        //        new Monster()
        //        {
        //            Name = "Sid",
        //            Age = 145,
        //            Attitude = Monster.EmotionalState.happy
        //        },

        //        new Monster()
        //        {
        //            Name = "Lucy",
        //            Age = 125,
        //            Attitude = Monster.EmotionalState.bored
        //        },

        //        new Monster()
        //        {
        //            Name = "Bill",
        //            Age = 934,
        //            Attitude = Monster.EmotionalState.sad
        //        },

        //        new Monster()
        //        {
        //            Name = "Wuxian",
        //            Age = 900,
        //            Attitude = Monster.EmotionalState.angry
        //        },

        //        new Monster()
        //        {
        //            Name = "Xichen",
        //            Age = 145,
        //            Attitude = Monster.EmotionalState.none
        //        }

        //    };

        //    return monsters;
        //}

        //static List<Monster> ReadMonsterList()
        //{
        //    List<Monster> monsters = new List<Monster>();

        //    //
        //    // Read all monsters into string array
        //    //
        //    string[] monstersString = File.ReadAllLines("Data\\Data.txt");

        //    //
        //    // Create monster list
        //    //
        //    string[] monsterProperties;
        //    foreach (string monster in monstersString)
        //    {
        //        monsterProperties = monster.Split(',');

        //        Monster newMonster = new Monster();

        //        newMonster.Name = monsterProperties[0];

        //        int.TryParse(monsterProperties[1], out int age);
        //        newMonster.Age = age;

        //        Enum.TryParse(monsterProperties[2], out Monster.EmotionalState attitude);
        //        newMonster.Attitude = attitude;

        //        Enum.TryParse(monsterProperties[3], out Monster.MonsterTribe tribe);
        //        newMonster.Tribe = tribe;

        //        bool.TryParse(monsterProperties[4], out bool active);
        //        newMonster.Active = active;

        //        DateTime.TryParse(monsterProperties[5], out DateTime birth);
        //        newMonster.Birth = birth;

        //        monsters.Add(newMonster);
        //    }

        //    return monsters;
        //}

        #endregion

        #region SCREEN DISPLAY METHODS

        /// <summary>
        /// SCREEN: display and process menu options
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayMenuScreen(List<Monster> monsters)
        {
            bool quitApplication = false;
            char menuChoice;
            ConsoleKeyInfo menuChoiceKey;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) List All Monsters");
                Console.WriteLine("\tb) View Monster Detail");
                Console.WriteLine("\tc) Add Monster");
                Console.WriteLine("\td) Delete Monster");
                Console.WriteLine("\te) Update Monster");
                Console.WriteLine("\tf) Write to Data File");
                Console.WriteLine("\tg) Filter Monsters");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice: ");
                menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case 'a':
                        DisplayAllMonsters(monsters);
                        break;

                    case 'b':
                        DisplayViewMonsterDetail(monsters);
                        break;

                    case 'c':
                        DisplayAddMonster(monsters);
                        break;

                    case 'd':
                        DisplayDeleteMonster(monsters);
                        break;

                    case 'e':
                        DisplayUpdateMonster(monsters);
                        break;

                    case 'f':
                        DisplayWriteToDataFile(monsters);
                        break;

                    case 'g':
                        DisplayFilterByTemperment(monsters);
                        break;

                    case 'q':
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a letter for the menu choice: ");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitApplication);
        }

        /// <summary>
        /// SCREEN: list all monsters
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAllMonsters(List<Monster> monsters)
        {
            DisplayScreenHeader("All Monsters");

            //Console.WriteLine("\t***************************");
            Console.WriteLine();
            MonsterInfoTable();
            foreach (Monster monster in monsters)
            {
                MonsterInfo(monster);
                Console.WriteLine();
                //Console.WriteLine("\t***************************");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: monster detail
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayViewMonsterDetail(List<Monster> monsters)
        {
            DisplayScreenHeader("Monster Detail");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine();
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name: ");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // display monster detail
            //
            Console.WriteLine();
            Console.WriteLine("\t*********************");
            MonsterInfoTable();
            MonsterInfo(selectedMonster);
            Console.WriteLine("\t*********************");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: add a monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAddMonster(List<Monster> monsters)
        {
            Monster newMonster = new Monster();
            bool validInput = false;
            string userResponse;

            //
            // add monster object property values
            //
            while (!validInput)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine();
                Console.WriteLine("\tWhat is the desired name for your monster? ");
                newMonster.Name = Console.ReadLine();
                Console.WriteLine($"\tIs {newMonster.Name} correct? [yes, no] ");
                userResponse = Console.ReadLine().ToLower();
                if (userResponse == "yes")
                {
                    validInput = true;
                    DisplayContinuePrompt();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease input the correct name for your monster");
                    Console.Clear();
                }
            }

            validInput = false;
            while (!validInput)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine();
                Console.WriteLine("\tWhat is the desired age for your monster? ");
                userResponse = Console.ReadLine();

                if (int.TryParse(userResponse, out int age))
                {
                    if (age >= 0)
                    {
                        newMonster.Age = age;
                        Console.WriteLine();
                        Console.WriteLine($"\tIs {newMonster.Age} correct? [yes, no] ");
                        userResponse = Console.ReadLine().ToLower();

                        if (userResponse == "yes")
                        {
                            validInput = true;
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\tPlease input the correct age for your monster");
                            DisplayContinuePrompt();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tMonster's age must be greater than or equal to 0");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("\tInvalid Response. Please enter a valid integer.");
                    DisplayContinuePrompt();
                    Console.Clear();
                }
            }

            validInput = false;
            while (!validInput)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine();
                Console.WriteLine("\tWhat is the desired attitude for your monster? [none, happy, sad, angry, bored] ");
                userResponse = Console.ReadLine();

                if (Enum.TryParse(userResponse, out Monster.EmotionalState attitude))
                {
                    newMonster.Attitude = attitude;
                    Console.WriteLine();
                    Console.WriteLine($"\tIs {newMonster.Attitude} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();

                    if (userResponse == "yes")
                    {
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct attitude for your monster");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("\tInvalid Response. Please enter a valid attitude.");
                    DisplayContinuePrompt();
                    Console.Clear();
                }
            }

            validInput = false;
            while (!validInput)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine();
                Console.WriteLine("\tWhat tribe is your monster from? [undead, amazonian, elf, orc] ");
                userResponse = Console.ReadLine();

                if (Enum.TryParse(userResponse, out Monster.MonsterTribe tribe))
                {
                    newMonster.Tribe = tribe;
                    Console.WriteLine();
                    Console.WriteLine($"\tIs {newMonster.Tribe} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();

                    if (userResponse == "yes")
                    {
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct tribe of your monster");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("\tInvalid Response. Please enter a valid tribe.");
                    DisplayContinuePrompt();
                    Console.Clear();
                }
            }

            validInput = false;
            while (!validInput)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine();
                Console.WriteLine("\tIs your monster active? [true, false] ");
                userResponse = Console.ReadLine();

                if (bool.TryParse(userResponse, out bool active))
                {
                    newMonster.Active = active;
                    Console.WriteLine();
                    Console.WriteLine($"\tIs {newMonster.Active} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();

                    if (userResponse == "yes")
                    {
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct status of your monster");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("\tInvalid Response. Please enter a valid status.");
                    DisplayContinuePrompt();
                    Console.Clear();
                }
            }

            validInput = false;
            while (!validInput)
            {
                DisplayScreenHeader("Add Monster");
                Console.WriteLine();
                Console.WriteLine("\tWhen was your monster born? [mm/dd/yyyy 00:00:00] ");
                userResponse = Console.ReadLine();

                if (DateTime.TryParse(userResponse, out DateTime birth))
                {
                    newMonster.Birth = birth;
                    Console.WriteLine();
                    Console.WriteLine($"\tIs {newMonster.Birth} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();

                    if (userResponse == "yes")
                    {
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct birthdate of your monster");
                        DisplayContinuePrompt();
                        Console.Clear();

                    }
                }
                else
                {
                    Console.WriteLine("\tInvalid Response. Please enter a valid date.");
                    DisplayContinuePrompt();
                    Console.Clear();
                }
            }

            monsters.Add(newMonster);

            //
            // echo new monster properties
            //
            Console.WriteLine();
            Console.WriteLine("\tNew Monster's Properties");
            Console.WriteLine();
            Console.WriteLine("\t*************");
            Console.WriteLine();
            MonsterInfoTable();
            MonsterInfo(newMonster);
            Console.WriteLine();
            Console.WriteLine("\t*************");

            DisplayContinuePrompt();

            monsters.Add(newMonster);
        }

        /// <summary>
        /// SCREEN: delete monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayDeleteMonster(List<Monster> monsters)
        {
            DisplayScreenHeader("Delete Monster");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // delete monster
            //
            if (selectedMonster != null)
            {
                monsters.Remove(selectedMonster);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedMonster.Name} deleted");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{monsterName} not found");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: update monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        /// 
        static void DisplayUpdateMonster(List<Monster> monsters)
        {
            bool validResponse = false;
            Monster selectedMonster = null;

            do
            {
                DisplayScreenHeader("Update Monster");

                //
                // display all monster names
                //
                Console.WriteLine("\tMonster Names");
                Console.WriteLine("\t-------------");
                foreach (Monster monster in monsters)
                {
                    Console.WriteLine("\t" + monster.Name);
                }

                //
                // get user monster choice
                //
                Console.WriteLine();
                Console.Write("\tEnter name: ");
                string monsterName = Console.ReadLine();

                //
                // get monster object
                //
                foreach (Monster monster in monsters)
                {
                    if (monster.Name == monsterName)
                    {
                        selectedMonster = monster;
                        validResponse = true;
                        break;
                    }
                }

                //
                // feedback for wrong name choice
                //
                if (!validResponse)
                {
                    Console.WriteLine("\tPlease select an existing monster.");
                    DisplayContinuePrompt();
                }

                //
                // update monster
                //

            } while (!validResponse);


            //
            // update monster properties
            //
            bool validInput = false;
            string userResponse;
            string name;
            while (!validInput)
            {
                DisplayScreenHeader("Update Monster Name");
                Console.WriteLine();
                Console.WriteLine("\tReady to update. Press the Enter to keep the current info.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Name: {selectedMonster.Name}");
                Console.WriteLine();
                Console.Write("\tNew Name: ");
                name = Console.ReadLine();
                if (name != "")
                {
                    Console.WriteLine($"\tIs {name} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        selectedMonster.Name = name;
                        Console.WriteLine($"\tMonster's name changed to {selectedMonster.Name}");
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct name for your monster");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine($"\tIs {selectedMonster.Name} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct name for your monster");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
            }

            validInput = false;
            int monsterAge;
            while (!validInput)
            {
                DisplayScreenHeader("Update Monster Age");
                Console.WriteLine();
                Console.WriteLine("\tReady to update. Press the Enter to keep the current info.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Age: {selectedMonster.Age}");
                Console.WriteLine();
                Console.Write("\tNew Age: "); 
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    if (int.TryParse(userResponse, out int age))
                    {
                        if (age >= 0)
                        {
                            monsterAge = age;
                            Console.WriteLine();
                            Console.WriteLine($"\tIs {monsterAge} correct? [yes, no] ");
                            userResponse = Console.ReadLine().ToLower();

                            if (userResponse == "yes")
                            {
                                selectedMonster.Age = monsterAge;
                                validInput = true;
                                DisplayContinuePrompt();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("\tPlease input the correct age for your monster");
                                DisplayContinuePrompt();
                                Console.Clear();
                            }
                        }
                        else
                        {
                            Console.WriteLine("\tMonster's age must be greater than or equal to 0");
                            DisplayContinuePrompt();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tInvalid Response. Please enter a valid integer.");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine($"\tIs {selectedMonster.Age} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct age for your monster");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
            }

            validInput = false;
            string temperment;
            while (!validInput)
            {
                DisplayScreenHeader("Update Monster Attitude");
                Console.WriteLine();
                Console.WriteLine("\tReady to update. Press the Enter to keep the current info.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Attitude: {selectedMonster.Attitude}");
                Console.WriteLine();
                Console.Write("\tNew Attitude [none, happy, sad, angry, bored]: ");
                temperment = Console.ReadLine();
                if (temperment != "")
                {
                    if (Enum.TryParse(temperment, out Monster.EmotionalState attitude))
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tIs {temperment} correct? [yes, no] ");
                        userResponse = Console.ReadLine().ToLower();

                        if (userResponse == "yes")
                        {
                            selectedMonster.Attitude = attitude;
                            validInput = true;
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\tPlease input the correct attitude for your monster");
                            DisplayContinuePrompt();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tInvalid Response. Please enter a valid attitude.");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine($"\tIs {selectedMonster.Attitude} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct attitude for your monster");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
            }

            validInput = false;
            string monsterTribe;
            while (!validInput)
            {
                DisplayScreenHeader("Update Monster Tribe");
                Console.WriteLine();
                Console.WriteLine("\tReady to update. Press the Enter to keep the current info.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Tribe: {selectedMonster.Tribe}");
                Console.WriteLine();
                Console.Write("\tNew Tribe [undead, amazonian, elf, orc]: ");
                monsterTribe = Console.ReadLine();
                if (monsterTribe != "")
                {
                    if (Enum.TryParse(monsterTribe, out Monster.MonsterTribe tribe))
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tIs {monsterTribe} correct? [yes, no] ");
                        userResponse = Console.ReadLine().ToLower();

                        if (userResponse == "yes")
                        {
                            selectedMonster.Tribe = tribe;
                            validInput = true;
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\tPlease input the correct tribe of your monster");
                            DisplayContinuePrompt();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tInvalid Response. Please enter a valid tribe.");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine($"\tIs {selectedMonster.Tribe} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct tribe of your monster");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
            }

            validInput = false;
            while (!validInput)
            {
                DisplayScreenHeader("Update Monster Activity Status");
                Console.WriteLine();
                Console.WriteLine("\tReady to update. Press the Enter to keep the current info.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Status: {selectedMonster.Active}");
                Console.WriteLine();
                Console.Write("\tIs monster active? [true, false]: ");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    if (bool.TryParse(userResponse, out bool active))
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tIs {userResponse} correct? [yes, no] ");
                        userResponse = Console.ReadLine().ToLower();

                        if (userResponse == "yes")
                        {
                            selectedMonster.Active = active;
                            validInput = true;
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\tPlease input the correct status of your monster");
                            DisplayContinuePrompt();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tInvalid Response. Please enter a valid status.");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine($"\tIs {selectedMonster.Active} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct status for your monster");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
            }

            validInput = false;
            while (!validInput)
            {
                DisplayScreenHeader("Update Monster Birthdate");
                Console.WriteLine();
                Console.WriteLine("\tReady to update. Press the Enter to keep the current info.");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent Birthdate: {selectedMonster.Birth}");
                Console.WriteLine();
                Console.Write("\tNew Birthdate [mm/dd/yyyy 00:00:00]: ");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    if (DateTime.TryParse(userResponse, out DateTime birth))
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tIs {userResponse} correct? [yes, no] ");
                        userResponse = Console.ReadLine().ToLower();

                        if (userResponse == "yes")
                        {
                            selectedMonster.Birth = birth;
                            validInput = true;
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\tPlease input the correct birthdate of your monster");
                            DisplayContinuePrompt();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tInvalid Response. Please enter a valid date.");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine($"\tIs {selectedMonster.Birth} correct? [yes, no] ");
                    userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "yes")
                    {
                        validInput = true;
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease input the correct birthdate for your monster");
                        DisplayContinuePrompt();
                        Console.Clear();
                    }
                }
            }

            //
            // echo updated monster properties
            //
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\tMonster's Updated Properties");
            Console.WriteLine();
            Console.WriteLine("\t-------------");
            Console.WriteLine();
            MonsterInfoTable();
            MonsterInfo(selectedMonster);
            Console.WriteLine();
            Console.WriteLine("\t-------------");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: write list of monsters to data file
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayWriteToDataFile(List<Monster> monsters)
        {
            DisplayScreenHeader("Write to Data File");

            //
            // prompt the user to continue
            //
            Console.WriteLine("\tThe application is ready to write to the data file.");
            Console.Write("\tEnter 'y' to continue or 'n' to cancel.");
            if (Console.ReadLine().ToLower() == "y")
            {
                DisplayContinuePrompt();
                WriteToDataFile(monsters);
                //
                // TODO process I/O exceptions
                //

                Console.WriteLine();
                Console.WriteLine("\tList written to data the file.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\tList not written to the data file.");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Filter monsters
        /// </summary>
        /// <param name="monsters"></param>
        static void DisplayFilterByTemperment(List<Monster> monsters)
        {
            //
            // Get attitude from user
            //
            Monster.EmotionalState selectedAttitude;
            List<Monster> filteredMonsters = new List<Monster>();

            DisplayScreenHeader("Filter by Attitude");

            Console.WriteLine("What attitude do you wish to filter for?");
            if (Enum.TryParse(Console.ReadLine(), out selectedAttitude))
            {
                foreach (Monster monster in monsters)
                {
                    if (monster.Attitude == selectedAttitude)
                    {
                        filteredMonsters.Add(monster);
                    }
                }

                //
                // Display List of Filtered Monsters
                //
                Console.WriteLine();
                Console.WriteLine($"Monsters with attitude: {selectedAttitude}: ");
                Console.WriteLine();
                MonsterInfoTable();
                foreach (Monster monster in filteredMonsters)
                {
                    Console.WriteLine();
                    MonsterInfo(monster);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Invalid attitde entered. Returning to main menu.");
            }
            
            
            DisplayContinuePrompt();
        }

        #endregion

        #region FILE I/O METHODS

        /// <summary>
        /// write monster list to data file
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void WriteToDataFile(List<Monster> monsters)
        {
            string[] monstersString = new string[monsters.Count];

            //
            // create array of monster strings
            //
            for (int index = 0; index < monsters.Count; index++)
            {
                string monsterString =
                    monsters[index].Name + "," +
                    monsters[index].Age + "," +
                    monsters[index].Attitude + "," +
                    monsters[index].Tribe + "," +
                    monsters[index].Active + "," +
                    monsters[index].Birth;

                monstersString[index] = monsterString;
            }

            File.WriteAllLines("Data\\Data.txt", monstersString);
        }

        /// <summary>
        /// read monsters from data file and return a list of monsters
        /// </summary>
        /// <returns>list of monsters</returns>        
        static List<Monster> ReadFromDataFile()
        {
            List<Monster> monsters = new List<Monster>();

            //
            // read all lines in the file
            //
            string[] monstersString = File.ReadAllLines("Data\\Data.txt");

            //
            // create monster objects and add to the list
            //
            foreach (string monsterString in monstersString)
            {
                //
                // get individual properties
                //
                string[] monsterProperties = monsterString.Split(',');

                //
                // create monster
                //
                Monster newMonster = new Monster();

                //
                // update monster property values
                //
                newMonster.Name = monsterProperties[0];

                int.TryParse(monsterProperties[1], out int age);
                newMonster.Age = age;

                Enum.TryParse(monsterProperties[2], out Monster.EmotionalState attitude);
                newMonster.Attitude = attitude;

                Enum.TryParse(monsterProperties[3], out Monster.MonsterTribe tribe);
                newMonster.Tribe = tribe;

                bool.TryParse(monsterProperties[4], out bool active);
                newMonster.Active = active;

                DateTime.TryParse(monsterProperties[5], out DateTime birth);
                newMonster.Birth = birth;

                //
                // add new monster to list
                //
                monsters.Add(newMonster);
            }

            return monsters;
        }

        #endregion

        #region CONSOLE HELPER METHODS

        /// <summary>
        /// display all monster properties
        /// </summary>
        /// <param name="monster">monster object</param>
        static void MonsterInfo(Monster monster)
        {
            Console.WriteLine(
               "\t" +
               $"{monster.Name}".PadRight(10) +
               $"{monster.Age}".PadRight(10) +
               $"{monster.Attitude}".PadRight(15) +
               $"{monster.Tribe}".PadRight(15) +
               $"{monster.Active}".PadRight(10) +
               $"{monster.Birth}".PadRight(23) 
               );
            Console.WriteLine("\t\t" + monster.Greeting()); 
            //Console.WriteLine($"\tName: {monster.Name}");
            //Console.WriteLine($"\tAge: {monster.Age}");
            //Console.WriteLine($"\tAttitude: {monster.Attitude}");
            //Console.WriteLine($"\tTribe: {monster.Tribe}");
            //Console.WriteLine($"\tActive: {monster.Active}");
            //Console.WriteLine($"\tBirthdate: {monster.Birth}");
            //Console.WriteLine("\t" + monster.Greeting());
        }

        /// <summary>
        /// Create table to display monster properties
        /// </summary>
        static void MonsterInfoTable()
        {
            Console.WriteLine(
                "\t" +
                "Name".PadRight(10) +
                "Age".PadRight(10) +
                "Attitude".PadRight(15) +
                "Tribe".PadRight(15) +
                "Status".PadRight(10) +
                "Birthdate".PadRight(23) 
             );
            Console.WriteLine(
                "\t" + 
                "----".PadRight(10) +
                "---".PadRight(10) +
                "--------".PadRight(15) +
                "-----".PadRight(15) +
                "------".PadRight(10) +
                "---------".PadRight(23) 
             );
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThe Monster Tracker");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using the Monster Tracker!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.Write("\tPress any key to continue.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
