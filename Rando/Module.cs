using HutongGames.PlayMaker;
using ItemChanger;
using ItemChanger.Extensions;
using ItemChanger.FsmStateActions;
using ItemChanger.Modules;

namespace Shuriken.Rando
{
    class ShurikenModule : Module
    {
        public override void Initialize()
        {
            if (!Shuriken.GS.shurikenRando) return;

        }

        public override void Unload()
        {
        }

        public void GiveShuriken()
        { 
            Shuriken.Instance.Log("shuriken given");
            if(Shuriken.LS.shurikenLevel>0 && Shuriken.LS.hasTeleport==false) Shuriken.LS.hasTeleport=true;
            else Shuriken.LS.shurikenLevel += 1;
        }
    }
}
