using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;

public class MalaiseV2 : ScriptableRendererFeature
{
   

    CustomRenderPass pass;
    public Material material;

  
    public override void Create()
    {
        pass = new CustomRenderPass();
        pass.material = material;
       
        pass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

 
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (renderingData.cameraData.cameraType == CameraType.Game || renderingData.cameraData.cameraType == CameraType.SceneView)
            renderer.EnqueuePass(pass);
    }
}
class CustomRenderPass : ScriptableRenderPass
{
    public Material material;

    public static Vector3 center;
    public static float radius;
    private class PassData
    {
        public Material material;
        public MaterialPropertyBlock mpb;
    }

    

    public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
    {
        const string passName = "Render Custom Pass";


        using (var builder = renderGraph.AddRasterRenderPass<PassData>(passName, out var passData))
        {
            if (passData.mpb == null)
                passData.mpb = new MaterialPropertyBlock();

            passData.material = material;
            UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
            builder.SetRenderAttachment(resourceData.activeColorTexture, 0);


            builder.SetRenderFunc((PassData data, RasterGraphContext context) => ExecutePass(data, context));
        }
    }


    static void ExecutePass(PassData data, RasterGraphContext context)
    {
        CoreUtils.DrawFullScreen(context.cmd, data.material);
    }
}