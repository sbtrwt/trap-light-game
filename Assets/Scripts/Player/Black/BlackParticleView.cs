﻿using System;
using TMPro;
using UnityEngine;

namespace TrapLight.Player.Black
{
    public class BlackParticleView : MonoBehaviour
    {
        private BlackParticleController controller;

        private SpriteRenderer spriteRenderer;
        private Rigidbody2D blackRigidbody2D;
        private Animator animator;
        [SerializeField] TMP_Text healthText;
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            blackRigidbody2D = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            controller.Move(blackRigidbody2D);
        }
        public void SetController(BlackParticleController controller)
        {
            this.controller = controller;
        }

        public BlackParticleController GetController()
        {
            return controller;
        }

        public void SetHealthText()
        {
            controller.SetHealthText(healthText);
        }
    }
}
