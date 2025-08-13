using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>Outlines parameters for an address search</summary>
public class AddressSearchRequest
{
    /// <summary>Line 1 of the street address.</summary>
    [Required]
    public string? Address1 { get; set; }
    /// <summary>The city or municipality in which the property is located</summary>
    public string? City { get; set; }
    /// <summary>The U.S. state or territory where the property is located.</summary>
    public string? State { get; set; }
    /// <summary>The Postal code of the property.</summary>S
    public int? PostCode { get; set; }
}