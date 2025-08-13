using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.Text.Json;

namespace WebApi.Controllers
{
    /// <summary></summary>
    [ApiController]
    [Route("[controller]")]
    public class AddressSearchController : ControllerBase
    {
        private string jsonFilePath = "Data/AddressData.json";

        /// <summary>Returns property address and details using address, state, city, and postal code.</summary>
        /// <param name="addressLine1">Address line 1 of the property.</param>
        /// <param name="state">The state the property is located in.</param>
        /// <param name="city">The city the property is located in.</param>
        /// <param name="postalCode">The postal code of the property.</param>
        /// <returns>AddressSearchItem</returns>
        [HttpGet(Name = "(GET) Property Addresses and Information")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful response")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Unsuccessful response")]
        public IActionResult Get(
            [FromQuery] string addressLine1,
            [FromQuery] string state,
            [FromQuery] string city,
            [FromQuery] int? postalCode)
        {
            if (containsNullParameters(addressLine1, state, city, postalCode))
                return BadRequest("Cannot have null parameters.");

            var addresses = ReturnAddressList(addressLine1, state, city, postalCode);

            if (addresses == null)
                return BadRequest("No matches found.");

            return Ok(addresses);
        }

        /// <summary>Search for property address and details using address, state, city, and postal code.</summary>
        /// <param name="request">AddressSearchRequest object used for address search.</param>
        /// <returns>AddressSearchItem</returns>
        [HttpPost("search")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful response")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Unsuccessful response")]
        public async Task<ActionResult<List<AddressSearchItem>>> PostSearchAsync([FromBody] AddressSearchRequest request)
        {
            // await Task.Delay(100);

            if (request == null || containsNullParameters(request.Address1, request.State, request.City, request.PostCode))
                return BadRequest("Request body is null.");

            var filteredAddresses = ReturnAddressList(request.Address1, request.State, request.City, request.PostCode);

            if (filteredAddresses == null)
                return BadRequest("No matches found.");

            return Ok(filteredAddresses);
        }

        /// <summary>Add a new address item if it doesn not already exist.</summary>
        /// <param name="addressSearchItem"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful response")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Unsuccessful response")]
        public IActionResult Post([FromBody] AddressSearchItem addressSearchItem)
        {
            if (!addAddressItem(addressSearchItem))
                return BadRequest("Address item already exists");

            return Ok("Created and added object");
        }

        // Returns list of addresses that match the arguments
        private IEnumerable<AddressSearchItem> ReturnAddressList(string? addressLine1, string? state, string? city, int? postalCode)
        {
            List<AddressSearchItem> addresses = new List<AddressSearchItem>();
            
            try
            {
                var json = System.IO.File.ReadAllText(jsonFilePath);
                addresses = JsonSerializer.Deserialize<List<AddressSearchItem>>(json) ?? new List<AddressSearchItem>();
            } 
            catch
            {
                return new List<AddressSearchItem>();
            }

            return addresses.Where(a => MatchesRequest(a, addressLine1, state, city, postalCode));
        }

        // Returns true if the paramaters match the AddressSearchItem object
        private bool MatchesRequest(AddressSearchItem a, string? addressLine1, string? state, string? city, int? postalCode)
        {
            if (containsNullParameters(a.AddressLine1, a.State, a.City, a.PostalCode))
                return false;

            return  a.AddressLine1.Equals(addressLine1, StringComparison.OrdinalIgnoreCase) &&
                    a.State.Equals(state, StringComparison.OrdinalIgnoreCase) &&
                    a.City.Equals(city, StringComparison.OrdinalIgnoreCase) &&
                    a.PostalCode == postalCode;
        }

        private bool containsNullParameters(string? addr, string? state, string? city, int? postCode)
        {
            return  string.IsNullOrWhiteSpace(addr) || string.IsNullOrWhiteSpace(state) ||
                    string.IsNullOrWhiteSpace(city) || postCode == null;
        }

        // Append new address to data file
        private bool addAddressItem(AddressSearchItem addressSearchItem)
        {
            var json = System.IO.File.ReadAllText(jsonFilePath);
            var addresses = JsonSerializer.Deserialize<List<AddressSearchItem>>(json) ?? new List<AddressSearchItem>();

            // check for duplicates
            if (addresses.Contains(addressSearchItem))
                return false;

            addresses.Add(addressSearchItem);

            var updatedJson = JsonSerializer.Serialize(addresses, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(jsonFilePath, updatedJson);

            return true;
        }
    }
}