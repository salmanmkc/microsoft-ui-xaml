﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.Globalization.NumberFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MUXControlsTestApp
{
    [TopLevelTestPage(Name = "NumberBox")]
    public sealed partial class NumberBoxPage : TestPage
    {
        public DataModelWithINPC DataModelWithINPC { get; set; } = new DataModelWithINPC();

        public NumberBoxPage()
        {
            this.InitializeComponent();

            TestNumberBox.RegisterPropertyChangedCallback(NumberBox.TextProperty, new DependencyPropertyChangedCallback(TextPropertyChanged));
        }

        private void SpinMode_Changed(object sender, RoutedEventArgs e)
        {
            if (TestNumberBox != null)
            {
                if (SpinModeComboBox.SelectedIndex == 0)
                {
                    TestNumberBox.SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Hidden;
                }
                else if (SpinModeComboBox.SelectedIndex == 1)
                {
                    TestNumberBox.SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact;
                }
                else if (SpinModeComboBox.SelectedIndex == 2)
                {
                    TestNumberBox.SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Inline;
                }
            }
        }

        private void Validation_Changed(object sender, RoutedEventArgs e)
        {
            if (TestNumberBox != null)
            {
                if (ValidationComboBox.SelectedIndex == 0)
                {
                    TestNumberBox.ValidationMode = NumberBoxValidationMode.InvalidInputOverwritten;
                }
                else if (ValidationComboBox.SelectedIndex == 1)
                {
                    TestNumberBox.ValidationMode = NumberBoxValidationMode.Disabled;
                }
            }
        }

        private void MinCheckBox_CheckChanged(object sender, RoutedEventArgs e)
        {
            MinNumberBox.IsEnabled = (bool)MinCheckBox.IsChecked;
            MinValueChanged(null, null);
        }

        private void MaxCheckBox_CheckChanged(object sender, RoutedEventArgs e)
        {
            MaxNumberBox.IsEnabled = (bool)MaxCheckBox.IsChecked;
            MaxValueChanged(null, null);
        }

        private void MinValueChanged(object sender, object e)
        {
            if (TestNumberBox != null)
            {
                TestNumberBox.Minimum = (bool)MinCheckBox.IsChecked ? MinNumberBox.Value : double.MinValue;
            }
        }

        private void MaxValueChanged(object sender, object e)
        {
            if (TestNumberBox != null)
            {
                TestNumberBox.Maximum = (bool)MaxCheckBox.IsChecked ? MaxNumberBox.Value : double.MaxValue;
            }
        }

        private void NumberBoxValueChanged(object sender, NumberBoxValueChangedEventArgs e)
        {
            if (TestNumberBox != null && NewValueTextBox != null && OldValueTextBox != null)
            {
                NewValueTextBox.Text = e.NewValue.ToString();
                OldValueTextBox.Text = e.OldValue.ToString();
            }
        }

        private void CustomFormatterButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> languages = new List<string>() { "fr-FR" };
            DecimalFormatter formatter = new DecimalFormatter(languages, "FR");
            formatter.IntegerDigits = 1;
            formatter.FractionDigits = 2;
            TestNumberBox.NumberFormatter = formatter;
        }

        private void SetTextButton_Click(object sender, RoutedEventArgs e)
        {
            TestNumberBox.Text = "15";
        }

        private void SetValueButton_Click(object sender, RoutedEventArgs e)
        {
            TestNumberBox.Value = 42;
        }

        private void SetNaNButton_Click(object sender, RoutedEventArgs e)
        {
            TestNumberBox.Value = Double.NaN;
        }

        private void SetTwoWayBoundNaNButton_Click(object sender, RoutedEventArgs e)
        {
            DataModelWithINPC.Value = Double.NaN;
            TwoWayBoundNumberBoxValue.Text = TwoWayBoundNumberBox.Value.ToString();
        }

        private void TextPropertyChanged(DependencyObject o, DependencyProperty p)
        {
            TextTextBox.Text = TestNumberBox.Text;
        }

        private void ScrollviewerWithScroll_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            VerticalOffsetDisplayBlock.Text = (sender as Windows.UI.Xaml.Controls.ScrollViewer).VerticalOffset.ToString();
        }
    }

    public class DataModelWithINPC : INotifyPropertyChanged
    {
        private double _value;
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double Value
        {
            get => _value;
            set
            {
                if (value != _value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(this.Value));
                }
            }
        }
    }
}