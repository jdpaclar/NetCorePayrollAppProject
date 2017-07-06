using Company.Website.Payroll.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using Company.Common.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Website.Payroll.Extensions
{
    public static class ResponseBaseExtensions
    {
        public static ResponseBase ToFailedResponseBase(this ResponseBase response, object errorMessage)
        {
            var newResponse = new ResponseBase()
            {
                IsSuccess = false,
                ListMessage = null
            };

            try
            {
                switch (errorMessage)
                {
                    case IEnumerable<ModelError> modelError:

                        var errLst = new List<string>();

                        foreach (var errors in modelError)
                        {
                            errLst.Add("{0}".FormatString(errors.ErrorMessage));
                        }

                        newResponse.ListMessage = errLst.ToArray();
                        break;
                    case IEnumerable<string> customMessages:
                        newResponse.ListMessage = customMessages.ToArray();

                        break;
                    default:
                        break;
                }

            }
            catch (Exception)
            {
                response.Message = "Failed To Interpret Result.";
            }

            return newResponse;
        }
    }
}
