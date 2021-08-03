using System.ComponentModel.DataAnnotations;

namespace ControleUsinas.Helpers
{
    public class RequiredHelperAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string name) => !string.IsNullOrEmpty(this.ErrorMessage) ?
                this.ErrorMessage :
                $"O campo {name} é de preenchimento obrigatário.";
    }
}
