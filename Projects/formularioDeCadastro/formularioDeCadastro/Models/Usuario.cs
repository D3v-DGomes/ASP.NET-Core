﻿using System.ComponentModel.DataAnnotations;

namespace formularioDeCadastro.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Senha { get; set; }
    }
}
