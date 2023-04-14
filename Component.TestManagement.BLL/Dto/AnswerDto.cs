namespace Component.TestManagement.BLL.Dto
{
	public class AnswerDto
	{
		public int Id { get; set; }
		public int TestTaskId { get; set; }
		public int? TaskOptionId { get; set; }
		public string? Value { get; set; }
	}
}