using UnityEngine;

namespace SweetLibs
{
    [CreateAssetMenu(fileName = "Data", menuName = "Custom/Sound Asset", order = 1)]
    public class SoundAsset : ScriptableObject
    {
        public AudioClip Clip;

        [Range(0f, 1f)] public float Volume = 1f;

        [Range(-3f, 3f)] public float Pitch = 1f;

        [Space] public bool RandomPitch;
        public float MinRandomPitch;
        public float MaxRandomPitch;
    }
}