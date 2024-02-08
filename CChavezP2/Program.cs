using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
//  Program: CChavezP2
//  Programed by: Colby Chavez
//  Email: CChavez572@cnm.edu
//  Program goal: Convert the idela gas calulator from program 1 into a class pper the spec
namespace CChavezP2
{
    internal class Program
    {
        //  Change program.cs so that it now instantiates the class and lets the class
        //  do the calculation of the pressure.
        //  You will still likeley need the following methods in Program.cs:
        //  DisplayHeader, GetMolecularWeights, DisplayGasNames,
        //  GetMolecularWeightFromName, DisplayPresure, PaToPSI.
        static void DisplayHeader()
        {
            Console.WriteLine(".NET I/C SHARP (U01) Class Header\nProgramed by: Colby Chavez\nEmail:cchavez572@cnm.edu\nProgram goal: The program will use the Ideal Gas Equation to will calculate the pressure\nexerted by a gas in a container. All from inputs selected by the user.\nExciting right?\n\nFirst, here is a list of all the gasses aviable for you to calulate!\n\n");
        }
        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {
            string filePath = "MolecularWeightsGasesAndVapors.csv";
            StreamReader reader = null;
            count = 0;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));
                reader.ReadLine(); // this line Discards header row from CSV
                while (!reader.EndOfStream)
                {
                    var inputLine = reader.ReadLine();
                    var inputValues = inputLine.Split(',');
                    gasNames[count] = inputValues[0];
                    molecularWeights[count] = double.Parse(inputValues[1]);
                    count++;
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }

        }

        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            countGases = 0;
            int outputCount = 0;
            string lastGasLenthBuffer;
            Console.WriteLine("---------------------AVAILABLE GASSES---------------------");
            for (int i = 0; i < gasNames.Length; i++)
            {
                if (outputCount == 0)
                {

                    Console.Write("{0,-20}", gasNames[i]);
                    countGases++;
                    outputCount++;
                }
                else if (outputCount == 1)
                {
                    Console.Write("{0,-20}", gasNames[i]);
                    countGases++;
                    outputCount++;

                }
                else
                {
                    Console.Write("{0,-20} \n", gasNames[i]);
                    countGases++;
                    outputCount = 0;
                }

            }
            Console.WriteLine("\n----------------------------------------------------\n");
        }
        private static double GetMolecularWeightFromName(string gasName, string[] gasNames, double[] molecularWeights, out int countGases)
        {
            countGases = 0;
            for (int i = 0; i < gasNames.Length; i++)
            {

                if (gasNames[i] == gasName)
                {
                    return molecularWeights[i];
                }

            }
            return 0;
        }
        private static void DisplayPresure(double pressure)
        {

            Console.Write($"The pressure in pascals is: {pressure}\n");
            double psiPressure = PaToPSI(pressure);
            Console.WriteLine($"The pressure in PSI is: {psiPressure}\n");
        }

        static double PaToPSI(double pascals)
        {
            return pascals * 0.000145038;
        }
        // I also brought over DoAnother code you showed us in the properies demo.
        public static bool DoAnother()
        {
            Console.WriteLine("Do you want to enter another student? (y/n): ");
            string? answer = Console.ReadLine();
            if (string.IsNullOrEmpty(answer)) { return false; }
            return answer.ToLower()[0] == 'y';
        }
        static void Main(string[] args)
        {
            //  The main method will do the following(Changes to the existing main method are underlined):
            //Declare double arrays for gas names and molecular weights.
            //Declare an int to keep track of number of elements in the arrays.
            string[] gasNames = new string[100];
            double[] molecularWeights = new double[100];
            int count;
            int countGases = 0;
            //Call DisplayHeader to show the program header.
            DisplayHeader();
            //Call GetMolecularWeights to fill the arrays and get the count of items in the list.
            GetMolecularWeights(ref gasNames, ref molecularWeights, out count);
            //Call DisplayGasNames to display the gas names to the user in three columns.
            DisplayGasNames(gasNames, countGases);
            //In a do another loop do the following:
            do
            {
                //Ask the user the name of the gas.
                Console.WriteLine($"\nWhich of the gasses would you like to run a calculation on?\n");
                string gasName = Console.ReadLine();
                //Use GetMolecularWeightFromName method to get the molecular weight of the gas selected by the user.
                double bufferMolWei = GetMolecularWeightFromName(gasName, gasNames, molecularWeights, out countGases);
                //If the gas is not found display an error message, and drop out to the do another loop.
                if (bufferMolWei == 0)
                {
                    Console.WriteLine("You did not select a gas on the list.");
                }
                //If it is found:
                else
                {
                    //  Instantiate an IdealGas object(something like IdealGas gas = new IdealGas();)
                    IdealGas gas = new IdealGas();
                    //  Ask the user for the volume of gas in cubic meters, mass of the gas in grams
                    //  and temperature in celcius.
                    //  Use the IdealGas’ SetMolecularWeight, SetVolume, SetMass and SetTemperature
                    //  methods to set the values receive from the user into the gas.The class will
                    //  automatically calculate pressure.
                    string inputBuffer;
                    Console.WriteLine($"\nWhat is the volume of {gasName} in cubic meters?\n");
                    inputBuffer = Console.ReadLine();
                    gas.SetVolume(double.Parse(inputBuffer));
                    Console.WriteLine($"\nWhat is the mass of {gasName} in grams?\n");
                    inputBuffer = Console.ReadLine();
                    gas.SetMass(double.Parse(inputBuffer));
                    Console.WriteLine($"\nWhat is the tempeture of the {gasName} in celcius?\n");
                    inputBuffer= Console.ReadLine();
                    gas.SetTemp(double.Parse(inputBuffer));
                    //  Use the IdealGas GetPressure method to pass the pressure in pascals to the
                    //  DisplayPressure method to display the pressure in Pascals and Degrees Celcius.
                    DisplayPresure(gas.GetPresure);
                }
            //  Ask the user if they want to do another.
            } while (DoAnother());
            //  Display a good bye message when they are done.
            Console.WriteLine($"\nThanks for using CChavezP2 for your gas calulation needs, with class.\n");
        }

    }
}
