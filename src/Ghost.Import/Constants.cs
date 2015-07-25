namespace Ghost.Import
{
	public sealed class PostType : TypeSafeEnum
	{
		public static readonly PostType Page = new PostType(1, "Page");
		public static readonly PostType Post = new PostType(0, "Post");

		private PostType(int value, string name) : base(value,name)
		{
			
		}
	}

	public sealed class PostStatus : TypeSafeEnum
	{
		public static readonly PostStatus Published = new PostStatus(1, "published");
		public static readonly PostStatus Draft = new PostStatus(2, "draft");

		private PostStatus(int value, string name) : base(value, name)
		{
			
		}
	}

	public sealed class Language : TypeSafeEnum
	{
		private Language(int value, string name) : base(value, name)
		{

		}

		public static readonly Language EnglishUk = new Language(1, "en_GB");
		public static readonly Language EnglishUs = new Language(2, "en_US");
	}

	public enum UserStatus
	{
		Active
	}

	public class TypeSafeEnum
	{
		private readonly string _name;
		private readonly int _value;

		protected TypeSafeEnum(int value, string name)
		{
			_value = value;
			_name = name;
		}

		public override string ToString()
		{
			return _name;
		}

		public int ToValue()
		{
			return _value;
		}
	}
}
