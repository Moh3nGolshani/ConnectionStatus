using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ConnectionStatus
{
    public partial class RingControl : UserControl
    {
        public const int WH = 200;

        #region dependency properties

        static RingControl()
        {
            FillProperty =
                DependencyProperty.Register(
                    nameof(Fill),
                    typeof(Brush),
                    typeof(RingControl),
                    new FrameworkPropertyMetadata(Brushes.White, new PropertyChangedCallback(OnUIDataChanged)),
                    null);

            InternalRadiusProperty =
                DependencyProperty.Register(
                    nameof(InternalRadius),
                    typeof(double),
                    typeof(RingControl),
                    new FrameworkPropertyMetadata(0.3, new PropertyChangedCallback(OnUIDataChanged)),
                    null);

            ExternalRadiusProperty =
                DependencyProperty.Register(
                    nameof(ExternalRadius),
                    typeof(double),
                    typeof(RingControl),
                    new FrameworkPropertyMetadata(0.4, new PropertyChangedCallback(OnUIDataChanged)),
                    null);

            ThetaProperty =
                DependencyProperty.Register(
                    nameof(Theta),
                    typeof(double),
                    typeof(RingControl),
                    new FrameworkPropertyMetadata(10.0, new PropertyChangedCallback(OnUIDataChanged)),
                    null);

            TranslateProperty =
                DependencyProperty.Register(
                    nameof(Translate),
                    typeof(double),
                    typeof(RingControl),
                    new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnUIDataChanged)),
                    null);

            RotateProperty =
                DependencyProperty.Register(
                    nameof(Rotate),
                    typeof(double),
                    typeof(RingControl),
                    new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnUIDataChanged)),
                    null);
        }

        public static readonly DependencyProperty FillProperty;
        public static readonly DependencyProperty InternalRadiusProperty;
        public static readonly DependencyProperty ExternalRadiusProperty;
        public static readonly DependencyProperty ThetaProperty;
        public static readonly DependencyProperty TranslateProperty;
        public static readonly DependencyProperty RotateProperty;

        public Brush Fill
        {
            get
            {
                return (Brush)GetValue(FillProperty);
            }
            set
            {
                SetValue(FillProperty, value);
            }
        }

        public double InternalRadius
        {
            get
            {
                return (double)GetValue(InternalRadiusProperty);
            }
            set
            {
                SetValue(InternalRadiusProperty, value);
            }
        }

        public double ExternalRadius
        {
            get
            {
                return (double)GetValue(ExternalRadiusProperty);
            }
            set
            {
                SetValue(ExternalRadiusProperty, value);
            }
        }

        public double Theta
        {
            get
            {
                return (double)GetValue(ThetaProperty);
            }
            set
            {
                SetValue(ThetaProperty, value);
            }
        }

        public double Translate
        {
            get
            {
                return (double)GetValue(TranslateProperty);
            }
            set
            {
                SetValue(TranslateProperty, value);
            }
        }

        public double Rotate
        {
            get
            {
                return (double)GetValue(RotateProperty);
            }
            set
            {
                SetValue(RotateProperty, value);
            }
        }

        private static void OnUIDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null && e.NewValue != null && !e.OldValue.Equals(e.NewValue))
            {
                var control = d as RingControl;
                if (control != null)
                {
                    control.UIDataChanged.Invoke(control, EventArgs.Empty);
                }
            }
        }

        #endregion

        private event EventHandler UIDataChanged;
        private UIDataContainer dataContainer;

        public RingControl()
        {
            InitializeComponent();

            Canvas.Width = WH;
            Canvas.Height = WH;
            RotateTransform.CenterX = WH / 2;
            RotateTransform.CenterY = WH / 2;

            dataContainer = new UIDataContainer { Data = new UIData(this) };
            Canvas.DataContext = dataContainer;
        }

        private class UIDataContainer
        {
            public UIData Data { get; set; }
        }

        private class UIData : INotifyPropertyChanged
        {
            private readonly RingControl control;
            private readonly double WH = RingControl.WH;

            public UIData(RingControl control)
            {
                this.control = control;
                this.control.UIDataChanged += delegate { Refresh(); };
            }

            public Brush Fill
            {
                get
                {
                    return control.Fill;
                }
            }

            public Point this[int index]
            {
                get
                {
                    return this[index / 4, index % 4];
                }
            }

            public Point this[int ringNumber, int pointNumber]
            {
                get
                {
                    var theta = control.Theta;
                    while (theta < 0) theta += 45;
                    while (theta > 45) theta -= 45;
                    var angle = 2.0 * Math.PI * theta / 360.0;
                    var sin = Math.Sin(angle);
                    var cos = Math.Cos(angle);
                    var radius = pointNumber < 2 ? control.ExternalRadius * WH : control.InternalRadius * WH;

                    switch (ringNumber)
                    {
                        case 0:
                            {
                                if (pointNumber % 3 == 0)
                                {
                                    return new Point(WH / 2 - Math.Abs(radius * sin), WH / 2 - Math.Abs(radius * cos));
                                }
                                else
                                {
                                    return new Point(WH / 2 - Math.Abs(radius * cos), WH / 2 - Math.Abs(radius * sin));
                                }
                            }
                        case 1:
                            {
                                if (pointNumber % 3 == 0)
                                {
                                    return new Point(WH / 2 - Math.Abs(radius * cos), WH / 2 + Math.Abs(radius * sin));
                                }
                                else
                                {
                                    return new Point(WH / 2 - Math.Abs(radius * sin), WH / 2 + Math.Abs(radius * cos));
                                }
                            }
                        case 2:
                            {
                                if (pointNumber % 3 == 0)
                                {
                                    return new Point(WH / 2 + Math.Abs(radius * sin), WH / 2 + Math.Abs(radius * cos));
                                }
                                else
                                {
                                    return new Point(WH / 2 + Math.Abs(radius * cos), WH / 2 + Math.Abs(radius * sin));
                                }
                            }
                        case 3:
                            {
                                if (pointNumber % 3 == 0)
                                {
                                    return new Point(WH / 2 + Math.Abs(radius * cos), WH / 2 - Math.Abs(radius * sin));
                                }
                                else
                                {
                                    return new Point(WH / 2 + Math.Abs(radius * sin), WH / 2 - Math.Abs(radius * cos));
                                }
                            }
                    }

                    return new Point();
                }
            }

            public Size InternalSegmentSize
            {
                get
                {
                    return new Size(control.InternalRadius * WH, control.InternalRadius * WH);
                }
            }

            public Size ExternalSegmentSize
            {
                get
                {
                    return new Size(control.ExternalRadius * WH, control.ExternalRadius * WH);
                }
            }

            public double Translate
            {
                get
                {
                    return control.Translate * WH;
                }
            }

            public double Rotate
            {
                get
                {
                    return control.Rotate;
                }
            }

            private void Refresh()
            {
                OnPropertyChanged(string.Empty);
            }

            #region INotifyPropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion
        }
    }
}
