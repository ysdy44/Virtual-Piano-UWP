﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace Virtual_Piano.TestApp
{
    internal sealed class MainGrouping : List<string>, IList<string>, IGrouping<char, string>
    {
        public char Key { get; }
        public MainGrouping(char key, IEnumerable<string> collection) : base(collection) => this.Key = key;
        public override string ToString() => this.Key.ToString();
    }

    public sealed partial class MainPage : Page
    {

        //@Converter
        private bool BooleanNullableConverter(bool? value) => value is true;
        private Visibility BooleanNullableToVisibilityConverter(bool? value) => value is true ? Visibility.Visible : Visibility.Collapsed;

        //@Instance
        private readonly Lazy<ApplicationView> ViewLazy = new Lazy<ApplicationView>(() => ApplicationView.GetForCurrentView());
        private ApplicationView View => this.ViewLazy.Value;

        readonly IDictionary<string, Type> Dictionary = MainPage.CreatePages(typeof(MainPage)).OrderBy(x => x.Name).ToDictionary(x => x.Name, x => x);
        private IEnumerable<MainGrouping> Groupings => this.Dictionary.Keys.GroupBy(Enumerable.First).Select(c => new MainGrouping(c.Key, c));

        public MainPage()
        {
            this.InitializeComponent();
            base.Loaded += (s, e) => this.AutoSuggestBox.Focus(FocusState.Keyboard);

            this.ListView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is string item)
                {
                    this.Navigate(item);
                }
            };
            this.AutoSuggestBox.SuggestionChosen += (s, e) =>
            {
                this.ListView.SelectedItem = e.SelectedItem;
                this.TextBlock.Text = "Pages";

                if (e.SelectedItem is string item)
                {
                    this.Navigate(item);
                }
            };
            this.AutoSuggestBox.TextChanged += (sender, args) =>
            {
                switch (args.Reason)
                {
                    case AutoSuggestionBoxTextChangeReason.ProgrammaticChange:
                    case AutoSuggestionBoxTextChangeReason.SuggestionChosen:
                        break;
                    default:
                        if (string.IsNullOrEmpty(sender.Text))
                        {
                            sender.ItemsSource = null;
                            this.TextBlock.Text = "Pages";
                        }
                        else
                        {
                            string text = sender.Text.ToLower();
                            IEnumerable<string> suitableItems = this.Dictionary.Keys.Where(x => x.ToLower().Contains(text));

                            int count = suitableItems.Count();
                            if (count is 0)
                            {
                                sender.ItemsSource = null;
                                this.TextBlock.Text = "No results found";
                            }
                            else
                            {
                                sender.ItemsSource = suitableItems;
                                this.TextBlock.Text = $"{count} results";
                            }
                        }
                        break;
                }
            };
        }

        private void Navigate(string item)
        {
            this.View.Title = item;
            this.Frame.Navigate(this.Dictionary[item]);
        }

        private static IEnumerable<Type> CreatePages(Type assemblyType)
        {
            Assembly assembly = assemblyType.GetTypeInfo().Assembly;
            IEnumerable<TypeInfo> typeInfos = assembly.DefinedTypes;

            foreach (TypeInfo typeInfo in typeInfos)
            {
                Type type = typeInfo.AsType();
                if (type == assemblyType) continue;

                if (type.BaseType == typeof(Page))
                {
                    yield return type;
                }
            }
        }

    }
}