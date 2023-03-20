namespace PracticeApi.Models
{
    public class Hero : BaseModel
    {
        public int? HeroId {get; set;}
        public String? HeroName {get; set;}
        public String? HeroAddress {get; set;}
        public String? HeroSecret { get; set;}
    }

}