using System;
using System.Collections.Generic;
using JetBrains.Application;
using JetBrains.Application.DataContext;
using JetBrains.Application.Progress;
using JetBrains.DataFlow;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Intentions.Extensibility;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.TextControl;
using JetBrains.Util;
using ReSynonym.Workflow;

namespace ReSynonym
{
    [ContextAction(Name = "Synonym", Description = "Suggest Synonyms", Group = "C#")]
    public class SynonymAction : ContextActionBase
    {
        private readonly ICSharpContextActionDataProvider provider;
        

        public SynonymAction(ICSharpContextActionDataProvider provider)
        {
            this.provider = provider;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            var synonyms = new List<string>() {"Sam", "Sam", "Sam"};

            IRefactoringWorkflow inlineSynonymWorkflow = new InlineSynonymWorkflow(synonyms);

            Lifetimes.Using(lifetime =>
              RefactoringActionUtil.ExecuteRefactoring(
                          JetBrains.ActionManagement.ShellComponentsEx.ActionManager(Shell.Instance.Components)
                            .DataContexts.CreateOnSelection(lifetime, DataRules.AddRule("DoTypoRenameWorkflow",
                            JetBrains.ProjectModel.DataContext.DataConstants.SOLUTION, solution)), inlineSynonymWorkflow));
            return null;
        }

        public override string Text { get { return "Suggest Synonyms"; } }

        public override bool IsAvailable(IUserDataHolder cache) { return true; }
        
    }
}