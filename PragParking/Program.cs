using System;

namespace PragParking
{
    class Program
    {
        static bool varRunApplication = true;
        //static bool helpSearch = false;
        static bool loopCanParkVehicle = true;
        static string[] P_Garage = new string[101];
        static string registrationNumber;
        //static string availableP_slot;
        //static string ParkedVehicles;
        static char vehicleType;
        static int P_Slot;
        static int currentP_Slot;
        static int userInput;
        const int regLength = 10;
        static void Main(string[] args)
        {
            Initialize();
            RunApplication();
        }

        #region Initialize
        static void Initialize()
        {
            for (int i = 1; i < P_Garage.Length; i++)
            {
                P_Garage[i] = "";
            }

            // Sätt upp litet testdata
            P_Garage[9] = "C#ABC123";   // Bil med regnummer ABC123
            P_Garage[2] = "C#CAR002";
            P_Garage[3] = "M#MC001";
            P_Garage[8] = "M#MC002|M#MC003";
            P_Garage[10] = "M#SYP305";
        }
        #endregion

        #region RunAppCloseAPP
        private static void RunApplication()
        {
            while (varRunApplication)
            {
                StartMenu();
            }
            Console.WriteLine("Exit Aplication\nPress any key to close");
            Console.ReadKey();
        }
        private static void CloseApplication()
        {
            varRunApplication = false;
        }
        #endregion

        #region SelectOption

        private static void StartMenu()
        {
            Console.Clear();

            int userInput;
            TextStartMenu();
            bool isValid = int.TryParse(Console.ReadLine(), out userInput);
            switch (userInput)
            {
                case 1:
                    ParkVehicle();
                    break;
                case 2:
                    ShowGarage();
                    break;
                case 3:
                    MoveVehicle();
                    break;
                case 4:
                    CloseApplication();
                    break;
                default:
                    DefaultOption();
                    break;
            }
        }

