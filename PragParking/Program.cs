using System;

namespace PragParking
{
    class Program
    {
        static string[] P_Garage = new string[101];
        static bool runAppliction = true;
        static void Main(string[] args)
        {
            Initialize();
            while (runAppliction)
            {
                RunApplication();
            }

        }

        #region Initialize
        static void Initialize()
        {
            for (int i = 1; i < P_Garage.Length; i++)
            {
                P_Garage[i] = "";
            }

            // Sätt upp litet testdata
            P_Garage[3] = "CAR#JASON";
            P_Garage[1] = "CAR#DICK";
            P_Garage[78] = "MC#DAMIAN";
            P_Garage[8] = "MC#BRUCE|MC#SELINA";
            P_Garage[2] = "MC#TIM|MC#BARBARA";
            P_Garage[30] = "MC#HARVEY";
            P_Garage[40] = "MC#ALFRED";
            P_Garage[16] = "MC#JIM";
            P_Garage[28] = "CAR#LILLIAN";
            P_Garage[48] = "CAR#EDWARD";
        }
        #endregion

        #region RunAppCloseAPP
        private static void RunApplication()
        {
            while (runAppliction)
            {
                StartMenu();
            }
            Console.WriteLine("Exit Aplication\nPress any key to close");
            Console.ReadKey();
        }

        private static void CloseApplication()
        {
            runAppliction = false;
        }
        #endregion

        #region StartMenu
        public static void MenuScreen(out int userInput)
        {
            Console.Clear();

            Console.WriteLine("Welcome\n1.Park vehicle\n2.Search vehicle\n3.Show all Parked vehicle\n4.Close application");
            bool isValid = int.TryParse(Console.ReadLine(), out userInput);
            if (!isValid || userInput != 1 && userInput != 2 && userInput != 3)
            {
                Console.WriteLine("Invalid input...Press any key to continue");
                Console.ReadKey();
            }
        }
        public static void StartMenu()
        {
            MenuScreen(out int userInput);
            switch (userInput)
            {
                case 1:
                    ParkVehicleMenu();
                    break;
                case 2:
                    SearchVehicleMenu();
                    break;
                case 3:
                    ShowGarageMenu();
                    break;
                case 4:
                    CloseApplication();
                    break;
                default:
                    break;
            }
        }
        #endregion

        public static bool DeclareRegistrationNumber(out string registrationNumber)
        {
            while (true)
            {
                bool duplicate = false;
                Console.Clear();
                ShowAllParkedVehicle();

                Console.WriteLine("Enter registration number...Max 10 char!");
                string declareRegistrationNumber = Console.ReadLine().ToUpper();
                for (int i = 1; i < P_Garage.Length; i++)
                {
                    if (declareRegistrationNumber == P_Garage[i])
                    {
                        duplicate = true;
                        Console.WriteLine("A vehicle with that registration number already exits\nPress any key to continue");
                        Console.ReadKey();
                        break;
                    }
                    else if (P_Garage[i].StartsWith("CAR#") && declareRegistrationNumber == P_Garage[i].Substring(4))
                    {
                        duplicate = true;
                        Console.WriteLine("A vehicle with that registration number already exits\nPress any key to continue");
                        Console.ReadKey();
                        break;
                    }
                    else if (P_Garage[i].StartsWith("MC#") && declareRegistrationNumber == P_Garage[i].Substring(3))
                    {
                        duplicate = true;
                        Console.WriteLine("A vehicle with that registration number already exits\nPress any key to continue");
                        Console.ReadKey();
                        break;
                    }
                    else if (P_Garage[i].Contains("|"))
                    {
                        int divider = P_Garage[i].IndexOf("|");
                        if (declareRegistrationNumber == P_Garage[i].Substring(3, divider - 3) || declareRegistrationNumber == P_Garage[i].Substring(0, divider)) //3 och -3 är till för att hamna rätt
                        {
                            duplicate = true;
                            Console.WriteLine("A vehicle with that registration number already exits\nPress any key to continue");
                            Console.ReadKey();
                            break;
                        }
                        else if (declareRegistrationNumber == P_Garage[i].Substring(divider + 1) || declareRegistrationNumber == P_Garage[i].Substring(divider + 4))
                        {
                            duplicate = true;
                            Console.WriteLine("A vehicle with that registration number already exits\nPress any key to continue");
                            Console.ReadKey();
                            break;
                        }
                    }
                }
                if (declareRegistrationNumber.Length > 10)
                {
                    Console.WriteLine("To long...Press any key to continue");
                    Console.ReadKey();
                }
                else if (!duplicate)
                {
                    registrationNumber = declareRegistrationNumber;
                    return true;
                }
            }
        }

