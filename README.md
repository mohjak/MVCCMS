# MVCCMS
## Post Management
### Create New Post
User can add new post initial version.
### Edit a Post
User can edit a post initial version.
### Add tags for a post
User can add tags for a post initial version.
### Friendly URL Code
The below code helps make a friendly urls **(input="Fist Post" => output="first-post")**:
```C#
public static string MakeUrlFriendly(this string value)
{
	value = value.ToLowerInvariant().Replace(" ", "-");
	value = Regex.Replace(value, @"[^0-9a-z-]", string.Empty);

	return value;
}
```
## User Management
### Add New User
