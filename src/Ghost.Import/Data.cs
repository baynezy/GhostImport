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
			PostsTags = new List<Tuple<int, int>>();
			Users = new List<User>();
			UserRoles = new List<Tuple<int, int>>();
		}
		public IList<Post> Posts { get; set; }
		public IList<Tag> Tags { get; set; }
		public IList<Tuple<int,int>> PostsTags { get; set; }
		public IList<User> Users { get; set; }
		public IList<Tuple<int, int>> UserRoles { get; set; }

	}
}
