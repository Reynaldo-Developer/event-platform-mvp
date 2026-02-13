using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Contracts
{
    public record EventCreated(
        Guid MessageId,
        Guid EventId,
        string Name,
        DateTime OccurredAt
    );
}
