using System.Collections.Generic;

namespace Aelfhe1m.TheRepairMan
{
    public class ModuleTheRepairMan : PartModule
    {
        [KSPField]
        public float EVARange = 5f;

        [KSPField]
        public string Menu = "Do Repairs";

        [KSPField]
        public int MinSolarPanelRepairLevel = 2;

        [KSPField]
        public int MinAntennaeRepairLevel = 1;

        [KSPField]
        public int MinRadiatorRepairLevel = 3;

        [KSPEvent(active = true, guiActiveUnfocused = true, externalToEVAOnly = true, guiName = "Do Repairs", unfocusedRange = 5f)]
        public void DoRepairs()
        {
            var kerbal = FlightGlobals.ActiveVessel.rootPart.protoModuleCrew[0];
            if (!kerbal.HasEffect("RepairSkill"))
            {
                ScreenMessages.PostScreenMessage("Only Kerbals with repair skills can do repairs!", 5f, ScreenMessageStyle.UPPER_CENTER);
                return;
            }

            var repairables = FindRepairables();

            if (repairables.Count > 0)
            {
                var repaired = RepairAntennae(kerbal, repairables);
                if (repaired > 0)
                    ScreenMessages.PostScreenMessage($"Repaired {repaired} antennae", 5f, ScreenMessageStyle.UPPER_CENTER);

                repaired = RepairSolarPanels(kerbal, repairables);
                if (repaired > 0)
                    ScreenMessages.PostScreenMessage($"Repaired {repaired} solar panels", 5f, ScreenMessageStyle.UPPER_CENTER);

                repaired = RepairRadiators(kerbal, repairables);
                if (repaired > 0)
                    ScreenMessages.PostScreenMessage($"Repaired {repaired} radiators", 5f, ScreenMessageStyle.UPPER_CENTER);

            }
            else
            {
                ScreenMessages.PostScreenMessage("Nothing to repair", 5f, ScreenMessageStyle.UPPER_CENTER);
            }

        }

        public override void OnStart(StartState state)
        {
            // make range and menu action name configurable
            Events["DoRepairs"].unfocusedRange = EVARange;
            Events["DoRepairs"].guiName = Menu;
            base.OnStart(state);
        }

        public Repairables FindRepairables()
        {
            var found = new Repairables();

            found.Antennae = this.vessel.FindPartModulesImplementing<ModuleDeployableAntenna>();
            found.SolarPanels = this.vessel.FindPartModulesImplementing<ModuleDeployableSolarPanel>();
            found.Radiators = this.vessel.FindPartModulesImplementing<ModuleDeployableRadiator>();

            return found;
        }

        public int RepairAntennae(ProtoCrewMember kerbal, Repairables repairables)
        {
            if (kerbal.experienceLevel > MinAntennaeRepairLevel)
            {
                return repairables.Antennae.Count;
            }
            else
            {
                ScreenMessages.PostScreenMessage($"Engineer must be at least level {MinAntennaeRepairLevel} to repair antennae.", 5f, ScreenMessageStyle.UPPER_CENTER);
                return 0;
            }
        }

        public int RepairSolarPanels(ProtoCrewMember kerbal, Repairables repairables)
        {
            if (kerbal.experienceLevel > MinSolarPanelRepairLevel)
            {
                return repairables.SolarPanels.Count;
            }
            else
            {
                ScreenMessages.PostScreenMessage($"Engineer must be at least level {MinSolarPanelRepairLevel} to repair solar panels.", 5f, ScreenMessageStyle.UPPER_CENTER);
                return 0;
            }
        }

        public int RepairRadiators(ProtoCrewMember kerbal, Repairables repairables)
        {
            if (kerbal.experienceLevel > MinRadiatorRepairLevel)
            {
                return repairables.Radiators.Count;
            }
            else
            {
                ScreenMessages.PostScreenMessage($"Engineer must be at least level {MinRadiatorRepairLevel} to repair radiators.", 5f, ScreenMessageStyle.UPPER_CENTER);
                return 0;
            }
        }

        public void FixedUpdate()
        {
            if (HighLogic.LoadedScene == GameScenes.EDITOR)
                return;

            // do check for repairable parts
            var repairables = FindRepairables();
            if (repairables.Count > 0)
            {
                Events["DoRepairs"].active = true;
            }
            else
            {
                Events["DoRepairs"].active = false;
            }
        }
    }
}
