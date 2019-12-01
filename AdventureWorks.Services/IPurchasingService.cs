using AdventureWorks.DAL.GenericRepository;
using AdventureWorks.Services.HelperModels;
using System;

namespace AdventureWorks.Services
{
    public interface IPurchasingService
    {
        PagedResult<PurchaseOrderDetailData> GetPurchaseOrderDetailData(int pageSize, int currentPage, DateTime from, DateTime to);
        PurchaseOrderDetailData GetTotalSelectedTimeFrame(DateTime from, DateTime to);
    }
}