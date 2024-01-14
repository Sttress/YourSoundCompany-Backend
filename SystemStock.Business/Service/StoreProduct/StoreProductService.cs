using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model;
using SystemStock.Business.Model.Store;
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

        public async Task<BaseResponse<List<StoreProductModel>>> SaveProductListForStore(StoreProductRequestModel model)
        {
            try
            {
                var result = new BaseResponse<List<StoreProductModel>>();

                if (model.StoreId <= 0)
                {
                    result.Message.Add("Store inválida");
                }

                var storeProductList = await _storeProductRepository.GetByStore(model.StoreId);
                var productDeleted = new List<StoreProductEntity>();
                var productAdd = new List<StoreProductEntity>();


                if(!storeProductList.Any() && model.Products.Any())
                {
                    foreach(var item in model.Products.ToList())
                    {
                        productAdd.Add(new StoreProductEntity()
                        {
                            ProductId = item.ProductId,
                            StoreId = model.StoreId,
                            Amount = item.Amount,
                        });

                    }
                }

                if(model.Products.Any() && storeProductList.Any())
                {
                    productDeleted.AddRange(storeProductList.ToList());
                }

                if(model.Products.Any() && storeProductList.Any())
                {

                    foreach(var item in storeProductList.ToList())
                    {
                        var productModel = model.Products.Where(e => e.ProductId == item.ProductId).FirstOrDefault();

                        if (productModel is not null)
                        {

                            item.Amount = productModel.Amount;
                        }
                        else
                        {
                            productDeleted.Add(item);
                        }

                    }

                    foreach(var item in model.Products.ToList())
                    {
                        if(storeProductList.Where(e => e.ProductId == item.ProductId).Count() > 0)
                        {
                            productAdd.Add(new StoreProductEntity()
                            {
                                ProductId = item.ProductId,
                                StoreId = model.StoreId,
                                Amount = item.Amount,
                            });
                        }
                    }
                }


                await Delete(productDeleted);
                await _storeProductRepository.GetDbSetStoreProduct().AddRangeAsync(productAdd);

                await _storeProductRepository.SaveChanges();

                result.Data = _mapper.Map<List<StoreProductModel>>(await _storeProductRepository.GetByStore(model.StoreId));
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
