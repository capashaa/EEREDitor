using System;
using System.Collections.Generic;
using System.Linq;

namespace EELVL
{
	using static Helpers;

	public static class Blocks
	{
		public enum BlockType
		{
			None = 1 << 8,
			Normal = None | (1 << 0),

			I = 1 << 9,
			Morphable = I | (1 << 0),
			Rotatable = I | (1 << 1),
			RotatableButNotReally = Rotatable | (1 << 2),
			Number = I | (1 << 3),
			Enumerable = I | (1 << 4),
			Music = I | (1 << 5),

			SI = 1 << 10,
			Sign = SI | (1 << 0),
			WorldPortal = SI | (1 << 1),

			III = 1 << 11,
			Portal = III | (1 << 0),

			SSI = 1 << 12,
			Label = SSI | (1 << 0),

			SSSS = 1 << 13,
			NPC = SSSS | (1 << 0),
		}

		internal static readonly LookupTable<int, BlockType> BlockTypes = new LookupTable<int, BlockType>(BlockType.Normal) {
			{ BlockType.Morphable,
				327, 328, 273, 440, 276, 277, 279, 280, 447, 449,
				450, 451, 452, 456, 457, 458, 464, 465, 471, 477,
				475, 476, 481, 482, 483, 497, 492, 493, 494, 1502,
				1500, 1507, 1506, 1581, 1587, 1588, 1592, 1593, 1160,
				1594, 1595, 1597
			},
			{ BlockType.Rotatable,
				375, 376, 379, 380, 377, 378, 438, 439, 1001, 1002,
				1003, 1004, 1052, 1053, 1054, 1055, 1056, 1092, 275, 329,
				338, 339, 340, 448, 1536, 1537, 1041, 1042, 1043, 1075,
				1076, 1077, 1078, 499, 1116, 1117, 1118, 1119, 1120, 1121,
				1122, 1123, 1124, 1125, 1535, 1135, 1134, 1538, 1140, 1141,
				1155, 1596, 1605, 1606, 1607, 1609, 1610, 1611, 1612, 1614,
				1615, 1616, 1617, 361, 1625, 1627, 1629, 1631, 1633, 1635
			},
			{ BlockType.RotatableButNotReally,
				1101, 1102, 1103, 1104, 1105
			},
			{ BlockType.Number,
				165, 43, 213, 214, 1011, 1012, 113, 1619, 184, 185,
				467, 1620, 1079, 1080, 1582, 421, 422, 461, 1584
			},
			{ BlockType.Enumerable,
				423, 1027, 1028, 418, 417, 420, 419, 453, 1517
			},
			{ BlockType.Music,
				83, 77, 1520
			},
			{ BlockType.Portal,
				381, 242
			},
			{ BlockType.WorldPortal,
				374
			},
			{ BlockType.Sign,
				385
			},
			{ BlockType.Label,
				1000
			},
			{ BlockType.NPC,
				1550, 1551, 1552, 1553, 1554, 1555, 1556, 1557, 1558, 1559,
				1569, 1570, 1571, 1572, 1573, 1574, 1575, 1576, 1577, 1578,
				1579
			}
		};

		public static readonly Block Empty = new Block(0);

		public static BlockType GetBlockType(int bid) => BlockTypes[bid];

		public static bool IsType(int bid, BlockType type) => (GetBlockType(bid) & type) == type;

		internal static Tuple<Block, int, IEnumerable<Tuple<ushort, ushort>>> Read(AS3BinaryReader reader)
		{
			int bid = reader.ReadInt();
			int layer = reader.ReadInt();
			ushort[] xs = reader.ReadUShortArray();
			ushort[] ys = reader.ReadUShortArray();

			Block b = GetBlockType(bid) switch
			{
				BlockType.Normal => new Block(bid),
				BlockType.Morphable => new MorphableBlock(bid, reader.ReadInt()),
				BlockType.Rotatable => new RotatableBlock(bid, reader.ReadInt()),
				BlockType.RotatableButNotReally => new RotatableBlock(bid, reader.ReadInt()),
				BlockType.Number => new NumberBlock(bid, reader.ReadInt()),
				BlockType.Enumerable => new EnumerableBlock(bid, reader.ReadInt()),
				BlockType.Music => new MusicBlock(bid, reader.ReadInt()),
				BlockType.Portal => new PortalBlock(bid, reader.ReadInt(), reader.ReadInt(), reader.ReadInt()),
				BlockType.Sign => new SignBlock(bid, reader.ReadString(), reader.ReadInt()),
				BlockType.WorldPortal => new WorldPortalBlock(bid, reader.ReadString(), reader.ReadInt()),
				BlockType.Label => new LabelBlock(bid, reader.ReadString(), reader.ReadString(), reader.ReadInt()),
				BlockType.NPC => new NPCBlock(bid, reader.ReadString(), reader.ReadString(), reader.ReadString(), reader.ReadString()),
				_ => throw new Exception("This shouldn't happen")
			};

			return new Tuple<Block, int, IEnumerable<Tuple<ushort, ushort>>>(b, layer, xs.Zip(ys, (x, y) => new Tuple<ushort, ushort>(x, y)));
		}

