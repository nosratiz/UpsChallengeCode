﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MediatR;
using UPS.Application.Users.Commands.Create;
using UPS.Application.Users.Commands.Delete;
using UPS.Application.Users.Commands.Update;
using UPS.Application.Users.Queries;

namespace Ups.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMediator _mediator;
        private int currentPage=1;

        public MainWindow(IMediator mediator)
        {

            InitializeComponent();
            _mediator = mediator;
            Loaded += Window_Loaded;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetUserPagedListQuery
                {
                    Page = currentPage
                });


                // Process and display the data in your UI
                DataListView.ItemsSource =result.Data.Users;
                DataListView.DataContext=result.Data.Users;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during data loading
                MessageBox.Show(ex.Message);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private async void AddUser_Click(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private async void EditUser_Click(object sender, RoutedEventArgs e)
        {
            Button? deleteButton = sender as Button;
            int userId = (int)deleteButton.Tag;
            var result=await _mediator.Send(new GetUserQuery(userId));

            if(result.Success==false)
                MessageBox.Show(result.Message);

            EditNameTextBox.Text = result.Data.Name;
            EditEmailTextBox.Text = result.Data.Email;
            EditGenderComboBox.SelectedIndex = result.Data.Gender  =="male"?0:1;
            EditStatusComboBox.SelectedIndex = result.Data.Status == "active" ? 0: 1;
            Button editButton = (Button)FindName("EditBtn");
            editButton.Tag = userId;

            EditPopup.IsOpen = true;

        }

        private async void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            Button? deleteButton = sender as Button;
            int userId = (int)deleteButton.Tag;

            try
            {
                await _mediator.Send(new DeleteUserCommand(userId));

                MessageBox.Show("User added successfully");

                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var createUserCommand = new CreateUserCommand
            {
                Name = NameTextBox.Text,
                Email = EmailTextBox.Text,
                Gender = GenderComboBox.Text,
                Status = StatusComboBox.Text
            };

           var result= await _mediator.Send(createUserCommand);

           if(result.Success==false)
               MessageBox.Show(result.Errors.First().Message);
           else
           {
               MessageBox.Show("User added successfully");
               await LoadDataAsync();
           }
        }

        private async void  PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage <= 1) return;
           
            currentPage--;
            await LoadDataAsync();
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            await LoadDataAsync();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Button? saveButton = sender as Button;
            int userId = (int)saveButton.Tag;

            var updateUserCommand = new UpdateUserCommand
            {
                Email = EditEmailTextBox.Text,
                Name = EditNameTextBox.Text,
                Gender = EditGenderComboBox.Text,
                Status = EditStatusComboBox.Text,
                Id = userId
            };

           var result= await _mediator.Send(updateUserCommand);

           if (result.Success == false)
               MessageBox.Show(result.Errors.First().Message);
           else
           {
               MessageBox.Show("User Updated successfully");
               await LoadDataAsync();

           }
        }

        private void CancelEditButton_Click(object sender, RoutedEventArgs e)
        {
            EditPopup.IsOpen = false;
        }

       
    }
}
