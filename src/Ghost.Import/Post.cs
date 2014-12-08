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
		public PostType PostType { get; set; }
		public PostStatus PostStatus { get; set; }
		public Language Language { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		public int AuthorId { get; set; }
		public long CreatedAt { get; set; }
		public int CreatedBy { get; set; }
		public long UpdatedAt { get; set; }
		public int UpdatedBy { get; set; }
		public long PublishedAt { get; set; }
		public int PublishedBy { get; set; }
	}
}
