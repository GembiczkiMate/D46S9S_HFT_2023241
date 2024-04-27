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
                        Price = value.Price,
                        ProductId = value.ProductId
                    };
                    OnPropertyChanged();
                    (DeleteProductCommand as RelayCommand).NotifyCanExecuteChanged();
                   
                }

            }
        }

        public RestCollection<Order> Orders { get; set; }

        private Order selectedOrder;

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                if (value != null)
                {
                    selectedOrder = new Order()
                    {
                        OrderId = value.OrderId,
                        UserId = value.UserId,
                        ProductId = value.ProductId,
                        OrderDate = value.OrderDate
                        
                        
                        
                    };
                    OnPropertyChanged();
                    (DeleteOrderCommand as RelayCommand).NotifyCanExecuteChanged();
                   

                }

            }
        }





        public ICommand CreateUserCommand { get; set; }

        public ICommand DeleteUserCommand { get; set; }

        public ICommand UpdateUserCommand { get; set; }


        public ICommand CreateProductCommand { get; set; }

        public ICommand DeleteProductCommand { get; set; }

        public ICommand UpdateProductCommand { get; set; }


        public ICommand CreateOrderCommand { get; set; }

        public ICommand DeleteOrderCommand { get; set; }

        public ICommand UpdateOrderCommand { get; set; }



        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public List<User> NonCrud { get; set; }
       



        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Users = new RestCollection<User>("http://localhost:39354/", "user", "hub");
                Products = new RestCollection<Product>("http://localhost:39354/", "product", "hub");
                Orders = new RestCollection<Order>("http://localhost:39354/", "order", "hub");

                NonCrud = new RestService("http://localhost:39354/").Get<User>("NonCrud/GetMostBuysID");
                
                
                



                CreateUserCommand = new RelayCommand(() =>
                {
                    Users.Add(new User()
                    {
                        Username = SelectedUser.Username
                        

                    });
                    
                    
                });

                UpdateUserCommand = new RelayCommand(() =>
                {
                    
                    
                        Users.Update(SelectedUser);
                        
                    
                   
                    
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
                        ProductName = SelectedProduct.ProductName,
                        Price = SelectedProduct.Price


                    });


                });

                UpdateProductCommand = new RelayCommand(() =>
                {
                    
                          Products.Update(SelectedProduct);

                    

                });

                DeleteProductCommand = new RelayCommand(() =>
                {
                    Products.Delete(SelectedProduct.ProductId);
                    



                },
                () =>
                {
                    return SelectedProduct != null;

                });

                SelectedProduct = new Product();
                int i = 1;
                
                CreateOrderCommand = new RelayCommand(() =>
                {
                    
                    Orders.Add(new Order()
                    {
                        ProductId = selectedOrder.ProductId,
                        UserId = selectedOrder.UserId,
                        OrderId = Orders.Count()+i,                       
                        OrderDate = DateTime.Now



                    });
                    i++;
;
                });

                UpdateOrderCommand = new RelayCommand(() =>
                {
                    
                        Orders.Update(SelectedOrder);

                    

                });

                DeleteOrderCommand = new RelayCommand(() =>
                {
                    Orders.Delete(SelectedOrder.OrderId);
                    

                },
                () =>
                {
                    return SelectedOrder != null;

                });

                SelectedOrder = new Order();

                
                

            }

        }
    }
}
