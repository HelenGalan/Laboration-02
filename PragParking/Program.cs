using System;

namespace PragParking
{
    class Program
    {
        static bool varRunApplication = true;
        static string[] P_Garage = new string[11];
        static void Main(string[] args)
        {
            Initialize();
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
                TestSwitch1();
            }
            Console.WriteLine("Exit Aplication\nPress any key to close");
            Console.ReadKey();
        }
        private static void CloseApplication()
        {
            varRunApplication = false;
        }

        private static void TestSwitch1()
        {
            Console.Clear();

            int userInput;
            TestTextMenu1();
            bool isValid = int.TryParse(Console.ReadLine(), out userInput);
            switch (userInput)
            {
                case 1:
                    TestSwitch2();
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
        private static void TestSwitch2()
        {
            Console.Clear();

            int switchInput;
            TestTextMenu2();
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
                    TestSwitch1();
                    break;
                default:
                    Console.WriteLine("Enter valid number!");
                    Console.WriteLine("Press any button to continue (2)");
                    Console.ReadKey();
                    TestSwitch2();
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
                    TestSwitch2();
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
        private static void TestTextMenu1()
        {
            Console.Write("1.Switch1:\n2.Close Application\n");
        }

        private static void TestTextMenu2()
        {
            Console.Write("1.Switch2\n2.Show all vihecles\n3.Back\n");
        }

        private static void TestTextMenu3()
        {
            Console.Write("1.Switch3\n2.Back\n3.Close Application");
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
