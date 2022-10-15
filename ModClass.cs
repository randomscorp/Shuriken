using Modding;
using Satchel.BetterMenus;
using Shuriken.GO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using UnityEngine;
using UObject = UnityEngine.Object;


namespace Shuriken
{
    public class Shuriken : Mod, ICustomMenuMod, IGlobalSettings<SGlobalSettings>, ILocalSettings<SLocalSettings>
    {
        internal static Shuriken Instance;

        public static SGlobalSettings GS = new();
        public static SLocalSettings LS = new();

        #region Settings and menu
        public bool ToggleButtonInsideMenu => false;

        public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? toggleDelegates)
        {
            return SMenu.MakeMyMenu(modListMenu);
        }

        public void OnLoadGlobal(SGlobalSettings s)
        {
            GS = s;
        }

        public SGlobalSettings OnSaveGlobal()
        {
            return GS;

        }

        public void OnLoadLocal(SLocalSettings s)
        {
            LS = s;
        }

        public SLocalSettings OnSaveLocal()
        {
            return LS;
        }
        #endregion

        public SpawnController controller= null;
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Instance = this;
            Hooks();


        }

        private void Hooks()
        {
            On.HeroController.Start += HeroController_Start;
            On.HeroController.TakeDamage += DEstroyOnDamage;
            On.HeroController.ResetAirMoves += HeroController_ResetAirMoves;
            bool rando = ModHooks.GetMod("Randomizer 4") is Mod;
            bool ic = ModHooks.GetMod("ItemChangerMod") is Mod;

            if (rando) Rando.MenuHolder.Hook();
            Rando.ShurikenRando.Hook(rando, ic);

        }

        private void HeroController_ResetAirMoves(On.HeroController.orig_ResetAirMoves orig, HeroController self)
        {
            controller.teleported = false;
            orig(self);
        }

        private void DEstroyOnDamage(On.HeroController.orig_TakeDamage orig, HeroController self, GameObject go, GlobalEnums.CollisionSide damageSide, int damageAmount, int hazardType)
        {
            orig(self, go, damageSide, damageAmount, hazardType);
            if (damageAmount > 0) GameObject.Destroy(HeroController.instance.GetComponent<SpawnController>().shurikenInstance);
        }

        private void HeroController_Start(On.HeroController.orig_Start orig, HeroController self)
        {
            orig(self);
            controller = self.gameObject.GetAddComponent<SpawnController>();
            controller.shuriken=LoadSprite("sprite.png");
            GameObject.DontDestroyOnLoad(controller.shuriken);
            if(LS.shurikenRando == false)
            {
                LS.shurikenLevel = 1;
                LS.hasTeleport = true;
            }
        }

        private static GameObject LoadSprite(string name)
        {
            GameObject shuriken = new GameObject("shuriken");
            Texture2D texture;
            using (FileStream s = File.Open((Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/static/{name}").ToString(), FileMode.Open))
            {

                byte[] buffer = new byte[s.Length];
                s.Read(buffer, 0, buffer.Length);
                s.Dispose();
                texture = new Texture2D(1, 1);
                texture.LoadImage(buffer);

                texture.Apply();
            }
            SpriteRenderer spriteRenderer = shuriken.AddComponent<SpriteRenderer>();
            spriteRenderer
                .sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return shuriken;
        }

        public static Vector2 InputVector()
        {
            Vector2 direction = new Vector2();

            if (GameManager.instance.inputHandler.inputActions.up.IsPressed)
            {
                direction.y = 1;
            }
            else if (GameManager.instance.inputHandler.inputActions.down.IsPressed)
            {
                direction.y = -1;
            }
            else { direction.y = 0; }

            if (GameManager.instance.inputHandler.inputActions.right.IsPressed)
            {
                direction.x = 1;
            }
            else if (GameManager.instance.inputHandler.inputActions.left.IsPressed)
            {
                direction.x = -1;
            }

            if (direction == new Vector2(0, 0)) { direction = new Vector2(HeroController.instance.cState.facingRight ? 1 : -1, 0); }

            return direction;
        }
    }

}
