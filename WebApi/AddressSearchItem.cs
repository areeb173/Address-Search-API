using Swashbuckle.AspNetCore.Annotations;

/// <summary>The property search and standardization result.</summary>
public class AddressSearchItem : IEquatable<AddressSearchItem>
{
    /// <summary>The assessor parcel number.</summary>
    public string? AssessorsParcelNumber { get; set; }
    /// <summary>The standardized assessor parcel number.</summary>
    public string? StandardizedAssessorsParcelNumber { get; set; }
    /// <summary>Line 1 of the street address.</summary>
    public string? AddressLine1 { get; set; }
    /// <summary>Line 2 of the street address.</summary>
    public string? AddressLine2 { get; set; }
    /// <summary>The city or municipality in which the property is located</summary>
    public string? City { get; set; }
    /// <summary>The U.S. state or territory where the property is located.</summary>
    public string? State { get; set; }
    /// <summary>The Postal code of the property.</summary>
    public int? PostalCode { get; set; }
    /// <summary>The four-digit extension to the standard postal code</summary>
    public int? PostalCodePlus4 { get; set; }
    /// <summary>House or building number.</summary>
    public int? AddressHouseNumber { get; set; }
    /// <summary>Pre Directional code.</summary>
    public string? AddressPreDirection { get; set; }
    /// <summary>The Street name of the property.</summary>
    public string? AddressStreetName { get; set; }
    /// <summary>Address Street suffix.</summary>
    public string? AddressStreetSuffix { get; set; }
    /// <summary>Post Directional code.</summary>
    public string? AddressPostDirection { get; set; }
    /// <summary>The classification of the property type.</summary>
    public string? AddressUnitType { get; set; }
    /// <summary>Apartment or unit number.</summary>
    public string? AddressUnitNumber { get; set; }
    /// <summary>The FIPS code which uniquely idenitfies the county</summary>
    public int? CountyFips { get; set; }
    /// <summary>The county in which the property is located.</summary>
    public string? County { get; set; }
    /// <summary>Latitude coordinate of the property.</summary>
    public double? Latitude { get; set; }
    /// <summary>Longitude coordinate of the property.</summary>
    public double? Longitude { get; set; }
    /// <summary>The full name of the property’s owner</summary>
    public string? CurrentOwner { get; set; }
    /// <summary>Specifies whether the property is occupied by Owner.</summary>
    public bool? OwnerOccupied { get; set; }
    /// <summary>First 3 digits of the zip code.</summary>
    public int? ZIP1st3 { get; set; }
    /// <summary>First 4 digits of the zip code.</summary>
    public int? ZIP1st4 { get; set; }
    /// <summary>Full property address.</summary>
    public string? FullAddress { get; set; }

    /// <summary>Overrides Equals operation</summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(AddressSearchItem? other)
    {
        if (other == null) return false;

        if (AddressLine1 == null || State == null || City == null) return false;

        return  AddressLine1.Equals(other.AddressLine1, StringComparison.OrdinalIgnoreCase) &&
                State.Equals(other.State, StringComparison.OrdinalIgnoreCase) &&
                City.Equals(other.City, StringComparison.OrdinalIgnoreCase) &&
                PostalCode == other.PostalCode;
    }

    /// <summary></summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as AddressSearchItem);
    }
}