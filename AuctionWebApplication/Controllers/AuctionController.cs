using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AuctionWebApplication.Controllers
{
    public class AuctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Bid(int id)
		{
			if (id <= 0) return RedirectToAction("index");
			AuctionService.AuctionsServiceClient client = new AuctionService.AuctionsServiceClient();
			AuctionService.AuctionItem item = client.GetAuctionItemAsync(id).Result;
			if (item == null) return RedirectToAction("index");
			ViewBag.id = item.ItemNumber;
			ViewBag.itemdesc = item.ItemDescription;
			ViewBag.ratingprice = item.RatingPrice;
			return View();
		}
		[HttpPost]
		public IActionResult SendBid(string BidCustomName, string BidCustomPhone, int ItemNumber, int BidPrice)
		{
			List<BidProvider> bids;
			if (HttpContext.Session.GetString("bids") == null)
			{
				bids = new List<BidProvider>();
			}
			else
			{
				bids = JsonConvert.DeserializeObject<List<BidProvider>>(HttpContext.Session.GetString("bids"));
			}
			BidProvider bid = new BidProvider() {BidCustomName = BidCustomName, BidCustomPhone = BidCustomPhone, ItemNumber = ItemNumber, BidPrice = BidPrice, BidTimestamp = DateTime.Now };
			bids.Add(bid);
			HttpContext.Session.SetString("bids", JsonConvert.SerializeObject(bids));
			AuctionService.AuctionsServiceClient client = new AuctionService.AuctionsServiceClient();
			client.ProvideBidAsync(ItemNumber, BidPrice, BidCustomName, BidCustomPhone);
			return RedirectToAction("index");
		}

		public IActionResult MyBids()
		{
			string json = HttpContext.Session.GetString("bids");
			if (json == null) return RedirectToAction("index");

			List<BidProvider> bids = JsonConvert.DeserializeObject<List<BidProvider>>(json);
			ViewBag.bids = bids;
			return View();
		}
    }
}