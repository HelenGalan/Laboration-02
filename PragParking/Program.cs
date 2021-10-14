using System;

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
        static void Main(string[] args)
        {
            Initialize();

            Console.WriteLine("Enter vehicle type");
            bool isCharValid = char.TryParse(Console.ReadLine().ToUpper(), out vehicleType);
            Console.WriteLine("Enter reg number");
            registrationNumber = Console.ReadLine().ToUpper();

            Console.WriteLine("Where should you park");
            bool isIntValid = int.TryParse(Console.ReadLine(), out P_Slot);
            if (CheckSpace(vehicleType, P_Slot))
            {
                ParkVehicle(vehicleType, registrationNumber, P_Slot);
            }
            else
            {
                Console.WriteLine("Occupied");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();




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
                    MoveVehicle();
                    break;
                case 3:
                    ShowAvailableP_slots();
                    break;
                case 4:
                    CloseApplication();
                    break;
                default:
                    Console.WriteLine("Enter valid number!");
                    Console.WriteLine("Press any button to continue (1)");
                    Console.ReadKey();
                    break;
            }
        }
        private static void ParkVehicle()
        {
            Console.Clear();

            int switchInput;
            TextParkVehicle();
            bool isValid = int.TryParse(Console.ReadLine(), out switchInput);
            switch (switchInput)
            {
                case 1:
                    TestSwitch3();
                    break;
                case 2:
                    ShowAllParkedVehicle();
                    break;
                case 3:
                    StartMenu();
                    break;
                default:
                    Console.WriteLine("Enter valid number!");
                    Console.WriteLine("Press any button to continue (2)");
                    Console.ReadKey();
                    ParkVehicle();
                    break;
            }
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
                    Console.WriteLine("Enter valid number!");
                    Console.WriteLine("Press any button to continue (2)");
                    Console.ReadKey();
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

                    break;
                case 2:
                    StartMenu();
                    break;
                case 3:
                    CloseApplication();
                    break;
                default:
                    Console.WriteLine("Enter valid number!");
                    Console.WriteLine("Press any button to continue (2)");
                    Console.ReadKey();
                    TestSwitch3();
                    break;
            }
        }

        #endregion

        #region UserInterface
        private static void TextStartMenu()
        {
            Console.WriteLine("Welcome!");
        }

        private static void TextParkVehicle()
        {

            Console.Write("Press 1 to park a car" +
                        "\nPress 2 to park a motorcicle" +
                        "\nPress 3 back to the main menu\n");

        }

        private static void TestTextMenu3()
        {

            Console.Write("Press 1 to confirm the parking" +
                        "\nPress 2 move the vehicle" +
                        "\nPress 3 remove the vehicle" +
                        "\nPress 4 to back to the previous menu" +
                        "\nPress 5 to close the application");
        }
        #endregion


        private static void ShowAllParkedVehicle()
        {
            for (int i = 1; i < P_Garage.Length; i++)
            {

                ParkedVehicles = P_Garage[i];
                if (ParkedVehicles == "")
                {

                }
                else
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
            Console.WriteLine("Enter any key to continue");
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

        #region MyRegion

        #endregion
    }
}