        #region SearchVehicle
        public static void SearchVehicleMenu()
        {
            SearchRegistrationNumber(out int currentParkingSpace, out string registrationNumber, out string wholeRegistrationNumber);
            if (MoveOrRemove())
            {
                GetVehicleType(wholeRegistrationNumber, out string vehicleType);
                DeclareParkingSpace(vehicleType, out int newParkingSpace);
                MoveVehicle(newParkingSpace, currentParkingSpace, vehicleType, registrationNumber);
                EndMessage(registrationNumber, newParkingSpace);
            }
            else
            {
                RemoveVehicle(registrationNumber, currentParkingSpace);
                RemoveMessage(registrationNumber);
            }

        }
        public static bool MoveOrRemove()
        {
            while (true)
            {
                Console.Clear();
                ShowAllParkedVehicle();
                Console.WriteLine("1.Move vehicle\n2.Remove vehicle");
                _ = int.TryParse(Console.ReadLine(), out int userInput);
                if (userInput == 1)
                {
                    return true;
                }
                else if (userInput == 2)
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input...Press any key to continue");
                    Console.ReadKey();
                }
            }
        }
        public static bool SearchRegistrationNumber(out int currentParkingSpace, out string registrationNumber, out string wholeRegistrationNumber)
        {
            int divider;

            while (true)
            {
                Console.Clear();
                ShowAllParkedVehicle();
                Console.WriteLine("Enter the registration number for the vehicle to search for");
                registrationNumber = Console.ReadLine().ToUpper();

                for (int i = 1; i < P_Garage.Length; i++)
                {
                    if (registrationNumber == P_Garage[i] && registrationNumber.Length > 0)
                    {
                        wholeRegistrationNumber = P_Garage[i];
                        currentParkingSpace = i;
                        return true;
                    }
                    else if (P_Garage[i].StartsWith("CAR#") && registrationNumber == P_Garage[i].Substring(4))
                    {
                        wholeRegistrationNumber = P_Garage[i];
                        currentParkingSpace = i;
                        return true;
                    }
                    else if (P_Garage[i].StartsWith("MC#") && registrationNumber == P_Garage[i].Substring(3))
                    {
                        wholeRegistrationNumber = P_Garage[i];
                        currentParkingSpace = i;
                        return true;
                    }
                    else if (P_Garage[i].Contains("|"))
                    {
                        divider = P_Garage[i].IndexOf("|");
                        if (registrationNumber == P_Garage[i].Substring(3, divider - 3) || registrationNumber == P_Garage[i].Substring(0, divider)) //3 och -3 är till för att hamna rätt för mc
                        {
                            wholeRegistrationNumber = P_Garage[i];
                            currentParkingSpace = i;
                            return true;
                        }
                        else if (registrationNumber == P_Garage[i].Substring(divider + 1) || registrationNumber == P_Garage[i].Substring(divider + 4))
                        {
                            wholeRegistrationNumber = P_Garage[i];
                            currentParkingSpace = i;
                            return true;
                        }
                    }
                }
                Console.WriteLine("Couldn't found...Tips look at the garage to see all parked vehicle's");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
        public static void GetVehicleType(string wholeRegistrationNumber, out string vehicleType)
        {
            if (wholeRegistrationNumber.Contains("|"))
            {
                //Behövs inte tror jag, tabort senare
                vehicleType = "MC#";
            }
            else if (wholeRegistrationNumber.StartsWith("CAR#"))
            {
                vehicleType = "CAR#";
            }
            else if (wholeRegistrationNumber.StartsWith("MC#"))
            {
                vehicleType = "MC#";
            }
            else
            {
                vehicleType = "?";
            }
        }
        public static bool MoveVehicle(int newParkingSpace, int currentParkingSpace, string vehicleType, string registrationNumber)
        {

            string availableSpot = P_Garage[newParkingSpace];
            string[] tempArray;

            if (availableSpot == "")
            {
                if (P_Garage[currentParkingSpace].Contains("|"))
                {
                    tempArray = P_Garage[currentParkingSpace].Split('|');

                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        if (registrationNumber == tempArray[i] || registrationNumber == tempArray[i].Substring(3))
                        {
                            tempArray[i] = "";
                        }
                    }
                    P_Garage[currentParkingSpace] = tempArray[0] + tempArray[1];
                    P_Garage[newParkingSpace] += vehicleType + registrationNumber;

                    return true;
                }
                else
                {
                    P_Garage[currentParkingSpace] = "";
                    P_Garage[newParkingSpace] = vehicleType + registrationNumber;
                    return true;
                }
            }
            else if (!availableSpot.Contains("|") && vehicleType == "MC#" && !availableSpot.Contains("CAR#") && availableSpot != P_Garage[currentParkingSpace])
            {
                if (P_Garage[currentParkingSpace].Contains("|"))
                {
                    tempArray = P_Garage[currentParkingSpace].Split('|');

                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        if (registrationNumber == tempArray[i] || registrationNumber == tempArray[i].Substring(3))// 3?
                        {
                            tempArray[i] = "";
                        }
                    }
                    P_Garage[currentParkingSpace] = tempArray[0] + tempArray[1];
                    P_Garage[newParkingSpace] += "|" + vehicleType + registrationNumber;
                    return true;
                }
                else
                {
                    P_Garage[currentParkingSpace] = "";
                    P_Garage[newParkingSpace] += "|" + vehicleType + registrationNumber;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void RemoveVehicle(string registrationNumber, int currentParkingSpace)
        {
            if (P_Garage[currentParkingSpace].Contains("|"))
            {
                string[] tempArray = P_Garage[currentParkingSpace].Split('|');

                for (int i = 0; i < tempArray.Length; i++)
                {
                    if (registrationNumber == tempArray[i] || registrationNumber == tempArray[i].Substring(3))
                    {
                        tempArray[i] = "";
                    }
                }
                P_Garage[currentParkingSpace] = tempArray[0] + tempArray[1];
            }
            else
            {
                P_Garage[currentParkingSpace] = "";
            }
        }
        public static void RemoveMessage(string registrationNumber)
        {
            ShowAllParkedVehicle();
            Console.WriteLine($"Vehicle {registrationNumber} has now been removed from the garage");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        #endregion

        #region Garage
        public static void ShowGarageMenu()
        {
            Console.Clear();
            ShowAllParkedVehicle();
            Console.WriteLine();
            Console.WriteLine("1.Press any key to go back to meny");
            Console.ReadKey();
        }
        private static void ShowAllParkedVehicle()
        {
            Console.Clear();
            Console.WriteLine("----------GARAGE----------");
            string parkedVehicles;
            for (int i = 1; i < P_Garage.Length; i++)
            {
                parkedVehicles = P_Garage[i];
                if (parkedVehicles != "")
                {
                    Console.WriteLine($"[{i}]{parkedVehicles}");
                }
            }
            Console.WriteLine("----------GARAGE----------");
        }
        private static void ShowAvailableP_slots(string VehicleType, int GamlaParkingsRuta)
        {
            Console.Clear();
            string availableP_slot;
            int counter = 0;
            Console.WriteLine("-----Here are all the available parking spot-----\n");   //Change

            for (int i = 1; i < P_Garage.Length; i++)
            {
                availableP_slot = P_Garage[i];
                if (counter == 5)
                {
                    Console.WriteLine();
                    counter = 0;
                }

                if (VehicleType == "MC#")
                {
                    if (!availableP_slot.Contains("|") && availableP_slot.Contains("MC#") && availableP_slot != P_Garage[GamlaParkingsRuta])
                    {
                        Console.Write($"[{i}]MC".ToString().PadRight(10));
                        counter++;
                    }
                    else if (availableP_slot == "")
                    {
                        Console.Write($"[{i}]".ToString().PadRight(10));
                        counter++;
                    }
                }
                else if (availableP_slot == "")
                {
                    Console.Write($"[{i}]".ToString().PadRight(10));
                    counter++;
                }
            }
            Console.WriteLine("\n-------------------------------------------------");   //Change
        }
        #endregion
    }
}
