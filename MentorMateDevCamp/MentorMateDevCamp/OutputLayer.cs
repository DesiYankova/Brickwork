using System;
using System.Collections.Generic;
using System.Text;

namespace MentorMateDevCamp
{
    /// <summary>
    /// Class that represent the output layer. 
    /// </summary>
    /// <remarks>
    /// In the constuctor we fill the  output layer data with nulls, which later we will change to the brick numbers.
    /// This class can print the solution with or without asterics.
    /// </remarks>
    class OutputLayer : Layer
    {
        #region Constructors
        public OutputLayer(int height, int width)
            : base(height, width)
        {
            layerData = new int[height, width];
            FillWithNulls(height, width); //fill the output layer data with nulls
        }
        #endregion


        #region Public methods
        /// <summary>
        /// Prints the output layer bricks, surrounded with astericks.
        /// </summary>
        public void PrintWithAsterisks()
        {

            for (int row = 0; row < layerHeight; row++)
            {
                //if we are on the first row, we print  whole row of '*':
                if (row == 0)
                {
                    int UpAndDownAsterics = layerWidth * 9 + 1;

                    for(int i = 0; i < UpAndDownAsterics; i++)
                    {
                        CementGrayAstericsWrite("*");
                    }
                    Console.WriteLine();
                }

                //Two parts of one horizontal brick?
                //Cycle to scan the rows of each brick
                for (int brickRow = 0; brickRow < 3; brickRow++) //In my case each brick has 3 rows
                {
                    for (int column = 0; column < layerWidth; column++) //Check each column
                    {
                        //If we are on the first column, we print one "*"
                        if (column == 0)
                        {
                            CementGrayAstericsWrite("*");
                        }

                        //Check the rows, on which we don't have brick numbers:
                        if (brickRow != 1)
                        {
                            //If it is not from the first part of a horizontal brick:
                            if ((column + 1 < layerWidth && !IsTheFirstPartOfHorizontalBrick(row, column)) || column == layerWidth - 1)
                            {
                                BrickRedWrite(" ".PadLeft(8));
                                CementGrayAstericsWrite("*");
                            }
                            //If it is from the first part of a horizontal brick:
                            else
                            {
                                BrickRedWrite(" ".PadLeft(9));
                            }
                        }
                        //If we are on the row with brick numbers, we output the number
                        else
                        {
                            //If it is not from the first part of a horizontal brick:
                            if ((column + 1 < layerWidth && !IsTheFirstPartOfHorizontalBrick(row, column)) || column == layerWidth - 1) 
                            {
                                BrickRedWrite(layerData[row, column].ToString().PadLeft(6) + "  ");
                                CementGrayAstericsWrite("*");
                            }
                            //If it is from the first part of a horizontal brick:
                            else
                            {
                                BrickRedWrite(layerData[row, column].ToString().PadLeft(6));
                                BrickRedWrite(" ".PadLeft(3));
                            }
                        }
                    }
                    Console.WriteLine();
                }

                //Two parts of one vertical bricks?
                for (int column = 0; column < layerWidth; column++)
                {
                    //If we are on the first column, we print one "*"
                    if (column == 0)
                    {
                        CementGrayAstericsWrite("*");
                    }
                    
                    //If it is not from the first part of vertical brick:
                    if ((row + 1 < LayerHeight && !IsTheFirstPartOfVerticalBrick(row,column)) || row == layerHeight - 1)
                    {
                        //We have two bricks, stacked on top of each other, so we separate them with asterics:
                        CementGrayAstericsWrite("*********");
                    }
                    //If it is from the first part of vertical brick:
                    else
                    {
                        //we output spaces to indicate that they are two parts of a vertical brick
                        BrickRedWrite(" ".PadLeft(8));
                        CementGrayAstericsWrite("*");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints the output layer bricks without astericks.
        /// </summary>
        public void Print()
        {
            for (int rows = 0; rows < layerHeight; rows++)
            {
                for (int columns = 0; columns < layerWidth; columns++)
                {
                    Console.Write(layerData[rows, columns] + " ");
                }
                Console.WriteLine();
            }
        }
        #endregion


        #region Private methods
        /// <summary>
        /// Prints the output on gray background with yellow foreground color.
        /// We use it to print the borders of the brick.
        /// </summary>
        /// <param name="value">The value to be printed.</param>
        private void CementGrayAstericsWrite(string value)
        {
            //Set colors
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(value.PadRight(1));
            // Reset the color.
            Console.ResetColor();
        }

        /// <summary>
        /// Prints the output on red background with yellow foreground color.
        /// We use it to print the bricks.
        /// </summary>
        /// <param name="value">The value to be printed.</param> 
        private void BrickRedWrite(string value)
        {
            //Set colors
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(value.PadRight(1));
            // Reset the color.
            Console.ResetColor();
        }

        /// <summary>
        /// Checks if this position is part of horizontally placed brick.
        /// </summary>
        /// <param name="row">The number of the row between two brick parts.</param>
        /// <param name="column">The number of the column between two brick parts.</param>
        /// <returns>True if the position is part of horizontal brick, False if the position is not part of horizontal brick.</returns>
        private bool IsTheFirstPartOfHorizontalBrick(int row, int column)
        {
            if(layerData[row, column] == layerData[row, column + 1])
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if this position is part of vertically placed brick.
        /// </summary>
        /// <param name="row">The number of the row between two brick parts.</param>
        /// <param name="column">The number of the column between two brick parts.</param>
        /// <returns>True if the position is part of vertical brick, False if the position is not part of vertical brick.</returns>
        private bool IsTheFirstPartOfVerticalBrick(int row, int column)
        {
            if(layerData[row, column] == layerData[row+1, column])
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Fills the output layer data with nulls.
        /// </summary>
        /// <param name="height">The number of lines-N.</param>
        /// <param name="width">The number of numbers-M.</param>
        private void FillWithNulls(int height, int width)
        {
            for (int rows = 0; rows < height; rows++)
            {
                for (int columns = 0; columns < width; columns++)
                {
                    this.layerData[rows, columns] = 0;
                }
            }
        }
        #endregion
    }
}
