using UnityEngine;
using VRGIN.Core;
using VRGIN.Modes;

namespace VookaRaylee
{
    public class VookaSeatedMode : SeatedMode
    {

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (VR.Camera.HasValidBlueprint)
            {
                VR.Camera.SteamCam.origin.transform.position += (VR.Settings as RayleeSettings).VerticalOffset * Vector3.up;
            }
        }
    }
}
