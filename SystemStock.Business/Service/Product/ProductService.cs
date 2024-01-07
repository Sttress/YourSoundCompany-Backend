
using AutoMapper;
using PagedList;
using SystemStock.Business.Model;
using SystemStock.Business.Model.Product;
using SystemStock.Business.Validation.Product;
using SystemStock.RelationalData;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserService _userService;
        private readonly ProductCreateValidator _productCreateValidator;
        private readonly IMapper _mapper;

        public ProductService
            (
                IProductRepository productRepository,
                IUserService userService,
                ProductCreateValidator productCreateValidator,
                IMapper mapper
            )
        {
            _productRepository = productRepository;
            _userService = userService;
            _productCreateValidator = productCreateValidator;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ProductModel>> CreateUpdate(ProductModel model)
        {
            try
            {

                var result = new BaseResponse<ProductModel>();
                result.Message = result.Validate(await _productCreateValidator.ValidateAsync(model));
                if (result.Message.Count() > 0)
                {
                    return result;
                }

                var user = await _userService.GetCurrentUser();

                var entity = model.Id > 0 ? await _productRepository.GetById(model.Id, user.Id) : null;

                if (entity != null)
                {
                    entity.Price = model.Price;
                    entity.CategoryId = model.CategoryId;
                    entity.Description = model.Description;
                    entity.Name = model.Name;
                }
                else
                {
                    entity = new ProductEntity()
                    {
                        Price = model.Price,
                        CategoryId = model.CategoryId,
                        Description = model.Description,
                        Name = model.Name,
                        Active = true,
                        UserId = user.Id
                    };

                    entity = (await _productRepository.GetDbSetProduct().AddAsync(entity)).Entity;
                }

                await _productRepository.SaveChanges();

                result.Data = _mapper.Map<ProductModel>(entity);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Active(long Id)
        {
            try
            {

                var product = await GetProductById(Id);

                if (product is not null)
                {
                    product.Active = false;
                }

                await _productRepository.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<ProductModel> GetProductById(long Id)
        {
            try
            {
                var user = await _userService.GetCurrentUser();

                return _mapper.Map<ProductModel>(await _productRepository.GetById(Id, user.Id));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse<ProductModel>> GetById(long Id)
        {
            try
            {
                var result = new BaseResponse<ProductModel>();

                var product = await GetProductById(Id);
                if(product is null)
                {
                    result.Message.Add("Produto inválido");
                    return result;
                }

                result.Data = product;

                return result;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IPagedList<ProductModel>> GetList()
        {
            var user = await _userService.GetCurrentUser();
            var products = _mapper.Map<List<ProductModel>>(await _productRepository.GetList(user.Id)); 

            return new StaticPagedList<ProductModel>(products, 1, 20, products.Count());
        }
    }
}
