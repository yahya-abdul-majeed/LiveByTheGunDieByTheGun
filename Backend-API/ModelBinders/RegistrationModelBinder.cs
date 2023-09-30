using Backend_API.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using System.Text;

namespace Backend_API.ModelBinders
{
    public class RegistrationModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var request = bindingContext.HttpContext.Request;
            using (var reader  = new StreamReader(request.Body, Encoding.UTF8))
            {
                var bodyString = reader.ReadToEnd();
                var test = JsonSerializer.Deserialize(bodyString, typeof(RegistrationDTO));
                RegistrationDTO dto = new RegistrationDTO()
                {

                };
                bindingContext.Result = ModelBindingResult.Success(bodyString);
            }
            return Task.CompletedTask;
        }
    }
}
