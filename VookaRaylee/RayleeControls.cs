using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VRGIN.Core;
using XInputDotNetPure;

namespace VookaRaylee
{


    class RayleeControls : ProtectedBehaviour
    {
        private GamePadState? _PrevState;
        private const string TRIGGER = "Joy1Axis3";

        private const string DPADX = "Joy1Axis6";
        private const string DPADY = "Joy1Axis7";
        private const KeyCode RIGHT_STICK = KeyCode.Joystick1Button9;

        private float? _RightStickPressTime = null;
        private const float TIME_TO_RECENTER = 1.5f;

        protected override void OnUpdate()
        {
            base.OnUpdate();
            var state = GamePad.GetState(PlayerIndex.One);
            var state2 = GamePad.GetState(PlayerIndex.Two);
            var state3 = GamePad.GetState(PlayerIndex.Three);
            var state4 = GamePad.GetState(PlayerIndex.Four);
            
            bool thumbStickPressed = state.Buttons.RightStick == ButtonState.Pressed;

            if (state.Triggers.Left > 0.5f || state.Triggers.Right > 0.5f)
            {
                if (state.DPad.Up == ButtonState.Pressed) HandlePressUp(state.Triggers.Left <= 0.5f);
                else if (state.DPad.Down == ButtonState.Pressed) HandlePressDown(state.Triggers.Left <= 0.5f);
                if (state.DPad.Right == ButtonState.Pressed) HandlePressRight();
                else if (state.DPad.Left == ButtonState.Pressed) HandlePressLeft();
            }

            if (thumbStickPressed)
            {
                if (_RightStickPressTime == null)
                {
                    _RightStickPressTime = Time.time;
                }
                if (Time.time - _RightStickPressTime > TIME_TO_RECENTER)
                {
                    (VR.Mode as VookaSeatedMode).Recenter();
                    _RightStickPressTime = null;
                }
            }
            else
            {
                _RightStickPressTime = null;
            }

            _PrevState = state;
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
