namespace Base.Api.Model
{
	public class WithdrawalLimitDto
	{
		public int WebId { get; set; }
		public int CustomerId { get; set; }
		public decimal WithdrawalLimit { get; set; }
		public bool IsProcess { get; set; }
	}
}