		internal static void Write(AS3BinaryWriter writer, Block block, int l, IEnumerable<Tuple<ushort, ushort>> locs)
		{
			writer.WriteInt(block.BlockID);
			writer.WriteInt(l);
			writer.WriteUShortArray(locs.Select(loc => loc.Item1).ToArray());
			writer.WriteUShortArray(locs.Select(loc => loc.Item2).ToArray());

			switch (block)
			{
				case NPCBlock b: writer.WriteString(b.Name); writer.WriteString(b.Message1); writer.WriteString(b.Message2); writer.WriteString(b.Message3); break;
				case LabelBlock b: writer.WriteString(b.Text); writer.WriteString(b.Color); writer.WriteInt(b.Wrap); break;
				case WorldPortalBlock b: writer.WriteString(b.Target); writer.WriteInt(b.Spawn); break;
				case SignBlock b: writer.WriteString(b.Text); writer.WriteInt(b.Morph); break;
				case PortalBlock b: writer.WriteInt(b.Rotation); writer.WriteInt(b.ID); writer.WriteInt(b.Target); break;
				case MusicBlock b: writer.WriteInt(b.Note); break;
				case EnumerableBlock b: writer.WriteInt(b.Variant); break;
				case NumberBlock b: writer.WriteInt(b.Number); break;
				case RotatableBlock b: writer.WriteInt(b.Rotation); break;
				case MorphableBlock b: writer.WriteInt(b.Morph); break;
				case Block b: break;
			}
		}

		public class Block
		{
			public int BlockID { get; }

			internal Block(BlockType type, int bid)
			{
				if (!IsType(bid, type)) throw new ArgumentException($"The ID {bid} is not of type {type}.");

				BlockID = bid;
			}

			public Block(int bid)
				: this(BlockType.Normal, bid) { }

			public override bool Equals(object obj)
				=> obj is Block b
				&& b.BlockID == BlockID;

			public override int GetHashCode()
				=> BlockID;

			public static bool operator ==(Block a, Block b) => a.Equals(b);
			public static bool operator !=(Block a, Block b) => !a.Equals(b);
		}

		public class MorphableBlock : Block
		{
			public int Morph { get; }

			internal MorphableBlock(BlockType type, int bid, int morph) : base(type, bid)
			{
				Morph = morph;
			}

			public MorphableBlock(int bid, int morph)
				: this(BlockType.Morphable, bid, morph) { }

			public override bool Equals(object obj)
				=> obj is MorphableBlock b && base.Equals(b)
				&& b.Morph == Morph;

			public override int GetHashCode()
				=> base.GetHashCode() * 1619 + Morph;
		}

		public class RotatableBlock : Block
		{
			public int Rotation { get; }

			internal RotatableBlock(BlockType type, int bid, int rotation) : base(type, bid)
			{
				Rotation = rotation;
			}

			public RotatableBlock(int bid, int rotation)
				: this(BlockType.Rotatable, bid, rotation) { }

			public override bool Equals(object obj)
				=> obj is RotatableBlock b && base.Equals(b)
				&& b.Rotation == Rotation;

			public override int GetHashCode()
				=> base.GetHashCode() * 1619 + Rotation;
		}

		public class NumberBlock : Block
		{
			public int Number { get; }

			internal NumberBlock(BlockType type, int bid, int number) : base(type, bid)
			{
				Number = number;
			}

			public NumberBlock(int bid, int number)
				: this(BlockType.Number, bid, number) { }

			public override bool Equals(object obj)
				=> obj is NumberBlock b && base.Equals(b)
				&& b.Number == Number;

			public override int GetHashCode()
				=> base.GetHashCode() * 1619 + Number;
		}

		public class EnumerableBlock : Block
		{
			public int Variant { get; }

			internal EnumerableBlock(BlockType type, int bid, int variant) : base(type, bid)
			{
				Variant = variant;
			}

