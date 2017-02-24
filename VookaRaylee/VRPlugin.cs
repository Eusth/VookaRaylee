using IllusionPlugin;
using System;
using VRGIN.Core;
using VRGIN.Helpers;
using VRGIN.Modes;

namespace VookaRaylee
{
    public class VRPlugin : IPlugin
    {
        public string Name
        {
            get
            {
                return "Vooka Laylee";
            }
        }

        public string Version
        {
            get
            {
                return "0.1";
            }
        }

        public void OnApplicationStart()
        {
            if (Environment.CommandLine.Contains("--vr"))
            {
                var context = new VookaContext();
                VRManager.Create<RayleeInterpreter>(context);
                VR.Manager.SetMode<VookaSeatedMode>();
            }
        }

        public void OnLevelWasInitialized(int level) {
            // Makes sure that the menu won't freeze everything VR-related
            SteamVR_Render.instance.tag = "DoNotPause";
            VR.Camera.Origin.tag = "DoNotPause";
            foreach(var el in VR.Camera.Origin.Descendants())
            {
                el.tag = "DoNotPause";
            }
        }

        #region Unused
        public void OnApplicationQuit() { }
        public void OnFixedUpdate() { }
        public void OnLevelWasLoaded(int level) { }
        public void OnUpdate() { }
        #endregion

    }
}
