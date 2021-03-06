using Plugins.zPooling;
using UnityEngine;

namespace _Root.Scripts.Audio
{
    public class Music : MonoBehaviour, IGenericPoolElement
    {
        [SerializeField] private AudioSource audioSource = default;

        [Header("Components")]
        [SerializeField]
        private GameObject _gameObject = default;

        [SerializeField]
        private Transform _transform = default;
        
        public int PoolRef { get; set; }
        public bool IsAvailable { get; }
        public bool IsCommissioned { get; set; }
        public bool UsesAutoPool { get; set; }
        
        public void Play(SoundData soundData)
        {
            audioSource.clip = soundData.ChooseRandomClip ? soundData.RandomClip : soundData.Clip;
            
            audioSource.bypassEffects = soundData.ByPassEffects;
            audioSource.bypassListenerEffects = soundData.ByPassListenerEffects;
            audioSource.bypassReverbZones = soundData.ByPassReverbZones;
            audioSource.priority = soundData.Priority;
            audioSource.volume = soundData.Volume;
            audioSource.pitch = soundData.Pitch;
            audioSource.panStereo = soundData.StereoPan;
            audioSource.spatialBlend = soundData.SpatialBlend;
            audioSource.reverbZoneMix = soundData.ReverbZoneMix;
            
            audioSource.playOnAwake = false;
            audioSource.loop = true;
            
            audioSource.Play();
        }
        
        public void Commission()
        {
            _gameObject.SetActive(true);
        }

        public void Decommission()
        {
            _gameObject.SetActive(false);
            
            this.ReturnToPool();
        }

        public void OnDestroy()
        {
            this.RemoveFromPool();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            audioSource = GetComponent<AudioSource>();
            _gameObject = gameObject;
            _transform = transform;
        }
#endif
    }
}
