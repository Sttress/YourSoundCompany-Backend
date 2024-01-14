
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
        private readonly IUserService _userService;

        public StoreService
            (
                IStoreRepository storeRepository,
                IMapper mapper,
                StoreCreateValidator storeCreateValidator,
                IUserService userService
            )
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
            _storeCreateValidator = storeCreateValidator;
            _userService = userService;
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
                var user = await _userService.GetCurrentUser();


                if (entity == null)
                {
                    entity  = new StoreEntity()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        ColorPrimary = model.ColorPrimary,
                        ColorSecondary = model.ColorSecondary,
                        Logo = model.Logo,
                        UserId = user.Id,
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
                result.Data = _mapper.Map<List<StoreModel>>(await _storeRepository.GetByUser(user.Id));

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 

        public async Task<BaseResponse<IPagedList<StoreModel>>> GetList()
        {
            try
            {
                var result = new BaseResponse<IPagedList<StoreModel>>();
                var user = await _userService.GetCurrentUser();

                var query = await _storeRepository.GetByUser(user.Id);
                var storeList = _mapper.Map<List<StoreModel>>(query);
                result.Data = new StaticPagedList<StoreModel>(storeList, 1, 20, storeList.Count());

                return result;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
