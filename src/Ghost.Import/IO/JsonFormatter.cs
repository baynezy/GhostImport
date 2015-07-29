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
									page = post.Type.ToValue(),
									status = post.Status.ToString(),
									language = post.Language.ToString(),
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
					posts_tags = from postTag in import.Data.PostsTags
									select new
										{
											tag_id = postTag.TagId,
											post_id = postTag.PostId
										},
					users = from user in import.Data.Users
								select new
									{
										id = user.Id,
										name = user.Name,
										slug = user.Slug,
										email = user.Email,
										image = user.Image,
										cover = user.Cover,
										bio = user.Bio,
										website = user.Website,
										location = user.Location,
										accessibility = user.Accessibility,
										status = user.Status.ToString(),
										language = user.Language.ToString(),
										meta_title = user.MetaTitle,
										meta_description = user.MetaDescription,
										last_login = FormatLastLogin(user.LastLogin),
										created_at = _epochTime.ConvertTo(user.CreatedAt),
										created_by = user.CreatedBy,
										updated_at = _epochTime.ConvertTo(user.UpdatedAt),
										updated_by = user.UpdatedBy
									},
					roles_users = from userRole in import.Data.UserRoles
								  select new
									  {
										  user_id = userRole.UserId,
										  role_id = userRole.RoleId
									  }
				}
			};
			return JsonConvert.SerializeObject(json);
		}

		private long? FormatLastLogin(DateTime? lastLogin)
		{
			if (lastLogin == null)
			{
				return null;
			}
			return _epochTime.ConvertTo((DateTime) lastLogin);
		}
	}
}
