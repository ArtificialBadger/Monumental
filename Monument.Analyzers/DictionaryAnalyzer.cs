using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Monument.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DictionaryAnalyzer : DiagnosticAnalyzer
    {
        private Dictionary<string, string> test = new Dictionary<string, string>();

        private static DiagnosticDescriptor DiagnosticDescriptor = new DiagnosticDescriptor(
            "Test",
            "Test Title",
            "Test Message",
            "Classes with State",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticDescriptor);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(
                Analyze,
                SyntaxKind.FieldDeclaration);

        }

        private void Analyze(SyntaxNodeAnalysisContext context)
        {
            var fieldDeclarationSyntaxNode = (FieldDeclarationSyntax)context.Node;

            //node.Declaration.Variables
            context.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticDescriptor,
                    fieldDeclarationSyntaxNode.GetLocation()));
        }
    }
}
