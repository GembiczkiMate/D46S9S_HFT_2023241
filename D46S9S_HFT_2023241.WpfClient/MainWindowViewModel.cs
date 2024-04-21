using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using D46S9S_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace D46S9S_HFT_2023241.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<User> Users { get; set; }

        private User selectedUser;

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (value != null)
                {
                    selectedUser = new User()
                    {
                        Username = value.Username,
                        UserId = value.UserId
                    };
                    OnPropertyChanged();
                    (DeleteUserCommand as RelayCommand).NotifyCanExecuteChanged();
                    (CreateUserCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateUserCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                
            }
        }

        public RestCollection<Product> Products { get; set; }

        private Product selectedProduct;

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                if (value != null)
                {
                    selectedProduct = new Product()
                    {
                        ProductName = value.ProductName,
                        ProductId = value.ProductId
                    };
                    OnPropertyChanged();
                    (DeleteProductCommand as RelayCommand).NotifyCanExecuteChanged();
                    (CreateProductCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateProductCommand as RelayCommand).NotifyCanExecuteChanged();
                }

            }
        }





        public ICommand CreateUserCommand { get; set; }

        public ICommand DeleteUserCommand { get; set; }

        public ICommand UpdateUserCommand { get; set; }

        public ICommand CreateProductCommand { get; set; }

        public ICommand DeleteProductCommand { get; set; }

        public ICommand UpdateProductCommand { get; set; }



        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Users = new RestCollection<User>("http://localhost:39354/", "user", "hub");
                Products = new RestCollection<Product>("http://localhost:39354/", "product", "hub");


                CreateUserCommand = new RelayCommand(() =>
                {
                    Users.Add(new User()
                    {
                        Username = SelectedUser.Username
                        

                    });
                    
                    
                });

                UpdateUserCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Users.Update(SelectedUser);
                        
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                    
                });

                DeleteUserCommand = new RelayCommand(() =>
                {
                    Users.Delete(SelectedUser.UserId);
                    

                },
                () =>
                {
                    return SelectedUser != null;
                    
                });

                SelectedUser = new User();

                CreateProductCommand = new RelayCommand(() =>
                {
                    Products.Add(new Product()
                    {
                        ProductName = selectedProduct.ProductName


                    });


                });

                UpdateProductCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Products.Update(selectedProduct);

                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteProductCommand = new RelayCommand(() =>
                {
                    Products.Delete(selectedProduct.ProductId);


                },
                () =>
                {
                    return selectedProduct != null;

                });

                selectedProduct = new Product();


            }

        }
    }
}
