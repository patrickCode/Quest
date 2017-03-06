using Common.Domain;
using System.Collections.Generic;
using Domain.Categories.ValueObject;

namespace Domain.Categories.Aggregate
{
    public class Category : AggregateRoot
    {
        private string _id;
        public override string Id
        {
            get
            {
                return _id;
            }
        }

        private string _code;
        public string Code
        {
            get
            {
                return _code;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
        }

        private List<Subcategory> _subCategories;
        public List<Subcategory> SubCategories
        {
            get
            {
                return _subCategories;
            }
        }

        private Audit _audit;
        public Audit Audit
        {
            get
            {

                return _audit;
            }
        }

        public Category(string id, string code, string name, string description, List<Subcategory> subCategories) 
        {
            _id = id;
            _code = code;
            _name = name;
            _description = description;
            _subCategories = subCategories;
        }
    }
}