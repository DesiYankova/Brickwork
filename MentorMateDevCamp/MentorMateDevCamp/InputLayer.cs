using System;
using System.Collections.Generic;
using System.Text;

namespace MentorMateDevCamp
{
    /// <summary>
    /// Class that represent the input layer.
    /// </summary>
    /// <remarks>
    /// In the constructor we fill the input layer with the input data of the user.
    /// </remarks>
    public class InputLayer: Layer
    {
        #region Constructors
        public InputLayer(int height, int width, int[,] inputData)
            :base(height, width)
        {
            layerData = inputData; //set the input layer data to be equal to the user input data.
            
        }
        #endregion
    }
}
