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
                    buttonBindAction:Shuriken.GS.shurikenButton.ShurikenKey
                    ),
/*                    
                    new HorizontalOption("Teleport","",new[]{"ON","OFF" },
                    Action=>Shuriken.LS.hasTeleport= Action==0,
                    ()=>Shuriken.LS.hasTeleport?0:1),
*/

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
