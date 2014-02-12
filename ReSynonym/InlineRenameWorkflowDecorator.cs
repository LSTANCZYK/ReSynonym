using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Application.DataContext;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
using JetBrains.ReSharper.Refactorings.Conflicts;
using JetBrains.ReSharper.Refactorings.Rename.Impl;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.UI.Icons;

namespace ReSynonym
{
    public class InlineRenameWorkflowDecorator: RefactoringWorkflowBase
    {
        private readonly List<string> synonyms;
        private readonly InlineRenameWorkflow inlineRenameWorkflow;

        public InlineRenameWorkflowDecorator(List<string> synonyms)
        {
            this.synonyms = synonyms;
            inlineRenameWorkflow = new InlineRenameWorkflow("InlineTypoRename");
        }

        public override bool PreExecute(IProgressIndicator progressIndicator)
        {
            var result = inlineRenameWorkflow.PreExecute(progressIndicator);

            var type = typeof(InlineRenameWorkflow);
            var fieldInfoField = type.GetField("myFieldInfo", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfoField != null)
            {
                var value = (HotspotInfo)fieldInfoField.GetValue(inlineRenameWorkflow);
                var nameSuggestionType = typeof(NameSuggestionsExpression);
                var namesField = nameSuggestionType.GetField("myNames", BindingFlags.NonPublic | BindingFlags.Instance);
                if (namesField != null)
                {
                    var names = (List<string>)namesField.GetValue(value.TemplateField.Expression);
                    names.Clear();
                    names.AddRange(synonyms);
                }
            }
            return result;
        }

        public override int GetHashCode()
        {
            return inlineRenameWorkflow.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return inlineRenameWorkflow.Equals(obj);
        }

        public override string ToString()
        {
            return inlineRenameWorkflow.ToString();
        }

        public override bool HasUI
        {
            get { return inlineRenameWorkflow.HasUI; }
        }
        public override string Title
        {
            get { return inlineRenameWorkflow.Title; }
        }
        public override IconId Icon
        {
            get {  return inlineRenameWorkflow.Icon; }
        }
        public override string ActionId
        {
            get {  return inlineRenameWorkflow.ActionId; }
        }
        public override RefactoringActionGroup ActionGroup
        {
            get {  return inlineRenameWorkflow.ActionGroup; }
        }
        public override string HelpKeyword
        {
            get {  return inlineRenameWorkflow.HelpKeyword; }
        }
        public override ISolution Solution
        {
            get {  return inlineRenameWorkflow.Solution; }
        }
        public override IConflictSearcher ConflictSearcher
        {
            get {  return inlineRenameWorkflow.ConflictSearcher; }
        }
        public override IRefactoringPage FirstPendingRefactoringPage
        {
            get {  return inlineRenameWorkflow.FirstPendingRefactoringPage; }
        }
        public override bool MightModifyManyDocuments
        {
            get {  return inlineRenameWorkflow.MightModifyManyDocuments; }
        }

        public override bool Execute(IProgressIndicator progressIndicator)
        {
            return inlineRenameWorkflow.Execute(progressIndicator);
        }

        public override bool PostExecute(IProgressIndicator pi)
        {
            return inlineRenameWorkflow.PostExecute(pi);
        }

        public override bool Initialize(IDataContext context)
        {
            return inlineRenameWorkflow.Initialize(context);
        }

        public override bool IsAvailable(IDataContext context)
        {
            return inlineRenameWorkflow.IsAvailable(context);
        }

        public override bool RefreshData()
        {
            return inlineRenameWorkflow.RefreshData();
        }

        public override void UnsuccessfulFinish(IProgressIndicator pi)
        {
            inlineRenameWorkflow.UnsuccessfulFinish(pi);
        }

        public override void SuccessfulFinish(IProgressIndicator pi)
        {
            inlineRenameWorkflow.SuccessfulFinish(pi);
        }

        public override bool RequiresSolutionTransation
        {
            get { return inlineRenameWorkflow.RequiresSolutionTransation; }
        }

    }
}