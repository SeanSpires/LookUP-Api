namespace LookUpApi.Models
{
    public class LuisResponse
    {
        public string query { get; set; }

        public Intent topScoringIntent { get; set; }
        
    }

    public class Intent
    {
        public string intent { get; set; }

        public string score { get; set; }
    }
}