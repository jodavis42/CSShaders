namespace CSShaders.Compositing
{
  public enum StageType { None = -1, Start = 0, Cpu = 0, Vertex, Pixel, Gpu, Count }

  public static class Extensions
  {
    public static FragmentType GetFragmentType(this StageType stageType)
    {
      if (stageType == StageType.Vertex)
        return FragmentType.Vertex;
      else if (stageType == StageType.Pixel)
        return FragmentType.Pixel;
      return FragmentType.None;
    }
  }
}