			public EnumerableBlock(int bid, int variant)
				: this(BlockType.Enumerable, bid, variant) { }

			public override bool Equals(object obj)
				=> obj is EnumerableBlock b && base.Equals(b)
				&& b.Variant == Variant;

			public override int GetHashCode()
				=> base.GetHashCode() * 1619 + Variant;
		}

		public class MusicBlock : Block
		{
			public int Note { get; }

			internal MusicBlock(BlockType type, int bid, int note) : base(type, bid)
			{
				Note = note;
			}

			public MusicBlock(int bid, int note)
				: this(BlockType.Music, bid, note) { }

			public override bool Equals(object obj)
				=> obj is MusicBlock b && base.Equals(b)
				&& b.Note == Note;

			public override int GetHashCode()
				=> base.GetHashCode() * 1619 + Note;
		}

		public class PortalBlock : RotatableBlock
		{
			public int ID { get; }
			public int Target { get; }

			internal PortalBlock(BlockType type, int bid, int rotation, int id, int target) : base(type, bid, rotation)
			{
				ID = id;
				Target = target;
			}

			public PortalBlock(int bid, int rotation, int id, int target)
				: this(BlockType.Portal, bid, rotation, id, target) { }

			public override bool Equals(object obj)
				=> obj is PortalBlock b && base.Equals(b)
				&& b.ID == ID
				&& b.Target == Target;

			public override int GetHashCode()
				=> (base.GetHashCode() * 1619 + ID) * 1619 + Target;
		}

		public class SignBlock : MorphableBlock
		{
			public string Text { get; }

			internal SignBlock(BlockType type, int bid, string text, int morph) : base(type, bid, morph)
			{
				Text = text;
			}

			public SignBlock(int bid, string text, int morph)
				: this(BlockType.Sign, bid, text, morph) { }

			public override bool Equals(object obj)
				=> obj is SignBlock b && base.Equals(b) && b.Text == Text;

			public override int GetHashCode()
				=> base.GetHashCode() * 1619 + Text.GetHashCode();
		}

		public class WorldPortalBlock : Block
		{
			public string Target { get; }
			public int Spawn { get; }

			internal WorldPortalBlock(BlockType type, int bid, string target, int spawn) : base(type, bid)
			{
				Target = target;
				Spawn = spawn;
			}

			public WorldPortalBlock(int bid, string target, int spawn)
				: this(BlockType.WorldPortal, bid, target, spawn) { }

			public override bool Equals(object obj)
				=> obj is WorldPortalBlock b && base.Equals(b)
				&& b.Target == Target
				&& b.Spawn == Spawn;

			public override int GetHashCode()
				=> (base.GetHashCode() * 1619 + Target.GetHashCode()) * 1619 + Spawn;
		}

		public class LabelBlock : Block
		{
			public string Text { get; }
			public string Color { get; }
			public int Wrap { get; }

			internal LabelBlock(BlockType type, int bid, string text, string color, int wrap) : base(type, bid)
			{
				Text = text;
				Color = color;
				Wrap = wrap;
			}

			public LabelBlock(int bid, string text, string color, int wrap)
				: this(BlockType.Label, bid, text, color, wrap) { }

			public override bool Equals(object obj)
				=> obj is LabelBlock b && base.Equals(b)
				&& b.Text == Text
				&& b.Color == Color
				&& b.Wrap == Wrap;

			public override int GetHashCode()
				=> ((base.GetHashCode() * 1619 + Text.GetHashCode()) * 1619 + Color.GetHashCode()) * 1619 + Wrap;
		}

		public class NPCBlock : Block
		{
			public string Name { get; }
			public string Message1 { get; }
			public string Message2 { get; }
			public string Message3 { get; }

			internal NPCBlock(BlockType type, int bid, string name, string message1, string message2, string message3) : base(type, bid)
			{
				Name = name;
				Message1 = message1;
				Message2 = message2;
				Message3 = message3;
			}

			public NPCBlock(int bid, string name, string message1, string message2, string message3)
				: this(BlockType.NPC, bid, name, message1, message2, message3) { }

			public override bool Equals(object obj)
				=> obj is NPCBlock b && base.Equals(b)
				&& b.Name == Name
				&& b.Message1 == Message1
				&& b.Message2 == Message2
				&& b.Message3 == Message3;

			public override int GetHashCode()
				=> (((base.GetHashCode() * 1619 + Name.GetHashCode()) * 1619 + Message1.GetHashCode()) * 1619 + Message2.GetHashCode()) * 1619 + Message3.GetHashCode();
		}
	}
}
