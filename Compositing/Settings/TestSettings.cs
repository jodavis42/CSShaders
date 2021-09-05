using System;
using System.Collections.Generic;

namespace CSShaders.Compositing
{
  /// A collection of settings mostly for testing. These settings also show how settings can be created
  public class TestSettings
  {
    class Context
    {
      internal List<ShaderType> Int32Types = new List<ShaderType>();
      internal List<ShaderType> FloatTypes = new List<ShaderType>();
      internal ShaderType Float4x4Type;
    }

    public static CompositorSettings BuildTestCompositorSettings(ShaderModule module)
    {
      var context = new Context();
      context.Int32Types.Add(module.FindType(new TypeKey(typeof(Int32))));
      context.Int32Types.Add(module.FindType(new TypeKey(typeof(Math.Integer2))));
      context.Int32Types.Add(module.FindType(new TypeKey(typeof(Math.Integer3))));
      context.Int32Types.Add(module.FindType(new TypeKey(typeof(Math.Integer4))));
      context.FloatTypes.Add(module.FindType(new TypeKey(typeof(float))));
      context.FloatTypes.Add(module.FindType(new TypeKey(typeof(Math.Vector2))));
      context.FloatTypes.Add(module.FindType(new TypeKey(typeof(Math.Vector3))));
      context.FloatTypes.Add(module.FindType(new TypeKey(typeof(Math.Vector4))));
      context.Float4x4Type = module.FindType(new TypeKey(typeof(Math.Float4x4)));

      var compositorSettings = new CSShaders.Compositing.CompositorSettings();
      compositorSettings.HardwareBuiltInSettings = CreateBasicHardwareBuiltInSettings(module, context);
      compositorSettings.UniformSettings = CreateBasicUniformSettings(module, context);
      compositorSettings.AttributeSettings = CreateBasicAttributeSettings(module, context);
      compositorSettings.RenderTargetSettings = CreateBasicRenderTargetSettings(module, context);
      compositorSettings.Lock();

      return compositorSettings;
    }

    static UniformDescriptionSettings CreateBasicUniformSettings(ShaderModule module, Context context)
    {
      var floatType = context.FloatTypes[0];
      var float2Type = context.FloatTypes[1];
      var float4x4Type = context.Float4x4Type;

      var uniformSettings = new UniformDescriptionSettings();
      var frameDataDesc = uniformSettings.CreateBuffer("FrameData", new UniformBindings { BindingId = 0, DescriptorSet = 0 });
      frameDataDesc.Add(floatType, "FrameTime");
      frameDataDesc.Add(floatType, "LogicTime");

      var cameraDataDesc = uniformSettings.CreateBuffer("CameraData", new UniformBindings { BindingId = 1, DescriptorSet = 0 });
      cameraDataDesc.Add(floatType, "NearPlane");
      cameraDataDesc.Add(floatType, "FarPlane");
      cameraDataDesc.Add(float2Type, "ViewportSize");

      var transformDataDesc = uniformSettings.CreateBuffer("TransformData", new UniformBindings { BindingId = 2, DescriptorSet = 0 });
      transformDataDesc.Add(float4x4Type, "LocalToWorld");
      transformDataDesc.Add(float4x4Type, "WorldToView");
      transformDataDesc.Add(float4x4Type, "ViewToPerspective");
      transformDataDesc.Add(float4x4Type, "LocalToPerspective");
      return uniformSettings;
    }

    static HardwareBuiltInSettings CreateBasicHardwareBuiltInSettings(ShaderModule module, Context context)
    {
      var int32Type = context.Int32Types[0];
      var floatType = context.FloatTypes[0];
      var float2Type = context.FloatTypes[1];
      var float4Type = context.FloatTypes[3];

      // Now create hardware built-ins. These perform name/type matching and then generate the appropriate attribute using the actual spv.BuiltIn enum.
      // The name matching allows the attributes to work more nicely with the generic [Input]/[Output] attributes.
      var hardwareBuiltInSettings = new HardwareBuiltInSettings();
      var vertexHWBISettings = hardwareBuiltInSettings.GetOrCreateFragmentDesciptions(FragmentType.Vertex);
      vertexHWBISettings.AddInput(int32Type, "VertexIndex", Shader.HardwareBuiltinType.VertexIndex);
      vertexHWBISettings.AddInput(int32Type, "InstanceId", Shader.HardwareBuiltinType.InstanceId);
      vertexHWBISettings.AddOutput(float4Type, "ApiPerspectivePosition", Shader.HardwareBuiltinType.Position);
      vertexHWBISettings.AddOutput(floatType, "PointSize", Shader.HardwareBuiltinType.PointSize);

      var pixelHWBISettings = hardwareBuiltInSettings.GetOrCreateFragmentDesciptions(FragmentType.Pixel);
      pixelHWBISettings.AddInput(float4Type, "FragCoord", Shader.HardwareBuiltinType.FragCoord);
      pixelHWBISettings.AddInput(float2Type, "PointCoord", Shader.HardwareBuiltinType.PointCoord);
      pixelHWBISettings.AddOutput(floatType, "FragDepth", Shader.HardwareBuiltinType.FragDepth);
      return hardwareBuiltInSettings;
    }

    static AttributeSettings CreateBasicAttributeSettings(ShaderModule module, Context context)
    {
      var float2Type = context.FloatTypes[1];
      var float3Type = context.FloatTypes[2];
      var float4Type = context.FloatTypes[3];
      var int4Type = context.Int32Types[3];

      var attributeSettings = new AttributeSettings();
      attributeSettings.AddAttribute(float2Type, "Uv", 0);
      attributeSettings.AddAttribute(float3Type, "LocalNormal", 1);
      attributeSettings.AddAttribute(float3Type, "LocalPosition", 2);
      attributeSettings.AddAttribute(float4Type, "Color", 3);
      attributeSettings.AddAttribute(float4Type, "Aux0", 4);
      attributeSettings.AddAttribute(int4Type, "BoneIndices", 5);
      return attributeSettings;
    }

    static RenderTargetSettings CreateBasicRenderTargetSettings(ShaderModule module, Context context)
    {
      var float4Type = context.FloatTypes[3];
      var renderTargetSettings = new RenderTargetSettings();
      for(var i = 0; i < 8; ++i)
        renderTargetSettings.AddNamedRenderTarget(float4Type, $"Target{i}", i);
      return renderTargetSettings;
    }
  }
}
