using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptions.Models;

public class Adopter
{
    [Key]
    public int AdopterId {get; set;}

    [Required, StringLength (30)]
    public string FirstName {get; set;} = string.Empty;

    [Required, StringLength (30)]
    public string LastName {get; set;} = string.Empty;

    [Required, EmailAddress]
    public string Email {get; set;} = string.Empty;

    [Required, Phone]
    public string Phone {get; set;} = string.Empty;

    [Required]
    public string Address {get; set;} = string.Empty;

    public ICollection<Pet> Pets {get; set;} = new List<Pet>();
}