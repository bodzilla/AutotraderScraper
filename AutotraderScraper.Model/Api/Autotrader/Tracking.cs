using System;
using System.Collections.Generic;
using AutotraderScraper.Model.Interfaces;
using Newtonsoft.Json;

namespace AutotraderScraper.Model.Api.Autotrader
{
    public class Tracking : IBaseModel
    {
        public Tracking()
        {
            VirtualPageDatas = new HashSet<PageData>();
        }

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        [JsonProperty("advertiser_segment", NullValueHandling = NullValueHandling.Ignore)]
        public string AdvertiserSegment { get; set; }

        [JsonProperty("body_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BodyType { get; set; }

        [JsonProperty("number_of_seats", NullValueHandling = NullValueHandling.Ignore)]
        public int? NumberOfSeats { get; set; }

        [JsonProperty("average_mpg", NullValueHandling = NullValueHandling.Ignore)]
        public string AverageMpg { get; set; }

        [JsonProperty("page_name", NullValueHandling = NullValueHandling.Ignore)]
        public string PageName { get; set; }

        [JsonProperty("fuel_type", NullValueHandling = NullValueHandling.Ignore)]
        public string FuelType { get; set; }

        [JsonProperty("userid", NullValueHandling = NullValueHandling.Ignore)]
        public string Userid { get; set; }

        [JsonProperty("loc_one", NullValueHandling = NullValueHandling.Ignore)]
        public string LocOne { get; set; }

        [JsonProperty("dealer_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? DealerId { get; set; }

        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public string Platform { get; set; }

        [JsonProperty("great_value", NullValueHandling = NullValueHandling.Ignore)]
        public string GreatValue { get; set; }

        [JsonProperty("vehicle_check_id", NullValueHandling = NullValueHandling.Ignore)]
        public string VehicleCheckId { get; set; }

        [JsonProperty("engine_size", NullValueHandling = NullValueHandling.Ignore)]
        public string EngineSize { get; set; }

        [JsonProperty("co2_emissions", NullValueHandling = NullValueHandling.Ignore)]
        public int? Co2Emissions { get; set; }

        [JsonProperty("logged_in_status", NullValueHandling = NullValueHandling.Ignore)]
        public string LoggedInStatus { get; set; }

        [JsonProperty("finance_view_selected", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FinanceViewSelected { get; set; }

        [JsonProperty("dealer_products", NullValueHandling = NullValueHandling.Ignore)]
        public string DealerProducts { get; set; }

        [JsonProperty("annual_tax", NullValueHandling = NullValueHandling.Ignore)]
        public int? AnnualTax { get; set; }

        [JsonProperty("mileage", NullValueHandling = NullValueHandling.Ignore)]
        public int? Mileage { get; set; }

        [JsonProperty("vehicle_year", NullValueHandling = NullValueHandling.Ignore)]
        public int? VehicleYear { get; set; }

        [JsonProperty("page_name_granular", NullValueHandling = NullValueHandling.Ignore)]
        public string PageNameGranular { get; set; }

        [JsonProperty("page_number", NullValueHandling = NullValueHandling.Ignore)]
        public int? PageNumber { get; set; }

        [JsonProperty("vehicle_price", NullValueHandling = NullValueHandling.Ignore)]
        public int? VehiclePrice { get; set; }

        [JsonProperty("manufacturer_approved", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ManufacturerApproved { get; set; }

        [JsonProperty("top_speed", NullValueHandling = NullValueHandling.Ignore)]
        public string TopSpeed { get; set; }

        [JsonProperty("make", NullValueHandling = NullValueHandling.Ignore)]
        public string Make { get; set; }

        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }

        [JsonProperty("acceleration", NullValueHandling = NullValueHandling.Ignore)]
        public string Acceleration { get; set; }

        [JsonProperty("gearbox", NullValueHandling = NullValueHandling.Ignore)]
        public string Gearbox { get; set; }

        [JsonProperty("ad_id", NullValueHandling = NullValueHandling.Ignore)]
        public string AdId { get; set; }

        [JsonProperty("vehicle_check_status", NullValueHandling = NullValueHandling.Ignore)]
        public string VehicleCheckStatus { get; set; }

        [JsonProperty("number_of_doors", NullValueHandling = NullValueHandling.Ignore)]
        public int? NumberOfDoors { get; set; }

        [JsonProperty("experiment_variant", NullValueHandling = NullValueHandling.Ignore)]
        public string ExperimentVariant { get; set; }

        [JsonProperty("section", NullValueHandling = NullValueHandling.Ignore)]
        public string Section { get; set; }

        [JsonProperty("location_area", NullValueHandling = NullValueHandling.Ignore)]
        public string LocationArea { get; set; }

        [JsonProperty("monthly_vehicle_price", NullValueHandling = NullValueHandling.Ignore)]
        public string MonthlyVehiclePrice { get; set; }

        [JsonProperty("number_of_photos", NullValueHandling = NullValueHandling.Ignore)]
        public int? NumberOfPhotos { get; set; }

        [JsonProperty("aoi", NullValueHandling = NullValueHandling.Ignore)]
        public string Aoi { get; set; }

        [JsonProperty("finance_representative_apr", NullValueHandling = NullValueHandling.Ignore)]
        public string FinanceRepresentativeApr { get; set; }

        [JsonProperty("advert_attributes", NullValueHandling = NullValueHandling.Ignore)]
        public string AdvertAttributes { get; set; }

        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; }

        public virtual ICollection<PageData> VirtualPageDatas { get; set; }
    }
}
