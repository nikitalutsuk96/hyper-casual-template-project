using System.Collections.Generic;
using System.Linq;
using _Root.Scripts.MonoExtension;
using UnityEngine;

namespace _Root.Scripts.Services
{
    public class ServicesInitializer : MonoCached
    {
        [SerializeField] private List<BaseService> services = new List<BaseService>();
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (var service in services)
            {
                service.Init();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            services = GetComponentsInChildren<BaseService>().ToList();
        }
#endif
    }
}
