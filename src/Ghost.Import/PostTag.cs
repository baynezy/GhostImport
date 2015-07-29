namespace Ghost.Import
{
	public class PostTag
	{
		public PostTag(int tagId, int postId)
		{
			TagId = tagId;
			PostId = postId;
		}

		public int TagId { get; set; }

		public int PostId { get; set; }
	}
}
