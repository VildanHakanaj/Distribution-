using System;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

/*======================================================================
| Distribution For Simulations
|
| Name: Distribution for simulations
|
| Written by: Vildan Hakanaj - September 26th 2018
|
| Purpose: See how different distribution work and to use them
| Visualizes on graphs all distributions used in this program.
|
|
| Subroutines/libraries required:
| See using statements.
| this routine uses the MathNet.Numerics Distribution && Statistics
| math library. The program will include both packages so no need to install anything
| Those were taken from: https://rosettacode.org/wiki/Statistics/Normal_distribution#C 
|
|------------------------------------------------------------------
*/

namespace Vildan_Hakanaj_Lab1
{
    class Program
    {
        static readonly int MAX_STAR = 400;
        static Random rnd = new Random();
        private static int answer;

        static void Main(string[] args)
        {

            DisplayMenu(); //Displays a console menu

            do //Do while to ensure the user enters the correct answer and the correct format
            {
                Console.ForegroundColor = ConsoleColor.Cyan; //Change the color of the line
                Console.Write("Command #: ");
                Console.ResetColor();
                try //using a try catch block to check for the user input
                {
                    answer = Convert.ToInt32(Console.ReadLine());  //Get the answer from the user 
                    Console.Clear(); //Clear the console
                    DisplayMenu(); //Display the menu
                    Command(answer); //Process the answer given from the user
                }
                catch (FormatException err) //Print the error to the user and retake the answer
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(err.Message);
                    Console.ResetColor();
                    DisplayMenu();
                    Console.WriteLine("Press 5 to clear the console");

                }

            } while (answer != 7); //Quit when user enter command # 6 as their answer
        }

        /// <summary>
        /// This shows the distribution that comes form the rand function
        /// and only one input.
        /// </summary>
        /// <param name="numTrials">The number of trials</param>
        /// <param name="numBins">The number of bins</param>
        public static void Question1(int numTrials, int numBins)
        {
            int[] bin = new int[numBins]; //Create the bins
            int bin_pos; //Bin position
            for (int i = 0; i < numTrials; i++) //A loop to generate the x amout of random numbers
            {
                bin_pos = rnd.Next(numBins); //Generate the random numbers and use it to find the bin pos
                ++bin[bin_pos]; //Increment the appropriate bin
            }
            Console.WriteLine("\nQuestion 1\nThe number of trials: {0:N0}\nNumber of Bins: {1}\n", numTrials, numBins);
            CalculateStar(numTrials, numBins, bin, false); //Calculate and print the graph for the question
        }
        /// <summary>
        /// 
        /// Takes the value of 2 dices and add them together and find the bin for them
        /// and stores them into the bins 
        /// The graph we get out of this we see that the number 7 is the most common
        /// </summary>
        /// <param name="numTrials">Number of number to be generated</param>
        public static void Question2(int numTrials)
        {
            int numBins = 11; //predefined number of bins
            int[] bin = new int[numBins]; //Create the bin array
            for (int i = 0; i < numTrials; i++) //Loop numTrials of times to generate the random numbers 
            {
                ++bin[rnd.Next(1, 7) + rnd.Next(1, 7) - 2]; //i generate the 2 die number with a restrict min = 1 max = 6 and add them together 
                                                            // I then decrement it with a - 2 so it will be within the index of the bins 
            }
            Console.WriteLine("\nQuestion 2\nThe number of trials: {0:N0}\nNumber of Bins: {1}\n", numTrials, numBins);
            CalculateStar(numTrials, numBins, bin, true); //Calculate and print the graph
        }

        /// <summary>
        /// Question 3 
        /// Libraries and tool i used: MathNet
        /// install using nugpack from visual studio
        /// I have used code that was provided from 
        /// https://rosettacode.org/wiki/Statistics/Normal_distribution#C
        /// I have used the code given in this site for the solution of question 3
        /// 
        /// This show the normal distribution
        /// 
        /// </summary>
        /// <param name="numTrials">How many numbers are generated </param>
        static void Question3(int numTrials, int numBins)
        {
            double[] X = new double[numTrials]; //Create a array to contain the generated numbers 
            var norm = new Normal(12, 5); //Create the normal distribution passing a mean and a stdev 
            norm.Samples(X); //Create samples using the array 

            var histogram = new Histogram(X, numBins); //Create the histogram 
            Console.WriteLine("Sample size: {0:N0}", numTrials);
            Console.WriteLine("Number of Bins: {0}", numBins);
            var statistics = new DescriptiveStatistics(X); //Just contains the mean and stdev tha is used
            Console.WriteLine("  Mean: " + statistics.Mean); 
            Console.WriteLine("StdDev: " + statistics.StandardDeviation);
            Console.WriteLine("\n");
            for (int i = 0; i < numBins; i++)
            {
                string bar = new String('#', (int)(histogram[i].Count / numTrials * MAX_STAR)); //calculate the stars
                Console.WriteLine(" {0:0.0}\t : {1} {2: 0.00}%", histogram[i].LowerBound, bar, (histogram[i].Count / numTrials)); //print the bars 
            }
            Console.WriteLine();
        }

