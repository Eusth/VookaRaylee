using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRGIN.Core;

namespace VookaRaylee
{
    public class RayleeInterpreter : GameInterpreter
    {
        private readonly string[] FX_BLACKLIST = new string[] { "DepthOfField" };
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
    }
}