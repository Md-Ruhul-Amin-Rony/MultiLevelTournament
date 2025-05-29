using System.ComponentModel.DataAnnotations;

namespace MultiLevelTournament.Models
{
    public class CreatePlayerModel
    {
        //public int Id { get; set; }
        public required string Name { get; set; }
        [Range(1, 120)]
        public int Age { get; set; }
    }
}
