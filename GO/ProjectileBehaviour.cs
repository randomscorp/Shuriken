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


        void Start()
        {
            controller = HeroController.instance.gameObject.GetAddComponent<SpawnController>();
            body = this.GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            speed=controller.speed;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (currentState != states.Foward && col.gameObject.name == "Knight")
            {
                Destroy(this.gameObject);

            }

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


        public void ChangeState()
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
                if (controller.hasTeleport)
                {
                    if (controller.teleported) return;
                    controller.teleported = true;
                    HeroController.instance.transform.position = this.transform.position;
                HeroController.instance.ResetHardLandingTimer();

                        Destroy(this.gameObject);
                }
                }
                if (InputHandler.Instance.inputActions.down.IsPressed) Destroy(this.gameObject);
                else currentState = states.Back;

            }
            else currentState = states.Back;
        }
    }

    public enum states
    {
        Foward = 0,
        Hang = 1,
        Back = 2
    }

}
