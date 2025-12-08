using System;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptions.Models;

public class Pet
{
    [Key]
    public int PetId {get; set;}

    [Required, StringLength(30)]
    public string Name {get; set;} = string.Empty;

    [Required]
    public string Species {get; set;} = string.Empty;

    [Required]
    public string Breed {get; set;} = string.Empty;

    [Required]
    public int Age {get; set;}

    [Required]
    public string Status {get; set;} = "Available";

    [Required]
    public DateTime IntakeDate {get; set;}

    public string? PhotoUrl {get; set;}

    public int? AdopterId {get; set;}

    public Adopter? Adopter {get; set;}
}