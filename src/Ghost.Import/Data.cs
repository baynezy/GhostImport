using System.Collections.Generic;

namespace Ghost.Import
{
	public class Data
	{
		public IList<Post> Posts { get; set; }
		public IList<Tag> Tags { get; set; }
		public IDictionary<Post, Tag> PostTags { get; set; }
		public IList<User> Users { get; set; }
		public IDictionary<User, Role> UseRoles { get; set; }

	}
}
