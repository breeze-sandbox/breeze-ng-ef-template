using Breeze.ContextProvider.EF6;
using Template.Data;

namespace Template.Web.Models
{
  public class TemplateContextProvider : EFContextProvider<TemplateContext>
    {
        private string _connectionString;

        public TemplateContextProvider(string connectionString) : base()
        {
            this._connectionString = connectionString;
        }

        protected override TemplateContext CreateContext()
        {
            return new TemplateContext(_connectionString);
        }
    }
}