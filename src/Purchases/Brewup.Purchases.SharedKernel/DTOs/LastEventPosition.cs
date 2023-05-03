using Brewup.Purchases.SharedKernel.ReadModel;

namespace Brewup.Purchases.SharedKernel.DTOs
{
  public class LastEventPosition : Dto
  {
    public long CommitPosition { get; set; }
    public long PreparePosition { get; set; }
  }
}
