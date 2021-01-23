using System;
using System.Collections.Generic;
using System.Text;

namespace MentorMateDevCamp
{
    /// <summary>
    /// The abstract class that represent main layer data.
    /// </summary>
    /// <remarks>
    /// It stores array of integers(bricks), height and width of the layer.
    /// </remarks>
    public abstract class Layer
    {
        #region Fields
         protected int[,] layerData; //array of integers, that represents the bricks
         protected int layerHeight; //integer that represents the N lines 
        protected int layerWidth;  //integer that represents the M numbers
        #endregion


        #region Constructors
        //We want only the inheriting child 
        //classes to have access to the 
        //constructor, that's why we make
        //the constructor protected.
        protected Layer(int height, int width)
        {
            layerHeight = height;
            layerWidth = width;
            layerData = new int[height, width];
        }
        #endregion


        #region Properties
        public int[,] LayerData
        {
            get { return layerData; }
            set {layerData = value; }
        }

        public int LayerWidth
        {
            get { return layerWidth; }
            set { layerWidth = value; }
        }

        public int LayerHeight
        {
            get { return layerHeight; }
            set { layerHeight = value; }
        }
        #endregion
    }
}
