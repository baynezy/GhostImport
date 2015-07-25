using System;

namespace Ghost.Import
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Slug { get; set; }
		public string Markdown { get; set; }
		public string Html { get; set; }
		public string Image { get; set; }
		public bool Featured { get; set; }
		public PostType Type { get; set; }
		public PostStatus Status { get; set; }
		public Language Language { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		public int AuthorId { get; set; }
		public DateTime CreatedAt { get; set; }
		public int CreatedBy { get; set; }
		public DateTime UpdatedAt { get; set; }
		public int UpdatedBy { get; set; }
		public DateTime PublishedAt { get; set; }
		public int PublishedBy { get; set; }
	}
}
