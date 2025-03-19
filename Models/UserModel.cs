using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models; 

public class UserModel {

    public int Id { get; set; }

    [Required]
    public string nome { get; set; } = string.Empty;
    
    [Required]
    public string senhaHash { get; set; } = string.Empty;

}