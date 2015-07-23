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
			Assert.NotNull(parsed.data.post_tags, "There should be a data.post_tags attribute");
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
