using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingPlanner.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime Date { get; set; }
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public bool Global { get; set; } = true;
        
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public List<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
