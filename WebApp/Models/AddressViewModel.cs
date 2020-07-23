using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.AddressViewModel;
namespace WebApp.Models
{
    public class AddressViewModel : EntityViewModel
    {
        [Display(Name = Lang.CodeLabel)]
        public string Code { get; set; }

        [Display(Name = Lang.StreetLabel)]
        public string Street { get; set; }

        [Display(Name = Lang.StreetNumberLabel)]
        public string StreetNumber { get; set; }

        [Display(Name = Lang.PostalCodeLabel)]
        public string PostalCode { get; set; }

        [Display(Name = Lang.LocalityLabel)]
        public string Locality { get; set; }

        [Display(Name = Lang.CityLabel)]
        public string City { get; set; }

        [Display(Name = Lang.CountryLabel)]
        public string Country { get; set; }

        [Display(Name = Lang.LatitudeLabel)]
        public double Latitude { get; set; }

        [Display(Name = Lang.LongitudeLabel)]
        public double Longitude { get; set; }

        public AddressViewModel()
        {

        }

        public AddressViewModel(IgniteAddress address)
        {
            this.Code = address.Code;
            this.Street = address.Street;
            this.StreetNumber = address.StreetNumber;
            this.PostalCode = address.PostalCode;
            this.Locality = address.Locality;
            this.City = address.City;
            this.Country = address.Country;
            this.Latitude = address.Latitude;
            this.Longitude = address.Longitude;
        }

        public IgniteAddress ToEntity()
        {
            return new IgniteAddress()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Code = this.Code,
                Street = this.Street,
                StreetNumber = this.StreetNumber,
                PostalCode = this.PostalCode,
                Locality = this.Locality,
                City = this.City,
                Country = this.Country,
                Latitude = this.Latitude,
                Longitude = this.Longitude
            };
        }
    }
}