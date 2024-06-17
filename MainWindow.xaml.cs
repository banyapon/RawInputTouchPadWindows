using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RawInput.Touchpad
{
	public partial class MainWindow : Window
	{

        //new
        private Canvas _canvas;
        private Dictionary<int, Ellipse> _fingerCircles = new Dictionary<int, Ellipse>();

        public bool TouchpadExists
		{
			get { return (bool)GetValue(TouchpadExistsProperty); }
			set { SetValue(TouchpadExistsProperty, value); }
		}
		public static readonly DependencyProperty TouchpadExistsProperty =
			DependencyProperty.Register("TouchpadExists", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

		public string TouchpadContacts
		{
			get { return (string)GetValue(TouchpadContactsProperty); }
			set { SetValue(TouchpadContactsProperty, value); }
		}
		public static readonly DependencyProperty TouchpadContactsProperty =
			DependencyProperty.Register("TouchpadContacts", typeof(string), typeof(MainWindow), new PropertyMetadata(null));

		public MainWindow()
		{
			InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

		private HwndSource _targetSource;
		private readonly List<string> _log = new();

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }


        protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			_targetSource = PresentationSource.FromVisual(this) as HwndSource;
			_targetSource?.AddHook(WndProc);

			TouchpadExists = TouchpadHelper.Exists();

			_log.Add($"Precision touchpad exists: {TouchpadExists}");

			if (TouchpadExists)
			{
				var success = TouchpadHelper.RegisterInput(_targetSource.Handle);

				_log.Add($"Precision touchpad registered: {success}");
			}

            //new
            _canvas = new Canvas();
            _canvas.Background = Brushes.LightGray;
            this.Content = _canvas;
        }

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			switch (msg)
			{
				case TouchpadHelper.WM_INPUT:
					var contacts = TouchpadHelper.ParseInput(lParam);
					TouchpadContacts = string.Join(Environment.NewLine, contacts.Select(x => x.ToString()));

					_log.Add("---");
					_log.Add(TouchpadContacts);


                    //new
                    foreach (var contact in contacts)
                    {
                        if (_fingerCircles.ContainsKey(contact.ContactId))
                        {
                            // Update existing circle
                            UpdateFingerCircle(contact);
                        }
                        else
                        {
                            // Create new circle
                            CreateFingerCircle(contact);
                        }
                    }

                    // Remove circles for contacts that no longer exist
                    var contactsToRemove = _fingerCircles.Keys.Except(contacts.Select(c => c.ContactId)).ToList();
                    foreach (var contactId in contactsToRemove)
                    {
                        RemoveFingerCircle(contactId);
                    }

                    break;



			}
			return IntPtr.Zero;
		}

        //new
        private void CreateFingerCircle(TouchpadContact contact)
        {
            var circle = new Ellipse
            {
                Width = 30,
                Height = 30,
                Fill = Brushes.DarkCyan,
                Stroke = Brushes.DarkCyan,
            };

            // Add a TextBlock to display ContactId
            var textBlock = new TextBlock
            {
                Text = contact.ContactId.ToString(),
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            //circle.Content = textBlock; // Add the text block as the content of the circle

            Canvas.SetLeft(circle, contact.X - circle.Width / 2);
            Canvas.SetTop(circle, contact.Y - circle.Height / 2);
            _canvas.Children.Add(circle);
            _fingerCircles[contact.ContactId] = circle;
        }

        private void UpdateFingerCircle(TouchpadContact contact)
        {
            if (_fingerCircles.TryGetValue(contact.ContactId, out var circle))
            {
                Canvas.SetLeft(circle, contact.X - circle.Width / 2);
                Canvas.SetTop(circle, contact.Y - circle.Height / 2);
            }
        }

        private void RemoveFingerCircle(int contactId)
        {
            if (_fingerCircles.TryGetValue(contactId, out var circle))
            {
                _canvas.Children.Remove(circle);
                _fingerCircles.Remove(contactId);
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(string.Join(Environment.NewLine, _log));
		}
	}
}