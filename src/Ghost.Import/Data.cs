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
			UserRoles = new Dictionary<int, int>();
		}
		public IList<Post> Posts { get; set; }
		public IList<Tag> Tags { get; set; }
		public IDictionary<int, int> PostsTags { get; set; }
		public IList<User> Users { get; set; }
		public IDictionary<int, int> UserRoles { get; set; }

	}
}
