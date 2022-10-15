using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shuriken.GO
{
    public class NewDamage : MonoBehaviour
    {

        public int damageLimit = 60;
        public float ratio =>HeroController.instance.GetComponent<SpawnController>().ratio;

        private float counter = 0;


        private void Reset()
        {
            foreach (PlayMakerFSM playMakerFSM in base.GetComponents<PlayMakerFSM>())
            {
                if (playMakerFSM.FsmName == "damages_enemy")
                {
                    this.attackType = (AttackTypes)playMakerFSM.FsmVariables.GetFsmInt("attackType").Value;
                    this.circleDirection = playMakerFSM.FsmVariables.GetFsmBool("circleDirection").Value;
                    //this.damageDealt = playMakerFSM.FsmVariables.GetFsmInt("damageDealt").Value;
                    this.direction = playMakerFSM.FsmVariables.GetFsmFloat("direction").Value;
                    this.ignoreInvuln = playMakerFSM.FsmVariables.GetFsmBool("Ignore Invuln").Value;
                    this.magnitudeMult = playMakerFSM.FsmVariables.GetFsmFloat("magnitudeMult").Value;
                    this.moveDirection = playMakerFSM.FsmVariables.GetFsmBool("moveDirection").Value;
                    this.specialType = (SpecialTypes)playMakerFSM.FsmVariables.GetFsmInt("Special Type").Value;
                    return;
                }
            }
        }

        // Token: 0x06000896 RID: 2198 RVA: 0x0002F0A2 File Offset: 0x0002D2A2
        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.DoDamage(collision.gameObject);
        }

        // Token: 0x06000897 RID: 2199 RVA: 0x0002F0B0 File Offset: 0x0002D2B0
        private void OnTriggerEnter2D(Collider2D collision)
        {


            if (!base.enabled)
            {
                return;
            }
            int layer = collision.gameObject.layer;
            if (layer == 20 || layer == 9 || layer == 26 || layer == 31)
            {
                return;
            }
            if (collision.CompareTag("Geo"))
            {
                return;
            }
            if (!this.enteredColliders.Contains(collision))
            {
                this.enteredColliders.Add(collision);
            }
        }

        // Token: 0x06000898 RID: 2200 RVA: 0x0002F10F File Offset: 0x0002D30F
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (this.enteredColliders.Contains(collision))
            {
                this.enteredColliders.Remove(collision);
            }
        }

        // Token: 0x06000899 RID: 2201 RVA: 0x0002F12C File Offset: 0x0002D32C
        private void OnDisable()
        {
            this.enteredColliders.Clear();
        }

        // Token: 0x0600089A RID: 2202 RVA: 0x0002F13C File Offset: 0x0002D33C
        private void FixedUpdate()
        {
            for (int i = this.enteredColliders.Count - 1; i >= 0; i--)
            {
                Collider2D collider2D = this.enteredColliders[i];
                if (collider2D == null || !collider2D.isActiveAndEnabled)
                {
                    this.enteredColliders.RemoveAt(i);
                }
                else
                {
                    this.DoDamage(collider2D.gameObject);
                }
            }
        }

        // Token: 0x0600089B RID: 2203 RVA: 0x0002F19C File Offset: 0x0002D39C
        private void DoDamage(GameObject target)
        {
            /*damageDealt = this.gameObject.GetComponent<ProjectileBehaviour>().currentState == states.Hang ?
                                    HeroController.instance.GetComponent<SpawnController>().damageHover :
                                    HeroController.instance.GetComponent<SpawnController>().damageContact; ;*/
            if (counter < ratio)
            {
                counter++;
                return;
            }

            if (this.damageDealt <= 0)
            {
                return;
            }
            FSMUtility.SendEventToGameObject(target, "TAKE DAMAGE", false);
            HitTaker.Hit(target, new HitInstance
            {
                Source = base.gameObject,
                AttackType = this.attackType,
                CircleDirection = this.circleDirection,
                DamageDealt = this.damageDealt,
                Direction = this.direction,
                IgnoreInvulnerable = this.ignoreInvuln,
                MagnitudeMultiplier = this.magnitudeMult,
                MoveAngle = 0f,
                MoveDirection = this.moveDirection,
                Multiplier = 1f,
                SpecialType = this.specialType,
                IsExtraDamage = false
            }, 3);

            counter = 0;
            /*if (damage < damageLimit && this.gameObject.GetComponent<ShurikenBehaviour>().currentState == ShurikenBehaviour.states.Hang)
            {

                damage += damageDealt;
                return;
            }*/
        }

        // Token: 0x0600089C RID: 2204 RVA: 0x0002F264 File Offset: 0x0002D464

        // Token: 0x0400097C RID: 2428
        public AttackTypes attackType;

        // Token: 0x0400097D RID: 2429
        public bool circleDirection;

        // Token: 0x0400097E RID: 2430
        public int damageDealt => HeroController.instance.GetComponent<SpawnController>().damage();

        // Token: 0x0400097F RID: 2431
        public float direction;

        // Token: 0x04000980 RID: 2432
        public bool ignoreInvuln;

        // Token: 0x04000981 RID: 2433
        public float magnitudeMult;

        // Token: 0x04000982 RID: 2434
        public bool moveDirection;

        // Token: 0x04000983 RID: 2435
        public SpecialTypes specialType;

        // Token: 0x04000984 RID: 2436
        private List<Collider2D> enteredColliders = new List<Collider2D>();

    }
}
