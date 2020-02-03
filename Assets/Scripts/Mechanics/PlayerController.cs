using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Framework.ScriptableObjects.Variables;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject, IControllable
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
		bool _climbing;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

		public SharedBool ConnectionActive;


        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

		public void OnInputAcquired(MobileInput inp)
		{
			if (inp.InputType == InputTypes.Movement)
			{
				InputHorizontalAxis = inp.Value.x;
				InputVerticalAxis = inp.Value.y;
			}
			if (inp.InputType == InputTypes.Jump)
			{
				if (inp.Value.y > 0)
				{
					if (InputJump && !InputJumpDown)
					{
						InputJumpUp = true;
						InputJumpDown = false;
					}
					InputJump = false;
				}
				else if (inp.Value.y < 0)
				{
					if (!InputJump)
					{
						InputJumpDown = true;
						InputJumpUp = false;
					}
					InputJump = true;
				}
			}
		}

		protected override void Update()
        {
			if (!ConnectionActive.Value)
			{
				InputHorizontalAxis = Input.GetAxis("Horizontal");
				InputVerticalAxis = Input.GetAxis("Vertical");
				InputJumpDown = Input.GetButtonDown("Jump");
				InputJumpUp = Input.GetButtonUp("Jump");
			}

			if (controlEnabled)
            {
				move.x = InputHorizontalAxis;

                if (jumpState == JumpState.Grounded && InputJumpDown)
                    jumpState = JumpState.PrepareToJump;
                else if (InputJumpUp)
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }

            UpdateJumpState();
            base.Update();

			if (ConnectionActive.Value)
			{
				InputJumpDown = false;
				InputJumpUp = false;
			}
        }

		public override void FixedUpdate()
		{
			if (_climbing)
				return;

			base.FixedUpdate();
		}

		private float InputHorizontalAxis;
		private float InputVerticalAxis;
		private bool InputJumpDown;
		private bool InputJumpUp;
		private bool InputJump;



        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
			if (_climbing)
				return;

            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

		public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.tag == "Vine") // sorry 
			{
				GrabVine(col);

				// can probably cut some of these, but im afraid to do so
				controlEnabled = false;
				velocity = Vector2.zero;
				gravityModifier = 0;
				body.gravityScale = 0;
				_climbing = true;
			}
		}

		void GrabVine(Collider2D col)
		{
			Vector2 position = transform.position;
			position.x = col.transform.position.x;
			transform.position = position;
		}

		void OnTriggerStay2D(Collider2D col)
		{
			if (col.tag != "Vine" && _climbing == false) // sorry 
				return;

			move.y = InputVerticalAxis;
            GetComponent<Animator>().Play("Player-Climb");

			transform.Translate(move * Time.deltaTime);

			if (InputJumpDown)
			{
				QuitClimbing();
				velocity.y = jumpTakeOffSpeed * model.jumpModifier;
			}
		}

		void OnTriggerExit2D(Collider2D col)
		{
			QuitClimbing();
		}

		void QuitClimbing()
		{
			//reset them things
			_climbing = false;
			controlEnabled = true;
			gravityModifier = 1;
			body.gravityScale = 1;
		}

		public void Death(bool isDead)
		{
			controlEnabled = !isDead;
		}
	}
}