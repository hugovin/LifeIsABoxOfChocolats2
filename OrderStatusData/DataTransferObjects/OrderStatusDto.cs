using System;
using System.Runtime.Serialization;

namespace OrderStatusData.DataTransferObjects
{
    [DataContract]
    public class OrderStatusDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StoreId { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public bool IsPublic { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }
        [DataMember]
        public DateTime DateModified { get; set; }

    }
}
