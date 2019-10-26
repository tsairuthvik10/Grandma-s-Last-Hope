using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using Rewired;
using LarryIRL;
using System;

namespace LarryIRL
{
    public class KidLarryMovementAnimationsDemoScript : MonoBehaviour
    {
        // NOTE FOR PROGRAMMERS
        // The following code was thrown together in an attempt to quickly
        // demonstrate the Kid Larry animations. It's not recommened to be 
        // used or adapted within a real project as it's not very optimissed.
        // I will endevour to provide a proper example of a charcter controller
        // in future updates however please don't review my project based on the
        // character controller as the animations are the focus. Thanks! 

        public Text uiText;
        
        private SpriteRenderer spriteRenderer;
        private Animator animator;

        private float horzInput;
        private float vertInput;

        private bool crouchButtonPressed;
        private bool toggleSpeedButtonPressed;
        private bool jumpButtonPressed;

        private bool baseAnimationsEnabled = true;

        private enum Direction { left, right, down, up };
        private Direction moveDirection;

        private void Awake()
        {
            // Gets required components
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponentInChildren<Animator>();

            // set moveDirection to right before start
            moveDirection = Direction.right;
        }

        void Update()
        {
            GetUserInput();

            FlipSpriteRendererX();

            UpdateBaseAnimations();
        }

        private void GetUserInput()
        {
            // DIRECTIONAL INPUT
            horzInput = Input.GetAxisRaw("Horizontal");
            vertInput = Input.GetAxisRaw("Vertical");

            // CROUCH BUTTON INPUT
            if (vertInput < 0)
            {
                crouchButtonPressed = true;
            }
            else
            {
                crouchButtonPressed = false;
            }

            // WALK/RUN BUTTON INPUT
            if (Input.GetKey(KeyCode.LeftShift))
            {
                toggleSpeedButtonPressed = true;
            }
            else
            {
                toggleSpeedButtonPressed = false;
            }

            // JUMP BUTTON INPUT
            if (Input.GetKeyDown(KeyCode.Space) || vertInput > 0)
            {
                jumpButtonPressed = true;
            }
            else
            {
                jumpButtonPressed = false;
            }
        } 

        private void FlipSpriteRendererX()
        {            
            // Set faceDir
            if (horzInput < 0)
            {
                moveDirection = Direction.left;
            }
            else if (horzInput > 0)
            {
                moveDirection = Direction.right;
            }

            //If player is moving left
            if (moveDirection == Direction.left && spriteRenderer.flipX == false)
            {
                //flip the sprite to also be facing left
                spriteRenderer.flipX = true;
            }
            //otherwise if player is moving right
            else if (moveDirection == Direction.right && spriteRenderer.flipX == true)
            {
                //flip the sprite to also be facing left
                spriteRenderer.flipX = false;
            }
        }

        private void UpdateBaseAnimations()
        {
            // This bool disables all base animations aside from the jump
            // It's a bit of a hot fix for demostraion purposes
            // & not recommened for use with actual character controllers :D
            if (baseAnimationsEnabled)
            {
                // IDLE
                if (Mathf.Abs(horzInput) < 0.1f && !crouchButtonPressed && !jumpButtonPressed)
                {
                    animator.Play("Idle");
                    uiText.text = "Idle";
                }

                // CROUCHING
                if (crouchButtonPressed)
                {
                    // CROUCH IDLE
                    if (Mathf.Abs(horzInput) < 0.1f)
                    {
                        animator.Play("Crouch");
                        uiText.text = "Crouching";
                    }

                    // CROUCH WALK
                    if (Mathf.Abs(horzInput) >= 0.1f) //&& toggleSpeedButtonPressed)
                    {
                        animator.Play("CrouchWalk");
                        uiText.text = "Crouch Walking";
                    }
                }

                // STANDING
                else
                {
                    // WALK
                    if (Mathf.Abs(horzInput) >= 0.1f && toggleSpeedButtonPressed)
                    {
                        animator.Play("Walk");
                        uiText.text = "Walking";
                    }

                    // RUN
                    else if (Mathf.Abs(horzInput) >= 0.1f)
                    {
                        animator.Play("Run");
                        uiText.text = "Running";
                    }
                }

                // JUMP
                if (jumpButtonPressed)
                {
                    StartCoroutine(Jump());
                }
            }
        }

        IEnumerator Jump()
        {
            baseAnimationsEnabled = false;

            animator.Play("PreJump");
            uiText.text = "Pre-Jump";
            yield return new WaitForSeconds(0.1f);

            animator.Play("Jumping");
            uiText.text = "Jumping";
            yield return new WaitForSeconds(0.5f);

            animator.Play("Falling");
            uiText.text = "Falling";
            yield return new WaitForSeconds(0.5f);

            animator.Play("Landing");
            uiText.text = "Landing";
            yield return new WaitForSeconds(0.1f);

            baseAnimationsEnabled = true;
        }

        public void LinkToYouTube()
        {
            Application.OpenURL("https://www.youtube.com/channel/UCBlwt8HeEG7Nfnhvq5SiBSA/playlists");
        }

    } // class closing bracket
} // namespace closing bracket
