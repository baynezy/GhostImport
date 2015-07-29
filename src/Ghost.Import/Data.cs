using System;
using System.Collections.Generic;

namespace Ghost.Import
{
	public class Data
	{
		public Data()
		{
			Posts = new List<Post>();
			Tags = new List<Tag>();
			PostsTags = new List<PostTag>();
			Users = new List<User>();
			UserRoles = new List<UserRole>();
		}
		public IList<Post> Posts { get; set; }
		public IList<Tag> Tags { get; set; }
		public IList<PostTag> PostsTags { get; set; }
		public IList<User> Users { get; set; }
		public IList<UserRole> UserRoles { get; set; }

	}
}
