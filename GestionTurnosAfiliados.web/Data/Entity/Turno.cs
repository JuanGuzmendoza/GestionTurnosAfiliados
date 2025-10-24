using System;
using System.ComponentModel.DataAnnotations;

namespace SaludLinux.Models
{
    public enum TurnStatus
    {
        Waiting,
        Called,
        InService,
        Completed,
        Skipped,
        Cancelled
    }

    public class Turn
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public int Number { get; set; }

        public string ServiceCode { get; set; }

        public DateTimeOffset RequestedAt { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? CalledAt { get; set; }

        public DateTimeOffset? CompletedAt { get; set; }

        public TurnStatus Status { get; set; } = TurnStatus.Waiting;

        public Guid? CajaId { get; set; }

        public Guid? AfiliadoId { get; set; }

        public string Description { get; set; }

        public string Metadata { get; set; }
    }
}
