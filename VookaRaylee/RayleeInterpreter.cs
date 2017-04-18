using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRGIN.Core;

namespace VookaRaylee
{
    public class RayleeInterpreter : GameInterpreter
    {
        private readonly string[] FX_BLACKLIST = new string[] { "DepthOfField" };
        private const string TRIGGER = "Joy1Axis3";

        private const string DPADX = "Joy1Axis6";
        private const string DPADY = "Joy1Axis7";
        private const KeyCode RIGHT_STICK = KeyCode.Joystick1Button9;

        private float? _RightStickPressTime = null;
        private const float TIME_TO_RECENTER = 1.5f;

        // Not used
        public override IEnumerable<IActor> Actors
        {
            get
            {
                yield break;
            }
        }

        protected override void OnAwake()
        {
            base.OnAwake();

            // Make sure those are rendered
            VR.Camera.gameObject.AddComponent<QuillieCameraUpdate>();
        }

        public override bool IsAllowedEffect(MonoBehaviour effect)
        {
            return !FX_BLACKLIST.Contains(effect.GetType().Name) && base.IsAllowedEffect(effect);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            float trigger = Input.GetAxisRaw(TRIGGER);
            float yPress = Input.GetAxisRaw(DPADY);
            float xPress = Input.GetAxisRaw(DPADX);

            if (Mathf.Abs(trigger) > 0.5f)
            {
                if (yPress > 0.5f) HandlePressUp(trigger < 0);
                else if (yPress < -0.5f) HandlePressDown(trigger < 0);
                if (xPress > 0.5f) HandlePressRight();
                else if (xPress < -0.5f) HandlePressLeft();
            }

            if(Input.GetKeyDown(RIGHT_STICK))
            {
                _RightStickPressTime = Time.time;
            }
            if(Input.GetKeyUp(RIGHT_STICK))
            {
                _RightStickPressTime = null;
            }
            if(_RightStickPressTime != null && Time.time - _RightStickPressTime > TIME_TO_RECENTER)
            {
                (VR.Mode as VookaSeatedMode).Recenter();
                _RightStickPressTime = null;
            }
        }

        private void HandlePressLeft()
        {
            
            VR.Settings.Rotation -= Time.deltaTime * 90;
        }

        private void HandlePressRight()
        {
            VR.Settings.Rotation += Time.deltaTime * 90;
        }

        private void HandlePressUp(bool alternativeMode)
        {
            if (alternativeMode)
            {
                VR.Settings.Distance += Time.deltaTime * 0.5f;
            }
            else
            {
                VR.Settings.OffsetY += Time.deltaTime * 0.5f;
            }
        }

        private void HandlePressDown(bool alternativeMode)
        {
            if (alternativeMode)
            {
                VR.Settings.Distance -= Time.deltaTime * 0.5f;
            }
            else
            {
                VR.Settings.OffsetY -= Time.deltaTime * 0.5f;

            }
        }
    }
}