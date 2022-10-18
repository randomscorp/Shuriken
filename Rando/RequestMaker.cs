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
    public static class RequestMaker
    {
        public static void Hook()
        {
            RequestBuilder.OnUpdate.Subscribe(50, AddShuriken);
            RequestBuilder.OnUpdate.Subscribe(-499.2f, SetupRefs);

        }

        private static void SetupRefs(RequestBuilder rb)
        {
            if ((!Shuriken.GS.RS.shurikenRando)) return;
            rb.EditItemRequest("Shuriken", info =>
            {
                info.getItemDef = () => new ItemDef()
                {
                    Name = "Shuriken",
                    Pool = "Shuriken",
                    MajorItem = false, // true if progressive
                    PriceCap = 500,

                };
            });

        }

        private static void AddShuriken(RequestBuilder rb)
        {

            if (Shuriken.GS.RS.shurikenRando)
            {
                Shuriken.LS.shurikenRando = true;
                if (Shuriken.GS.RS.StartWithShuriken) Shuriken.LS.shurikenLevel = 1;
                if (Shuriken.GS.RS.RandomizeTeleport) Shuriken.LS.hasTeleport = false;
                if (Shuriken.GS.RS.items == 0) Shuriken.GS.RS.items = 1;
                if (Shuriken.GS.RS.RandomizeTeleport && Shuriken.GS.RS.items == 1) Shuriken.GS.RS.items = 2;
                rb.AddItemByName("Shuriken", Shuriken.GS.RS.items);

            }

        }
    }
}
