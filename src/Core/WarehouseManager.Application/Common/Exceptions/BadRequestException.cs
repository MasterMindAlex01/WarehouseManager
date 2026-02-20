using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WarehouseManager.Application.Common.Exceptions;

public class BadRequestException : CustomException
{
    public BadRequestException(string message)
    : base(message, null, HttpStatusCode.BadRequest)
    {
    }
}
