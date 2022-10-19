using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MenuChanger.MenuElements;
using Satchel.BetterMenus;

namespace Shuriken
{
    public static class SMenu
    {
        public static Menu MyMenu;

        public static MenuScreen MakeMyMenu(MenuScreen modListMenu)
        {
            MyMenu ??= new Menu(
                name: "Shuriken", 
                elements: new Element[] 
                {
                    Blueprints.KeyAndButtonBind(
                    name: "Shuriken Key",
                    keyBindAction: Shuriken.GS.shuriknBind.ShurikenKey,
                    buttonBindAction:Shuriken.GS.shurikenButton.ShurikenBind
                    ),
                   
                    new HorizontalOption("Teleport","",new[]{"Enabled","Disabled" },
                    Action=>Shuriken.GS.TeleportEnabled= Action==0,
                    ()=>Shuriken.GS.TeleportEnabled?0:1),

                  /*  new HorizontalOption("Soul Mode","Teleport now costs soul",new[]{"Enabled","Disabled" },
                    Action=>Shuriken.GS.SoulMode= Action==0,
                    ()=>Shuriken.GS.SoulMode?0:1
                    ),*/
                    new CustomSlider(
                        name:"Rotate Speed",
                        storeValue:val =>{ Shuriken.GS.RotatingSpeed=val; },
                        loadValue: ()=>Shuriken.GS.RotatingSpeed,
                        minValue:0,
                        maxValue:40
                        ),

                });
            return MyMenu.GetMenuScreen(modListMenu);

        }

        /*private static List<string> GetNames()
        {
            var infos = new DirectoryInfo(((Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/static").ToString())).GetFiles();
            List<string> names = new();

            foreach (var info in infos)
            {
                names.Add(info.Name);
            }


           return names;

        }*/


    }



}
