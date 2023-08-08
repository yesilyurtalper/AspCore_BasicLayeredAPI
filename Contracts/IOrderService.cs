using BasicLayeredAPI.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLayeredAPI.API.Contracts;

public interface IOrderService
{
    public  Task<ResponseDto<string>> MakeOrderAsync();
}
