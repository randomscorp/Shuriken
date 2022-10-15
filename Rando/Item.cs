using ItemChanger;
using ItemChanger.UIDefs;
using ItemChanger.Locations;

using RandomizerCore;
using RandomizerCore.Logic;
using RandomizerCore.LogicItems;
using RandomizerMod.RC;
using RandomizerMod.Settings;
using RandomizerCore.Randomization;
using RandomizerMod.RandomizerData;

namespace Shuriken.Rando
{
    public class ShurikenItem : AbstractItem
    {
        protected override void OnLoad()
        {
            ItemChangerMod.Modules.GetOrAdd<ShurikenModule>();
        }

        public override void GiveImmediate(GiveInfo info)
        {
            ItemChangerMod.Modules.Get<ShurikenModule>().GiveShuriken();
        }
    }

    public static class ItemDefinition
    {
        public static void DefineItemsAndLocations()
        {
            AbstractItem Shuriken = new ShurikenItem()
            {
                name = "Shuriken",
                UIDef = new MsgUIDef()
                {
                    name = new BoxedString("Shuriken"),
                    shopDesc = new BoxedString("Zoooooooom"),
                    sprite = new ItemSprite()
                }
            };

            Finder.DefineCustomItem(Shuriken);
        }
    }


    class LogicAdder
    {
        public static void Hook()
        {
            RCData.RuntimeLogicOverride.Subscribe(50f, DefineTermsAndItems);
            RCData.RuntimeLogicOverride.Subscribe(50, ApplyLogic);
        }

        private static void ApplyLogic(GenerationSettings gs, LogicManagerBuilder lmb)
        {
            if (!Shuriken.GS.shurikenRando) return;


        }
        private static void DefineTermsAndItems(GenerationSettings gs, LogicManagerBuilder lmb)
        {
            if (!Shuriken.GS.shurikenRando) return;
            Term shurikenterm= lmb.GetOrAddTerm("SHURIKEN");
            if (Shuriken.GS.items == 0) Shuriken.GS.items = 1;
            if (Shuriken.GS.RandomizeTeleport&& Shuriken.GS.items == 1) Shuriken.GS.items = 2;

            lmb.AddItem(new SingleItem("Shuriken",new TermValue(shurikenterm, Shuriken.GS.items)));

        }
    }



}