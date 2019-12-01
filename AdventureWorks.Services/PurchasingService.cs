using AdventureWorks.DAL;
using AdventureWorks.DAL.GenericRepository;
using AdventureWorks.DAL.UnitOfWork;
using AdventureWorks.Services.HelperModels;
using System;
using System.Data.Entity;
using System.Linq;

namespace AdventureWorks.Services
{
    public class PurchasingService : IPurchasingService
    {
        private readonly IRepository<PurchaseOrderDetail> purchaseRepository;
        private readonly IUnitOfWork unitOfWork;

        public PurchasingService(IRepository<PurchaseOrderDetail> purchaseRepository, IRepository<ProductDescription> productDescriptionRepository, IUnitOfWork unitOfWork)
        {
            this.purchaseRepository = purchaseRepository;
            this.unitOfWork = unitOfWork;
        }

        public PagedResult<PurchaseOrderDetailData> GetPurchaseOrderDetailData(int pageSize, int currentPage, DateTime from, DateTime to)
        {
            IOrderedQueryable<PurchaseOrderDetailData> query = purchaseRepository.Get(orderBy: q => q.OrderBy(d => d.PurchaseOrderDetailID), x=> x.ModifiedDate < to.Date && x.ModifiedDate > from.Date).GroupBy(v => DbFunctions.TruncateTime(v.ModifiedDate)).Select(g => new
            PurchaseOrderDetailData
            {
                SumOfTraffic = g.Sum(i => i.LineTotal),
                NumberOfProductUnitSold = g.Sum(z => z.OrderQty)
            }).OrderBy(d=>d.SumOfTraffic);

            PagedResultExtension extension = new PagedResultExtension();
            return extension.GetPaged<PurchaseOrderDetailData>(query, currentPage, pageSize);
        }

        public PurchaseOrderDetailData GetTotalSelectedTimeFrame(DateTime from, DateTime to)
        {
            Nullable<decimal> TotalSumOfTraffic = purchaseRepository.Get(orderBy: q => q.OrderBy(d => d.PurchaseOrderDetailID), x => x.ModifiedDate < to.Date && x.ModifiedDate > from.Date).Select(x=>x.LineTotal).DefaultIfEmpty(0).Sum();
            Nullable<int> TotalNumberOfProductUnitSold = purchaseRepository.Get(orderBy: q => q.OrderBy(d => d.PurchaseOrderDetailID), x => x.ModifiedDate < to.Date && x.ModifiedDate > from.Date).Select(x=>(int)x.OrderQty).DefaultIfEmpty(0).Sum();
            return new PurchaseOrderDetailData { SumOfTraffic = TotalSumOfTraffic, NumberOfProductUnitSold = TotalNumberOfProductUnitSold };
        }
    }
}
