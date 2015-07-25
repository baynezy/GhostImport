using System;

namespace Ghost.Import
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public string Email { get; set; }
		public string Image { get; set; }
		public string Cover { get; set; }
		public string Bio { get; set; }
		public string Website { get; set; }
		public string Location { get; set; }
		public string Accessibility { get; set; }
		public UserStatus Status { get; set; }
		public Language Language { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		public DateTime? LastLogin { get; set; }
		public DateTime CreatedAt { get; set; }
		public int CreatedBy { get; set; }
		public DateTime UpdatedAt { get; set; }
		public int UpdatedBy { get; set; }
	}
}
