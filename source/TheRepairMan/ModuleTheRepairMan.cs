namespace Aelfhe1m.TheRepairMan
{
    public class ModuleTheRepairMan : PartModule
    {
        [KSPField]
        public float EVARange = 5f;

        [KSPField]
        public string Menu = "Do Repairs";

        [KSPEvent(active = true, guiActiveUnfocused = true, externalToEVAOnly = true, guiName = "Do Repairs", unfocusedRange = 5f)]
        public void DoRepairs()
        {
            var kerbal = FlightGlobals.ActiveVessel.rootPart.protoModuleCrew[0];
            if (!kerbal.HasEffect("RepairSkill"))
            {
                ScreenMessages.PostScreenMessage("Only Kerbals with repair skills can do repairs!", 5f, ScreenMessageStyle.UPPER_CENTER);
                return;
            }

            UnityEngine.Debug.Log("[A_TRM]: Do repairs");
            ScreenMessages.PostScreenMessage("Doing repairs", 5f, ScreenMessageStyle.UPPER_CENTER);

        }

        public override void OnStart(StartState state)
        {
            // make range and menu action name configurable
            Events["DoRepairs"].unfocusedRange = EVARange;
            Events["DoRepairs"].guiName = Menu;
            base.OnStart(state);
        }

        public void FixedUpdate()
        {
            if (HighLogic.LoadedScene == GameScenes.EDITOR)
                return;

            // do check for repairable parts
            var repairableParts = false;
            if (repairableParts)
            {
                Events["DoRepairs"].active = false;
            }
            else
            {
                Events["DoRepairs"].active = true;
            }
        }
    }
}
