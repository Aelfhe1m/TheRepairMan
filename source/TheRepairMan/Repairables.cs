using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aelfhe1m.TheRepairMan
{
    public class Repairables
    {
        public List<ModuleDeployableAntenna> Antennae = new List<ModuleDeployableAntenna>();
        public List<ModuleDeployableSolarPanel> SolarPanels = new List<ModuleDeployableSolarPanel>();
        public List<ModuleDeployableRadiator> Radiators = new List<ModuleDeployableRadiator>();

        public int Count
        {
            get
            {
                return Antennae.Count + SolarPanels.Count + Radiators.Count;
            }
        }
    }
}
