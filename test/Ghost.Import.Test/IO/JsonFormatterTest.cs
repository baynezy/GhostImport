using System;
using System.Collections.Generic;
using Ghost.Import.IO;
using Moq;
using Moq.Language.Flow;
using MoqExtentions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Ghost.Import.Test.IO
{
	[TestFixture]
	class JsonFormatterTest
	{
		[Test]
		public void JsonFormatter_ShouldImplementIFormatter()
		{
			var formatter = CreateFormatter();

			Assert.That(formatter, Is.InstanceOf<IFormatter>());
		}

		[Test]
		public void Process_WhenPassedEmptyImport_ThenExportShouldContainBaseMetaAttribute()
		{
			var import = CreateEmptyImport();
			var formatter = CreateFormatter();

			var json = formatter.Process(import);

			dynamic parsed = JObject.Parse(json);

			Assert.NotNull(parsed.meta, "There should be a meta attribute");
			Assert.NotNull(parsed.meta.exported_on, "There should be a meta.exported_on attribute");
			Assert.NotNull(parsed.meta.version, "There should be a meta.version attribute");
		}

		[Test]
		public void Process_WhenPassedEmptyImport_ThenExportShouldContainBaseDataAttribute()
		{
			var import = CreateEmptyImport();
			var formatter = CreateFormatter();

			var json = formatter.Process(import);

			dynamic parsed = JObject.Parse(json);

			Assert.NotNull(parsed.data, "There should be a data attribute");
			Assert.NotNull(parsed.data.posts, "There should be a data.posts attribute");
			Assert.NotNull(parsed.data.tags, "There should be a data.tags attribute");
			Assert.NotNull(parsed.data.posts_tags, "There should be a data.post_tags attribute");
			Assert.NotNull(parsed.data.users, "There should be a data.users attribute");
			Assert.NotNull(parsed.data.roles_users, "There should be a data.roles_users attribute");
		}

		[Test]
		public void Process_WhenPassedEmptyImport_ThenExportedOnShouldBeEpoch()
		{
			const long epoch = 1419508800000;
			var import = CreateEmptyImport();
			var mockEpoch = new Mock<IEpochTime>();
			mockEpoch.Setup(m => m.ConvertTo(It.IsAny<DateTime>())).Returns(epoch);
			var formatter = CreateFormatter(mockEpoch.Object);

			var json = formatter.Process(import);

			dynamic parsed = JObject.Parse(json);

			Assert.That((long) parsed.meta.exported_on, Is.EqualTo(epoch));
		}

		[Test]
		public void Process_WhenPassedEmptyImport_ThenVersionShouldBeExpectedValue()
		{
			const string version = "003";
			var import = CreateEmptyImport();
			var formatter = CreateFormatter();

			var json = formatter.Process(import);

			dynamic parsed = JObject.Parse(json);

			Assert.That((string)parsed.meta.version, Is.EqualTo(version));
		}

		[Test]
		public void Process_WhenPassedImportWithPosts_ThenShouldHaveBaseAttributes()
		{
			var import = CreateEmptyImport();
			import.Data.Posts.Add(CreateDummyPost());
			var formatter = CreateFormatter();

			var json = formatter.Process(import);
			dynamic parsed = JObject.Parse(json);
			var post = parsed.data.posts[0];

			Assert.That(post.id, Is.Not.Null, "id attribute should exist");
			Assert.That(post.title, Is.Not.Null, "title attribute should exist");
			Assert.That(post.slug, Is.Not.Null, "slug attribute should exist");
			Assert.That(post.markdown, Is.Not.Null, "markdown attribute should exist");
			Assert.That(post.html, Is.Not.Null, "html attribute should exist");
			Assert.That(post.image, Is.Not.Null, "image attribute should exist");
			Assert.That(post.featured, Is.Not.Null, "featured attribute should exist");
			Assert.That(post.page, Is.Not.Null, "page attribute should exist");
			Assert.That(post.status, Is.Not.Null, "status attribute should exist");
			Assert.That(post.language, Is.Not.Null, "language attribute should exist");
			Assert.That(post.meta_title, Is.Not.Null, "meta_title attribute should exist");
			Assert.That(post.meta_description, Is.Not.Null, "meta_description attribute should exist");
			Assert.That(post.author_id, Is.Not.Null, "author_id attribute should exist");
			Assert.That(post.created_at, Is.Not.Null, "created_at attribute should exist");
			Assert.That(post.created_by, Is.Not.Null, "created_by attribute should exist");
			Assert.That(post.updated_at, Is.Not.Null, "updated_at attribute should exist");
			Assert.That(post.updated_by, Is.Not.Null, "updated_by attribute should exist");
			Assert.That(post.published_at, Is.Not.Null, "published_at attribute should exist");
			Assert.That(post.published_by, Is.Not.Null, "published_by attribute should exist");
		}

		[Test]
		public void Process_WhenPassedImportWithPosts_ThenShouldBePopulatedProperly()
		{
			var import = CreateEmptyImport();
			import.Data.Posts.Add(CreateDummyPost());
			var epochTime = new Mock<IEpochTime>();
            epochTime.Setup(m => m.ConvertTo(It.IsAny<DateTime>())).ReturnsInOrder(0, 1418495760000, 1418495760000, 1418495760000);

			var formatter = CreateFormatter(epochTime.Object);

			var json = formatter.Process(import);
			dynamic parsed = JObject.Parse(json);
			var post = parsed.data.posts[0];

			Assert.That((int)post.id, Is.EqualTo(1), "post Id is incorrect");
			Assert.That((string)post.title, Is.EqualTo("Title"), "post title is incorrect");
			Assert.That((string)post.slug, Is.EqualTo("/title+post/"), "post slug is incorrect");
			Assert.That((string)post.markdown, Is.EqualTo("# Title"), "post markdown is incorrect");
			Assert.That((string)post.html, Is.EqualTo("<h1>Title</h1>"), "post html is incorrect");
			Assert.That((string)post.image, Is.Null, "post image is incorrect");
			Assert.That((Byte)post.featured, Is.EqualTo(0), "post featured is incorrect");
			Assert.That((Byte)post.page, Is.EqualTo(0), "post page is incorrect");
			Assert.That((string)post.status, Is.EqualTo("published"), "post status is incorrect");
			Assert.That((string)post.language, Is.EqualTo("en_GB"), "post language is incorrect");
			Assert.That((string)post.meta_title, Is.EqualTo("Post Title"), "post meta title is incorrect");
			Assert.That((string)post.meta_description, Is.EqualTo("Post Description"), "post meta description is incorrect");
			Assert.That((int)post.author_id, Is.EqualTo(1), "post author id is incorrect");
            Assert.That((long)post.created_at, Is.EqualTo(1418495760000), "post created at is incorrect");
			Assert.That((int)post.created_by, Is.EqualTo(1), "post created by is incorrect");
            Assert.That((long)post.updated_at, Is.EqualTo(1418495760000), "post updated at is incorrect");
			Assert.That((int)post.updated_by, Is.EqualTo(1), "post updated by is incorrect");
            Assert.That((long)post.published_at, Is.EqualTo(1418495760000), "post published at is incorrect");
			Assert.That((int)post.published_by, Is.EqualTo(1), "post published by is incorrect");
		}

        [Test]
        public void Process_WhenPassedImportWithTags_ThenShouldHaveBaseAttributes()
        {
            var import = CreateEmptyImport();
            import.Data.Tags.Add(CreateDummyTag());
            var formatter = CreateFormatter();

            var json = formatter.Process(import);
            dynamic parsed = JObject.Parse(json);
            var tag = parsed.data.tags[0];

            Assert.That(tag.id, Is.Not.Null, "id attribute should exist");
            Assert.That(tag.name, Is.Not.Null, "name attribute should exist");
            Assert.That(tag.slug, Is.Not.Null, "slug attribute should exist");
			Assert.That(tag.description, Is.Not.Null, "description attribute should exist");
        }

		[Test]
		public void Process_WhenPassedImportWithTags_ThenShouldBePopulatedProperly()
		{
			var import = CreateEmptyImport();
			import.Data.Tags.Add(CreateDummyTag());
			var formatter = CreateFormatter();

			var json = formatter.Process(import);
			dynamic parsed = JObject.Parse(json);
			var tag = parsed.data.tags[0];

			Assert.That((int)tag.id, Is.EqualTo(1), "tag id is incorrect");
			Assert.That((string)tag.name, Is.EqualTo("Orchestration"), "tag name is incorrect");
			Assert.That((string)tag.slug, Is.EqualTo("orchestration"), "tag slug is incorrect");
			Assert.That((string)tag.description, Is.EqualTo("All about orchestration"), "tag description is incorrect");
		}

		[Test]
		public void Process_WhenPassingImportWithPostTags_ThenShouldHaveBaseAttributes()
		{
			var import = CreateEmptyImport();
			import.Data.PostsTags.Add(1, 1);
			var formatter = CreateFormatter();

			var json = formatter.Process(import);
			dynamic parsed = JObject.Parse(json);
			var postTag = parsed.data.posts_tags[0];

			Assert.That(postTag.tag_id, Is.Not.Null, "tag_id attribute should exist");
			Assert.That(postTag.post_id, Is.Not.Null, "post_id attribute should exist");
		}

		[Test]
		public void Process_WhenPassingImportWithPostTags_ThenShouldBePopulatedProperly()
		{
			var import = CreateEmptyImport();
			import.Data.PostsTags.Add(1, 1);
			var formatter = CreateFormatter();

			var json = formatter.Process(import);
			dynamic parsed = JObject.Parse(json);
			var postTag = parsed.data.posts_tags[0];

			Assert.That((int)postTag.tag_id, Is.EqualTo(1), "tag id is incorrect");
			Assert.That((int)postTag.post_id, Is.EqualTo(1), "post id is incorrect");
		}

		[Test]
		public void Process_WhenPassingImportWithUsers_ThenShouldHaveBaseAttributes()
		{
			var import = CreateEmptyImport();
			import.Data.Users.Add(CreateDummyUser());
			var formatter = CreateFormatter();

			var json = formatter.Process(import);
			dynamic parsed = JObject.Parse(json);
			var user = parsed.data.users[0];

			Assert.That(user.id, Is.Not.Null, "id attribute should exist");
			Assert.That(user.name, Is.Not.Null, "name attribute should exist");
			Assert.That(user.slug, Is.Not.Null, "slug attribute should exist");
			Assert.That(user.email, Is.Not.Null, "enail attribute should exist");
			Assert.That(user.image, Is.Not.Null, "image attribute should exist");
			Assert.That(user.cover, Is.Not.Null, "cover attribute should exist");
			Assert.That(user.bio, Is.Not.Null, "bio attribute should exist");
			Assert.That(user.website, Is.Not.Null, "website attribute should exist");
			Assert.That(user.location, Is.Not.Null, "location attribute should exist");
			Assert.That(user.accessibility, Is.Not.Null, "accessibility attribute should exist");
			Assert.That(user.status, Is.Not.Null, "status attribute should exist");
			Assert.That(user.language, Is.Not.Null, "language attribute should exist");
			Assert.That(user.meta_title, Is.Not.Null, "meta_title attribute should exist");
			Assert.That(user.meta_description, Is.Not.Null, "meta_description attribute should exist");
			Assert.That(user.last_login, Is.Not.Null, "last_login attribute should exist");
			Assert.That(user.created_at, Is.Not.Null, "created_at attribute should exist");
			Assert.That(user.created_by, Is.Not.Null, "created_by attribute should exist");
			Assert.That(user.updated_at, Is.Not.Null, "updated_at attribute should exist");
			Assert.That(user.updated_by, Is.Not.Null, "updated_by attribute should exist");
		}

		[Test]
		public void Process_WhenPassingImportWithUsers_ThenShouldBePopulatedCorrectly()
		{
			var import = CreateEmptyImport();
			import.Data.Users.Add(CreateDummyUser());
			var epochTime = new Mock<IEpochTime>();
			epochTime.Setup(m => m.ConvertTo(It.IsAny<DateTime>())).ReturnsInOrder(0, 1418495760000, 1528599960000, 1628599960000);

			var formatter = CreateFormatter(epochTime.Object);

			var json = formatter.Process(import);
			dynamic parsed = JObject.Parse(json);
			var user = parsed.data.users[0];

			Assert.That((int)user.id, Is.EqualTo(1), "id is incorrect");
			Assert.That((string)user.name, Is.EqualTo("Simon Baynes"), "name is incorrect");
			Assert.That((string)user.slug, Is.EqualTo("simon-baynes"), "slug is incorrect");
			Assert.That((string)user.email, Is.EqualTo("test@test.com"), "email is incorrect");
			Assert.That((string)user.image, Is.EqualTo("image.jpg"), "image is incorrect");
			Assert.That((string)user.cover, Is.EqualTo("cover.jpg"), "cover is incorrect");
			Assert.That((string)user.bio, Is.EqualTo("I know some things"), "bio is incorrect");
			Assert.That((string)user.website, Is.EqualTo("http://bayn.es"), "website is incorrect");
			Assert.That((string)user.location, Is.EqualTo("London, UK"), "location is incorrect");
			Assert.That((string)user.accessibility, Is.EqualTo("Accessible"), "accessibility is incorrect");
			Assert.That((string)user.status, Is.EqualTo(UserStatus.Active.ToString()), "status is incorrect");
			Assert.That((string)user.language, Is.EqualTo(Language.EnglishUk.ToString()), "language is incorrect");
			Assert.That((string)user.meta_title, Is.EqualTo("Meta Title"), "meta_title is incorrect");
			Assert.That((string)user.meta_description, Is.EqualTo("Meta Description"), "meta_description is incorrect");
			Assert.That((long)user.last_login, Is.EqualTo(1418495760000), "last_login is incorrect");
			Assert.That((long)user.created_at, Is.EqualTo(1528599960000), "created_at is incorrect");
			Assert.That((int)user.created_by, Is.EqualTo(1), "created_by is incorrect");
			Assert.That((long)user.updated_at, Is.EqualTo(1628599960000), "updated_at is incorrect");
			Assert.That((int)user.updated_by, Is.EqualTo(1), "updated_by is incorrect");
		}

		private static User CreateDummyUser()
		{
			var date = new DateTime(2014, 12, 13, 18, 36, 0);
			return new User
				{
					Id = 1,
					Name = "Simon Baynes",
					Slug = "simon-baynes",
					Email = "test@test.com",
					Image = "image.jpg",
					Cover = "cover.jpg",
					Bio = "I know some things",
					Website = "http://bayn.es",
					Location = "London, UK",
					Accessibility = "Accessible",
					Status = UserStatus.Active,
					Language = Language.EnglishUk,
					MetaTitle = "Meta Title",
					MetaDescription = "Meta Description",
					LastLogin = date,
					CreatedAt = date,
					CreatedBy = 1,
					UpdatedAt = date,
					UpdatedBy = 1
				};
		}

		private static Tag CreateDummyTag()
	    {
		    return new Tag
			    {
				    Id = 1,
					Name = "Orchestration",
					Slug = "orchestration",
					Description = "All about orchestration"
			    };
	    }

	    public Post CreateDummyPost()
		{
			var createdAt = new DateTime(2014, 12, 13, 18, 36, 0);
			return new Post
			{
				Id = 1,
				AuthorId = 1,
				CreatedAt = createdAt,
				CreatedBy = 1,
				Featured = false,
				Html = "<h1>Title</h1>",
				Image = null,
				Language = Language.EnglishUk,
				Markdown = "# Title",
				MetaDescription = "Post Description",
				MetaTitle = "Post Title",
				Status = PostStatus.Published,
				Type = PostType.Post,
				PublishedAt = createdAt,
				PublishedBy = 1,
				Slug = "/title+post/",
				Title = "Title",
				UpdatedAt = createdAt,
				UpdatedBy = 1
			};
		}

		private static Import CreateEmptyImport()
		{
			return new Import();
		}

		private static JsonFormatter CreateFormatter(IEpochTime epochTime = null)
		{
			return new JsonFormatter(epochTime ?? new Mock<IEpochTime>().Object);
		}
	}
}

namespace MoqExtentions
{
	public static class MockExtention
	{
		public static void ReturnsInOrder<T, TResult>(this ISetup<T, TResult> setup, params TResult[] results) where T : class
		{
			setup.Returns(new Queue<TResult>(results).Dequeue);
		}
	}
}
