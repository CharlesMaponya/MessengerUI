using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Models
{
    public class Text
    {
        [Key]
        public int TextId { get; set; }

        public string SenderId { get; set; }

        public virtual AppUser Sender { get; set; }

        public string RecipientId { get; set; }
        public virtual AppUser Recipient { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
    }
}
