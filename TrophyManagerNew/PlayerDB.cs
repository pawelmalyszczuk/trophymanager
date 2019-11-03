using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrophyManagerNew
{
    class PlayerDB
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Age { get; set; }
        public string Position { get; set; }      
        public string Code { get; set; }
        public double ASI { get; set; }
        public double ASIChange { get; set; }
        public double Strength { get; set; }
        public double StrengthChange { get; set; }
        public double Stamina { get; set; }
        public double StaminaChange { get; set; }
        public double Pace { get; set; }
        public double PaceChange { get; set; }
        public double Marking { get; set; }
        public double MarkingChange { get; set; }
        public double Tacking { get; set; }
        public double TackingChange { get; set; }
        public double Workrate { get; set; }
        public double WorkrateChange { get; set; }
        public double Positioning { get; set; }
        public double PositioningChange { get; set; }
        public double Passing { get; set; }
        public double PassingChange { get; set; }
        public double Crossing { get; set; }
        public double CrossingChange { get; set; }
        public double Technique { get; set; }
        public double TechniqueChange { get; set; }
        public double Heading { get; set; }
        public double HeadingChange { get; set; }
        public double Finishing { get; set; }
        public double FinishingChange { get; set; }
        public double Longshots { get; set; }
        public double LongshotsChange { get; set; }
        public double SetPieces { get; set; }
        public double SetPiecesChange { get; set; }
        public int CountChangesAttrib { get; set; }
        public int IntensivityLastTraning { get; set; }
        
    }
}
