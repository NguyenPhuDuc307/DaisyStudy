using System.ComponentModel.DataAnnotations;

namespace DaisyStudy.ViewModels.Catalog.Messages;

public class MessageViewModel
{
    [Required]
    public string? Content { get; set; }
    public DateTime? TimeStamp { get; set; }
    public string? From { get; set; }
    [Required]
    public string? RoomChatName { get; set; }
    public string? Avatar { get; set; }
}
