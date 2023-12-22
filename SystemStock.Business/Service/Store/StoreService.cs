
using AutoMapper;
using PagedList;
using SystemStock.Business.Model;
using SystemStock.Business.Model.Store;
using SystemStock.Business.Validation.Store;
using SystemStock.RelationalData;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Service.Store
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        private readonly StoreCreateValidator _storeCreateValidator;

        public StoreService
            (
                IStoreRepository storeRepository,
                IMapper mapper,
                StoreCreateValidator storeCreateValidator
            )
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
            _storeCreateValidator = storeCreateValidator;
        }

        public async Task<BaseResponse<List<StoreModel>>> CreateUpdate(StoreModel model)
        {
            try
            {

                var result = new BaseResponse<List<StoreModel>>();
                result.Message = result.Validate(await _storeCreateValidator.ValidateAsync(model));
                if (result.Message.Count() > 0)
                {
                    return result;
                }

                var entity = model.Id > 0 ? await _storeRepository.GetById(model.Id): null;

                if(entity == null)
                {
                    entity  = new StoreEntity()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        ColorPrimary = model.ColorPrimary,
                        ColorSecondary = model.ColorSecondary,
                        Logo = model.Logo,
                        UserId = model.UserId,
                    };

                    entity = (await _storeRepository.GetDbSetStore().AddAsync(entity)).Entity;

                }
                else
                {
                    entity.Name = model.Name;
                    entity.Description = model.Description;
                    entity.ColorPrimary = model.ColorPrimary;
                    entity.ColorSecondary = model.ColorSecondary;
                    entity.Logo = model.Logo;
                }



                await _storeRepository.SaveChanges();
                result.Data = _mapper.Map<List<StoreModel>>(await _storeRepository.GetByUser(entity.UserId));

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 

        public async Task<BaseResponse<IPagedList<StoreModel>>> GetByUser(long Id)
        {
            try
            {
                var result = new BaseResponse<IPagedList<StoreModel>>();
                var query = await _storeRepository.GetByUser(Id);
                var teste = _mapper.Map<List<StoreModel>>(query);
                result.Data = new StaticPagedList<StoreModel>(teste, 1, 20, teste.Count());

                return result;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
