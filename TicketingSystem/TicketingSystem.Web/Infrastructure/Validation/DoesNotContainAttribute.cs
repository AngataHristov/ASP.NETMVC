namespace TicketingSystem.Web.Infrastructure.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class DoesNotContainAttribute : ValidationAttribute
    {
        private string word;

        public DoesNotContainAttribute(string word)
        {
            this.word = word;
            this.ErrorMessage = "{0} should not contain the word " + word;
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (valueAsString == null)
            {
                throw new ArgumentException("Does not contain attribute not set on string property");
            }

            if (!valueAsString.ToLower().Contains(this.word.ToLower()))
            {
                return true;
            }

            return false;
        }
    }
}