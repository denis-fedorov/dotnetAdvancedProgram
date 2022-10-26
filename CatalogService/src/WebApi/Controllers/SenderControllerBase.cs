using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public abstract class SenderControllerBase : ControllerBase
{
    protected readonly ISender Sender;

    protected SenderControllerBase(ISender sender)
    {
        Sender = sender;
    }
}