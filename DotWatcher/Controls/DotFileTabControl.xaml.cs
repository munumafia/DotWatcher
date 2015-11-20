using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DotWatcher.Controls
{
    /// <summary>
    /// Custom tab control for showing dot files rendered as an image
    /// </summary>
    public partial class DotFileTabControl : UserControl
    {
        /// <summary>
        /// The ObservableCollection containing the <see cref="DotFileTabItem">DotFileTabItem</see> 
        /// instances to render as tabs
        /// </summary>
        public ObservableCollection<DotFileTabItem> ItemSource
        {
            get { return (ObservableCollection<DotFileTabItem>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        /// <summary>
        /// The DependencyProperty instance for the <see cref="ItemSource">ItemSource</see> property
        /// </summary>
        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register(
            "ItemSource",
            typeof(ObservableCollection<DotFileTabItem>),
            typeof(DotFileTabControl),
            new PropertyMetadata(new ObservableCollection<DotFileTabItem>(), ItemSource_PropertyChanged));

        /// <summary>
        /// The currently selected tab in the control
        /// </summary>
        public DotFileTabItem SelectedTab
        {
            get { return (DotFileTabItem)GetValue(SelectedTabProperty); }
            set { SetValue(SelectedTabProperty, value); }
        }

        /// <summary>
        /// The DependencyProperty instace for the <see cref="SelectedTab">SelectedTab</see> property
        /// </summary>
        public static readonly DependencyProperty SelectedTabProperty = DependencyProperty.Register(
            "SelectedTab",
            typeof(DotFileTabItem),
            typeof(DotFileTabControl),
            new PropertyMetadata(null, SelectedTab_PropertyChanged));

        /// <summary>
        /// Default constructor
        /// </summary>
        public DotFileTabControl()
        {
            InitializeComponent();

            RootLayout.DataContext = this;
        }

        /// <summary>
        /// Event handler used to select the newest tab added to <see cref="ItemSource">ItemSource</see>
        /// </summary>
        /// <param name="sender">The object that raised the CollectionChanged event</param>
        /// <param name="e">The event arguments</param>
        private void ItemSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SelectedTab = ItemSource.Last();
        }

        /// <summary>
        /// Event handler used to add/remove CollectionChanged event handlers each time the <see cref="ItemSource">ItemSource</see>
        /// property is changed
        /// </summary>
        /// <param name="d">The object containing the ItemSource dependency property</param>
        /// <param name="e">The event arguments</param>
        private static void ItemSource_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (DotFileTabControl)d;

            var oldValue = e.OldValue as ObservableCollection<DotFileTabItem>;
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= control.ItemSource_CollectionChanged;
            }

            var newValue = e.NewValue as ObservableCollection<DotFileTabItem>;
            if (newValue != null)
            {
                newValue.CollectionChanged += control.ItemSource_CollectionChanged;
            }
        }

        /// <summary>
        /// Event handler used to keep the IsSelected property in sync with the selected tab
        /// </summary>
        /// <remarks>
        /// Any way to make XAML keep this in sync? The only reason we are doing this is to make sure that
        /// the tab header of the selected tab is shown as selected in the UI
        /// </remarks>
        /// <param name="d">The object containing the dependent property</param>
        /// <param name="e">The event arguments</param>
        private static void SelectedTab_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldTab = e.OldValue as DotFileTabItem;
            if (oldTab != null)
            {
                oldTab.IsSelected = false;
            }

            var newTab = e.NewValue as DotFileTabItem;
            if (newTab != null)
            {
                newTab.IsSelected = true;
            }
        }
    }
}
