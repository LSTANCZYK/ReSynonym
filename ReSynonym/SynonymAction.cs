using System;
using System.Collections.Generic;
using JetBrains.Application;
using JetBrains.Application.DataContext;
using JetBrains.Application.Progress;
using JetBrains.DataFlow;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
using JetBrains.ReSharper.Feature.Services.LinqTools;
using JetBrains.ReSharper.Intentions.Extensibility;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.Refactorings.Rename.Impl;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.TextControl;
using JetBrains.Util;

namespace ReSynonym
{
    /// <summary>
    /// This is an example context action. The test project demonstrates tests for
    /// availability and execution of this action.
    /// </summary>
    [ContextAction(Name = "Synonym", Description = "Get synonyms for word", Group = "C#")]
    public class SynonymAction : ContextActionBase
    {
        private readonly ICSharpContextActionDataProvider _provider;
        private ILiteralExpression _stringLiteral;

        public SynonymAction(ICSharpContextActionDataProvider provider)
        {
            _provider = provider;
        }

        public override bool IsAvailable(IUserDataHolder cache)
        {
            return true;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            //ar renameRefactorService = solution.GetComponent<RenameRefactoringService>();
            var synonyms = new List<string>() {"Sam", "Sam", "Sam"};
            IRefactoringWorkflow renameRefactoringWorkflow = new InlineRenameWorkflowDecorator(synonyms);
            //var renameRefactoringWorkflow = new InlineRenameWorkflow("InlineTypoRename");
            Lifetimes.Using(lifetime =>
              RefactoringActionUtil.ExecuteRefactoring(
                          JetBrains.ActionManagement.ShellComponentsEx.ActionManager(Shell.Instance.Components)
                            .DataContexts.CreateOnSelection(lifetime, DataRules.AddRule("DoTypoRenameWorkflow",
                            JetBrains.ProjectModel.DataContext.DataConstants.SOLUTION, solution)), renameRefactoringWorkflow));
            return null;
//            CSharpElementFactory factory = CSharpElementFactory.GetInstance(_provider.PsiModule);
//
//            var stringValue = _stringLiteral.ConstantValue.Value as string;
//            if (stringValue == null)
//                return null;
//
//            var chars = stringValue.ToCharArray();
//            Array.Reverse(chars);
//            ICSharpExpression newExpr = factory.CreateExpressionAsIs("\"" + new string(chars) + "\"");
//            _stringLiteral.ReplaceBy(newExpr);
//            return null;
        }

        public override string Text
        {
            get { return "Get Synonym"; }
        }
    }
}