using MediatR;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ChatMessages.GetMySupportChat;

public record GetMySupportChatQuery(Guid CurrentUserId) : IRequest<List<MessageDTO>>;
