using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.WebApp.Models
{
    public class LocationProductRequestViewModel
    {
        public Guid SelectedLocationId { get; set; }
        public List<Tuple<string, Guid>> LocationNamesWithIds { get; set; }
        public ProductRequestViewModel ProductRequestViewModel { get; set; }

        public LocationProductRequestViewModel() { }
    }
}
