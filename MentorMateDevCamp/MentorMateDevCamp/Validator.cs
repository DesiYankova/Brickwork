using System;
using System.Collections.Generic;
using System.Text;

namespace MentorMateDevCamp
{
    /// <summary>
    /// Class for validating the input data.
    /// </summary>
    /// <remarks>
    /// This class can validate the dimensions, layer lenght, brick numbers and whether the bricks are in two parts.
    /// </remarks>
    public class Validator
    {
        #region Public methods
        /// <summary>
        /// Checks if the dimensions are valid.
        /// </summary>
        /// <param name="height">The number of lines-N.</param>
        /// <param name="width">The number of numbers-M.</param>
        /// <returns>True if the dimensions are valid, False if the dimensions are not valid.</returns>
        public static bool ValidateDimensions(int height, int width)
        {
            //N and M must be bigger than 0, lesser than 100 and even.
            //If it does not meet the conditions, we throw an exception:
            if (height >= 100 || height <= 0 || height % 2 != 0 
                || width % 2 != 0   || width >= 100 || width <= 0)
            {
                //if we want exception message, but I prefer to ask for new input, than throw an exception 
                //throw new Exception("-1 N and M must be even, bigger than 0 and lesser than 100!"); 

                Console.WriteLine();
                Console.WriteLine("N and M must be even, bigger than 0 and lesser than 100!");
                return false;
            }
            if(height > width)
            {
                //if we want exception message, but I prefer to ask for new input, than throw an exception 
                //throw new Exception("N must be smaller that M");

                Console.WriteLine();
                Console.WriteLine("N must be smaller that M");
                return false;
            }
            return true; 
        }

        /// <summary>
        /// Checks if the current line of the input data is with the correct lenght.
        /// </summary>
        /// <param name="currentRowLength">The lenght of the current line.</param>
        /// <param name="inputLengthOfTheFirstLayer">The input length of the line.</param>
        /// <returns>True if the current layer lenght is valid, False if the current layer lenght is not valid.</returns>
        public static bool ValidateLayerLength(int currentRowLength, int inputLengthOfTheFirstLayer)
        {
            //The length of the current line must be the same as the input length of the line.
            //If it does not meet this condition, we throw an exception:
            if (currentRowLength != inputLengthOfTheFirstLayer)
            {
                //if we want exception message, but I prefer to ask for new input, than throw an exception 
                //throw new Exception("-1 The number of bricks of the new layer line must be the same as the number in previous one!"); 

                Console.WriteLine();
                Console.WriteLine("The number of bricks of the new layer line must be the same as the number in previous one!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the number denoting the brick is valid.
        /// </summary>
        /// <param name="number">The number that denote the brick.</param>
        /// <param name="heigth">The number of lines-N.</param>
        /// <param name="width">The number of numbers-M.</param>
        /// <returns>True if the number that denotes the brick is valid, False if the number that denotes the brick is not valid.</returns>
        public static bool ValidateBrickNumber(int number, int heigth, int width)
        {
            //The max possible number that can denote the bricks is heigth * width / 2, because each brick is from two parts:
            int maxPossibleNumber = (heigth * width) / 2;
            //The numbers that can denote the bricks starts from 1 to the number of bricks.
            //If it does not meet this condition, we throw an exception:
            if ( number <= 0 || number > maxPossibleNumber)
            {
                //if we want exception message, but I prefer to ask for new input, than throw an exception 
                //throw new Exception("-1 The bricks must be numbered from 1 to " + ( (heigth * width) / 2) );
                Console.WriteLine();
                Console.WriteLine("The bricks must be numbered from 1 to " + ((heigth * width) / 2) );
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the bricks consist of two parts.
        /// </summary>
        /// <param name="firstLayerOfBricks">The input layer.</param>
        /// <returns>True if the bricks consist of two parts, False if the bricks does not consist of two parts.</returns>
        public static bool ValidateBricks(int[,] firstLayerOfBricks)
        {
            //Use a dictionary to keep track of the different bricks count.
            Dictionary<int, int> countPartsOfEachBricks = new Dictionary<int, int>();

            for(int row = 0; row < firstLayerOfBricks.GetLength(0); row++)
            {
                for(int column = 0; column < firstLayerOfBricks.GetLength(1); column++)
                {
                    int currentVal = firstLayerOfBricks[row, column];

                    //If the dictionary already contains the currentVal (current brick number), 
                    //we increase the value in the dictionary by the current key - currentVal:
                    if (countPartsOfEachBricks.ContainsKey(currentVal))
                    {
                        countPartsOfEachBricks[currentVal]++;
                    }
                    //If the dictionary does not contain the currentVal,
                    //we add the new key - currentVal and value = 1 (because this is the first part of currentVal):
                    else
                    {
                        countPartsOfEachBricks.Add(currentVal, 1);
                    }
                }  
            }

            //Check if any of the bricks consist of more or less than two part,
            //-occupies more than 2 rows or 2 columns:
            foreach (var (key, value) in countPartsOfEachBricks)
            {
                //If the brick consists of more than two parts, we throw an exception:
                if (value > 2)
                {
                    //if we want exception message, but I prefer to ask for new input, than throw an exception 
                    //throw new Exception("-1 Тhe brick should not occupy more than 2 rows or 2 columns!");
                    
                    Console.WriteLine();
                    Console.WriteLine("Тhe brick should not occupy more than 2 rows or 2 columns!");
                    return false;
                    
                }
                //If the brick consists of less than two parts, we throw an exception:
                else if (value < 2)
                {
                    //if we want exception message, but I prefer to ask for new input, than throw an exception 
                    //throw new Exception("-1 Тhe brick should occupy 2 rows or 2 columns!");

                    Console.WriteLine();
                    Console.WriteLine("Тhe brick should occupy 2 rows or 2 columns!");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check if any of the bricks in the new layer is with number 0.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>True if it have 0 as a number, False if it does not have 0 as a number.</returns>
        public static bool HaveNull(int[,] layerToCheck)
        {
            for (int row = 0; row < layerToCheck.GetLength(0); row++)
            {
                for (int col = 0; col < layerToCheck.GetLength(1); col++)
                {
                    if (layerToCheck[row, col] == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
