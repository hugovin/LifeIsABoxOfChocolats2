using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using OrderStatusCore.DataTransferObjects;
using OrderStatusData.DataTransferObjects;

namespace OrderStatusWeb.Models
{
    public class StoreModels
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Store Name"), DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The Store Name must be between 5 and 30 characters long")]
        public string StoreName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayName("API Key")]
        public string ApiKey { get; set; }

        [Required(AllowEmptyStrings = false)]

        [DisplayName("Store URL")]
        public string Url { get; set; }

        public int StoreId { get; set; }

        [DisplayName("Interval")]
        public string Interval { get; set; }

        public List<int> OrderStatusIds { get; set; }
        public List<OrderStatusDto> OrderStatus;
        public string OrderStatusVal { get; set; }

        [DisplayName("Custom Orders Status")]
        public string CustomOrderStatus { get; set; }

        public List<CustomShipmentsDto> CustomShipments;

        public string listOfIds { get; set; }

        public Object SelectedValue  { get; set; }

        [DisplayName("Interval")]
        public string DefaultInterval { get; set; }

        public string CustomShip1 { get; set; }
        public string CustomShip2 { get; set; }
        public string CustomShip3 { get; set; }

        public IEnumerable<SelectListItem> IntervalList;

        public StoreModels()
        {
            SelectedValue = "";
            Interval = "";
            CustomShip1 = "";
            CustomShip2 = "";
            CustomShip3 = "";
            OrderStatus = new List<OrderStatusDto>();
            CustomShipments = new List<CustomShipmentsDto>();

        }


    }

    public class AllStores
    {
        public List<StoreDto> ListOfStores;
        public AllStores()
        {
            ListOfStores = new List<StoreDto>();
        }
    }


}