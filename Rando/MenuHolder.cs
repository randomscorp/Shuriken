using MenuChanger;
using MenuChanger.MenuElements;
using MenuChanger.MenuPanels;
using MenuChanger.Extensions;
using RandomizerMod.Menu;
using UnityEngine.SceneManagement;
using Modding;

namespace Shuriken.Rando
{
    public class MenuHolder
    {
        internal MenuPage Shuriken;
        internal MenuElementFactory<SRandoSettings> rpMEF;
        internal VerticalItemPanel rpVIP;

        internal SmallButton JumpToRPButton;

        private static MenuHolder _instance = null;
        internal static MenuHolder Instance => _instance ?? (_instance = new MenuHolder());

        public static void OnExitMenu()
        {
            _instance = null;
        }

        public static void Hook()
        {
            RandomizerMenuAPI.AddMenuPage(Instance.ConstructMenu, Instance.HandleButton);
            MenuChangerMod.OnExitMainMenu += OnExitMenu;
        }

        private bool HandleButton(MenuPage landingPage, out SmallButton button)
        {
            JumpToRPButton = new(landingPage, "Shuriken");
            JumpToRPButton.AddHideAndShowEvent(landingPage, Shuriken);
            JumpToRPButton.Text.color= global::Shuriken.Shuriken.GS.RS.shurikenRando? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;

            button = JumpToRPButton;
            return true;
        }

        private void ConstructMenu(MenuPage landingPage)
        {
            Shuriken = new MenuPage("Shuriken", landingPage);
            rpMEF = new(Shuriken, global::Shuriken.Shuriken.GS.RS);
            foreach(var element in rpMEF.Elements) { element.SelfChanged+= (obj)=>
             JumpToRPButton.Text.color = global::Shuriken.Shuriken.GS.RS.shurikenRando ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
            }

            rpVIP = new(Shuriken, new(10, 300), 50f, false, rpMEF.Elements);
        }
    }
}
