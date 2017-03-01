using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Questions.Aggregate
{
    public class Question: AggregateRoot
    {
        public string Value { get; set; }

    }
}
