using System;
using System.Collections.Generic;
using System.IO;

namespace CSShaders
{
  public class SpirVStreamWriter
  {
    BinaryWriter mWriter;

    public SpirVStreamWriter(BinaryWriter writer)
    {
      mWriter = writer;
    }

    public void WriteWord(UInt32 word)
    {
      mWriter.Write(word);
    }

    public void Write(UInt32 word)
    {
      WriteWord(word);
    }

    public void Write(int value)
    {
      WriteWord((UInt32)value);
    }

    public void Write(UInt16 highOrder, UInt16 lowOrder)
    {
      UInt32 data;
      data = 0xFFFF0000 & ((UInt32)highOrder << 16);
      data |= 0x0000FFFF & ((UInt32)lowOrder << 0);
      Write(data);
    }

    public void Write(byte a, byte b, byte c, byte d)
    {
      UInt32 word;
      word = 0xFF000000 & ((UInt32)a << 24);
      word |= 0x00FF0000 & ((UInt32)b << 16);
      word |= 0x0000FF00 & ((UInt32)c << 8);
      word |= 0x000000FF & ((UInt32)d << 0);
      Write(word);
    }

    public void WriteInstruction(UInt16 size, Spv.Op instruction)
    {
      Write(size, (UInt16)instruction);
    }

    public void WriteInstruction(UInt16 size, Spv.Op instruction, UInt32 word0)
    {
      WriteInstruction(size, instruction);
      Write(word0);
    }

    public void WriteInstruction(UInt16 size, Spv.Op instruction, UInt32 word0, UInt32 word1)
    {
      WriteInstruction(size, instruction);
      Write(word0);
      Write(word1);
    }

    public void WriteInstruction(UInt16 size, Spv.Op instruction, UInt32 word0, UInt32 word1, UInt32 word2)
    {
      WriteInstruction(size, instruction);
      Write(word0);
      Write(word1);
      Write(word2);
    }
    public void WriteInstruction(UInt16 size, Spv.Op instruction, List<UInt32> words)
    {
      Write(size, (UInt16)instruction);
      foreach (var word in words)
        Write(word);
    }

    public void Write(string text)
    {
      var byteCount = text.Length;
      var totalSize = GetPaddedByteCount(text);

      var wordCount = byteCount / 4;
      var i = 0;
      while (i < totalSize)
      {
        UInt32 word = 0;
        for (var j = 0; j < 4 && i + j < byteCount; ++j)
        {
          UInt32 character = text[i + j];
          UInt32 shiftedData = character << (j * 8);
          word = word | shiftedData;
        }
        Write(word);
        i += 4;
      }
    }

    public int GetPaddedByteCount(string text)
    {
      var byteCount = text.Length;
      var paddedByteCount = byteCount + 1;
      var totalSize = (paddedByteCount) / 4;
      if ((paddedByteCount % 4) != 0)
        ++totalSize;
      totalSize *= 4;
      return totalSize;
    }

    public int GetPaddedWordCount(string text)
    {
      return GetPaddedByteCount(text) / 4;
    }
  }
}
