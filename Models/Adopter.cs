using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptions.Models;

public class Adopter
{
    // Primary key.
    [Key]
    public int AdopterId {get; set;}

    // Adopter first name.
    [Required, StringLength (30)]
    public string FirstName {get; set;} = string.Empty;

    // Adopter last name.
    [Required, StringLength (30)]
    public string LastName {get; set;} = string.Empty;

    // Adopter e-mail.
    [Required, EmailAddress]
    public string Email {get; set;} = string.Empty;

    // Adopter phone number.
    [Required, Phone]
    public string Phone {get; set;} = string.Empty;

    // Adopter home address.
    [Required]
    public string Address {get; set;} = string.Empty;

    // List of adopter's pets. 
    public ICollection<Pet> Pets {get; set;} = new List<Pet>();
    
}