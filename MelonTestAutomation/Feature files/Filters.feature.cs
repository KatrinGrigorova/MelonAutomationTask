﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.4.0.0
//      SpecFlow Generator Version:3.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace MelonTestAutomation.FeatureFiles
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Filters")]
    [NUnit.Framework.CategoryAttribute("filters")]
    public partial class FiltersFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = new string[] {
                "filters"};
        
#line 1 "Filters.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Feature files", "Filters", "\tIn order to search for products by certain characteristics\r\n\tAs a user\r\n\tI want " +
                    "to be able to apply filters correctly", ProgrammingLanguage.CSharp, new string[] {
                        "filters"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Choose category from third level category tree and apply filters on the Catalog p" +
            "age")]
        [NUnit.Framework.CategoryAttribute("thirdLevelCategoryTree")]
        [NUnit.Framework.TestCaseAttribute("de", "priceAscending", "Bosch", "Schwarz", null)]
        [NUnit.Framework.TestCaseAttribute("at", "priceAscending", "Bosch", "Schwarz", null)]
        [NUnit.Framework.TestCaseAttribute("ch", "priceAscending", "Bosch", "Schwarz", null)]
        [NUnit.Framework.TestCaseAttribute("it", "priceAscending", "Bosch", "Nero", null)]
        [NUnit.Framework.TestCaseAttribute("hu", "priceAscending", "Bosch", "Black", null)]
        [NUnit.Framework.TestCaseAttribute("cz", "priceAscending", "Bosch", "Black", null)]
        [NUnit.Framework.TestCaseAttribute("sk", "priceAscending", "Bosch", "Black", null)]
        [NUnit.Framework.TestCaseAttribute("si", "priceAscending", "Bosch", "Black", null)]
        [NUnit.Framework.TestCaseAttribute("se", "priceAscending", "Bosch", "Black", null)]
        [NUnit.Framework.TestCaseAttribute("pl", "priceAscending", "Bosch", "Czarny", null)]
        [NUnit.Framework.TestCaseAttribute("no", "priceAscending", "Bosch", "Black", null)]
        [NUnit.Framework.TestCaseAttribute("pt", "priceAscending", "Bosch", "Black", null)]
        public virtual void ChooseCategoryFromThirdLevelCategoryTreeAndApplyFiltersOnTheCatalogPage(string domain, string sortBy, string brandFilter, string colourFilter, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "thirdLevelCategoryTree"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("domain", domain);
            argumentsOfScenario.Add("sortBy", sortBy);
            argumentsOfScenario.Add("brandFilter", brandFilter);
            argumentsOfScenario.Add("colourFilter", colourFilter);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Choose category from third level category tree and apply filters on the Catalog p" +
                    "age", null, tagsOfScenario, argumentsOfScenario);
#line 8
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 9
 testRunner.Given(string.Format("I am on domain {0} Home page", domain), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 10
 testRunner.When("I press All categories dropdown menu", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 11
 testRunner.Then("First level category tree is displayed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 12
 testRunner.When("I choose DIY & Garden from the First level category tree", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 13
 testRunner.Then("Second level category tree is displayed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 14
 testRunner.When("I choose Do It Yourself (DIY) from the Second level category tree", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 15
 testRunner.Then("Third level category tree is displayed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 16
 testRunner.When("I press Do It Yourself (DIY) link from the Third level category tree", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 17
 testRunner.Then("Correct page is loaded", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 18
 testRunner.When(string.Format("I sort by {0}", sortBy), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 19
 testRunner.And(string.Format("I apply Brand {0} filter", brandFilter), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 20
 testRunner.Then(string.Format("{0} filter is applyed correctly", brandFilter), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 21
 testRunner.When(string.Format("I apply Colour {0} filter", colourFilter), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 22
 testRunner.Then(string.Format("{0} filter is applyed correctly", colourFilter), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
