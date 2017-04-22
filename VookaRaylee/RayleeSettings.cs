using System.Xml.Serialization;
using VRGIN.Core;

namespace VookaRaylee
{
    [XmlRoot("Settings")]
    public class RayleeSettings : VRSettings
    {


        [XmlComment("Sets the intensity of the vignetting effect.")]
        public float VignettingIntensity { get { return _VignettingIntensity; } set { _VignettingIntensity = value; } }
        private float _VignettingIntensity = 30f;

        [XmlComment("Sets whether or not to use the vignetting effect for comfort.")]
        public bool Vignetting { get { return _Vignetting; } set { _Vignetting = value; } }
        private bool _Vignetting = false;
    }
}
