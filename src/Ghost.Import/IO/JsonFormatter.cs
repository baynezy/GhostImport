using System;
using System.Linq;
using Newtonsoft.Json;

namespace Ghost.Import.IO
{
	public class JsonFormatter : IFormatter
	{
		private readonly IEpochTime _epochTime;

		public JsonFormatter(IEpochTime epochTime)
		{
			_epochTime = epochTime;
		}

		public string Process(Import import)
		{
			dynamic json = new
			{
				meta = new
				{
					exported_on = _epochTime.ConvertTo(import.Meta.ExportedOn),
					version = import.Meta.Version
				},
				data = new
				{
					posts = from post in import.Data.Posts
								select new
								{
									id = post.Id,
									title = post.Title,
									slug = post.Slug,
									markdown = post.Markdown,
									html = post.Html,
									image = post.Image,
									featured = post.Featured ? 1 : 0,
									page = (post.Type == PostType.Page) ? 1 : 0,
									status = CalculateStatus(post.Status),
									language = CalculateLanguage(post.Language),
									meta_title = post.MetaTitle,
									meta_description = post.MetaDescription,
									author_id = post.AuthorId,
									created_at = _epochTime.ConvertTo(post.CreatedAt),
									created_by = post.CreatedBy,
									updated_at = _epochTime.ConvertTo(post.UpdatedAt),
									updated_by = post.UpdatedBy,
									published_at = _epochTime.ConvertTo(post.PublishedAt),
									published_by = post.PublishedBy
								},
					tags = from tag in import.Data.Tags
							   select new
								   {
									   id = tag.Id,
									   name = tag.Name,
									   slug = tag.Slug,
									   description = tag.Description
								   },
					post_tags = import.Data.PostTags,
					users = import.Data.Users,
					roles_users = import.Data.UserRoles
				}
			};
			return JsonConvert.SerializeObject(json);
		}

		private static string CalculateLanguage(Language language)
		{
			switch (language)
			{
				case Language.EnglishUk:
					return "en_GB";
				case Language.EnglishUs:
					return "en_US";
				default:
					throw new Exception("Invalid Language");
			}
		}

		private static string CalculateStatus(PostStatus status)
		{
			switch (status)
			{
				case PostStatus.Published:
					return "published";
				default:
					return "draft";
			}
		}
	}
}
