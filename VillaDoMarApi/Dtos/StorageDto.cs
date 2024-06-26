﻿namespace VillaDoMarApi.Dtos
{
    public class StorageDto
    {
        public int ProductId { get; set; }
        public int MovementAmount { get; set; }
        public bool IsEntry { get; set; }
        public DateTime MovementDate { get; set; }
        public string Description { get; set; } = null!;
    }
}
