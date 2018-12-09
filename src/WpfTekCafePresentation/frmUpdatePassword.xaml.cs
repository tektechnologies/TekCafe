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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicLayer;
using DataObjects;

namespace WpfTekCafePresentation
{
    /// <summary>
    /// Interaction logic for formUpdatePassword.xaml
    /// </summary>
    public partial class frmUpdatePassword : Window
    {
        private Employee _employee;
        private EmployeeManager _employeeManager;
        private bool _isNewEmployee;

        public frmUpdatePassword()
        {
            this._employee = employee;
            this._employeeManager = employeeManager;
            this._isNewEmployee = _isNewEmployee;

            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = _employee.FirstName + ", Update Employee Email, Now.";
            if (_isNewEmployee)
            {
                this.tbkMessage.Text = _employee.FirstName + ", all new employees must comply. " + tbkMessage.Text;
            }
            else
            {
                this.tbkMessage.Text = _employee.FirstName + ", please " + tbkMessage.Text;
            }
            this.txtEmail.Focus();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // error checks
            if (this.txtEmail.Text.Length < 7 || this.txtEmail.Text.Length > 255)
            {
                MessageBox.Show("No Employee Found.");
                this.txtEmail.Focus();
                this.txtEmail.SelectAll();
                return;
            }
            if (this.pwdOldPassword.Password.Length < 6)
            {
                MessageBox.Show("UnAuthorized Password.");
                this.pwdOldPassword.Focus();
                this.pwdOldPassword.SelectAll();
                return;
            }
            if (this.pwdNewPassword.Password.Length < 6)
            {
                MessageBox.Show("Your new authorization code  is too short.");
                this.pwdNewPassword.Focus();
                this.pwdNewPassword.SelectAll();
                return;
            }
            if (string.Compare(this.pwdNewPassword.Password,
                                this.pwdRetypePassword.Password) != 0)
            {
                MessageBox.Show("New passwords must match, try again.");
                this.pwdRetypePassword.Clear();
                this.pwdNewPassword.Focus();
                this.pwdNewPassword.SelectAll();
                return;
            }
            // if we got this far, we have what we need!
            try
            {
                if (_employeeManager.UpdatePassword(this.txtEmail.Text,
                     this.pwdNewPassword.Password,
                    this.pwdOldPassword.Password))
                {
                    MessageBox.Show("Password updated.");
                    if (_isNewEmployee == true)
                    {
                        _employeeManager.RefreshRoles(_employee, this.txtEmail.Text);
                    }
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Pass Code Update failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}