﻿using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace YumBlazor.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int Count {  get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;
    }
}
