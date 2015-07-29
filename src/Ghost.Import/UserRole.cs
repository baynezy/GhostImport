namespace Ghost.Import
{
	public class UserRole
	{
		public UserRole(int userId, int roleId)
		{
			UserId = userId;
			RoleId = roleId;
		}

		public int UserId { get; set; }

		public int RoleId { get; set; }
	}
}
