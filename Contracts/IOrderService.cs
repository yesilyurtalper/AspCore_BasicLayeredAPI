using BasicLayeredService.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLayeredService.API.Contracts;

public interface IOrderService
{
    public  Task<bool> MakeOrderAsync();
}
