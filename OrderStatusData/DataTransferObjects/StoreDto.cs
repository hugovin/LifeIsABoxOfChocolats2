using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OrderStatusData.DataTransferObjects;

namespace OrderStatusCore.DataTransferObjects
{
    /// <summary>
    /// Class to map friends structure
    /// </summary>
    [DataContract]
    public class StoreDto
    {
        [DataMember]
        public int Id;
        [DataMember]
        public string Name;
        [DataMember]
        public string Url;
        [DataMember]
        public string ApiUser;
        [DataMember]
        public string ApiKey;
        [DataMember] 
        public int Interval;
        [DataMember]
        public int OrderStatus;
        [DataMember]
        public DateTime LastRun;

        [DataMember] 
        public List<CustomShipmentsDto> CustomShipmentsDtos;


        public StoreDto()
        {
            CustomShipmentsDtos = new List<CustomShipmentsDto>();
        }
    }
}
