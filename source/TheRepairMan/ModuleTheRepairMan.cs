namespace Aelfhe1m.TheRepairMan
{
    public class ModuleTheRepairMan : PartModule
    {
        [KSPEvent(guiActive = true, guiActiveEditor = false, guiName = "Do Repairs")]
        public void DoRepairs()
        {
            var callingPart = this.part;

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
