using System;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptions.Models;

public class Pet
{
    // Primary key.
    [Key]
    public int PetId {get; set;}

    // Pet name.
    [Required, StringLength(30)]
    public string Name {get; set;} = string.Empty;

    // Pet species.
    [Required]
    public string Species {get; set;} = string.Empty;

    // Pet breed.
    [Required]
    public string Breed {get; set;} = string.Empty;

    // Pet age.
    [Required]
    public int Age {get; set;}

    // Pet adoption status. Defaults to "Available."
    [Required]
    public string Status {get; set;} = "Available";

    // Pet intake date.
    [Required]
    public DateTime IntakeDate {get; set;}

    // Foreign key to adopter.
    public int? AdopterId {get; set;}

    // For access to adopter associated witih pet.
    public Adopter? Adopter {get; set;}
}