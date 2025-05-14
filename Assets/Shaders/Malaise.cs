using UnityEngine;

public class Malaise : MonoBehaviour
{
    public Material malaise;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, malaise);
    }
}