        #region Park
        public static void ParkVehicle()
        {
            loopCanParkVehicle = true;
            while (loopCanParkVehicle)
            {
                Console.Clear();
                Show();

                Console.WriteLine("Enter vehicle type");
                bool isCharValid = char.TryParse(Console.ReadLine().ToUpper(), out vehicleType);
                if (isCharValid && vehicleType == 'C' || vehicleType == 'M')
                {
                    Console.Clear();
                    Show();

                    Console.WriteLine("Enter reg number");
                    registrationNumber = Console.ReadLine().ToUpper();

                    if (registrationNumber.Length <= regLength)
                    {
                        SelectP_Slot();
                    }
                    else
                    {
                        Console.WriteLine("To long regnumber max 10 char...");
                    }
                }
                else
                {
                    Console.WriteLine("Invailed typ");
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
        public static void SelectP_Slot()
        {
            currentP_Slot = P_Slot;
            Console.Clear();
            Show();

            Console.WriteLine("Where should you park");
            bool isIntValid = int.TryParse(Console.ReadLine(), out P_Slot);
            if (isIntValid && CheckSpace(vehicleType, P_Slot))
            {
                ParkVehicle(vehicleType, registrationNumber, P_Slot);
                //P_Garage[P_Slot] = ""; // Hitta lösning för att remove vehicle
                loopCanParkVehicle = false;
                Console.WriteLine("Vehicle is now parked");
            }
            else
            {
                Console.WriteLine("Occupied");
            }
        }
        public static bool CheckSpace(char vehicleType, int parkingSpot)
        {
            string pPlats = P_Garage[parkingSpot];

            // Kolla om ett fordon av goven typ ryms i en given p-plats
            if (pPlats == "")
            {
                return true;
            }
            if (pPlats.Contains("M#") && !pPlats.Contains("|") && vehicleType == 'M')
            {
                return true;
            }
            return false;
        }
        public static bool ParkVehicle(char vehicleType, string regNumber, int parkingSpot)
        {
            if (!CheckSpace(vehicleType, parkingSpot))
            {
                return false;
            }

            if (vehicleType == 'C')
            {
                P_Garage[currentP_Slot] = "";// Kan ändras
                P_Garage[parkingSpot] = vehicleType + "#" + regNumber;
            }
            if (vehicleType == 'M')
            {
                if (P_Garage[parkingSpot] == "")
                {
                    if (P_Garage[currentP_Slot].Contains("|"))
                    {
                        string[] mcSplit = P_Garage[currentP_Slot].Split("|");
                        for (int i = 0; i < mcSplit.Length; i++)
                        {
                            if (mcSplit[i] == "M#" + regNumber)
                            {
                                mcSplit[i] = "";
                            }
                        }
                        P_Garage[currentP_Slot] = mcSplit[0] + mcSplit[1];
                        P_Garage[parkingSpot] = vehicleType + "#" + regNumber;
                    }
                    else
                    {
                        P_Garage[currentP_Slot] = "";
                        P_Garage[parkingSpot] = vehicleType + "#" + regNumber;
                    }

                }
                else
                {
                    P_Garage[parkingSpot] += "|" + vehicleType + "#" + regNumber;
                }
            }

            return true;
        }
        #endregion

        #region MoveVehicle
        public static void SearchVehicle()
        {
            bool searchLoop = true;
            while (searchLoop)
            {
                Console.Clear();
                Show();
                Console.WriteLine("Enter reg number");
                registrationNumber = Console.ReadLine().ToUpper();

                if (SearchRegNumber(registrationNumber, out P_Slot))
                {
                    Console.WriteLine("Found");
                    Console.WriteLine("1.Move it\n2.Remove it");
                    bool isValidInput = int.TryParse(Console.ReadLine(), out int userInput);
                    if (userInput == 1)
                    {
                        SelectP_Slot();
                        searchLoop = false;
                    }
                    else if (userInput == 2)
                    {
                        RemoveVehicle();
                    }
                    else
                    {

                    }
                    //SelectP_Slot();
                    //searchLoop = false;
                    Console.ReadKey();
                }
                else
                {
                    bool loopUserInput = true;
                    while (loopUserInput)
                    {
                        Console.Clear();
                        Show();

                        Console.WriteLine("Not found");
                        Console.WriteLine("1.Try one more time\n2.Back");
                        bool isInputValid = int.TryParse(Console.ReadLine(), out userInput);

                        if (userInput == 1)
                        {
                            loopUserInput = false;
                        }
                        else if (userInput == 2)
                        {
                            loopUserInput = false;
                            searchLoop = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }

        private static void RemoveVehicle()
        {
            throw new NotImplementedException();
        }

        public static bool SearchRegNumber(string regNumber, out int parkingSpot)
        {
            for (int i = 1; i < P_Garage.Length; i++)
            {
                // Se om vårt regnummer finns i ruta nummer i
                if (P_Garage[i].Contains(regNumber) && regNumber.Length > 2) // Hitta bättre lösning. Tar den första den hittar
                {
                    vehicleType = char.Parse(P_Garage[i].Substring(0, 1)); // behöver bättre lösning
                    parkingSpot = i;
                    return true;
                }
            }
            // Vi har sökt igenom hela Phuset, men regnumret fanna ingenstans
            parkingSpot = -1;
            return false;
        }
        private static void MoveVehicle()
        {
            Console.Clear();
            Show();

            int switchInput;
            // Text for option to vehicle
            //Console.WriteLine("Enter registration number for the vehicle you want to move");
            Console.WriteLine("1.Search for vehicle to move\n2.!Remove thisShow all parked Vehicle\n3Back");
            bool isValid = int.TryParse(Console.ReadLine(), out switchInput);
            switch (switchInput)
            {
                case 1:
                    SearchVehicle();
                    break;
                case 2:
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    //MoveVehicle();
                    break;
                case 3:
                    StartMenu();
                    break;
                default:
                    DefaultOption();
                    //TestSwitch3();
                    break;
            }
        }

        #endregion

        #endregion

        #region UserInterface

        private static void DefaultOption()
        {
            Console.WriteLine("Please, enter a valid number!");
            Console.WriteLine("Press any button to continue (2)");
            Console.ReadKey();
        }
        private static void TextStartMenu()
        {
            Console.WriteLine("Welcome to park in PRAG!" +
                              "\nPress 1 to park your vehicle" +
                              "\nPress 2 to show available spots" +
                              "\nPress 3 to exit the system");
        }

        private static void TextParkVehicle()
        {

            Console.Write("Press number 1 to park a car" +
                        "\nPress number 2 to park a motorcicle" +
                        "\nPress number 3 to back to the main menu\n");

        }

        private static void TestTextMenu3()
        {

            Console.Write("Press number 1 to confirm the parking" +
                        "\nPress number 2 move the vehicle" +
                        "\nPress number 3 remove the vehicle" +
                        "\nPress number 4 to back to the previous menu" +
                        "\nPress number 5 to close the application");
        }

        private static void TextVehicleType()
        {
            Console.WriteLine("Enter the vehicle type");
        }

        private static void TextRegistrationNumber()
        {
            Console.WriteLine("Please, enter the registration number");
        }
        private static void TextWherePark()
        {
            Console.WriteLine("Where should you park it?");
        }

        private static void TextAnyKey()
        {
            Console.WriteLine("Press any key to continue");
        }

        private static void TextOccupied()
        {
            Console.WriteLine("This spot is taken/occupied!");
        }

        #endregion

        public static void ShowGarage()
        {
            Console.Clear();

            Show();
            Console.WriteLine();
            // Text for option to vehicle
            Console.WriteLine("1.Press any key to go back to meny");
            Console.ReadKey();
        }
        private static void Show()
        {
            Console.WriteLine("----------GARAGE----------\n");
            int columns = 0; // const med //kod
            for (int i = 1; i < P_Garage.Length; i++)
            {

                //Console.Write($"\t[{i}]:" + P_Garage[i].PadRight(20));
                //if (i % columns == columns - 1)
                //{
                //    Console.WriteLine();
                //}
                if (columns == 6)
                {
                    Console.WriteLine();
                    columns = 0;
                }

                Console.Write(string.Format("{0,8}", $"[{i}]{P_Garage[i].PadRight(25)} "));
                columns++;

            }
            Console.WriteLine("\n----------GARAGE----------\n");
        }

        //private static void ShowAllParkedVehicle()
        //{
        //    for (int i = 1; i < P_Garage.Length; i++)
        //    {
        //        ParkedVehicles = P_Garage[i];
        //        if (ParkedVehicles != "")
        //        {
        //            Console.WriteLine($"[{i}]{ParkedVehicles}");
        //        }
        //    }
        //    //Console.ReadKey();
        //}

        //private static void ShowAvailableP_slots()
        //{
        //    Console.WriteLine("Here are all the available parking spot");   //Change

        //    for (int i = 1; i < P_Garage.Length; i++)
        //    {
        //        availableP_slot = P_Garage[i];

        //        if (vehicleType == 'M')
        //        {
        //            if (availableP_slot == "" || !availableP_slot.Contains("|") && availableP_slot.Contains("M#"))
        //            {
        //                Console.WriteLine($"[{i}]");
        //            }
        //        }
        //        else
        //        {
        //            if (availableP_slot == "")
        //            {
        //                Console.WriteLine($"[{i}]");
        //            }
        //        }

        //    }
        //}


    }
}
