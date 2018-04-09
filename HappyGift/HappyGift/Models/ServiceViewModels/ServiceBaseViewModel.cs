namespace HappyGift.Models.ServiceViewModels
{
    public class ServiceBaseViewModel
    {
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceImageURL { get; set; }
        public decimal ServicePrice { get; set; }
    }
}
