using UnityEngine;
using UnityStandardAssets.ImageEffects;
using VRGIN.Core;
using VRGIN.Modes;

namespace VookaRaylee
{
    public class VookaSeatedMode : SeatedMode
    {
        VignetteAndChromaticAberration _Vignette;
        private float MIN_VIGNETTING = 0f;
        private float MAX_VIGNETTING = 40f;
        private float _TargetVignetting = 0f;
        private bool _VignettingEnabled = false;

        protected override void OnStart()
        {
            base.OnStart();
            _VignettingEnabled = (VR.Settings as RayleeSettings).Vignetting;
            MAX_VIGNETTING = (VR.Settings as RayleeSettings).VignettingIntensity;

            if (_VignettingEnabled)
            {
                InstallVignetting();
            }
        }

        private void InstallVignetting()
        {
            _Vignette = VR.Camera.gameObject.AddComponent<VignetteAndChromaticAberration>();
            _Vignette.vignetteShader = Shader.Find("Hidden/Vignetting");
            _Vignette.separableBlurShader = Shader.Find("Hidden/SeparableBlur");
            _Vignette.chromAberrationShader = Shader.Find("Hidden/ChromaticAberration");

            _Vignette.chromaticAberration = 0.0f;
            _Vignette.intensity = MIN_VIGNETTING;

            VR.Camera.FixEffectOrder();
        }

        protected override void OnUpdate()
        {
            var prevRot = VR.Camera.SteamCam.origin.transform.eulerAngles;

            // Display logo from up close
            if(Application.loadedLevel == 1 && VR.Camera.HasValidBlueprint)
            {
                var pos = VR.Camera.Blueprint.transform.position;
                VR.Camera.Blueprint.transform.position = new Vector3(pos.x, pos.y, 5f);
            }

            base.OnUpdate();

            if (_VignettingEnabled)
            {
                var postRot = VR.Camera.SteamCam.origin.transform.eulerAngles;
                var angularVelocity = (postRot.y - prevRot.y) / Time.deltaTime;

                _TargetVignetting = Mathf.Lerp(MIN_VIGNETTING, MAX_VIGNETTING, Mathf.Abs(angularVelocity) / 360f);
                UpdateVignette();

            }
            //if (VR.Camera.HasValidBlueprint)
            //{
            //    VR.Camera.SteamCam.origin.transform.position += (VR.Settings as RayleeSettings).VerticalOffset * Vector3.up;
            //}
        }

        private void UpdateVignette()
        {
            if(!_Vignette)
            {
                InstallVignetting();
            }

            float speed = _TargetVignetting >= _Vignette.intensity ? 5f : 2f;
            _Vignette.intensity = Mathf.Lerp(_Vignette.intensity, _TargetVignetting, Time.deltaTime * speed);
        }

        protected override void CreateControllers()
        {
            // NOOP
        }
    }
}
