using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCrf.Domain.Events
{
    public class StoredEvent : Event
    {
        [Key]
        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }

        public StoredEvent(Event theEvent, string data, string user) 
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

    }
}
