﻿using System;

namespace PragParking
{
    class Program
    {
        static bool varRunApplication = true;
        static string[] P_Garage = new string[11];
        static string registrationNumber;
        static string availableP_slot;
        static string ParkedVehicles;
        static char vehicleType;
        static int P_Slot;
        static int userInput;
        const int regLength = 10;
        static void Main(string[] args)
        {
            Initialize();

            RunApplication();                        
        }

        

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
            P_Garage[3] = "C#CAR002";
            P_Garage[4] = "M#MC001";
            P_Garage[7] = "M#MC002|M#MC003";
            P_Garage[9] = "M#SYP305";
        }
        #endregion

        #region SelectOption
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
        private static void ParkVehicle()
        {
            Console.Clear();
            CanParkVehicle();
        }

        private static void TestSwitch3()
        {
            Console.Clear();

            int switchInput;
            TestTextMenu3();
            bool isValid = int.TryParse(Console.ReadLine(), out switchInput);
            switch (switchInput)
            {
                case 1:
                    Console.WriteLine("End...");
                    Console.ReadKey();
                    break;
                case 2:
                    ParkVehicle();
                    break;
                case 3:
                    CloseApplication();
                    break;
                default:
                    DefaultOption();
                    TestSwitch3();
                    break;
            }
        }

        private static void MoveVehicle()
        {
            Console.Clear();

            int switchInput;
            // Text for option to vehicle
            Console.WriteLine("1.Search for vehicle to move\n2.Back\n3.Close Application");
            bool isValid = int.TryParse(Console.ReadLine(), out switchInput);
            switch (switchInput)
            {
                case 1:
                    SearchVehicle();
                    break;
                case 2:
                    StartMenu();
                    break;
                case 3:
                    CloseApplication();
                    break;
                default:
                    DefaultOption();
                    TestSwitch3();
                    break;
            }
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
                              "\n" +
                              "\nPress 1 to park your vehicle" +
                              "\nPress 2 to show all spots" +
                              "\nPress 3 to move your vehicle" +
                              "\nPress 4 to exit the system");
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
            Console.WriteLine("Please, e" +
                "nter the vehicle type");
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
        private static void TextInvalidType()
        {
            Console.WriteLine("The type is invalid" +
                "\n Please enter a valid type");
        }
        private static void TextToLongRegistration()
        {
            Console.WriteLine("The registration number is too long" +
                "\nPlease, it has to be maximun 10 characteres");
        }

        #endregion


        private static void ShowAllParkedVehicle()
        {
            for (int i = 1; i < P_Garage.Length; i++)
            {
                ParkedVehicles = P_Garage[i];
                if (ParkedVehicles != "")
                {
                    Console.WriteLine(ParkedVehicles);
                }
            }
            Console.ReadKey();
        }
        private static void ShowAvailableP_slots()
        {
            for (int i = 1; i < P_Garage.Length; i++)
            {
                availableP_slot = P_Garage[i];

                if (availableP_slot == "")
                {
                    Console.WriteLine($"Parking slot {i} is available");
                }
            }
            TextAnyKey();
            Console.ReadKey();
        }

        public static bool SearchRegNumber(string regNumber, out int parkingSpot)
        {
            for (int i = 1; i < P_Garage.Length; i++)
            {
                // Se om vårt regnummer finns i ruta nummer i
                if (P_Garage[i]?.IndexOf(regNumber) > 0)   // "?" kollar om null
                {
                    parkingSpot = i;
                    return true;
                }
            }
            // Vi har sökt igenom hela Phuset, men regnumret fanna ingenstans
            parkingSpot = -1;
            return false;
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
                P_Garage[parkingSpot] = vehicleType + "#" + regNumber;
            }
            if (vehicleType == 'M')
            {
                if (P_Garage[parkingSpot] == "")
                {
                    P_Garage[parkingSpot] = vehicleType + "#" + regNumber;
                }
                else
                {
                    P_Garage[parkingSpot] += "|" + vehicleType + "#" + regNumber;
                }
            }

            return true;
        }

        public static void CanParkVehicle()
        {
            bool loopPark = true;
            while (loopPark)
            {
                Console.Clear();

                TextVehicleType();
                bool isCharValid = char.TryParse(Console.ReadLine().ToUpper(), out vehicleType);
                if (isCharValid && vehicleType == 'C' || vehicleType == 'M')
                {
                    TextRegistrationNumber();
                    registrationNumber = Console.ReadLine().ToUpper();

                    if (registrationNumber.Length <= regLength)
                    {
                        TextWherePark();
                        bool isIntValid = int.TryParse(Console.ReadLine(), out P_Slot);
                        if (isIntValid && CheckSpace(vehicleType, P_Slot))
                        {
                            ParkVehicle(vehicleType, registrationNumber, P_Slot);
                            loopPark = false;
                        }
                        else
                        {
                            TextOccupied();
                        }
                    }
                    else
                    {
                        TextToLongRegistration();
                        Console.ReadKey();
                    }
                    TextAnyKey();
                    Console.ReadKey();
                }
                else
                {
                    TextInvalidType();
                    TextAnyKey();
                    Console.ReadKey();
                }
            }
        }

        public static void SearchVehicle()
        {
            bool searchLoop = true;
            while (searchLoop)
            {
                Console.Clear();

                Console.WriteLine("Enter reg number");
                registrationNumber = Console.ReadLine().ToUpper();

                if (SearchRegNumber(registrationNumber, out P_Slot))
                {
                    Console.WriteLine("Found");
                    searchLoop = false;
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Not found");

                    Console.WriteLine("1.Try one more time\n2.Back");
                    bool isInputValid = int.TryParse(Console.ReadLine(), out userInput);
                    if (isInputValid)
                    {
                        if (userInput == 1)
                        {

                        }
                        else if (userInput == 2)
                        {
                            searchLoop = false;
                        }
                    }
                    
                }
            }
        }

        public static void ShowGarage()
        {
            Console.Clear();

            int switchInput;
            // Text for option to vehicle
            Console.WriteLine("1.Show all vehicle\n2.Show all available spot");
            bool isValid = int.TryParse(Console.ReadLine(), out switchInput);
            switch (switchInput)
            {
                case 1:
                    ShowAllParkedVehicle();
                    break;
                case 2:
                    ShowAvailableP_slots();
                    break;
                default:
                    break;
            }
        }
    }
}
