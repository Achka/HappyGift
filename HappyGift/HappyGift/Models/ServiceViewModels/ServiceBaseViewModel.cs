namespace HappyGift.Models.ServiceViewModels
{
    public class ServiceBaseViewModel
    {
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceImageURL { get; set; }
        public decimal ServicePrice { get; set; }
        public double AvarageAgeOfCustomer { get; set; }
        public int MinAgeOfUser { get; set; }
        public int MaxAgeOfUser { get; set; }
    }
}
