using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model;
using SystemStock.Business.Model.StoreProduct;
using SystemStock.RelationalData;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Service.StoreProduct
{
    public class StoreProductService : IStoreProductService
    {
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly IMapper _mapper;
        public StoreProductService
            (
                IStoreProductRepository storeProductRepository,
                IMapper mapper
            ) 
        {
            _storeProductRepository = storeProductRepository;
            _mapper = mapper;
        }

        public async Task<IPagedList<StoreProductModel>> GetByStore(long StoreId)
        {
            try
            {

                var storeProductList = _mapper.Map<List<StoreProductModel>>(await _storeProductRepository.GetByStore(StoreId));
                return new StaticPagedList<StoreProductModel>(storeProductList, 1, 20, storeProductList.Count());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task SaveProductListForStore(StoreProductRequestModel model) 
        //{
        //    try
        //    {
        //        var result = new BaseResponse<StoreProductRequestModel>();

        //        if(model.StoreId <= 0)
        //        {
        //            result.Message.Add("Store inválida");
        //        }

        //        var storeProductList = await _storeProductRepository.GetByStore(model.StoreId);
        //        var productDeleted = new List<StoreProductEntity>();
        //        var productAdd = new List<StoreProductEntity>();

        //        if (!model.Products.Any() && storeProductList.Any())
        //        {
        //            productDeleted.AddRange(storeProductList.ToList());
        //        }
        //        else
        //        {
        //            var storeProductListGroup = storeProductList.GroupBy(e => e.ProductId);
        //            foreach (var item in storeProductListGroup)
        //            {
        //                if (!model.Products.Any(e => e.ProductId == item.Key))
        //                {
        //                    productDeleted.Add(item.);
        //                }
        //            }
        //        }

        //        if (!storeProductList.Any() && model.Products.Any())
        //        {
        //            foreach (var item in model.Products)
        //            {
        //                productAdd.Add(new StoreProductEntity()
        //                {
        //                    Amount = item.Amount,
        //                    ProductId = item.ProductId,
        //                    StoreId = model.StoreId
        //                });
        //            }
        //        }
        //        else
        //        {
        //            foreach (var item in model.Products)
        //            {
        //                if(storeProductList.Any(e => e.Key == item.ProductId))
        //                {

        //                    productAdd.Add(new StoreProductEntity()
        //                    {
        //                        Amount = item.Amount,
        //                        ProductId = item.ProductId,
        //                        StoreId = model.StoreId
        //                    });
        //                }

        //            }
        //        }

        //        Delete(productDeleted)



        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        private async Task Delete(List<StoreProductEntity> list)
        {
            try
            {
                foreach(var item in list) 
                {
                    _storeProductRepository.GetDbSetStoreProduct().Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }
                await _storeProductRepository.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
