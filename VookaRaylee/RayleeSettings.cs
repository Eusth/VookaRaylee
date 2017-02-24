using System.Xml.Serialization;
using VRGIN.Core;

namespace VookaRaylee
{
    [XmlRoot("Settings")]
    public class RayleeSettings : VRSettings
    {
        [XmlComment("Sets how many meters the camera should be pushed upward.")]
        public float VerticalOffset { get { return _VerticalOffset; } set { _VerticalOffset = value; } }
        private float _VerticalOffset = 5f;

    }
}
