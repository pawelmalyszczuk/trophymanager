using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrophyManagerNew
{
    class DataGridColors
    {
        public int row;
        public int col;
        public string color;

        public DataGridColors(int row, int col, double value)
        {
            this.row = row;
            this.col = col;
            this.color = getColorByValue(value);
        }

        private string getColorByValue(double value)
        {
            if (value < 10)
                return "grey";
            else if (value < 15)
                return "green";
            else return "blue";
        }
    }
}
