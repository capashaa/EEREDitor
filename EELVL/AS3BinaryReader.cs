using System;
using System.IO;

namespace EELVL
{
	using static Helpers;

	internal class AS3BinaryReader
	{
		private readonly Stream inner;
		private byte[] buffer;

		public AS3BinaryReader(Stream inner)
		{
			this.inner = inner;
			this.buffer = new byte[256];
		}

		private Span<byte> Bytes(int count)
		{
			if (buffer.Length < count)
			{
				int length = buffer.Length;
				while (length < count) length *= 2;
				buffer = new byte[length];
			}

			int pos = 0;
			while (pos < count)
			{
				int read = inner.Read(buffer, pos, count);
				if (read == 0) throw new EndOfStreamException();
				pos += read;
			}

			return buffer.AsSpan(0, count);
		}

		private Span<byte> FromBigEndian(int count)
		{
			Span<byte> data = Bytes(count);
			if (BitConverter.IsLittleEndian) data.Reverse();
			return data;
		}

		public int ReadInt() => Span<byte>.ToInt32(FromBigEndian(4));
		public uint ReadUInt() => Span<byte>.ToUInt32(FromBigEndian(4));
		public short ReadShort() => Span<byte>.ToInt16(FromBigEndian(2));
		public ushort ReadUShort() => Span<byte>.ToUInt16(FromBigEndian(2));
		public float ReadFloat() => Span<byte>.ToSingle(FromBigEndian(4));
		public bool ReadBool() => Span<byte>.ToBoolean(Bytes(1));
		public string ReadString()
		{
			ushort length = ReadUShort();
			return Span<byte>.GetString(Bytes(length));
		}
		public ushort[] ReadUShortArray()
		{
			uint length = ReadUInt();
			return ToUShortArray(Bytes((int)length));
		}
	}
}
