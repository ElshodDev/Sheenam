//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Properties;
using Sheenam.Api.Models.Foundations.Users;
using System;

namespace Sheenam.Api.Models.Foundations.Messages
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public Guid? HomeId { get; set; }
        public Guid? PropertyId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public DateTimeOffset? ReadDate { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
        public Property Property { get; set; }
    }
}