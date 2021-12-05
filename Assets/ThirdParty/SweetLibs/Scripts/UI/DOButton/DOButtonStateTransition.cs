using UnityEngine;
using System.Collections.Generic;

namespace SweetLibs.UI
{
    [System.Serializable]
    public class DOButtonStateTransition
    {
        public Sprite Sprite;
        public SoundAsset Sound;

        [Space] public List<TweenSetting> TweenSettings;

    }
}