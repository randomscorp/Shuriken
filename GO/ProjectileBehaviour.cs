using GlobalEnums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Satchel;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Shuriken.GO
{
    public class ProjectileBehaviour : MonoBehaviour
    {


        public states currentState = states.Foward;
        states previousState;
        private SpawnController controller;
        public PlayerAction key => Shuriken.GS.shuriknBind.ShurikenKey;
        Rigidbody2D body;
        Collider2D col;
        public Vector2 direction;
        float time = 0;
        public float speed;
        public bool createdInAir = false;

        void Start()
        {
            controller = HeroController.instance.gameObject.GetAddComponent<SpawnController>();
            body = this.GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            speed = controller.speed;
            createdInAir = !HeroController.instance.CheckTouchingGround() && (controller.teleported);

        }

        void OnTriggerEnter2D(Collider2D col)
        {

            if (currentState != states.Foward && col.gameObject.name == "Knight")
            {
                Destroy(this.gameObject);
                //controller.teleported = false;
            }

        }
        void Update()
        {
            if(HeroController.instance.CheckTouchingGround()) controller.teleported = false;
        }

        void FixedUpdate()
        {
            if (currentState != previousState) previousState = currentState; ;
            switch (currentState)
            {
                case states.Foward:
                    Foward();
                    break;

                case states.Hang:
                    Hang();
                    break;

                case states.Back:
                    Back();
                    break;
            }

        }

        void Foward()
        {
            body.velocity = direction.normalized * speed;
            time += Time.fixedDeltaTime;
            if (time >= controller.fowardTime) { time = 0; currentState = states.Back; }
            this.transform.Rotate(0, 0, -18);
        }

        void Back()
        {
            this.transform.Rotate(0, 0, -18);
            speed = Math.Abs(speed);
            body.velocity = (body.transform.position - HeroController.instance.transform.position).normalized * -speed;
        }

        void Hang()
        {
            body.velocity = new Vector2(0, 0);
            this.transform.Rotate(0, 0, -30);//-10
        }


        public IEnumerator ChangeState()
        {
            if (currentState != states.Hang)
            {
                currentState = states.Hang;
                HeroController.instance.RelinquishControl();
                HeroController.instance.ResetHardLandingTimer();
                HeroController.instance.RegainControl();
            }
            else if (currentState == states.Hang)
            {
                if (InputHandler.Instance.inputActions.up.IsPressed)
                {
                    RaycastHit2D rayDown = Physics2D.Raycast(transform.position, -Vector2.up);
                    RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
                    var collider = this.GetComponent<Collider2D>();
                    if (controller.hasTeleport && Shuriken.GS.TeleportEnabled)
                    {
                        if (controller.teleported || collider.IsTouchingLayers(8) || createdInAir==true) { }
                        else
                        {


                                controller.teleported = true;
                                var anim = HeroController.instance.GetComponent<HeroAnimationController>();

                                HeroController.instance.RelinquishControl();
                                HeroController.instance.ResetHardLandingTimer();
                                HeroController.instance.StopAnimationControl();

                                HeroController.instance.dJumpFlashPrefab.SetActive(true);
                                anim.animator.Play("Scream Start");
                                anim.animator.Play("Scream");


                                yield return new WaitForSeconds(0.07f);
                                HeroController.instance.transform.position = this.transform.position;
                                yield return new WaitForSeconds(0.07f);
                                anim.animator.Play("Scream End");

                                HeroController.instance.RegainControl();
                                HeroController.instance.StartAnimationControl();
                            

                        }
                    }
                    else currentState = states.Back;

                }
                if (InputHandler.Instance.inputActions.down.IsPressed) Destroy(this.gameObject);
                else currentState = states.Back;
            }

        }


    }

    public enum states
    {
        Foward = 0,
        Hang = 1,
        Back = 2
    }

}
