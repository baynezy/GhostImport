# GhostImport

This is a library to help you create the import JSON for the Ghost Blogging Platform. Details of the format required can be found here.

[https://github.com/TryGhost/Ghost/wiki/Import-format]([https://github.com/TryGhost/Ghost/wiki/Import-format)

## Build Status

<table>
    <tr>
        <th>master</th>
		<td><a href="https://ci.appveyor.com/project/baynezy/ghostimport"><img src="https://ci.appveyor.com/api/projects/status/6qnsyy43p4arrjmc/branch/master?svg=true" alt="master" title="master" /></a></td>
    </tr>
    <tr>
        <th>develop</th>
		<td><a href="https://ci.appveyor.com/project/baynezy/ghostimport"><img src="https://ci.appveyor.com/api/projects/status/6qnsyy43p4arrjmc/branch/develop?svg=true" alt="develop" title="develop" /></a></td>
    </tr>
</table>

## Installing via NuGet

[![NuGet version](https://badge.fury.io/nu/Ghost.Import.svg)](http://badge.fury.io/nu/Ghost.Import)

	PM> Install-Package Ghost.Import

## Usage

	var formatter = new JsonFormatter(new EpochTime());
	var file = new FileOperation();
	var exporter = new GhostExporter(formatter, file);

	var import = new Import
		{
			Data =
				{
					Posts =
						{
							new Post
								{
									Id = 5,
									Title = "my blog post title",
									Slug = "my-blog-post-title",
									Markdown = "the *markdown* formatted post body",
									Html = "the <i>html</i> formatted post body",
									Image = null,
									Featured = false,
									Type = PostType.Post,
									Status = PostStatus.Published,
									Language = Language.EnglishUs,
									MetaTitle = null,
									MetaDescription = null,
									AuthorId = 1,
									CreatedAt = DateTime.Now,
									CreatedBy = 1,
									UpdatedAt = DateTime.Now,
									UpdatedBy = 1,
									PublishedAt = DateTime.Now,
									PublishedBy = 1
								}
						},
					Tags =
						{
							new Tag
								{
									Id = 3,
									Name = "Colorado Ho!",
									Slug = "colorado-ho",
									Description = ""
								},
							new Tag
								{
									Id = 4,
									Name = "blue",
									Slug = "blue",
									Description = ""
								}
						},
					PostsTags =
						{
							new PostTag(3,5),
							new PostTag(3,2),
							new PostTag(4,24)
						},
					Users =
						{
							new User
								{
									Id = 2,
									Name = "user's name",
									Slug = "users-name",
									Email = "user@example.com",
									Image = null,
									Cover = null,
									Bio = null,
									Website = null,
									Location = null,
									Accessibility = null,
									Status = UserStatus.Active,
									Language = Language.EnglishUs,
									MetaTitle = null,
									MetaDescription = null,
									LastLogin = null,
									CreatedAt = DateTime.Now,
									CreatedBy = 1,
									UpdatedAt = DateTime.Now,
									UpdatedBy = 1
								}
						},
					UserRoles =
						{
							new UserRole(2,3)
						}
				}
		};

	exporter.Export(import, "../../../../../export.json");
