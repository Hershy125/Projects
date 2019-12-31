using Dapper;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Production
{
    public class VehicleListingRepository : IVehicleListingRepository
    {
        public void Delete(int listingId)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleListingId", listingId);

                cn.Execute("DeleteVehicleListing",
                    parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public VehicleListing GetById(int listingId)
        {
            VehicleListing listing = null;

            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleListingId", listingId);

                listing = cn.Query<VehicleListing>("ListingsSelectById",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return listing;
        }

        public ListingDetailItem GetDetails(int listingId)
        {
            ListingDetailItem details = null;

            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleListingId", listingId);

                details = cn.Query<ListingDetailItem>("VehicleDetailsById",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return details;
        }

        public IEnumerable<FeaturedVehicle> GetFeatured()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<FeaturedVehicle>("FeaturedSelect",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }


        public IEnumerable<ListingSearchItem> GetSearchResults(ListingSearchParameters parameters)
        {
            List<ListingSearchItem> listings = new List<ListingSearchItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 VehicleListingId, [Year], vma.VehicleMakeId, vma.VehicleMakeName, VehicleTypeId, " +
                    "vmo.VehicleModelId, vmo.VehicleModelName, b.BodyStyleId, b.BodyStyleName, t.TransmissionTypeId, t.TransmissionTypeName, " +
                    "SalePrice, Mileage, MSRP, vl.InteriorColorId, ic.ColorName as InteriorColor, c.ColorId, c.ColorName, VIN, ImageFileName, Sold " +
                    "FROM VehicleListings vl " +
                    "INNER JOIN VehicleMakes vma ON vl.VehicleMakeId = vma.VehicleMakeId " +
                    "INNER JOIN VehicleModels vmo ON vl.VehicleModelId = vmo.VehicleModelId " +
                    "INNER JOIN BodyStyles b ON vl.BodyStyleId = b.BodyStyleId " +
                    "INNER JOIN TransmissionTypes t ON vl.TransmissionTypeId = t.TransmissionTypeId " +
                    "INNER JOIN Colors ic ON vl.InteriorColorId = ic.ColorId " +
                    "INNER JOIN Colors c ON vl.ColorId = c.ColorId " +
                    "WHERE Sold = 0 ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if(parameters.MinMSRP.HasValue)
                {
                    query += $"AND MSRP >= @MinMSRP ";
                    cmd.Parameters.AddWithValue("@MinMSRP", parameters.MinMSRP.Value);
                }

                if (parameters.MaxMSRP.HasValue)
                {
                    query += $"AND MSRP <= @MaxMSRP ";
                    cmd.Parameters.AddWithValue("@MaxMSRP ", parameters.MaxMSRP.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += $"AND [Year] >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += $"AND [Year] <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                }

                if (parameters.VehicleTypeId.HasValue)
                {
                    query += $"AND VehicleTypeId = @VehicleTypeId ";
                    cmd.Parameters.AddWithValue("@VehicleTypeId", parameters.VehicleTypeId.Value);
                }

                if (!string.IsNullOrEmpty(parameters.SearchTerm))
                {
                    query += $"AND (vma.VehicleMakeName LIKE @SearchTerm OR vmo.VehicleModelName LIKE @SearchTerm OR [Year] LIKE @SearchTerm) ";
                    cmd.Parameters.AddWithValue("@SearchTerm", parameters.SearchTerm + '%');
                }

                query += "ORDER BY MSRP DESC";

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        ListingSearchItem row = new ListingSearchItem();
                        row.VehicleListingId = (int)dr["VehicleListingId"];
                        row.Year = (int)dr["Year"];
                        row.VehicleMakeId = (int)dr["VehicleMakeId"];
                        row.VehicleMakeName = dr["VehicleMakeName"].ToString();
                        row.VehicleModelId = (int)dr["VehicleModelId"];
                        row.VehicleModelName = dr["VehicleModelName"].ToString();
                        row.BodyStyleId = (int)dr["BodyStyleId"];
                        row.BodyStyleName = dr["BodyStyleName"].ToString();
                        row.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        row.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        row.SalePrice = (decimal)dr["SalePrice"];
                        row.Mileage = (int)dr["Mileage"];
                        row.MSRP = (decimal)dr["MSRP"];
                        row.InteriorColorId = (int)dr["InteriorColorId"];
                        row.InteriorColor = dr["InteriorColor"].ToString();
                        row.ColorId = (int)dr["ColorId"];
                        row.Color = dr["ColorName"].ToString();
                        row.VIN = dr["VIN"].ToString();

                        if(dr["ImageFileName"] != DBNull.Value)
                            row.ImageFileName = dr["ImageFileName"].ToString();

                        listings.Add(row);
                    }
                }

            }

            return listings;
        }

        public void Insert(VehicleListing listing)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleListingId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@VehicleMakeId", listing.VehicleMakeId);
                parameters.Add("@VehicleModelId", listing.VehicleModelId);
                parameters.Add("@VehicleTypeId", listing.VehicleTypeId);
                parameters.Add("@BodyStyleId", listing.BodyStyleId);
                parameters.Add("@TransmissionTypeId", listing.TransmissionTypeId);
                parameters.Add("@Year", listing.Year);
                parameters.Add("@ColorId", listing.ColorId);
                parameters.Add("@InteriorColorId", listing.InteriorColorId);
                parameters.Add("@Mileage", listing.Mileage);
                parameters.Add("@VIN", listing.VIN);
                parameters.Add("@MSRP", listing.MSRP);
                parameters.Add("@SalePrice", listing.SalePrice);
                parameters.Add("@Description", listing.Description);
                parameters.Add("@FeaturedVehicle", listing.FeaturedVehicle);
                parameters.Add("@ImageFileName", listing.ImageFileName);
                parameters.Add("@Sold", listing.Sold);

                cn.Execute("InsertVehicleListing",
                parameters,
                commandType: CommandType.StoredProcedure);

                listing.VehicleListingId = parameters.Get<int>("@VehicleListingId");
            }
        }

        public void Update(VehicleListing listing)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleListingId", listing.VehicleListingId);
                parameters.Add("@VehicleMakeId", listing.VehicleMakeId);
                parameters.Add("@VehicleModelId", listing.VehicleModelId);
                parameters.Add("@VehicleTypeId", listing.VehicleTypeId);
                parameters.Add("@BodyStyleId", listing.BodyStyleId);
                parameters.Add("@TransmissionTypeId", listing.TransmissionTypeId);
                parameters.Add("@Year", listing.Year);
                parameters.Add("@ColorId", listing.ColorId);
                parameters.Add("@InteriorColorId", listing.InteriorColorId);
                parameters.Add("@Mileage", listing.Mileage);
                parameters.Add("@VIN", listing.VIN);
                parameters.Add("@MSRP", listing.MSRP);
                parameters.Add("@SalePrice", listing.SalePrice);
                parameters.Add("@Description", listing.Description);
                parameters.Add("@FeaturedVehicle", listing.FeaturedVehicle);
                parameters.Add("@ImageFileName", listing.ImageFileName);
                parameters.Add("@Sold", listing.Sold);

                cn.Execute("UpdateVehicleListing",
                parameters,
                commandType: CommandType.StoredProcedure);

            }
        }
    }
}
