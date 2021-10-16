using System;

namespace PragParking
{
    class Program
    {
        static bool varRunApplication = true;
        //static bool helpSearch = false;
        //static bool loopCanParkVehicle = true;
        static string[] P_Garage = new string[21];
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
            P_Garage[1] = "C#ABC123";   // Bil med regnummer ABC123
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

        private static void StartMenu()
        {
            Console.Clear();

            int userInput;
            TextStartMenu();
            bool isValid = int.TryParse(Console.ReadLine(), out userInput);
            switch (userInput)
            {
                case 1:
                    ParkVehicleMenu();
                    break;
                case 2:
                    ShowGarageMenu();
                    break;
                case 3:
                    MoveVehicleMenu();
                    break;
                case 4:
                    CloseApplication();
                    break;
                default:
                    DefaultOption();
                    break;
            }
        }

        #region ParkVehicle
        public static void ParkVehicleMenu()
        {
            char fordonsTyp;
            string registreringsNummer;
            int parkingsRuta;
            bool vTypeLoop = true, loopReg = true, loopPSpot = true;
            while (vTypeLoop)
            {
                Console.Clear();
                ShowGarage();

                Console.WriteLine("Enter vehicle type");
                bool isCharValid = char.TryParse(Console.ReadLine().ToUpper(), out fordonsTyp);
                if (VehicleType(fordonsTyp, out fordonsTyp))
                {
                    while (loopReg)
                    {
                        ShowGarage();
                        Console.WriteLine("Enter reg number");
                        registreringsNummer = Console.ReadLine().ToUpper();
                        if (EnterRegistrationNumber(registreringsNummer))
                        {
                            while (loopPSpot)
                            {
                                ShowGarage();
                                Console.WriteLine("Where should you park");
                                bool isIntValid = int.TryParse(Console.ReadLine(), out parkingsRuta);
                                if (SelectParkingSlot(parkingsRuta, fordonsTyp))
                                {
                                    ShowGarage();
                                    vTypeLoop = false;
                                    loopReg = false;
                                    loopPSpot = false;
                                    Console.WriteLine("Can park here");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Occupide");
                                    Console.ReadKey();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("To long regnumber max 10 char...");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input...Press any key to continue");
                    Console.ReadKey();
                }
            }
        }
        public static bool VehicleType(char VehicleType, out char FordonsTyp)
        {
            char _vehicleType = VehicleType;
            if (_vehicleType == 'C')
            {
                FordonsTyp = 'C';
                return true;
            }
            else if (_vehicleType == 'M')
            {
                FordonsTyp = 'M';
                return true;
            }
            else
            {
                FordonsTyp = '?';
                return false;
            }
        }
        public static bool EnterRegistrationNumber(string RegistrationNumber)
        {
            string _registrationNumber = RegistrationNumber;
            if (_registrationNumber.Length <= 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool SelectParkingSlot(int SelectParkingSlot, char VehicleType)
        {
            string availableSpot = P_Garage[SelectParkingSlot];
            char _vehicleType = VehicleType;
            if (availableSpot == "")
            {
                return true;
            }
            else if (_vehicleType == 'M' && availableSpot.Contains("M#") && !availableSpot.Contains("|"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void ParkVehicle(char VehicleType, string RegistrationNumber, int SelectParkingSlot)
        {
            char _vehicleType = VehicleType;
            string _registrationNumber = RegistrationNumber;
            int _selectParkingSlot = SelectParkingSlot;
        }
        public static void SelectP_Slot()
        {
            //currentP_Slot = P_Slot;
            Console.Clear();
            ShowGarage();

            Console.WriteLine("Where should you park");
            bool isIntValid = int.TryParse(Console.ReadLine(), out P_Slot);
            if (isIntValid && CheckSpace(vehicleType, P_Slot))
            {
                ParkVehicle(vehicleType, registrationNumber, P_Slot);
                //P_Garage[P_Slot] = ""; // Hitta lösning för att remove vehicle
                //loopCanParkVehicle = false;
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
        } // == SelectParkinspot
        public static bool ClaesParkVehicle(char vehicleType, string regNumber, int parkingSpot)
        {
            if (!CheckSpace(vehicleType, parkingSpot))
            {
                return false;
            }

            if (vehicleType == 'C')
            {
                //P_Garage[currentP_Slot] = "";// Kan ändras
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
                ShowGarage();
                Console.WriteLine("Enter reg number");
                registrationNumber = Console.ReadLine().ToUpper();

                if (SearchRegNumber(registrationNumber, out P_Slot))
                {
                    currentP_Slot = P_Slot;
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
                        ShowGarage();

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
        private static void MoveVehicleMenu()
        {
            int switchInput;
            Console.Clear();
            ShowGarage();

            Console.WriteLine("1.Enter registration number for the vehicle you want to move\n2.Back");
            bool isValid = int.TryParse(Console.ReadLine(), out switchInput);
            switch (switchInput)
            {
                case 1:
                    SearchVehicle();
                    break;
                case 2:
                    StartMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input...Press any key to continue");
                    Console.ReadKey();
                    MoveVehicleMenu();
                    break;
            }
        }
        private static void MoveVehicle()
        {

        }

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

        public static void ShowGarageMenu()
        {
            Console.Clear();

            ShowGarage();
            Console.WriteLine();
            // Text for option to vehicle
            Console.WriteLine("1.Press any key to go back to meny");
            Console.ReadKey();
        }
        private static void ShowGarage()
        {
            Console.Clear();
            Console.WriteLine("----------GARAGE----------\n");
            int topColum = 0; // const med //kod
            int bottomColum = 0;
            int topNumber = 0;
            for (int i = 1; i < P_Garage.Length; i++)
            {
                if (topColum % 11 == 0)
                {
                    Console.WriteLine();
                    topColum = 0;
                    for (int j = 1; j < P_Garage.Length; j++)
                    {
                        topNumber++;
                        Console.Write($"[{topNumber}]".ToString().PadRight(11));
                        bottomColum++;
                        if (bottomColum % 11 == 0)
                        {
                            //Console.WriteLine();
                            bottomColum = 0;
                            break;
                        }
                    }
                }
                //Console.Write($"[{i}]{P_Garage[i]} ");
                Console.Write(P_Garage[i].PadRight(10));
                topColum++;

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
