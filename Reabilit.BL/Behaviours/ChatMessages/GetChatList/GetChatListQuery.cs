using MediatR;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ChatMessages.GetChatList;

public record GetChatListQuery(Guid CurrentUserId) : IRequest<List<ChatDTO>>;
