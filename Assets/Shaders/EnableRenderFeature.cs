using Events.Runtime;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class EnableRenderFeature : MonoBehaviour
{
    public  bool toEnable = false;
    public bool isEnable = false;


    private void Start()
    {
        isEnable = true;
        GameEvents.OnEnableFeature += EnableFeature;
        GameEvents.OnDisableFeature += DisableFeature;
    }
    public  void EnableFeature()
    {
        isEnable = true;
    }
    public  void DisableFeature()
    {
        isEnable = false;
        
    }

    private void Update()
    {
        isEnable = toEnable;
    }
    private void OnDestroy()
    {
        GameEvents.OnEnableFeature -= EnableFeature;
        GameEvents.OnDisableFeature -= DisableFeature;
    }
}
