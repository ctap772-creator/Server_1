using System;
using System.Text;
using UnityEngine;

public class myReader
{
    public sbyte[] buffer;

    private int posRead;

    private int posMark;

    private static string fileName;

    private static int status;

    public myReader()
    {
    }

    public myReader(sbyte[] data)
    {
        buffer = data;
    }

    public myReader(string filename)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        buffer = mSystem.convertToSbyte(textAsset.bytes);
    }

    public sbyte readSByte()
    {
        if (posRead < buffer.Length)
        {
            return buffer[posRead++];
        }
        posRead = buffer.Length;
        throw new Exception(" loi doc sbyte eof ");
    }

    public sbyte readsbyte()
    {
        return readSByte();
    }

    public sbyte readByte()
    {
        return readSByte();
    }

    public void mark(int readlimit)
    {
        posMark = posRead;
    }

    public void reset()
    {
        posRead = posMark;
    }

    public byte readUnsignedByte()
    {
        return convertSbyteToByte(readSByte());
    }

    public short readShort()
    {
        short num = 0;
        for (int i = 0; i < 2; i++)
        {
            num <<= 8;
            num |= (short)(0xFF & buffer[posRead++]);
        }
        return num;
    }

    public ushort readUnsignedShort()
    {
        ushort num = 0;
        for (int i = 0; i < 2; i++)
        {
            num <<= 8;
            num |= (ushort)(0xFFu & (uint)buffer[posRead++]);
        }
        return num;
    }

    public int readInt()
    {
        int num = 0;
        for (int i = 0; i < 4; i++)
        {
            num <<= 8;
            num |= 0xFF & buffer[posRead++];
        }
        return num;
    }

    public long readLong()
    {
        long num = 0L;
        for (int i = 0; i < 8; i++)
        {
            num <<= 8;
            num |= 0xFF & buffer[posRead++];
        }
        return num;
    }
    public double readDouble()
    {
        double num = readInt();
        return num;
    }
    public double readDouble2()
    {
        long num = 0L;
        for (int i = 0; i < 8; i++)
        {
            num <<= 8;
            num |= 0xFF & buffer[posRead++];
        }
        return BitConverter.Int64BitsToDouble(num);
    }
    public bool readBool()
    {
        return (readSByte() > 0) ? true : false;
    }

    public bool readBoolean()
    {
        return (readSByte() > 0) ? true : false;
    }

    public string readString()
    {
        short num = readShort();
        byte[] array = new byte[num];
        for (int i = 0; i < num; i++)
        {
            array[i] = convertSbyteToByte(readSByte());
        }
        UTF8Encoding uTF8Encoding = new UTF8Encoding();
        return uTF8Encoding.GetString(array);
    }

	public string readStringUTF()
	{
		short num = readShort();
		byte[] array = new byte[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = convertSbyteToByte(readSByte());
		}
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		return cleanIncomingText(uTF8Encoding.GetString(array));
	}

	public string readUTF()
	{
		return readStringUTF();
	}

	private static string cleanIncomingText(string text)
	{
		text = fixMojibake(text);
		return removeItemDebugLine(text);
	}

	private static string removeItemDebugLine(string text)
	{
		if (string.IsNullOrEmpty(text) || (text.IndexOf("Item ID:", StringComparison.Ordinal) < 0 && text.IndexOf("Icon ID:", StringComparison.Ordinal) < 0))
		{
			return text;
		}
		string[] lines = text.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < lines.Length; i++)
		{
			string line = lines[i];
			if (line.IndexOf("Item ID:", StringComparison.Ordinal) >= 0 && line.IndexOf("Icon ID:", StringComparison.Ordinal) >= 0 && line.IndexOf("Type:", StringComparison.Ordinal) >= 0)
			{
				continue;
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append('\n');
			}
			stringBuilder.Append(line);
		}
		return stringBuilder.ToString().Trim('\n');
	}

	private static string fixMojibake(string text)
	{
		if (string.IsNullOrEmpty(text) || !looksMojibake(text))
		{
			return text;
		}
		try
		{
			Encoding sourceEncoding = Encoding.GetEncoding(1252);
			string best = text;
			int bestScore = getMojibakeScore(best);
			for (int i = 0; i < 4; i++)
			{
				string repaired = Encoding.UTF8.GetString(sourceEncoding.GetBytes(best));
				int score = getMojibakeScore(repaired);
				if (score >= bestScore)
				{
					break;
				}
				best = repaired;
				bestScore = score;
				if (score == 0)
				{
					break;
				}
			}
			return best;
		}
		catch
		{
			return text;
		}
	}

	private static bool looksMojibake(string text)
	{
		return getMojibakeScore(text) > 0;
	}

	private static int getMojibakeScore(string text)
	{
		int score = 0;
		for (int i = 0; i < text.Length; i++)
		{
			char c = text[i];
			if (c == 'Ã' || c == 'Æ' || c == '�')
			{
				score += 3;
			}
			else if (c == 'Â' || c == 'Ä')
			{
				score++;
			}
		}
		score += countMarker(text, "áº") * 3;
		score += countMarker(text, "á»") * 3;
		score += countMarker(text, "â€") * 3;
		score += countMarker(text, "â€¦") * 3;
		return score;
	}

	private static int countMarker(string text, string marker)
	{
		int count = 0;
		int index = 0;
		while ((index = text.IndexOf(marker, index, StringComparison.Ordinal)) >= 0)
		{
			count++;
			index += marker.Length;
		}
		return count;
	}

    public int read()
    {
        if (posRead < buffer.Length)
        {
            return readSByte();
        }
        return -1;
    }

    public int read(ref sbyte[] data)
    {
        if (data == null)
        {
            return 0;
        }
        int num = 0;
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = readSByte();
            if (posRead > buffer.Length)
            {
                return -1;
            }
            num++;
        }
        return num;
    }

    public void readFully(ref sbyte[] data)
    {
        if (data != null && data.Length + posRead <= buffer.Length)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = readSByte();
            }
        }
    }

    public int available()
    {
        return buffer.Length - posRead;
    }

    public static byte convertSbyteToByte(sbyte var)
    {
        if (var > 0)
        {
            return (byte)var;
        }
        return (byte)(var + 256);
    }

    public static byte[] convertSbyteToByte(sbyte[] var)
    {
        byte[] array = new byte[var.Length];
        for (int i = 0; i < var.Length; i++)
        {
            if (var[i] > 0)
            {
                array[i] = (byte)var[i];
            }
            else
            {
                array[i] = (byte)(var[i] + 256);
            }
        }
        return array;
    }

    public void Close()
    {
        buffer = null;
    }

    public void close()
    {
        buffer = null;
    }

    public void read(ref sbyte[] data, int arg1, int arg2)
    {
        if (data == null)
        {
            return;
        }
        for (int i = 0; i < arg2; i++)
        {
            data[i + arg1] = readSByte();
            if (posRead > buffer.Length)
            {
                break;
            }
        }
    }
}
