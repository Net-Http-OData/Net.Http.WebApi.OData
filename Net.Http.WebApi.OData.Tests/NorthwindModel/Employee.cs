﻿namespace NorthwindModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public AccessLevel AccessLevel { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Forename { get; set; }

        [Required]
        public string Id { get; set; }

        public string ImageData { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? LeavingDate { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Title { get; set; }
    }
}