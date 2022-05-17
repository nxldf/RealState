using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.GeHomesInformations
{
    public class GeHomesInformationAppService : RealEstateAppServiceBase , IGeHomesInformationAppService
    {


        public async Task<HomeInformation> HomeInformations(string Url)
        {

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(Url);
            var pageContents = await response.Content.ReadAsStringAsync();
            List<string> test = new List<string>();
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);
            Console.OutputEncoding = Encoding.UTF8;
            bool successfullyParsed = false;
            int convertedInt = 0;
            decimal convertedDec = 0;
            long convertedlong = 0;
            int count;

            HomeInformation homeInformation = new HomeInformation();
            //*****************************************************************************
            homeInformation.Name = pageDocument.DocumentNode.SelectSingleNode("(//h1[contains(@class,'Text-sc-10o2fdq-0 bLnjhH')])").InnerText;
            //*** I should convert it to decimal***
            var _HomePrice = pageDocument.DocumentNode.SelectSingleNode("(//span[contains(@class,'Text-sc-10o2fdq-0 iYbzSg')])").InnerText;
            _HomePrice = _HomePrice.Contains("m²") ? _HomePrice.Substring(0, _HomePrice.IndexOf("m²")) : _HomePrice;
            _HomePrice = _HomePrice.Replace(",", ".");
            successfullyParsed = decimal.TryParse(_HomePrice, out convertedDec);
            homeInformation.NetPrice = successfullyParsed ? convertedDec : 0;
            var _AdvertisementTypes = pageDocument.DocumentNode.SelectSingleNode("(//span[contains(@class,'Text-sc-10o2fdq-0 bIIYcF')])").InnerText;
            switch (_AdvertisementTypes)
            {
                case "Kaufpreis":
                    homeInformation.AdvertisementType = 20;
                    break;
                case "Gesamtmiete inkl. MWSt":
                    homeInformation.AdvertisementType = 10;
                    break;

                default:
                    homeInformation.AdvertisementType = 0;
                    break;
            }
            homeInformation.Description = pageDocument.DocumentNode.SelectSingleNode("(//div[contains(@class,'Box-sc-wfmb7k-0 Description___StyledBox2-sc-brstfz-1  kIUlQs')])").InnerText;
            var _HomeAddress = pageDocument.DocumentNode.SelectSingleNode("(//div[contains(@data-testid,'object-location-address')])").InnerText;
            int split = _HomeAddress.IndexOf(",");
            homeInformation.DistrictName = _HomeAddress.Substring(0, split);
            homeInformation.Address = _HomeAddress.Substring(++split, _HomeAddress.Length - split);

            #region homeInformationrmation
            var gethomeInformationrmationTags = pageDocument.DocumentNode.SelectNodes("(//div[contains(@class,'Box-sc-wfmb7k-0 Columns-sc-1kewbr2-0  cYzFnC')]//div)");
            count = gethomeInformationrmationTags.Count;


            for (int i = 1; i < count; i += 2)
            {
                var lable = pageDocument.DocumentNode.SelectSingleNode("(//div[contains(@class,'Box-sc-wfmb7k-0 Columns-sc-1kewbr2-0  cYzFnC')]//div)[" + (i) + "]").InnerText;
                var info = pageDocument.DocumentNode.SelectSingleNode("(//div[contains(@class,'Box-sc-wfmb7k-0 Columns-sc-1kewbr2-0  cYzFnC')]//div)[" + (i + 1) + "]").InnerText;

                switch (lable)
                {
                    case "Objekttyp":
                        switch (info)
                        {
                            case "detached house":
                                homeInformation.Type = (Entities.PropertyType)0;
                                break;
                            case "Wohnung":
                                homeInformation.Type = (Entities.PropertyType)10;
                                break;
                            case "Doppelhaushälfte":
                                homeInformation.Type = (Entities.PropertyType)20;
                                break;
                            default:
                                homeInformation.Type = (Entities.PropertyType)30;
                                break;
                        }
                        break;
                    case "Zustand":
                        homeInformation.Condition = info;
                        break;
                    case "Wohnfläche":
                        {
                            info = info.Contains("m²") ? info.Substring(0, info.IndexOf("m²")) : info;
                            info = info.Contains(",") ? info.Substring(0, info.IndexOf(",")) : info;
                            successfullyParsed = long.TryParse(info, out convertedlong);
                            homeInformation.Space = successfullyParsed ? convertedlong : 0;
                        }
                        break;
                    case "Zimmer":
                        {
                            successfullyParsed = int.TryParse(info, out convertedInt);
                            homeInformation.Room = successfullyParsed ? convertedInt : 0;
                            homeInformation.Bedrooms = successfullyParsed ? convertedInt - 1 : 0;
                        }
                        break;
                    case "TopNummer":
                        {
                            info = info.Contains("m²") ? info.Substring(0, info.IndexOf("m²")) : info;
                            successfullyParsed = int.TryParse(info, out convertedInt);
                            homeInformation.TopNumber = successfullyParsed ? convertedInt : 0;
                        }
                        break;
                    case "Stockwerk":
                        {
                            info = info.Contains("m²") ? info.Substring(0, info.IndexOf("m²")) : info;
                            successfullyParsed = int.TryParse(info, out convertedInt);
                            homeInformation.FloorSpace = successfullyParsed ? convertedInt : 0;
                        }
                        break;
                    case "Heizung":
                        homeInformation.Heating = info;
                        break;
                    case "Böden":
                        homeInformation.Floors = info;
                        break;
                    case "Verfügbar":
                        homeInformation.Accessible = info;
                        break;
                    case "Befristung":
                        homeInformation.Limitation = info;
                        break;
                    case "Einbauküche":
                        homeInformation.EquippedKitchen = "Einbauküche";
                        break;
                    case "Teilmöbliert /":
                        homeInformation.PartlyFurnished = "Teilmöbliert";
                        break;
                    case "Möbliert":
                        homeInformation.Furnished = "Möbliert";
                        break;
                    case "Fahrstuhl":
                        homeInformation.Elevator = "Fahrstuhl";
                        break;
                    case "Abstellraum":
                        homeInformation.StorageRoom = "Abstellraum";
                        break;
                    case "Balkon":
                        homeInformation.Balcony = "Balkon";
                        break;
                    case "Barrierefrei":
                        homeInformation.BarrierFree = "Barrierefrei";
                        break;
                    case "Parkplatz":
                        homeInformation.ParkingSpot = "Parkplatz";
                        break;
                    case "Garten":
                        homeInformation.Garden = "Garten";
                        break;
                    case "loggia":
                        homeInformation.Loggia = "loggia";
                        break;
                    case "Schwimmbad":
                        homeInformation.Pool = "Schwimmbad";
                        break;
                    case "Terrasse":
                        homeInformation.Terrace = "Terrasse";
                        break;
                    case "Carport":
                        homeInformation.Carport = "Carport";
                        break;
                    case "Klimaanlage":
                        homeInformation.AirConditioning = "Klimaanlage";
                        break;
                    case "Keller":
                        homeInformation.BasementCellar = "Keller";
                        break;
                    case "Garage":
                        homeInformation.Garage = "Garage";
                        break;

                    default:
                        break;
                }
            }
            #endregion

            #region homeInformationrmation
            var gethomeInformationrmation = pageDocument.DocumentNode.SelectNodes("(//div[contains(@class,'Box-sc-wfmb7k-0 cIctPl')]//span[contains(@class,'Text-sc-10o2fdq-0 PriceInformationAttributesBox___StyledText-sc-1a44msw-0 kTmUzk KkhrK')])");
            count = gethomeInformationrmation.Count * 2;

            for (int i = 1; i <= count; i += 2)
            {
                var lable = pageDocument.DocumentNode.SelectSingleNode("(//div[contains(@class,'Box-sc-wfmb7k-0 gJsgsd')]//span)[" + (i) + "]").InnerText;

                var info = pageDocument.DocumentNode.SelectSingleNode("(//div[contains(@class,'Box-sc-wfmb7k-0 gJsgsd')]//span)[" + (i + 1) + "]").InnerText;


                switch (lable)
                {
                    case "Miete (exkl. MWSt)":
                        {

                            info = info.Contains("€ ") ? info.Substring(2, info.Length - 2) : info;
                            info = info.Replace(",", ".");
                            successfullyParsed = decimal.TryParse(info, out convertedDec);
                            homeInformation.Rent_Excl = successfullyParsed ? convertedDec : 0;
                        }
                        break;
                    case "Miete (inkl. MWSt)":
                        {
                            info = info.Contains("€ ") ? info.Substring(2, info.Length - 2) : info;
                            info = info.Replace(",", ".");
                            successfullyParsed = decimal.TryParse(info, out convertedDec);
                            homeInformation.Rent_Incl = successfullyParsed ? convertedDec : 0;
                        }
                        break;
                    case "Betriebskosten (exkl. MWSt)":
                        {
                            info = info.Contains("€ ") ? info.Substring(2, info.Length - 2) : info;
                            info = info.Replace(",", ".");
                            successfullyParsed = decimal.TryParse(info, out convertedDec);
                            homeInformation.OperatingCosts_Excl = successfullyParsed ? convertedDec : 0;
                        }
                        break;
                    case "Betriebskosten (inkl. MWSt)":
                        {
                            info = info.Contains("€ ") ? info.Substring(2, info.Length - 2) : info;
                            info = info.Replace(",", ".");
                            successfullyParsed = decimal.TryParse(info, out convertedDec);
                            homeInformation.OperatingCosts_Incl = successfullyParsed ? convertedDec : 0;
                        }
                        break;
                    case "Heizkosten (exkl. MWSt)":
                        {
                            info = info.Contains("€ ") ? info.Substring(2, info.Length - 2) : info;
                            info = info.Replace(",", ".");
                            successfullyParsed = decimal.TryParse(info, out convertedDec);
                            homeInformation.Heating_Excl = successfullyParsed ? convertedDec : 0;
                        }
                        break;
                    case "Heizkosten (inkl. MWSt)":
                        {
                            info = info.Contains("€ ") ? info.Substring(2, info.Length - 2) : info;
                            info = info.Replace(",", ".");
                            successfullyParsed = decimal.TryParse(info, out convertedDec);
                            homeInformation.Heating_Incl = successfullyParsed ? convertedDec : 0;
                        }
                        break;
                    case "Kaution":
                        {
                            //info = info.Contains("€ ") ? info.Substring(2,info.Length-2) : info;
                            info = info.Replace(",", ".");
                            successfullyParsed = decimal.TryParse(info, out convertedDec);
                            homeInformation.Deposit = successfullyParsed ? convertedDec : 0;
                        }
                        break;
                    case "Zusatzinformation:":
                        homeInformation.AdditionalInformation = info;
                        break;

                    default:
                        break;
                }

            }
            #endregion

            #region FurtherInformation
            if (pageDocument.DocumentNode.SelectNodes("(//div[contains(@data-testid,'ad-description-Zusatzinformationen')]//li)") != null)
            { 
                var getFurtherInformation = pageDocument.DocumentNode.SelectNodes("(//div[contains(@data-testid,'ad-description-Zusatzinformationen')]//li)");
            count = getFurtherInformation.Count;

                for (int i = 1; i <= count; i++)
                {

                    var info = pageDocument.DocumentNode.SelectSingleNode("(//div[contains(@data-testid,'ad-description-Zusatzinformationen')]//li)[" + (i) + "]").InnerText;


                    switch (info)
                    {
                        case string a when a.Contains("Anzahl Badezimmer"):
                            {
                                var index = info.IndexOf(":");
                                info = index != -1 || index != 0 ? info.Substring(++index, info.Length - index) : info;
                                successfullyParsed = int.TryParse(info, out convertedInt);
                                homeInformation.Bathrooms = successfullyParsed ? convertedInt : 0;
                            }
                            break;
                        case string a when a.Contains("Anzahl WC"):

                            {
                                var index = info.IndexOf(":");
                                info = index != -1 || index != 0 ? info.Substring(++index, info.Length - index) : info;
                                successfullyParsed = int.TryParse(info, out convertedInt);
                                homeInformation.Toilets = successfullyParsed ? convertedInt : 0;
                            }
                            break;

                        default:
                            break;
                    }
                }

            }

            #endregion

            //*****************************************************************************

            return homeInformation;
        }
    }
}
