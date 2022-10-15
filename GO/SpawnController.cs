using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalEnums;
using InControl;



//Estudy singleton again
namespace Shuriken.GO
{
    public class SpawnController: MonoBehaviour
    {
        public GameObject shuriken;
        public PlayerAction key => Shuriken.GS.shuriknBind.ShurikenKey;
        public GameObject shurikenInstance;
        public ProjectileBehaviour projectileBehaviour;

        public float speed = 25;
        public float fowardTime = 0.33f;
        public int damageHover => PlayerData.instance.nailDamage/3;
        public int damageContact => ((int)(16 * (PlayerData.instance.equippedCharm_19 ? 1.3f : 1) * (Shuriken.LS.shurikenLevel * 0.5f)));
        public float ratio = 3;
        public bool teleported = false;

        public int damage()
        {
            try
            {

               return gameObject.GetComponent<ProjectileBehaviour>().currentState == states.Hang ?
                                    damageHover :
                                    damageContact;
            }
            catch
            {
                return damageContact;
            }

        }
        private void Start()
        {
            NewDamage damageEnemiesW = shuriken.GetAddComponent<NewDamage>();
            damageEnemiesW.gameObject.layer = ((int)PhysLayers.DEFAULT);
            damageEnemiesW.attackType = AttackTypes.Spell;
            damageEnemiesW.ignoreInvuln = false;
            damageEnemiesW.magnitudeMult = 0.001f;
            damageEnemiesW.moveDirection = false;
            damageEnemiesW.specialType = 0;
            damageEnemiesW.circleDirection = false;
            damageEnemiesW.damageLimit = 50;

            shuriken.GetAddComponent<Rigidbody2D>().gravityScale=0;
            var col = shuriken.AddComponent<CircleCollider2D>();
            col.isTrigger = true;

            shuriken.transform.localScale /= 4;// 2.7f;
            shuriken.SetActive(false);



            projectileBehaviour = shuriken.GetAddComponent<ProjectileBehaviour>();


        }


        private void Update()
        {
            if (canShuriken() && key.IsPressed && key.HasChanged)
            {
                if (shurikenInstance == null)
                {

                    shuriken.transform.position = HeroController.instance.transform.position;
                    shurikenInstance = Instantiate(shuriken);
                    shurikenInstance.GetAddComponent<ProjectileBehaviour>().direction = Shuriken.InputVector();
                    shurikenInstance.SetActive(true);
                }
                else shurikenInstance.GetAddComponent<ProjectileBehaviour>().ChangeState();

            }
        }
        public bool canShuriken()=> HeroController.instance.CanInput() && Shuriken.LS.shurikenLevel > 0;

        public bool hasTeleport => Shuriken.LS.hasTeleport;

    }
}
