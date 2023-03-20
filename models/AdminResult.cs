using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApi.Models
{
    public class AdminResult
    {
        public int Id { get; set; }
        public String AdminName {get; set;} = string.Empty;
        public String Email {get; set;} = string.Empty;
        public String LoginName {get; set;} = string.Empty;
        public bool Inactive {get; set;}
        public int AdminLevelId {get; set;}
        public String? AdminLevelName { get; set; }
    }
}