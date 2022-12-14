using InControl;
using Modding;
using Modding.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Mono.Security.X509.X520;

namespace Shuriken
{
    public class SGlobalSettings
    {
        [JsonConverter(typeof(PlayerActionSetConverter))]
        public KeyBinds shuriknBind = new();
        [JsonConverter(typeof(PlayerActionSetConverter))]
        public ButtonBinds shurikenButton = new();

        // public string spriteName = ( new DirectoryInfo(((Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/static").ToString())).GetFiles()[0].Name);
        //public int randomInt = 0;

        public bool TeleportEnabled = true;
        public bool SoulMode = false;
        public float RotatingSpeed = 30;
        public SRandoSettings RS= new();

    }

    [Serializable]
    public class SRandoSettings
    {
        public int items = 10;
        public bool shurikenRando = true;
        public bool StartWithShuriken = false;
        public bool RandomizeTeleport = false;
    }

    public class KeyBinds : PlayerActionSet
    {
        public PlayerAction ShurikenKey;

        public KeyBinds()
        {
            ShurikenKey = CreatePlayerAction("ShurikenKey");
            DefaultBinds();
        }

        private void DefaultBinds()
        {
            ShurikenKey.AddDefaultBinding(Key.V);
        }
    }

    public class ButtonBinds : PlayerActionSet
    {
        public PlayerAction ShurikenBind;

        public ButtonBinds()
        {
            ShurikenBind = CreatePlayerAction("ShurikenBind");
            DefaultBinds();
            ShurikenBind.AddDefaultBinding(InputControlType.LeftStickButton);
}

            private void DefaultBinds()
        {
            ShurikenBind.AddDefaultBinding(InputControlType.LeftStickButton);
        }

    }




}
