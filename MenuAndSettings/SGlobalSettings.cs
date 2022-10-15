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
        public KeyBinds shurikenButton = new();

        // public string spriteName = ( new DirectoryInfo(((Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/static").ToString())).GetFiles()[0].Name);
        //public int randomInt = 0;
        public int items = 1;
        public bool shurikenRando = true;
        public bool StartWithShuriken = true;
        public bool Randomize_Teleport = false;

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
        }

        private void DefaultBinds()
        {
            //ShurikenBind.AddInputControlType(InputControlType.);
        }

    }




}
