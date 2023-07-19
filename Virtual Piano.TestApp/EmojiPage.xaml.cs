using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    public struct EmojiModel
    {
        public string Text { get; set; }
        public override string ToString() => this.Text;
    }

    public sealed partial class EmojiPage : Page
    {
        readonly string[] Items = new string[]
        {
            "🪕",
            "🪗",
            "🪘",
            "🎷",

            "🎸",
            "🎻",
            "🥁",
            "🎼",

            "🎹",
            "🎬",
            "👾",
            "🤖",

            "🎤",
            "🎧",
            "🎺",
            "🎷",

            "📯",
            "📣",
            "💧",
            "💦",

            "⏲️",
            "👏",
            "🎯",
            "🎮",
        };

        public EmojiPage()
        {
            this.InitializeComponent();
            this.ListView.ItemsSource = this.ItemsSource(this.Items).ToArray();
            this.ListView.ItemClick += async (s, e) =>
            {
                if (e.ClickedItem is EmojiModel item)
                {
                    await new MessageDialog(item.ToString()).ShowAsync();
                }
            };
        }

        private IEnumerable<ListViewItem> ItemsSource(string[] items)
        {
            foreach (var item in items)
            {
                ListViewItem listViewItem = new ListViewItem
                {
                    Content = new EmojiModel
                    {
                        Text = item
                    }
                };
                ToolTipService.SetToolTip(listViewItem, item);
                yield return listViewItem;
            }
        }
    }
}