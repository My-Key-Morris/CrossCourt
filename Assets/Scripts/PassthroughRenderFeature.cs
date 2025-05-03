using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PassthroughRenderFeature : ScriptableRendererFeature
{
    class PassthroughPass : ScriptableRenderPass
    {
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {}
    }

    PassthroughPass pass;

    public override void Create()
    {
        pass = new PassthroughPass { renderPassEvent = RenderPassEvent.AfterRenderingOpaques };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }
}
