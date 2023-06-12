using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EELVL
{
	using static Helpers;

	internal static class Helpers
	{
		public class LookupTable<TFrom, TTo> : IEnumerable<Tuple<TFrom, TTo>>
		{
			private readonly TTo defaultValue;
			private readonly Dictionary<TFrom, TTo> internalDict;

			public LookupTable(TTo defaultValue)
			{
				this.defaultValue = defaultValue;
				this.internalDict = new Dictionary<TFrom, TTo>();
			}

			public void Add(TTo to, params TFrom[] froms)
			{
				foreach (TFrom from in froms)
					internalDict[from] = to;
			}

			public TTo this[TFrom from] {
				get => internalDict.GetValueOrDefault(from, defaultValue);
			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
			public IEnumerator<Tuple<TFrom, TTo>> GetEnumerator()
			{
				foreach (KeyValuePair<TFrom, TTo> kvp in internalDict)
					yield return new Tuple<TFrom, TTo>(kvp.Key, kvp.Value);
			}
		}

		public class Span<T>
		{
			private readonly T[] inner;
			private readonly int start;
			private readonly int count;

			public Span(T[] inner, int start, int count)
			{
				this.inner = inner;
				this.start = start;
				this.count = count;
			}

			public Span(T[] inner)
			{
				this.inner = inner;
				this.start = 0;
				this.count = inner.Length;
			}

			public int Length => count;
			public T this[int i] => inner[start + i];

			public void CopyTo(byte[] buffer) => Array.Copy(inner, start, buffer, 0, count);
			public void Reverse() => Array.Reverse(inner, start, count);

			public static Int32 ToInt32(Span<byte> data) => BitConverter.ToInt32(data.inner, data.start);
			public static UInt32 ToUInt32(Span<byte> data) => BitConverter.ToUInt32(data.inner, data.start);

			public static Int16 ToInt16(Span<byte> data) => BitConverter.ToInt16(data.inner, data.start);
			public static UInt16 ToUInt16(Span<byte> data) => BitConverter.ToUInt16(data.inner, data.start);
			public static Single ToSingle(Span<byte> data) => BitConverter.ToSingle(data.inner, data.start);
			public static Boolean ToBoolean(Span<byte> data) => BitConverter.ToBoolean(data.inner, data.start);
			public static string GetString(Span<byte> data) => Encoding.UTF8.GetString(data.inner, data.start, data.count);
			public static int Read(Stream stream, Span<byte> data) => stream.Read(data.inner, data.start, data.count);
			public static void Write(Stream stream, Span<byte> data) => stream.Write(data.inner, data.start, data.count);
		}

		public static ushort[] ToUShortArray(Span<byte> data)
		{
			int count = data.Length / 2;

			ushort[] arr = new ushort[count];

			for (int i = 0; i < count; i++)
				arr[i] = (ushort)(data[2*i] << 8 | data[2*i + 1]);

			return arr;
		}

		public static Span<byte> FromUShortArray(ushort[] data)
		{
			int count = data.Length;

			byte[] arr = new byte[count * 2];

			for (int i = 0; i < count; i++)
			{
				arr[2 * i] = (byte)(data[i] >> 8);
				arr[2 * i + 1] = (byte)(data[i] & 255);
			}

			return new Span<byte>(arr);
		}
	}

	internal static class Extensions
	{
		public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue def)
		{
			return dict.TryGetValue(key, out TValue value) ? value : def;
		}

		public static Span<T> AsSpan<T>(this T[] inner, int start, int count) => new Span<T>(inner, start, count);
	}
}
