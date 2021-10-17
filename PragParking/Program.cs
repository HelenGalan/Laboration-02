using System;

namespace PragParking
{
    class Program
    {
        static bool varRunApplication = true;
        static string[] P_Garage = new string[101];
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

        private static void StartMenu() // The start menu for the application
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
                    SearchVehicleMenu();
                    break;
                case 4:
                    CloseApplication();
                    break;
                default:
                    DefaultOption();
                    break;
            }
        }

        private static void CloseApplication()
        {
            varRunApplication = false;
        }
        #endregion

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
                ShowAllParkedVehicle();

                Console.WriteLine("Enter vehicle type");
                bool isCharValid = char.TryParse(Console.ReadLine().ToUpper(), out fordonsTyp);
                if (VehicleType(fordonsTyp, out fordonsTyp))
                {
                    while (loopReg)
                    {
                        ShowAllParkedVehicle();
                        Console.WriteLine("Enter reg number");
                        registreringsNummer = Console.ReadLine().ToUpper();
                        if (EnterRegistrationNumber(registreringsNummer))
                        {
                            while (loopPSpot)
                            {
                                ShowAvailableP_slots(fordonsTyp);
                                Console.WriteLine("Where should you park");
                                bool isIntValid = int.TryParse(Console.ReadLine(), out parkingsRuta);
                                if (SelectParkingSlot(parkingsRuta, fordonsTyp, registreringsNummer))
                                {
                                    ShowAllParkedVehicle();
                                    vTypeLoop = false;
                                    loopReg = false;
                                    loopPSpot = false;
                                    Console.WriteLine("Vehicle is parked now");
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
            if (VehicleType == 'C')
            {
                FordonsTyp = 'C';
                return true;
            }
            else if (VehicleType == 'M')
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
            if (RegistrationNumber.Length <= 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool SelectParkingSlot(int SelectParkingSlot, char VehicleType, string RegistrationNumber)
        {
            string availableSpot = P_Garage[SelectParkingSlot];
            if (availableSpot == "")
            {
                //P_Garage[_selectParkingSlot] = string.Join(vehicleType, "#", _registrationNumber);
                P_Garage[SelectParkingSlot] = VehicleType + "#" + RegistrationNumber;
                return true;
            }
            else if (VehicleType == 'M' && availableSpot.Contains("M#") && !availableSpot.Contains("|"))
            {
                P_Garage[SelectParkingSlot] += "|" + VehicleType + "#" + RegistrationNumber;
                return true;
            }
            else
            {
                return false;
            }
        }
       
        #endregion

        #region MoveVehicle
        public static void SearchVehicleMenu()
        {
            char fordonsTyp;
            string registreringsNummer;
            string FullStändigtRegNummer;
            int parkingsRuta;
            int användarVal;
            bool loopSearchReg = true, loopPSpot = true, loopUserInput = true;
            while (loopSearchReg)
            {
                Console.Clear();
                ShowAllParkedVehicle();
                Console.WriteLine("\nEnter reg number");
                registreringsNummer = Console.ReadLine().ToUpper();

                if (SearchVehicle(registreringsNummer, out parkingsRuta, out FullStändigtRegNummer))
                {
                    while (loopUserInput)
                    {
                        Console.Clear();
                        ShowAllParkedVehicle();
                        Console.WriteLine("1.Move it\n2.Remove it\n3.Search foor another vehicle\n4.Back to Start Menu");
                        bool isValidUserInput = int.TryParse(Console.ReadLine(), out användarVal);
                        switch (användarVal)
                        {
                            case 1:
                                GetVehicleType(FullStändigtRegNummer, out fordonsTyp);
                                Console.Clear();
                                ShowAvailableP_slots(fordonsTyp);
                                Console.WriteLine("Put where?");
                                int testUser = int.Parse(Console.ReadLine());
                                MoveVehicle(testUser, parkingsRuta, fordonsTyp, registreringsNummer);
                                break;
                            case 2:
                                RemoveVehicle(registreringsNummer, parkingsRuta);
                                loopUserInput = false;
                                loopSearchReg = false;
                                ShowGarageMenu();
                                break;
                            case 3:
                                SearchVehicleMenu();
                                break;
                            case 4:
                                StartMenu();
                                break;
                            default:
                                Console.WriteLine("Invalid input ... Press any key to continue");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Gone");
                    Console.ReadKey();
                }
            }
        }
        public static bool SearchVehicle(string RegistrationNumber, out int ParkingsRuta, out string FullStändigtRegNummer)
        {
            int markör;
            
            for (int i = 1; i < P_Garage.Length; i++)
            {
                if (RegistrationNumber == P_Garage[i] && RegistrationNumber.Length > 1 || P_Garage[i].EndsWith(RegistrationNumber) && RegistrationNumber.Length > 1)
                {
                    FullStändigtRegNummer = P_Garage[i];
                    ParkingsRuta = i;
                    return true;
                }
                else if (P_Garage[i].Contains("|"))
                {
                    markör = P_Garage[i].IndexOf("|");
                    if (RegistrationNumber == P_Garage[i].Substring(2, markör - 2) || RegistrationNumber == P_Garage[i].Substring(0, markör)) //2 och -2 är till för att hamna rätt
                    {
                        FullStändigtRegNummer = P_Garage[i];
                        ParkingsRuta = i;
                        return true;
                    }
                }
            }
            FullStändigtRegNummer = "";
            ParkingsRuta = -1;
            return false;
        }
        public static void GetVehicleType(string FullStändigtRegNummer, out char FordonsTyp)
        {
            if (FullStändigtRegNummer.Contains("|"))
            {
                //Behövs inte tror jag, tabort senare
                FordonsTyp = 'M';
            }
            else if (FullStändigtRegNummer.StartsWith("C"))
            {
                FordonsTyp = 'C';
            }
            else if (FullStändigtRegNummer.StartsWith('M'))
            {
                FordonsTyp = 'M';
            }
            else
            {
                FordonsTyp = '?';
            }
        }
        public static bool MoveVehicle(int SelectParkingSlot, int GamlaParkingsRuta, char VehicleType, string RegistrationNumber)
        {
            string availableSpot = P_Garage[SelectParkingSlot];
            string[] tempArray;

            if (availableSpot == "")
            {
                if (P_Garage[GamlaParkingsRuta].Contains("|"))
                {
                    tempArray = P_Garage[GamlaParkingsRuta].Split("|");

                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        if (RegistrationNumber == tempArray[i] || RegistrationNumber == tempArray[i].Substring(2))
                        {
                            tempArray[i] = "";
                        }
                    }
                    P_Garage[GamlaParkingsRuta] = tempArray[0] + tempArray[1];
                    P_Garage[SelectParkingSlot] += "|" + VehicleType + "#" + RegistrationNumber;
                    return true;
                }
                else
                {
                    P_Garage[GamlaParkingsRuta] = "";
                    P_Garage[SelectParkingSlot] = VehicleType + "#" + RegistrationNumber;
                    return true;
                }
            }
            else if (!availableSpot.Contains("|") && VehicleType == 'M')
            {
                if (P_Garage[GamlaParkingsRuta].Contains("|"))
                {
                    tempArray = P_Garage[GamlaParkingsRuta].Split("|");

                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        if (RegistrationNumber == tempArray[i] || RegistrationNumber == tempArray[i].Substring(2))
                        {
                            tempArray[i] = "";
                        }
                    }
                    P_Garage[GamlaParkingsRuta] = tempArray[0] + tempArray[1];
                    P_Garage[SelectParkingSlot] += "|" + VehicleType + "#" + RegistrationNumber;
                    return true;
                }
                else
                {
                    P_Garage[GamlaParkingsRuta] = "";
                    P_Garage[SelectParkingSlot] += "|" + VehicleType + "#" + RegistrationNumber;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void RemoveVehicle(string RegistrationNumber, int ParkingsRuta)
        {
            if (P_Garage[ParkingsRuta].Contains("|"))
            {
                string[] tempArray = P_Garage[ParkingsRuta].Split("|");

                for (int i = 0; i < tempArray.Length; i++)
                {
                    if (RegistrationNumber == tempArray[i] || RegistrationNumber == tempArray[i].Substring(2))
                    {
                        tempArray[i] = "";
                    }
                }
                P_Garage[ParkingsRuta] = tempArray[0] + tempArray[1];
            }
            else
            {
                P_Garage[ParkingsRuta] = "";
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

        private static void ShowAvailableP_slots(char VehicleType)
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

                if (VehicleType == 'M')
                {
                    if (!availableP_slot.Contains("|") && availableP_slot.Contains("M#"))
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
                else if(availableP_slot == "")
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
