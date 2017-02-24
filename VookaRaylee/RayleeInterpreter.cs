using System.Collections.Generic;
using VRGIN.Core;

namespace VookaRaylee
{
    public class RayleeInterpreter : GameInterpreter
    {

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
    }
}