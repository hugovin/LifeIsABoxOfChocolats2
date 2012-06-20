using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OrderStatusData.DataTransferObjects
{
    [DataContract]
    public class CustomShipmentsDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StoreId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }
        [DataMember]
        public DateTime DateModified { get; set; }
    }
}
