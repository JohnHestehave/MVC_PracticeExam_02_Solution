using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebApplication.Models
{
    public class BidProvider
    {
		[Required]
		[Display(Name="Bid Custom Name")]
		[MinLength(4)]
		public string BidCustomName { get; set; }

		[Required]
		[Display(Name = "Bid Custom Phone")]
		[MinLength(8)]
		[MaxLength(8)]
		public string BidCustomPhone { get; set; }

		[Required]
		[Display(Name = "Item Number")]
		public int ItemNumber { get; set; }

		[Required]
		[Display(Name = "Bid Price")]
		public int BidPrice{ get; set; }

		public DateTime BidTimestamp { get; set; }
	}
}
