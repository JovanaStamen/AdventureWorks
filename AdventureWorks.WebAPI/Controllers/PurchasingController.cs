using AdventureWorks.DAL.GenericRepository;
using AdventureWorks.Services;
using AdventureWorks.Services.HelperModels;
using AutoMapper;
using System;
using System.Web.Http;

namespace AdventureWorks.WebAPI.Controllers
{
    [RoutePrefix("api/purchase")]
    public class PurchasingController : ApiController
    {
        private readonly IPurchasingService purchasingService;

        public PurchasingController(IPurchasingService purchasingService)
        {
            this.purchasingService = purchasingService;
        }

        [Route("")]
        public IHttpActionResult GetPurchaseOrderDetailData([FromUri] string dateFrom,[FromUri] string dateTo, [FromUri] int offset, [FromUri] int count)
        {
            DateTime.TryParse(dateFrom, out DateTime from);
            DateTime.TryParse(dateTo, out DateTime to);
            PagedResult<PurchaseOrderDetailData> pagedPurchaseData = purchasingService.GetPurchaseOrderDetailData(count, offset, from, to);
            if (pagedPurchaseData.Results == null || pagedPurchaseData.Results.Count == 0)
            {
                return NotFound();
            }

            return Ok(pagedPurchaseData);
        }

        [Route("total")]
        public IHttpActionResult GetTotalSelectedTimeFrame([FromUri] string dateFrom, [FromUri] string dateTo)
        {
            DateTime.TryParse(dateFrom, out DateTime from);
            DateTime.TryParse(dateTo, out DateTime to);
            PurchaseOrderDetailData totalPurchase = purchasingService.GetTotalSelectedTimeFrame(from, to);
            if (totalPurchase == null)
            {
                return NotFound();
            }

            return Ok(totalPurchase);
        }
    }
}
