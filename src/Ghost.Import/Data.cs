using System.Collections.Generic;

namespace Ghost.Import
{
	public class Data
	{
		public Data()
		{
			Posts = new List<Post>();
			Tags = new List<Tag>();
			PostsTags = new Dictionary<int, int>();
			Users = new List<User>();
		}
		public IList<Post> Posts { get; set; }
		public IList<Tag> Tags { get; set; }
		public IDictionary<int, int> PostsTags { get; set; }
		public IList<User> Users { get; set; }
		public IDictionary<User, Role> UserRoles { get; set; }

	}
}
