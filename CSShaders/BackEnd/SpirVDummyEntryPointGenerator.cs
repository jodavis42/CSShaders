using System.Collections.Generic;

namespace CSShaders
{
  class SpirVDummyEntryPointGenerator
  {
    public void CreateDummyEntryPoint(TypeDependencyCollector typeCollector, ShaderLibrary library, FrontEndTranslator frontEnd)
    {
      var unitTestTypes = new List<ShaderType>();
      foreach (var ir in typeCollector.mReferencedTypesConstantsAndGlobals)
      {
        if(ir is ShaderType type)
        {
          if (type.mMeta.mAttributes.Contains("UnitTest") && type.mEntryPoints.Count == 0)
            unitTestTypes.Add(type);
        }
      }
      foreach (var type in unitTestTypes)
        CreateDummyEntryPoint(typeCollector, library, frontEnd, type);
    }

    public void CreateDummyEntryPoint(TypeDependencyCollector typeCollector, ShaderLibrary library, FrontEndTranslator frontEnd, ShaderType shaderType)
    {
      var context = new FrontEndContext();
      context.mCurrentType = shaderType;

      if (shaderType.mEntryPoints.Count != 0)
        return;
      
      ShaderEntryPointInfo entryPoint = null;
      if(shaderType.mFragmentType == FragmentType.Pixel || shaderType.mFragmentType == FragmentType.None)
        entryPoint = EntryPointGeneration.GeneratePixel(frontEnd, shaderType, null);
      else if(shaderType.mFragmentType == FragmentType.Vertex)
        entryPoint = EntryPointGeneration.GenerateVertex(frontEnd, shaderType, null);

      typeCollector.Visit(entryPoint);
    }
  }
}
