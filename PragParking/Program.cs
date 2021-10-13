using System;

namespace PragParking
{
    class Program
    {
        static bool varRunApplication = true;
        static string[] P_Garage = new string[11];
        static void Main(string[] args)
        {
            Console.WriteLine("Här började Prag Parking");
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
                    ShowP_Garage();
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
        #endregion

        #region UserInterface
        private static void TextStartMenu()
        {
            Console.Write("Welcome to Parking in Prag!" +
                "\nPress 1 if you want to park your vehicle" +
                "\nPress 2 if you want to close application");
        }

        private static void TextParkVehicle()
        {
            Console.Write("Press 1 to park a car" +
                "\nPress 2 to park a motocicle" +
                "\nPress 3 back to the main menu\n");
        }

        private static void TestTextMenu3()
        {
            Console.Write("Press 1 to confirm the parking" +
                "\nPress 2 move the vehicle" +
                "\nPress 2 to back to the previous menu" +
                "\nPress 3 to close the application");
        }
        #endregion

        private static void ShowP_Garage()
        {
            foreach (var vihecles in P_Garage)
            {
                Console.WriteLine(vihecles);
            }
            Console.ReadKey();
        }
    }
}