        /// <summary>
        /// This will show the poisson distribution 
        /// Question 3 
        /// Libraries and tool i used: MathNet
        /// install using nugpack from visual studio
        /// I have used code that was provided from 
        /// https://rosettacode.org/wiki/Statistics/Normal_distribution#C
        /// I have modified the code to give me a poisson distribution
        /// also comes with a histogram.
        /// I have used the code given in this site for the solution of question 4
        /// </summary>
        /// <param name="numTrials">The number of number to generate</param>
        /// <param name="lambda">The expected value</param>
        static void Question4(int numTrials, int lambda)
        {
            int[] X = new int[numTrials]; //just an array to store the generated number 
            var poisson = new Poisson(5); //Create a new poisson object provided from the library 
            poisson.Samples(X); //Create the sample 


            double[] Y = new double[numTrials]; //For this part of the code because the histogram doesn't accept integer array i have to convert them into double 

            for (int i = 0; i < numTrials; i++) //By using this for loop and transfering the numbers into a double
            {
                Y[i] = X[i];
            }

            const int numBuckets = 10; //number of buckets
            var histogram = new Histogram(Y, numBuckets); //The histogram will create a histogram based on the number of buckets and the sample array
            Console.WriteLine("Sample size: {0:N0}", numTrials);
            Console.WriteLine("The lambda value: {0}\n", lambda);
            for (int i = 0; i < numBuckets; i++) 
            {
                string bar = new String('#', (int)(histogram[i].Count * MAX_STAR / numTrials));
                Console.WriteLine("bin" + i + "  {0:0.00} \t: {1} {2: 0.00}%", histogram[i].LowerBound, bar, histogram[i].Count / numTrials);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// This method calculates the number of stars that should be printed
        /// I make sure to print out the right number of buckets by passing a bool so i 
        /// know if i should print for question one or question 2
        /// </summary>
        /// <param name="numTrials">The number of trials</param>
        /// <param name="num_bins">Number of bins there are</param>
        /// <param name="bin">The bin array to calculate from</param>
        /// <param name="dice">this is just to ensure to print the right number of buckets for question 1 and question 2</param>
        public static void CalculateStar(int numTrials, int num_bins, int[] bin, bool dice)
        {
            for (int i = 0; i < num_bins; i++) 
            {
                string bar = new string('#', (int)(MAX_STAR * bin[i] / (double)numTrials)); //Calculate the number of stars and create a string with that number
                if (dice == true) //Print the number of buckets for queston2 
                {
                    Console.WriteLine("Bin # {0}: {1}\t  |{2}", i + 2, bin[i], bar);
                }
                else //For question 1
                {
                    Console.WriteLine("Bin # {0}: {1}\t  |{2}", i + 1, bin[i], bar);
                }

            }
        }

        /// <summary>
        /// Just a simple console menu
        /// In order to use just press the number beside the line 
        /// </summary>
        public static void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("This application was written by: ||| Vildan Hakanaj |||");
            Console.ResetColor();

            Console.WriteLine("Enter the number of the command you want to run\n");
            Console.WriteLine("1.Question 1");
            Console.WriteLine("2.Question 2");
            Console.WriteLine("3.Question 3");
            Console.WriteLine("4.Question 4");
            Console.WriteLine("5.Clear console");
            Console.WriteLine("6.Autorun all programs with default values\n");
            Console.WriteLine("7.Exit the application\n");
        }

        /// <summary>
        /// Takes the user command and executes the appropriate code.
        /// </summary>
        /// <param name="answer">users command input</param>
        public static void Command(int answer)
        {
            //This block will take care of choosing the question to run based on the user input
            int numTrials, numBins; 
            switch (answer)
            {
                case 1: //Question 1
                    Console.Write("Enter the number of trials: ");
                    numTrials = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nEnter the number of bins: ");
                    numBins = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Question1(numTrials, numBins);
                    break;
                case 2: //Question 2
                    Console.Write("Enter the number of trials: ");
                    numTrials = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Question2(numTrials);
                    break;
                case 3: //Question 3
                    Console.Write("Enter the number of trials: ");
                    numTrials = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nEnter the number of bins: ");
                    numBins = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Question3(numTrials, numBins);
                    break;
                case 4: //Question 4
                    Console.Write("Enter the number of trials: ");
                    numTrials = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nEnter Lambda: ");
                    int lambda = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Question4(numTrials, lambda);
                    break;
                case 5: //Clear the console
                    Console.Clear();
                    DisplayMenu();
                    break;
                case 6: //Run all programs at once. with 3 different number trials
                    Question1(10000, 10);
                    Question1(100000, 10);
                    Question1(1000000, 10);
                    Console.WriteLine();
                    Question2(10000);
                    Question2(100000);
                    Question2(1000000);
                    Console.WriteLine("\nQuestion 3\n");
                    Question3(10000, 30);
                    Question3(100000, 30);
                    Question3(1000000, 30);
                    Console.WriteLine("\nQuestion 4\n");
                    Question4(10000, 5);
                    Question4(100000, 5);
                    Question4(1000000, 5);

                    break;
                default:
                    Console.Clear();
                    DisplayMenu();
                    break;
            }
        }
    }
}
