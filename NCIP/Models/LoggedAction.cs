namespace NCIP.Models
{
    public class LoggedAction
    {
        public int ID { get; set; }
        public string ActionDone { get; set; }

        public string ApplicationUserID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}