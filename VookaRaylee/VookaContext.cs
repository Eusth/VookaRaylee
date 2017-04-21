using System;
using UnityEngine;
using VRGIN.Controls.Speech;
using VRGIN.Core;
using VRGIN.Visuals;

namespace VookaRaylee
{
    public class VookaContext : IVRManagerContext
    {
        DefaultMaterialPalette _Materials;
        RayleeSettings _Settings;
        public VookaContext()
        {
            _Materials = new DefaultMaterialPalette();
            _Settings = VRSettings.Load<RayleeSettings>("vr_settings.xml");
        }

        public bool ConfineMouse
        {
            get
            {
                return false;
            }
        }

        public bool EnforceDefaultGUIMaterials
        {
            get
            {
                return false;
            }
        }

        public bool GUIAlternativeSortingMode
        {
            get
            {
                return false;
            }
        }

        public float GuiFarClipPlane
        {
            get
            {
                return 10000f;
            }
        }

        public string GuiLayer
        {
            get
            {
                return "Default";
            }
        }

        public float GuiNearClipPlane
        {
            get
            {
                return -10000f;
            }
        }

        public int IgnoreMask
        {
            get
            {
                return 0;
            }
        }

        public string InvisibleLayer
        {
            get
            {
                return "Ignore Raycast";
            }
        }

        public IMaterialPalette Materials
        {
            get
            {
                return _Materials;
            }
        }

        public global::UnityEngine.Color PrimaryColor
        {
            get
            {
                return Color.cyan;
            }
        }

        public VRSettings Settings
        {
            get
            {
                return _Settings;
            }
        }

        public bool SimulateCursor
        {
            get
            {
                return true;
            }
        }

        public string UILayer
        {
            get
            {
                return "UI";
            }
        }

        public int UILayerMask
        {
            get
            {
                return LayerMask.GetMask(UILayer);
            }
        }

        public float UnitToMeter
        {
            get
            {
                return 1f;
            }
        }

        public Type VoiceCommandType
        {
            get
            {
                return typeof(VoiceCommand);
            }
        }


        
    }
}