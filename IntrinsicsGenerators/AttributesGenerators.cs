using System;
using System.Collections.Generic;
using System.Text;

namespace IntrinsicsGenerators
{
  public abstract class IAttributesGenerator
  {
    public abstract string GetAttributes();
  }

  public class SimpleAttributeGenerator : IAttributesGenerator
  {
    string Attributes;

    public SimpleAttributeGenerator(string attributes)
    {
      Attributes = attributes;
    }

    public override string GetAttributes()
    {
      return Attributes;
    }
  }
}
