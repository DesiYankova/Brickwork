using System;
using System.Collections.Generic;
using System.Text;


namespace MentorMateDevCamp
{
    /// <summary>
    /// Class for reading the input data.
    /// </summary>
    /// <remarks>
    /// This class can read dimensions and input data for the first layer.
    /// </remarks>
    public class Reader
    {
        #region Public methods
        /// <summary>
        /// Reads the dimensions.
        /// </summary>
        /// <returns>One-dimensional array of int type, containing the layer dimensions.</returns>
        public static int[] DimensionsRead()
        {
            bool isValid = false;
            int[] dimensions = new int[2];
            string[] dims;

            do
            {
                Console.WriteLine("Please enter the size of the brick wall -N x M,  separated by space, ");
                Console.WriteLine("where N and M must be even, bigger than 0 and lesser than 100: ");

                dims = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries); //N and M are separated by a space
                                                                                             //we want to skip the empty ones

                //The dimensions must be two.
                //If it does not meet this condition, we throw an exception:
                if (dims.Length != 2)
                {
                    //if we want exception message, but in this case I prefer to ask for new input, than throw an exception 
                    //throw new Exception("-1 The dimensions must be two!");
                    Console.WriteLine();
                    Console.WriteLine("The dimensions must be two!");
                }
                else
                {
                    //Check if the input for the dimensions is only from integers, and if so put it in the array:
                    isValid = (int.TryParse(dims[0], out dimensions[0])) && (int.TryParse(dims[1], out dimensions[1]));

                    //If the input is from integers, we set the height and width of the input layer:
                    if (isValid)
                    {
                        int height = dimensions[0];
                        int width = dimensions[1];
                        //and check wether the dimensions are valid - bigger than 0, lesser than 100 and even.
                        isValid = Validator.ValidateDimensions(height, width);
                    }
                    //If the input for the dimensions is not from integers, we throw an exeption:
                    else
                    {
                        //if we want exception message, but in this case I prefer to ask for new input, than throw an exception 
                        //throw new Exception("Attempted conversion failed.");
                        Console.WriteLine();
                        Console.WriteLine("Attempted conversion failed.");
                    }
                }
            } while (!isValid);
            return dimensions;
        }

        /// <summary>
        /// Reads the input data(bricks) of the first layer.
        /// </summary>
        /// <param name="height">The number of lines-N.</param>
        /// <param name="width">The number of numbers-M.</param>
        /// <returns>Two-dimensional array of int type, containing the input layer data - bricks.</returns>
        public static int[,] FirstLayerRead(int height, int width)
        {
            //Create Two - dimensional array of N lines and M  numbers.
            int[,] firstLayer = new int[height, width];
            
            bool isValid;
            do
            {
                Console.WriteLine("Please enter the layout of the first layer of the wall - N lines M numbers separated by a space: ");

                isValid = true;

                for (int row = 0; row < height; row++)
                {
                    string[] strCurrRow = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries); //the numbers are separated by a space
                                                                                                                //we want to skip the empty ones

                    //If the currently entered line is not with the correct length:
                    if (!Validator.ValidateLayerLength(strCurrRow.Length, width))
                    {
                        isValid = false;
                        break;
                    }

                    for (int column = 0; column < width; column++)
                    {
                        //Check if the input for the current line of bricks is only from integers, and if so put it in the array:
                        isValid = int.TryParse(strCurrRow[column], out firstLayer[row, column]);

                        //If the input is not from integers, we throw an exception:
                        if (isValid == false)
                        {
                            row--;
                            Array.Clear(firstLayer, 0, height * width);

                            //if we want exception message, but in this case I prefer to ask for new input, than throw an exception 
                            //throw new Exception("Attempted conversion failed.");
                            Console.WriteLine();
                            Console.WriteLine("Attempted conversion failed.");
                            break;
                        }

                        //Check if the number denoting the brick is valid:
                        isValid = Validator.ValidateBrickNumber(firstLayer[row, column], height, width);
                        if (isValid == false)
                        {
                            row--;
                            Array.Clear(firstLayer, 0, height * width);
                            break;
                        }
                    }
                    
                }
                if (isValid)
                {
                        isValid = Validator.ValidateBricks(firstLayer);
                        if(!isValid)
                        {
                             Array.Clear(firstLayer, 0, height * width);
                        }
                }
                
            } while (!isValid);

            return firstLayer;
        }
        #endregion
    }
}
