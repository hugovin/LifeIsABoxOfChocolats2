using System;
using System.Runtime.Serialization;

namespace OrderStatusData.DataTransferObjects
{
    [DataContract]
    public class OrderDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StoreId { get; set; }
        [DataMember]
        public string OrderId { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Address2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Zip { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public int EmailFlag { get; set; }
        [DataMember]
        public string UpsUspsService { get; set; }
        [DataMember]
        public string TrackingCode { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }




        public OrderDto()
        {
            Id = 0;
            StoreId = 0;
            OrderId = "";
            CustomerId = "";
            Name = "";
            Company = "";
            Address = "";
            Address2 = "";
            City = "";
            State = "";
            Zip = "";
            Country = "";
            Phone = "";
            Email = "";
            EmailFlag = 0;
  
            TrackingCode = null;
        }
    }
    
}
