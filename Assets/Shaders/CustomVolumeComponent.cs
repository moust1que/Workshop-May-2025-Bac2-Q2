using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable, VolumeComponentMenu("Custom/MyEffect")]
public class CustomVolumeComponent : VolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);
    public BoolParameter isActive = new BoolParameter(true);

    public bool IsActive() => isActive.value && intensity.value > 0f;
    public bool IsTileCompatible() => false;
}
