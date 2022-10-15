using ItemChanger;
using System.Collections.Generic;


namespace Shuriken.Rando
{
    public static class ShurikenRando
    {
        public static void Hook(bool rando, bool ic)
        {
            if (ic) ItemDefinition.DefineItemsAndLocations();
            if (rando) RequestMaker.Hook();
            if (rando) LogicAdder.Hook();
        }

    }
}
