using System.ComponentModel.DataAnnotations;

namespace MultiLevelTournament.Models
{
    public class UpdatePlayerModel
    {
        public required string Name { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }
    }
}
