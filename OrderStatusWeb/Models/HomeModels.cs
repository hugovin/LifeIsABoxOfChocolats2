using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OrderStatusWeb.Models
{
    public class HomeModels
    {
        public IEnumerable<SelectListItem> StoreList { get; set; }
        [DisplayName("Select an Store"), DataType(DataType.Text)]
        public string SelectedStore { get; set; }
    }
}