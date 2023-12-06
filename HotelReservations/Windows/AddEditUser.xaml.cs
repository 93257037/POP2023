using HotelReservations.Model;
using HotelReservations.Repository;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HotelReservations.Windows
{
    public partial class AddEditUser : Window
    {
        public AddEditUser(User user = null)
        {
            InitializeComponent();
            AdjustWindow(user);
        }

        private void AdjustWindow(User user = null)
        {
            // TODO: Initialize combobox for user type
            UserTypeCB.Items.Add(nameof(Receptionist));
            UserTypeCB.Items.Add(nameof(Administrator));

            if (user != null)
            {
                Title = "Edit user";

                UserTypeCB.SelectedItem = user.GetType().Name;
                UserTypeCB.IsEnabled = false;

                // Load user-specific properties
                LoadUserSpecificFields(user);
            }
            else
            {
                Title = "Add user";
            }
        }

        private void UserTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserTypeCB.SelectedItem is string selectedUserType)
            {
                SpecificFieldsPanel.Children.Clear();

                switch (selectedUserType)
                {
                    case nameof(Receptionist):
                    case nameof(Administrator):
                        AddTextBox("FirstNameTextBox", "First Name:");
                        AddTextBox("LastNameTextBox", "Last Name:");
                        AddTextBox("JMBGTextBox", "JMBG:");
                        AddTextBox("UsernameTextBox", "Username:");
                        AddTextBox("PasswordTextBox", "Password:");
                        break;


                    default:
                        break;
                }
            }
        }

        private void AddTextBox(string textBoxName, string labelText)
        {
            var label = new Label
            {
                Content = labelText,
                Height = 30,
                Width = 150,
            };

            var textBox = new TextBox
            {
                Name = textBoxName,
                Height = 30,
                Width = 150,
                Margin = new Thickness(0, 5, 0, 0),
            };

            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };

            stackPanel.Children.Add(label);
            stackPanel.Children.Add(textBox);

            SpecificFieldsPanel.Children.Add(stackPanel);
        }

        private void LoadUserSpecificFields(User user)
        {
            // Clear existing user-specific fields
            SpecificFieldsPanel.Children.Clear();

            if (user != null)
            {
                var userProperties = user.GetType().GetProperties();

                foreach (var property in userProperties)
                {
                    // Exclude Id from the displayed fields
                    if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var label = new Label
                    {
                        Content = $"{property.Name}:",
                        Height = 30,
                        Width = 150,
                    };

                    var textBox = new TextBox
                    {
                        Name = property.Name,
                        Height = 30,
                        Width = 150,
                        Text = property.GetValue(user)?.ToString() ?? string.Empty,
                    };

                    var stackPanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                    };

                    stackPanel.Children.Add(label);
                    stackPanel.Children.Add(textBox);

                    SpecificFieldsPanel.Children.Add(stackPanel);
                }
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save button clicked!");

            if (UserTypeCB.SelectedItem is string selectedUserType)
            {
                var userType = Type.GetType($"HotelReservations.Model.{selectedUserType}");

                if (userType != null)
                {
                    var user = Activator.CreateInstance(userType) as User;

                    if (user != null)
                    {
                        // Get user-specific properties and update values
                        var userProperties = user.GetType().GetProperties();

                        foreach (var property in userProperties)
                        {
                            var textBox = SpecificFieldsPanel.FindName($"{property.Name}TextBox") as TextBox;

                            if (textBox != null)
                            {
                                var value = textBox.Text?.Trim();

                                if (string.IsNullOrEmpty(value))
                                {
                                    MessageBox.Show($"{property.Name} cannot be empty!");
                                    return;  // Stop further processing
                                }

                                try
                                {
                                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                                    {
                                        if (property.PropertyType.GetGenericArguments()[0].IsEnum)
                                        {
                                            // Handle nullable enum
                                            property.SetValue(user, Enum.Parse(property.PropertyType.GetGenericArguments()[0], value));
                                        }
                                        else
                                        {
                                            // Handle other nullable types
                                            property.SetValue(user, Convert.ChangeType(value, property.PropertyType.GetGenericArguments()[0]));
                                        }
                                    }
                                    else
                                    {
                                        // Handle non-nullable types
                                        property.SetValue(user, Convert.ChangeType(value, property.PropertyType));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error converting {property.Name} value: {ex.Message}");
                                    return;  // Stop further processing
                                }
                            }
                        }
                        Console.WriteLine($"Before Insert - FirstName: {user.FirstName}, LastName: {user.LastName}, JMBG: {user.JMBG}, Username: {user.Username}, Password: {user.Password}, UserType: {user.UserType}");

                        // At this point, 'user' should have all the properties set correctly
                        var userRepository = new UserRepository();
                        int userId = userRepository.Insert(user);
                        MessageBox.Show($"User saved with ID: {userId}");
                    }
                }
            }
        }

    }


}

