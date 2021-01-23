using System;
using System.Collections.Generic;
using System.Text;

namespace MentorMateDevCamp
{
    /// <summary>
    /// This class is used to find a solution for the second layer of brickwork.
    /// </summary>
    /// <remarks>
    /// It stores the input data for the dimensions and layer data (bricks) and lays the second layer of bricks.
    /// </remarks>
    class Brickwork
    {
        #region Fields
        private static int[] dimentions = Reader.DimensionsRead();
        private static int rows = dimentions[0];
        private static int columns = dimentions[1];

        private static int[,] firstLayerRead = Reader.FirstLayerRead(rows, columns);
        private InputLayer inputLayer = new InputLayer(rows, columns, firstLayerRead);

        private OutputLayer outputLayer = new OutputLayer(rows, columns);
        #endregion

        #region Public methods
        /// <summary>
        /// A method in which the function for constructing the second layer - LaySecondLayerOfBricks() and 
        /// printing it -PrintWithAsterisks()/Print() is called.
        /// </summary>
        public void Run()
        {
            LaySecondLayerOfBricks();
            Console.WriteLine();

            if(IsThereASolution())
            {
                outputLayer.PrintWithAsterisks();
            }
            else
            {
                Console.WriteLine("-1");
                Console.WriteLine("No solution exists!");
            }

             
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Finds the solution of the second layer of bricks.
        /// </summary>
        private void LaySecondLayerOfBricks()
        {
            int brickNumber = 1; //the number that denotes the current brick

            for (int row = 0; row < rows; row += 2) //we check two by two parts => += 2
            {
                for(int column = 0; column < columns; column += 2)
                {
                    //Check whether two neighboring parts are part of one horizontal brick.
                    //If the two parts are part of one horizontal brick:
                    if (inputLayer.LayerData[row, column] == inputLayer.LayerData[row, column + 1]) 
                    {
                        //If previous brick is vertical, we build two horizontals on top of each other followed by one vertical
                        if (column != 0 && outputLayer.LayerData[row, column - 1] == 0 && column - 1 % 2 != 0)
                        {
                            //first horizontal (above second one)
                            outputLayer.LayerData[row, column - 1] = brickNumber;
                            outputLayer.LayerData[row, column] = brickNumber;
                            brickNumber++;
                            
                            //second horizontal (under the first one)
                            outputLayer.LayerData[row + 1, column - 1] = brickNumber;
                            outputLayer.LayerData[row + 1, column] = brickNumber;
                            brickNumber++;

                            //one vertical, next to the two horizontal
                            outputLayer.LayerData[row, column + 1] = brickNumber;
                            outputLayer.LayerData[row + 1, column + 1] = brickNumber;
                            brickNumber++; 

                            //  .------.---.
                            //  |      |   |   
                            //  .------|   |
                            //  |      |   |
                            //  .------.---.

                        }
                        //If previous brick is horizontal, we build one vertical brick
                        else //(4)
                        {
                            outputLayer.LayerData[row, column] = brickNumber;
                            outputLayer.LayerData[row + 1, column] = brickNumber;
                            brickNumber++;  

                            //  .---.
                            //  |   |
                            //  |   |
                            //  |   |
                            //  .---.

                        }
                    }
                    //If the two neighboring parts are not part of one horizontal brick 
                    else   
                    {
                        //If previous brick is vertical, we build two horizontals on top of each other followed by one vertical
                        if (column != 0 && outputLayer.LayerData[row, column - 1] == 0 && column - 1 % 2 != 0) 
                        {
                            //first horizontal (above second one)
                            outputLayer.LayerData[row, column - 1] = brickNumber;
                            outputLayer.LayerData[row, column] = brickNumber;
                            brickNumber++;

                            //second horizontal (under the first one)
                            outputLayer.LayerData[row + 1, column - 1] = brickNumber;
                            outputLayer.LayerData[row + 1, column] = brickNumber;
                            brickNumber++;

                            //one vertical, next to the two horizontal
                            outputLayer.LayerData[row, column + 1] = brickNumber;
                            outputLayer.LayerData[row + 1, column + 1] = brickNumber;
                            brickNumber++;

                            //  .------.---.
                            //  |      |   |   
                            //  .------|   |
                            //  |      |   |
                            //  .------.---.

                        }

                        //If the prevous brick is horizontal, we build two horizontals on top of each other
                        else
                        {
                            //first horizontal (above second one)
                            outputLayer.LayerData[row, column] = brickNumber;
                            outputLayer.LayerData[row, column + 1] = brickNumber;
                            brickNumber++;

                            //second horizontal (under the first one)
                            outputLayer.LayerData[row + 1, column] = brickNumber;
                            outputLayer.LayerData[row + 1, column + 1] = brickNumber;
                            brickNumber++;

                            //  .------.
                            //  |      |     
                            //  .------|   
                            //  |      |   
                            //  .------.

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks for valid solution.
        /// </summary>
        /// <returns>True is the solution is valid, False if the solution is not valid.</returns>
        private bool IsThereASolution()
        {
            //Check for blanks in the new matrix:
            if (Validator.HaveNull(outputLayer.LayerData))
            {
                return false;
            }

            //Checks if a brick from the new layer lies on a brick from the first layer:
            for (int row = 0; row < outputLayer.LayerHeight; row++)
            {
                for (int column = 0; column < outputLayer.LayerWidth; column++)
                {
                    if(column < outputLayer.LayerWidth - 1)
                    {
                        //Check for equal horizontal bricks:
                        if (IsHorizontalBrickInOutput(row, column))
                        {
                            if(outputLayer.LayerData[row, column] == inputLayer.LayerData[row, column] &&
                               outputLayer.LayerData[row, column + 1] == inputLayer.LayerData[row, column + 1])
                            {
                                return false;
                            }
                        }
                        //Check for equal vertical bricks:
                        else
                        {
                            if (row < outputLayer.LayerHeight - 1)
                            {
                                if(outputLayer.LayerData[row, column] == inputLayer.LayerData[row, column] &&
                                    outputLayer.LayerData[row + 1, column] == inputLayer.LayerData[row + 1, column])
                                {
                                    return false;
                                }
                            }
                            
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the brick is horizontal.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>True if it is horizontal, False if it is not horizontal.</returns>
        private bool IsHorizontalBrickInOutput(int row, int col)
        {
            if(outputLayer.LayerData[row, col] == outputLayer.LayerData[row, col+1])
            {
                return true;
            }
            return false;
        }
        #endregion
        
    }
}
