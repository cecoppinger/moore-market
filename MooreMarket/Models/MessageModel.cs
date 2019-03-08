using System;
using System.Collections.Generic;
using MooreMarket.Data;

namespace MooreMarket.Models
{
    
  public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public int UserId { get; set; }
        public UserModel User { get; set; }

        public Message() 
        {

        }
        
        public Message(string title, string body, DateTime startDate, DateTime endDate) 
        {
            Title = title;
            Body = body;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}