namespace Application.Features.Titles.Commands.Add
{
	public class AddTitleResponse
	{
		public short Id { get; set; }

		public string Name { get; set; }

		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public bool IsDeleted { get; set; }
	}
}
