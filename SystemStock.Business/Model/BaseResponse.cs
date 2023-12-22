
using FluentValidation;
using FluentValidation.Results;
using System.Xml;

namespace SystemStock.Business.Model
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public List<string> Message { get; set; }

        public BaseResponse()
        {
        }

        public BaseResponse(T data, List<string> menssage)
        {


            Data = data;
            Message = menssage;
        }
        
        public List<string> Validate(ValidationResult? validationResult)
        {
            var mensages = new List<string>();

            if(validationResult is not null) 
            { 
                if(validationResult.Errors.Count() > 0)
                {
                    foreach(var error in validationResult.Errors)
                    {
                        mensages.Add(error.ErrorMessage.ToString());
                    }
                }
            }

            return mensages;

        }
    }
}
