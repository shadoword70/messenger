using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ServerMessenger.Behaviors
{
    public class ScrollIntoViewBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            TextBox textBox = AssociatedObject;
            textBox.TextChanged += TextBoxOnTextChanged;
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = AssociatedObject;
            textBox.CaretIndex = textBox.Text.Length;
            textBox.ScrollToEnd();
        }

        protected override void OnDetaching()
        {
            TextBox textBox = AssociatedObject;
            textBox.TextChanged -= TextBoxOnTextChanged;
        }
    }
}
