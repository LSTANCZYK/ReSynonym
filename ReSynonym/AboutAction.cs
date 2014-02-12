using System.Windows.Forms;
using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;

namespace ReSynonym
{
    [ActionHandler("ReSynonym.About")]
    public class AboutAction : IActionHandler
    {
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            // return true or false to enable/disable this action
            return true;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            MessageBox.Show(
              "ReSynonym\nReSynonym\n\nProvides synonyms for those things you need help naming",
              "About ReSynonym",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
        }
    }
}