using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.Json.Serialization;

namespace EmployeeRegistry
{
    public class Clearance
    {
        public int ClearanceLevel { get; private set; }
        public string ClearanceLevelName { get; private set; }
        [JsonInclude]
        private int ClearanceColorR { get; set; }
        [JsonInclude]
        private int ClearanceColorG { get; set; }
        [JsonInclude]
        private int ClearanceColorB { get; set; }

        public Clearance(int clearanceLevel, string clearanceLevelName, int clearanceColorR, int clearanceColorG, int clearanceColorB)
        {
            ClearanceLevel = clearanceLevel;
            ClearanceColorR = clearanceColorR;
            ClearanceColorG = clearanceColorG;
            ClearanceColorB = clearanceColorB;
            ClearanceLevelName = clearanceLevelName;
        }

        public override string ToString()
        {
            return ClearanceLevelName;
        }

        public Color GetClearanceColor()
        {
            return Color.FromArgb(ClearanceColorR, ClearanceColorG, ClearanceColorB);
        }
    }
}
